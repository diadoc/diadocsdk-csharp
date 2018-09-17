using System;
using System.Text;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Auth;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public string Authenticate(string login, string password, string key = null, string id = null)
		{
			var authenticateByLoginInfo = new AuthenticateByLoginInfo
			{
				Login = login,
				Password = password
			};

			if (!string.IsNullOrEmpty(key))
			{
				authenticateByLoginInfo.TrustedServiceKeyInfo = new AuthenticateByTrustedServiceKeyInfo
				{
					Id = id,
					Key = key
				};
			}

			return PerformHttpRequest(
				null,
				"/AuthenticateByLogin",
				authenticateByLoginInfo,
				response => Encoding.UTF8.GetString(response));
		}

		public string AuthenticateByKey(string key, string id)
		{
			var authenticateByTrustedServiceKeyInfo = new AuthenticateByTrustedServiceKeyInfo
			{
				Id = id,
				Key = key
			};

			return PerformHttpRequest(
				null,
				"/AuthenticateByTrustedServiceKey",
				authenticateByTrustedServiceKeyInfo,
				response => Encoding.UTF8.GetString(response));
		}

		[Obsolete("Use /AuthenticateByCertificate")]
		public string Authenticate(byte[] certificateBytes, bool useLocalSystemStorage = false)
		{
			return PerformHttpRequest(
				null,
				"POST",
				"/Authenticate",
				certificateBytes,
				response => Convert.ToBase64String(crypt.Decrypt(response, useLocalSystemStorage)));
		}

		[Obsolete("Use /AuthenticateByCertificate")]
		public string Authenticate(string thumbprint, bool useLocalSystemStorage = false)
		{
			var userCert = crypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage);

			return Authenticate(userCert.RawData, useLocalSystemStorage);
		}

		public string AuthenticateWithKey(byte[] certificateBytes, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true)
		{
			var authenticateByCertificateInfo = new AuthenticateByCertificateInfo
			{
				Certificate = certificateBytes
			};

			if (!string.IsNullOrEmpty(key))
			{
				authenticateByCertificateInfo.TrustedServiceKeyInfo = new AuthenticateByTrustedServiceKeyInfo
				{
					Id = id,
					Key = key
				};
			}

			var token = PerformHttpRequest(
				null,
				"/AuthenticateByCertificate",
				authenticateByCertificateInfo,
				response => Convert.ToBase64String(crypt.Decrypt(response, useLocalSystemStorage)));

			return autoConfirm
				? AuthenticateWithKeyConfirm(
					certificateBytes,
					token,
					!string.IsNullOrEmpty(key))
				: token;
		}

		public string AuthenticateWithKey(string thumbprint, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true)
		{
			var userCert = crypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage);
			var token = AuthenticateWithKey(userCert.RawData, useLocalSystemStorage, key, id, false);

			return autoConfirm
				? AuthenticateWithKeyConfirm(
					userCert.Thumbprint,
					token,
					!string.IsNullOrEmpty(key))
				: token;
		}

		public string AuthenticateWithKeyConfirm(byte[] certificateBytes, string token, bool saveBinding = false)
		{
			var confirmQsb = new PathAndQueryBuilder("/V2/AuthenticateConfirm");
			confirmQsb.AddParameter("token", token);
			confirmQsb.AddParameter("saveBinding", saveBinding.ToString());

			return PerformHttpRequest(
				null,
				"POST",
				confirmQsb.BuildPathAndQuery(),
				certificateBytes,
				response => Encoding.UTF8.GetString(response));
		}

		public string AuthenticateWithKeyConfirm(string thumbprint, string token, bool saveBinding = false)
		{
			var confirmQsb = new PathAndQueryBuilder("/V2/AuthenticateConfirm");
			confirmQsb.AddParameter("thumbprint", thumbprint);
			confirmQsb.AddParameter("token", token);
			confirmQsb.AddParameter("saveBinding", saveBinding.ToString());

			return PerformHttpRequest(
				null,
				"POST",
				confirmQsb.BuildPathAndQuery(),
				null,
				response => Encoding.UTF8.GetString(response));
		}
	}
}
