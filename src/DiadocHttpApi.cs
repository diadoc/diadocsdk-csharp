using System;
using System.Net;
using System.Text;
using Diadoc.Api.Cryptography;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		protected readonly string apiClientId;
		private readonly ICrypt crypt;
		private readonly IHttpSerializer httpSerializer;

		public DiadocHttpApi([NotNull] string apiClientId, [NotNull] IHttpClient httpClient, [NotNull] ICrypt crypt)
			: this(apiClientId, httpClient, crypt, new ProtobufHttpSerializer())
		{
		}

		public DiadocHttpApi(
			[NotNull] string apiClientId,
			[NotNull] IHttpClient httpClient,
			[NotNull] ICrypt crypt,
			[NotNull] IHttpSerializer httpSerializer)
		{
			if (apiClientId == null)
			{
				throw new ArgumentNullException("apiClientId");
			}
			if (httpClient == null)
			{
				throw new ArgumentNullException("httpClient");
			}
			if (crypt == null)
			{
				throw new ArgumentNullException("crypt");
			}
			this.apiClientId = apiClientId;
			HttpClient = httpClient;
			this.crypt = crypt;
			this.httpSerializer = httpSerializer;
			Docflow = new DocflowHttpApi(this);
		}

		[NotNull] public IHttpClient HttpClient { get; }

		[NotNull] public DocflowHttpApi Docflow { get; }
		
		/// <summary>
		/// Информация об интеграционном решении технологического партнера.
		/// </summary>
		[CanBeNull] public string SolutionInfo { get; set; }
		
		/// <summary>
		/// Использовать аутентификацию по протоколу OpenID Connect
		/// </summary>
		public bool UseOidc { get; set; }

		[NotNull]
		protected byte[] PerformHttpRequest(
			[CanBeNull] string token,
			[NotNull] string method,
			[NotNull] string queryString,
			[CanBeNull] byte[] requestBody = null)
		{
			return PerformHttpRequest(token, method, queryString, requestBody, responseContent => responseContent);
		}

		[NotNull]
		protected TResponse PerformHttpRequest<TRequest, TResponse>(
			[CanBeNull] string token,
			[NotNull] string queryString,
			[NotNull] TRequest request)
			where TRequest : class
			where TResponse : class
		{
			return PerformHttpRequest(token, "POST", queryString, Serialize(request), Deserialize<TResponse>);
		}

		[NotNull]
		protected TResponse PerformHttpRequest<TResponse>(
			[CanBeNull] string token,
			[NotNull] string method,
			[NotNull] string queryString,
			[CanBeNull] byte[] requestBody = null,
			params HttpStatusCode[] allowedStatusCodes)
			where TResponse : class
		{
			return PerformHttpRequest(token, method, queryString, requestBody, Deserialize<TResponse>, allowedStatusCodes);
		}

		[NotNull]
		protected TResponse PerformHttpRequest<TResponse>(
			[CanBeNull] string token,
			[NotNull] string method,
			[NotNull] string queryString,
			[CanBeNull] byte[] requestBody,
			[NotNull] Func<byte[], TResponse> convertResponse,
			params HttpStatusCode[] allowedStatusCodes)
		{
			var request = BuildHttpRequest(token, method, queryString, requestBody);
			var response = HttpClient.PerformHttpRequest(request, allowedStatusCodes);
			return DeserializeResponse(request, response, convertResponse);
		}

		protected TResponse DeserializeResponse<TResponse>(
			HttpRequest request,
			HttpResponse response,
			Func<byte[], TResponse> convertResponse,
			string errorMessage = null)
		{
			try
			{
				return convertResponse(response.Content);
			}
			catch (Exception e)
			{
				throw new Exception(
					FormatErrorMessage(errorMessage ?? "Could not deserialize http response", request, response), e);
			}
		}

		[NotNull]
		protected GeneratedFile PerformGenerateXmlHttpRequest<TRequest>(
			[CanBeNull] string token,
			[NotNull] string queryString,
			[NotNull] TRequest requestObject)
			where TRequest : class
		{
			var request = BuildHttpRequest(token, "POST", queryString, Serialize(requestObject));
			var response = HttpClient.PerformHttpRequest(request);
			return new GeneratedFile(response.ContentDispositionFileName, response.Content);
		}

		[NotNull]
		protected HttpRequest BuildHttpRequest(
			[CanBeNull] string token,
			[NotNull] string method,
			[NotNull] string queryString,
			[CanBeNull] byte[] requestBody)
		{
			var body = requestBody != null ? new HttpRequestBody(requestBody, httpSerializer.RequestContentType) : null;
			return BuildRequest(token, method, queryString, body);
		}

		[NotNull]
		protected virtual HttpRequest BuildRequest(
			[CanBeNull] string token,
			[NotNull] string method,
			[NotNull] string queryString,
			[CanBeNull] HttpRequestBody body)
		{
			var request = new HttpRequest(method, queryString, body, accept: httpSerializer.ResponseContentType);
			request.AddHeader("Authorization", GetAuthorizationHeaderValue(token));
			request.AddHeader("Accept-Encoding", "gzip, deflate, br, zstd");
			if (!string.IsNullOrEmpty(SolutionInfo))
			{
				request.AddHeader("X-Solution-Info", SolutionInfo);
			}
			return request;
		}

		private string GetAuthorizationHeaderValue([CanBeNull] string token)
		{
			if (UseOidc)
			{
				if (string.IsNullOrEmpty(token))
				{
					throw new ArgumentNullException(nameof(token), "access_token is required for OpenID Connect authentication");
				}
				return $"Bearer {token}";
			}
			var sb = new StringBuilder("DiadocAuth ");
			sb.AppendFormat("ddauth_api_client_id={0}", apiClientId);
			if (!string.IsNullOrEmpty(token))
			{
				sb.AppendFormat(",ddauth_token={0}", token);
			}

			return sb.ToString();
		}

		[NotNull]
		protected static string BuildQueryStringWithBoxId([NotNull] string action, [NotNull] string boxId)
		{
			return new PathAndQueryBuilder(action).WithBoxId(boxId).BuildPathAndQuery();
		}

		[NotNull]
		private static string FormatErrorMessage(
			[NotNull] string message,
			[NotNull] HttpRequest request,
			[NotNull] HttpResponse response)
		{
			var sb = new StringBuilder();
			sb.AppendLine(message);
			sb.AppendLine(string.Format("Request: {0}", request));
			sb.AppendLine(string.Format("Response: {0}", response));
			return sb.ToString();
		}

		[NotNull]
		protected byte[] Serialize<T>([NotNull] T obj) where T : class
		{
			return httpSerializer.Serialize(obj);
		}

		[NotNull]
		protected T Deserialize<T>([NotNull] byte[] bytes) where T : class
		{
			return httpSerializer.Deserialize<T>(bytes);
		}
	}
}
