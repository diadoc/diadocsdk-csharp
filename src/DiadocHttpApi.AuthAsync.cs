using System;
using System.Text;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<string> AuthenticateAsync(
			string login,
			string password,
			string key = null,
			string id = null)
		{
			var qsb = new PathAndQueryBuilder("/V3/Authenticate");
			qsb.AddParameter("type", "password");

			var request = BuildHttpRequest(
				null,
				"POST",
				qsb.BuildPathAndQuery(),
				Serialize(new LoginPassword
				{
					Login = login,
					Password = password
				}));

			if (!string.IsNullOrEmpty(key))
			{
				request.AddHeader("X-Diadoc-ServiceKey", key);
				request.AddHeader("X-Diadoc-ServiceUserId", id);
			}

			return PerformRequestAsync(request);
		}

		public Task<string> AuthenticateByKeyAsync(string key, string id)
		{
			var qsb = new PathAndQueryBuilder("/V3/Authenticate");
			qsb.AddParameter("type", "trust");

			var request = BuildHttpRequest(null, "POST", qsb.BuildPathAndQuery(), null);

			request.AddHeader("X-Diadoc-ServiceKey", key);
			request.AddHeader("X-Diadoc-ServiceUserId", id);

			return PerformRequestAsync(request);
		}

		public Task<string> AuthenticateBySidAsync(string sid)
		{
			var qsb = new PathAndQueryBuilder("/V3/Authenticate");
			qsb.AddParameter("type", "sid");
			var request = BuildRequest(
				null,
				"POST",
				qsb.BuildPathAndQuery(),
				new HttpRequestBody(Encoding.UTF8.GetBytes(sid), "text/plain"));

			return PerformRequestAsync(request);
		}

		public async Task<string> AuthenticateAsync(byte[] certificateBytes, bool useLocalSystemStorage = false)
		{
			var token = await AuthenticateByCertificateAsync(
					certificateBytes,
					useLocalSystemStorage,
					key: null,
					id: null)
				.ConfigureAwait(false);

			return await ConfirmAuthenticationByCertificateAsync(certificateBytes, token, saveBinding: false).ConfigureAwait(false);
		}

		public async Task<string> AuthenticateAsync(string thumbprint, bool useLocalSystemStorage = false)
		{
			var userCert = crypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage);

			var token = await AuthenticateByCertificateAsync(
					userCert.RawData,
					useLocalSystemStorage,
					key: null,
					id: null)
				.ConfigureAwait(false);

			return await ConfirmAuthenticationByCertificateThumbprintAsync(userCert.Thumbprint, token, saveBinding: false).ConfigureAwait(false);
		}

		public async Task<string> AuthenticateWithKeyAsync(byte[] certificateBytes, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true)
		{
			var authenticationWithKey = !string.IsNullOrEmpty(key);

			var token = await AuthenticateByCertificateAsync(certificateBytes, useLocalSystemStorage, key, id).ConfigureAwait(false);

			return autoConfirm
				? await ConfirmAuthenticationByCertificateAsync(certificateBytes, token, saveBinding: authenticationWithKey).ConfigureAwait(false)
				: token;
		}

		public async Task<string> AuthenticateWithKeyAsync(string thumbprint, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true)
		{
			var authenticationWithKey = !string.IsNullOrEmpty(key);
			var userCert = crypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage);

			var token = await AuthenticateByCertificateAsync(userCert.RawData, useLocalSystemStorage, key, id).ConfigureAwait(false);

			return autoConfirm
				? await ConfirmAuthenticationByCertificateThumbprintAsync(userCert.Thumbprint, token, saveBinding: authenticationWithKey).ConfigureAwait(false)
				: token;
		}

		public Task<string> AuthenticateWithKeyConfirmAsync(byte[] certificateBytes, string token, bool saveBinding = false)
		{
			return ConfirmAuthenticationByCertificateAsync(certificateBytes, token, saveBinding);
		}

		public Task<string> AuthenticateWithKeyConfirmAsync(string thumbprint, string token, bool saveBinding = false)
		{
			return ConfirmAuthenticationByCertificateThumbprintAsync(thumbprint, token, saveBinding);
		}

		private async Task<string> AuthenticateByCertificateAsync(
			byte[] certificateBytes,
			bool useLocalSystemStorage,
			string key,
			string id)
		{
			var qsb = new PathAndQueryBuilder("/V3/Authenticate");
			qsb.AddParameter("type", "certificate");

			var request = BuildRequest(
				null,
				"POST",
				qsb.BuildPathAndQuery(),
				new HttpRequestBody(certificateBytes, "application/octet-stream"));

			if (!string.IsNullOrEmpty(key))
			{
				request.AddHeader("X-Diadoc-ServiceKey", key);
				request.AddHeader("X-Diadoc-ServiceUserId", id);
			}

			var httpResponse = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
			return Convert.ToBase64String(crypt.Decrypt(httpResponse.Content, useLocalSystemStorage));
		}

		private Task<string> ConfirmAuthenticationByCertificateAsync(byte[] certificateBytes, string token, bool saveBinding)
		{
			var qsb = new PathAndQueryBuilder("/V3/AuthenticateConfirm");
			qsb.AddParameter("token", token);
			qsb.AddParameter("saveBinding", saveBinding.ToString());

			return PerformHttpRequestAsync(
				null,
				"POST",
				qsb.BuildPathAndQuery(),
				certificateBytes,
				responseContent => Encoding.UTF8.GetString(responseContent));
		}

		private Task<string> ConfirmAuthenticationByCertificateThumbprintAsync(string thumbprint, string token, bool saveBinding)
		{
			var qsb = new PathAndQueryBuilder("/V3/AuthenticateConfirm");
			qsb.AddParameter("thumbprint", thumbprint);
			qsb.AddParameter("token", token);
			qsb.AddParameter("saveBinding", saveBinding.ToString());

			return PerformHttpRequestAsync(
				null,
				"POST",
				qsb.BuildPathAndQuery(),
				null,
				responseContent => Encoding.UTF8.GetString(responseContent));
		}

		private async Task<string> PerformRequestAsync(HttpRequest request)
		{
			var httpResponse = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
			return Encoding.UTF8.GetString(httpResponse.Content);
		}
	}
}
