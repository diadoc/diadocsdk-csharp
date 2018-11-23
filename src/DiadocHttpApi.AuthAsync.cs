using System;
using System.Text;
using System.Threading.Tasks;
using Diadoc.Api.Http;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public async Task<string> AuthenticateAsync(string login, string password, string key = null, string id = null)
		{
			var qsb = new PathAndQueryBuilder("/V2/Authenticate");
			qsb.AddParameter("login", login);
			qsb.AddParameter("password", password);
			if (!string.IsNullOrEmpty(key))
			{
				qsb.AddParameter("key", key);
				qsb.AddParameter("id", id);
			}

			var httpResponse = await PerformHttpRequestAsync(null, "POST", qsb.BuildPathAndQuery()).ConfigureAwait(false);
			return Encoding.UTF8.GetString(httpResponse);
		}

		public async Task<string> AuthenticateByKeyAsync(string key, string id)
		{
			var qsb = new PathAndQueryBuilder("/V2/Authenticate");
			qsb.AddParameter("key", key);
			qsb.AddParameter("id", id);
			var httpResponse = await PerformHttpRequestAsync(null, "POST", qsb.BuildPathAndQuery()).ConfigureAwait(false);
			return Encoding.UTF8.GetString(httpResponse);
		}

		public async Task<string> AuthenticateBySidAsync(string sid)
		{
			var qsb = new PathAndQueryBuilder("/V2/Authenticate");
			qsb.AddParameter("sid", sid);
			var httpResponse = await PerformHttpRequestAsync(null, "POST", qsb.BuildPathAndQuery()).ConfigureAwait(false);
			return Encoding.UTF8.GetString(httpResponse);
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

		private Task<string> AuthenticateByCertificateAsync(byte[] certificateBytes, bool useLocalSystemStorage, string key, string id)
		{
			var qsb = new PathAndQueryBuilder("/V2/Authenticate");
			var authenticationWithKey = !string.IsNullOrEmpty(key);
			if (authenticationWithKey)
			{
				qsb.AddParameter("key", key);
				qsb.AddParameter("id", id);
			}

			return PerformHttpRequestAsync(
				null,
				"POST",
				qsb.BuildPathAndQuery(),
				certificateBytes,
				responseContent => Convert.ToBase64String(crypt.Decrypt(responseContent, useLocalSystemStorage)));
		}

		private Task<string> ConfirmAuthenticationByCertificateAsync(byte[] certificateBytes, string token, bool saveBinding)
		{
			var qsb = new PathAndQueryBuilder("/V2/AuthenticateConfirm");
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
			var qsb = new PathAndQueryBuilder("/V2/AuthenticateConfirm");
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
	}
}