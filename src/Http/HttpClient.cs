using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using JetBrains.Annotations;

namespace Diadoc.Api.Http
{
	public interface IHttpClient
	{
		bool UseSystemProxy { get; set; }
		void SetProxyUri([CanBeNull] string uri);
		void SetProxyCredentials([CanBeNull] NetworkCredential proxyCredentials);
		void SetProxyCredentials([NotNull] string user, [NotNull] string password);
		void SetProxyCredentials([NotNull] string user, [NotNull] SecureString password);

		[NotNull]
		HttpResponse PerformHttpRequest([NotNull] HttpRequest request, params HttpStatusCode[] allowedStatusCodes);
	}

	public class HttpClient : IHttpClient
	{
		private readonly string baseUrl;
		private NetworkCredential proxyCredential;
		private Uri proxyUri;

		public HttpClient([NotNull] string baseUrl)
		{
			if (baseUrl == null) throw new ArgumentNullException("baseUrl");
			if (!Uri.IsWellFormedUriString(baseUrl, UriKind.Absolute)) throw new ArgumentException(string.Format("{0} is not a well-formed uri", baseUrl));
			this.baseUrl = baseUrl.EndsWith("/") ? baseUrl.Substring(0, baseUrl.Length - 1) : baseUrl;
			UseSystemProxy = true;
		}

		/// <summary>
		/// The default value is true
		/// </summary>
		public bool UseSystemProxy { get; set; }

		/// <summary>
		/// Client certificate used to establish https requests
		/// </summary>
		public X509Certificate2 ClientCertificate { get; set; }

		public void SetProxyUri([CanBeNull] string uri)
		{
			proxyUri = uri != null ? new Uri(uri) : null;
			UseSystemProxy = false;
		}

		public void SetProxyCredentials([CanBeNull] NetworkCredential proxyCredentials)
		{
			proxyCredential = proxyCredentials;
		}

		public void SetProxyCredentials([NotNull] string user, [NotNull] string password)
		{
			if (user == null) throw new ArgumentNullException("user");
			var userDomain = UserDomain.Parse(user);
			proxyCredential = new NetworkCredential(userDomain.User, password, userDomain.Domain);
		}

		public void SetProxyCredentials([NotNull] string user, [NotNull] SecureString password)
		{
			var userDomain = UserDomain.Parse(user);
			proxyCredential = new NetworkCredential(userDomain.User, SecureStringToString(password), userDomain.Domain);
		}

		[NotNull]
		private static string SecureStringToString([NotNull] SecureString secureString)
		{
			IntPtr intPtr = Marshal.SecureStringToBSTR(secureString);
			try
			{
				return Marshal.PtrToStringBSTR(intPtr);
			}
			finally
			{
				Marshal.ZeroFreeBSTR(intPtr);
			}
		}

		[NotNull]
		public virtual HttpResponse PerformHttpRequest([NotNull] HttpRequest request, params HttpStatusCode[] allowedStatusCodes)
		{
			HttpResponse response = null;
			try
			{
				var webRequest = PrepareWebRequest(request);
				using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
					response = new HttpResponse(webResponse);
				if (!StatusCodeIsAllowed(response.StatusCode, allowedStatusCodes))
				{
					var message = string.Format("Unexpected http status code: {0}", response.StatusCode);
					throw new WebException(message, null, WebExceptionStatus.ProtocolError, null);
				}
				return response;
			}
			catch (WebException e)
			{
				var webResponse = e.Response as HttpWebResponse;
				if (webResponse != null)
					response = new HttpResponse(webResponse);
				string diadocErrorCode = null;
				var additionalMessage = string.Empty;
				HttpStatusCode? statusCode = null;
				if (response != null)
				{
					statusCode = response.StatusCode;
					if (StatusCodeIsAllowed(statusCode.Value, allowedStatusCodes)) return response;
					additionalMessage = GetAdditionalMessage(response);
					diadocErrorCode = response.DiadocErrorCode;
				}
				if (e.Status == WebExceptionStatus.ReceiveFailure)
					additionalMessage += " Ошибка подключения: Возможно, неправильные аутентификационные данные для прокси.";
				var message = string.Format("BaseUrl={0}, PathAndQuery={1}, AdditionalMessage={2}, StatusCode={3}, DiadocErrorCode: {4}", baseUrl, request.PathAndQuery, additionalMessage, statusCode, diadocErrorCode);
				throw new HttpClientException(message, additionalMessage, request.PathAndQuery, e, response);
			}
		}

