using System;
using System.Text;
using Diadoc.Api.Http;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public string Authenticate(string login, string password, string key = null, string id = null)
		{
			var qsb = new PathAndQueryBuilder("/Authenticate");
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
			var qsb = new PathAndQueryBuilder("/Authenticate");
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
			return PerformHttpRequest(null, "POST", "/Authenticate", certificateBytes,
				responseContent => Convert.ToBase64String(crypt.Decrypt(responseContent, useLocalSystemStorage)));
		}

		public string Authenticate(string thumbprint, bool useLocalSystemStorage = false)
		{
			var userCert = crypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage);
			return Authenticate(userCert.RawData, useLocalSystemStorage);
		}

		public string AuthenticateWithKey(byte[] certificateBytes, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true)
		{
			var qsb = new PathAndQueryBuilder("/V2/Authenticate");
			var authenticationWithKey = !string.IsNullOrEmpty(key);
			if (authenticationWithKey)
			{
				qsb.AddParameter("key", key);
				qsb.AddParameter("id", id);
			}
			var token = PerformHttpRequest(null, "POST", qsb.BuildPathAndQuery(), certificateBytes,
				responseContent => Convert.ToBase64String(crypt.Decrypt(responseContent, useLocalSystemStorage)));

			return autoConfirm
				? AuthenticateWithKeyConfirm(certificateBytes, token, saveBinding: authenticationWithKey)
				: token;
		}

		public string AuthenticateWithKey(string thumbprint, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true)
		{
			var authenticationWithKey = !string.IsNullOrEmpty(key);
			var userCert = crypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage);
			var token = AuthenticateWithKey(userCert.RawData, useLocalSystemStorage, key, id, false);
			return autoConfirm
				? AuthenticateWithKeyConfirm(userCert.Thumbprint, token, saveBinding: authenticationWithKey)
				: token;
		}

		public string AuthenticateWithKeyConfirm(byte[] certificateBytes, string token, bool saveBinding = false)
		{
			var confirmQsb = new PathAndQueryBuilder("/V2/AuthenticateConfirm");
			confirmQsb.AddParameter("token", token);
			confirmQsb.AddParameter("saveBinding", saveBinding.ToString());
			return PerformHttpRequest(null, "POST", confirmQsb.BuildPathAndQuery(), certificateBytes,
				responseContent => Encoding.UTF8.GetString(responseContent));
		}

		public string AuthenticateWithKeyConfirm(string thumbprint, string token, bool saveBinding = false)
		{
			var confirmQsb = new PathAndQueryBuilder("/V2/AuthenticateConfirm");
			confirmQsb.AddParameter("thumbprint", thumbprint);
			confirmQsb.AddParameter("token", token);
			confirmQsb.AddParameter("saveBinding", saveBinding.ToString());
			return PerformHttpRequest(null, "POST", confirmQsb.BuildPathAndQuery(), null,
				responseContent => Encoding.UTF8.GetString(responseContent));
		}
	}
}
