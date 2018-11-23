using System;
using System.Text;
using Diadoc.Api.Http;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public string Authenticate(string login, string password, string key = null, string id = null)
		{
			var qsb = new PathAndQueryBuilder("/V2/Authenticate");
			qsb.AddParameter("login", login);
			qsb.AddParameter("password", password);
			if (!string.IsNullOrEmpty(key))
			{
				qsb.AddParameter("key", key);
				qsb.AddParameter("id", id);
			}

			var httpResponse = PerformHttpRequest(null, "POST", qsb.BuildPathAndQuery());
			return Encoding.UTF8.GetString(httpResponse);
		}

		public string AuthenticateByKey(string key, string id)
		{
			var qsb = new PathAndQueryBuilder("/V2/Authenticate");
			qsb.AddParameter("key", key);
			qsb.AddParameter("id", id);
			var httpResponse = PerformHttpRequest(null, "POST", qsb.BuildPathAndQuery());
			return Encoding.UTF8.GetString(httpResponse);
		}

		public string AuthenticateBySid(string sid)
		{
			var qsb = new PathAndQueryBuilder("/V2/Authenticate");
			qsb.AddParameter("sid", sid);
			var httpResponse = PerformHttpRequest(null, "POST", qsb.BuildPathAndQuery());
			return Encoding.UTF8.GetString(httpResponse);
		}

		public string Authenticate(byte[] certificateBytes, bool useLocalSystemStorage = false)
		{
			var token = AuthenticateByCertificate(
				certificateBytes,
				useLocalSystemStorage,
				key: null,
				id: null);

			return ConfirmAuthenticationByCertificate(certificateBytes, token, saveBinding: false);
		}

		public string Authenticate(string thumbprint, bool useLocalSystemStorage = false)
		{
			var userCert = crypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage);

			var token = AuthenticateByCertificate(
				userCert.RawData,
				useLocalSystemStorage,
				key: null,
				id: null);

			return ConfirmAuthenticationByCertificateThumbprint(userCert.Thumbprint, token, saveBinding: false);
		}

		public string AuthenticateWithKey(byte[] certificateBytes, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true)
		{
			var authenticationWithKey = !string.IsNullOrEmpty(key);
			var token = AuthenticateByCertificate(certificateBytes, useLocalSystemStorage, key, id);

			return autoConfirm
				? ConfirmAuthenticationByCertificate(certificateBytes, token, saveBinding: authenticationWithKey)
				: token;
		}

		public string AuthenticateWithKey(string thumbprint, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true)
		{
			var authenticationWithKey = !string.IsNullOrEmpty(key);
			var userCert = crypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage);

			var token = AuthenticateByCertificate(userCert.RawData, useLocalSystemStorage, key, id);

			return autoConfirm
				? ConfirmAuthenticationByCertificateThumbprint(userCert.Thumbprint, token, saveBinding: authenticationWithKey)
				: token;
		}

		public string AuthenticateWithKeyConfirm(byte[] certificateBytes, string token, bool saveBinding = false)
		{
			return ConfirmAuthenticationByCertificate(certificateBytes, token, saveBinding);
		}

		public string AuthenticateWithKeyConfirm(string thumbprint, string token, bool saveBinding = false)
		{
			return ConfirmAuthenticationByCertificateThumbprint(thumbprint, token, saveBinding);
		}

		private string AuthenticateByCertificate(byte[] certificateBytes, bool useLocalSystemStorage, string key, string id)
		{
			var qsb = new PathAndQueryBuilder("/V2/Authenticate");
			var authenticationWithKey = !string.IsNullOrEmpty(key);
			if (authenticationWithKey)
			{
				qsb.AddParameter("key", key);
				qsb.AddParameter("id", id);
			}

			return PerformHttpRequest(null,
				"POST",
				qsb.BuildPathAndQuery(),
				certificateBytes,
				responseContent => Convert.ToBase64String(crypt.Decrypt(responseContent, useLocalSystemStorage)));
		}

		private string ConfirmAuthenticationByCertificate(byte[] certificateBytes, string token, bool saveBinding)
		{
			var qsb = new PathAndQueryBuilder("/V2/AuthenticateConfirm");
			qsb.AddParameter("token", token);
			qsb.AddParameter("saveBinding", saveBinding.ToString());
			return PerformHttpRequest(
				null,
				"POST",
				qsb.BuildPathAndQuery(),
				certificateBytes,
				responseContent => Encoding.UTF8.GetString(responseContent));
		}

		private string ConfirmAuthenticationByCertificateThumbprint(string thumbprint, string token, bool saveBinding)
		{
			var qsb = new PathAndQueryBuilder("/V2/AuthenticateConfirm");
			qsb.AddParameter("thumbprint", thumbprint);
			qsb.AddParameter("token", token);
			qsb.AddParameter("saveBinding", saveBinding.ToString());
			return PerformHttpRequest(
				null,
				"POST",
				qsb.BuildPathAndQuery(),
				null,
				responseContent => Encoding.UTF8.GetString(responseContent));
		}
	}
}