		private static bool StatusCodeIsAllowed(HttpStatusCode statusCode, params HttpStatusCode[] allowedStatusCodes)
		{
			return statusCode == HttpStatusCode.OK || allowedStatusCodes.Contains(statusCode);
		}

		[NotNull]
		private static string GetAdditionalMessage([NotNull] HttpResponse response)
		{
			string additionalMessage;
			try
			{
				additionalMessage = Encoding.UTF8.GetString(response.Content);
			}
			catch (Exception ex)
			{
				additionalMessage = string.Format("Ошибка во время получения дополнительной информации от сервера: {0}.", ex.Message);
			}
			return additionalMessage;
		}

		[NotNull]
		private WebRequest PrepareWebRequest([NotNull] HttpRequest request)
		{
			var normalizedQueryString = request.PathAndQuery.StartsWith("/") ? request.PathAndQuery.Substring(1) : request.PathAndQuery;
			var uri = string.Format("{0}/{1}", baseUrl, normalizedQueryString);
			var webRequest = (HttpWebRequest)WebRequest.Create(uri);
			ProxifyWebRequest(webRequest);
			webRequest.Method = request.Method;
			webRequest.Timeout = request.TimeoutInSeconds * 1000;
			webRequest.AllowAutoRedirect = true;
			if (ClientCertificate != null)
				webRequest.ClientCertificates.Add(ClientCertificate);
			if (request.AdditionalHeaders != null)
			{
				foreach (var kvp in request.AdditionalHeaders)
					webRequest.Headers.Add(kvp.Key, kvp.Value);
			}
			if (!string.IsNullOrEmpty(request.Accept))
				webRequest.Accept = request.Accept;
			if (request.Range != null)
				webRequest.AddRange(request.Range.From, request.Range.To);
			if (request.Body != null && request.Body.ContentLength > 0)
			{
				webRequest.ContentType = request.Body.ContentType;
				webRequest.ContentLength = request.Body.ContentLength;
				using (var requestStream = webRequest.GetRequestStream())
					request.Body.WriteToStream(requestStream);
			}
			else webRequest.ContentLength = 0;

			webRequest.UserAgent = UserAgentString;
			return webRequest;
		}

		private void ProxifyWebRequest([NotNull] WebRequest request)
		{
			if (!UseSystemProxy)
				request.Proxy = proxyUri != null ? new WebProxy(proxyUri) { Credentials = proxyCredential } : null;
			else if (request.Proxy != null && proxyCredential != null)
				request.Proxy.Credentials = proxyCredential;
		}

		private static string UserAgentString { get; set; }
		static HttpClient()
		{
			string sdkVersion = typeof(HttpClient).Assembly.GetName().Version.ToString();
			string netFxVersion = Environment.Version.ToString();
			try
			{
				sdkVersion = FileVersionInfo.GetVersionInfo(typeof(HttpClient).Assembly.Location).FileVersion;
			}
			catch { }
			try
			{
				netFxVersion = FileVersionInfo.GetVersionInfo(typeof(int).Assembly.Location).FileVersion;
			}
			catch { }

			UserAgentString = string.Format("Diadoc C# SDK={0};OS={1};NETFX={2}",
				sdkVersion,
				Environment.OSVersion.VersionString,
				netFxVersion);
		}
	}
}
