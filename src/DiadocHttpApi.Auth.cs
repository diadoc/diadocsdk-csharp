using System;
using System.Text;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public string Authenticate(string login, string password, string key = null, string id = null)
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

			return PerformRequest(request);
		}

		public string AuthenticateByKey(string key, string id)
		{
			var qsb = new PathAndQueryBuilder("/V3/Authenticate");
			qsb.AddParameter("type", "trust");
			
			var request = BuildHttpRequest(null,"POST", qsb.BuildPathAndQuery(),null);
			
			request.AddHeader("X-Diadoc-ServiceKey", key);
			request.AddHeader("X-Diadoc-ServiceUserId", id);
			
			return PerformRequest(request);
		}

		private string PerformRequest(HttpRequest request)
		{
			var httpResponse = HttpClient.PerformHttpRequest(request);
			return Encoding.UTF8.GetString(httpResponse.Content);
		}

		public string AuthenticateBySid(string sid)
		{
			var qsb = new PathAndQueryBuilder("/V3/Authenticate");
			qsb.AddParameter("type", "sid");
			var request = BuildRequest(
				null,
				"POST", 
				qsb.BuildPathAndQuery(),
				new HttpRequestBody(Encoding.UTF8.GetBytes(sid), "text/plain"));
			
			return PerformRequest(request);
		}

		public string Authenticate(byte[] certificateBytes, bool useLocalSystemStorage = false)
		{
			var token = AuthenticateByCertificate(
				certificateBytes,
				useLocalSystemStorage,
				null,
				null);

			return ConfirmAuthenticationByCertificate(certificateBytes, token, false);
		}

		public string Authenticate(string thumbprint, bool useLocalSystemStorage = false)
		{
			var userCert = crypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage);

			var token = AuthenticateByCertificate(
				userCert.RawData,
				useLocalSystemStorage,
				null,
				null);

			return ConfirmAuthenticationByCertificateThumbprint(userCert.Thumbprint, token, false);
		}

		public string AuthenticateWithKey(byte[] certificateBytes, bool useLocalSystemStorage = false,
			string key = null, string id = null, bool autoConfirm = true)
		{
			var authenticationWithKey = !string.IsNullOrEmpty(key);
			var token = AuthenticateByCertificate(certificateBytes, useLocalSystemStorage, key, id);

			return autoConfirm
				? ConfirmAuthenticationByCertificate(certificateBytes, token, authenticationWithKey)
				: token;
		}

		public string AuthenticateWithKey(string thumbprint, bool useLocalSystemStorage = false, string key = null,
			string id = null, bool autoConfirm = true)
		{
			var authenticationWithKey = !string.IsNullOrEmpty(key);
			var userCert = crypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage);

			var token = AuthenticateByCertificate(userCert.RawData, useLocalSystemStorage, key, id);

			return autoConfirm
				? ConfirmAuthenticationByCertificateThumbprint(userCert.Thumbprint, token, authenticationWithKey)
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

		private string AuthenticateByCertificate(byte[] certificateBytes, bool useLocalSystemStorage, string key,
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
			var httpResponse = HttpClient.PerformHttpRequest(request);
			return Convert.ToBase64String(crypt.Decrypt(httpResponse.Content, useLocalSystemStorage));
		}

		private string ConfirmAuthenticationByCertificate(byte[] certificateBytes, string token, bool saveBinding)
		{
			var qsb = new PathAndQueryBuilder("/V3/AuthenticateConfirm");
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
			var qsb = new PathAndQueryBuilder("/V3/AuthenticateConfirm");
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