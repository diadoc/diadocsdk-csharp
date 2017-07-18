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
			var qsb = new PathAndQueryBuilder("/Authenticate");
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
			var qsb = new PathAndQueryBuilder("/Authenticate");
			qsb.AddParameter("key", key);
			qsb.AddParameter("id", id);
			var httpResponse = await PerformHttpRequestAsync(null, "POST", qsb.BuildPathAndQuery()).ConfigureAwait(false);
			return Encoding.UTF8.GetString(httpResponse);
		}

		public Task<string> AuthenticateAsync(byte[] certificateBytes, bool useLocalSystemStorage = false)
		{
			return PerformHttpRequestAsync(null, "POST", "/Authenticate", certificateBytes,
				responseContent => Convert.ToBase64String(crypt.Decrypt(responseContent, useLocalSystemStorage)));
		}

		public Task<string> AuthenticateAsync(string thumbprint, bool useLocalSystemStorage = false)
		{
			var userCert = crypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage);
			return AuthenticateAsync(userCert.RawData, useLocalSystemStorage);
		}

		public async Task<string> AuthenticateWithKeyAsync(byte[] certificateBytes, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true)
		{
			var qsb = new PathAndQueryBuilder("/V2/Authenticate");
			var authenticationWithKey = !string.IsNullOrEmpty(key);
			if (authenticationWithKey)
			{
				qsb.AddParameter("key", key);
				qsb.AddParameter("id", id);
			}
			var token = await PerformHttpRequestAsync(null, "POST", qsb.BuildPathAndQuery(), certificateBytes,
				responseContent => Convert.ToBase64String(crypt.Decrypt(responseContent, useLocalSystemStorage)))
				.ConfigureAwait(false);

			return autoConfirm
				? await AuthenticateWithKeyConfirmAsync(certificateBytes, token, saveBinding: authenticationWithKey).ConfigureAwait(false)
				: token;
		}

		public async Task<string> AuthenticateWithKeyAsync(string thumbprint, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true)
		{
			var authenticationWithKey = !string.IsNullOrEmpty(key);
			var userCert = crypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage);
			var token = await AuthenticateWithKeyAsync(userCert.RawData, useLocalSystemStorage, key, id, false).ConfigureAwait(false);
			return autoConfirm
				? await AuthenticateWithKeyConfirmAsync(userCert.Thumbprint, token, saveBinding: authenticationWithKey).ConfigureAwait(false)
				: token;
		}

		public Task<string> AuthenticateWithKeyConfirmAsync(byte[] certificateBytes, string token, bool saveBinding = false)
		{
			var confirmQsb = new PathAndQueryBuilder("/V2/AuthenticateConfirm");
			confirmQsb.AddParameter("token", token);
			confirmQsb.AddParameter("saveBinding", saveBinding.ToString());
			return PerformHttpRequestAsync(null, "POST", confirmQsb.BuildPathAndQuery(), certificateBytes,
				responseContent => Encoding.UTF8.GetString(responseContent));
		}

		public Task<string> AuthenticateWithKeyConfirmAsync(string thumbprint, string token, bool saveBinding = false)
		{
			var confirmQsb = new PathAndQueryBuilder("/V2/AuthenticateConfirm");
			confirmQsb.AddParameter("thumbprint", thumbprint);
			confirmQsb.AddParameter("token", token);
			confirmQsb.AddParameter("saveBinding", saveBinding.ToString());
			return PerformHttpRequestAsync(null, "POST", confirmQsb.BuildPathAndQuery(), null,
				responseContent => Encoding.UTF8.GetString(responseContent));
		}
	}
}
