using System;
using System.Text;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Auth;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<string> AuthenticateAsync(string login, string password, string key = null, string id = null)
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

			return PerformHttpRequestAsync(
				null,
				"/AuthenticateByLogin",
				authenticateByLoginInfo,
				response => Encoding.UTF8.GetString(response));
		}

		public Task<string> AuthenticateByKeyAsync(string key, string id)
		{
			var authenticateByTrustedServiceKeyInfo = new AuthenticateByTrustedServiceKeyInfo
			{
				Id = id,
				Key = key
			};

			return PerformHttpRequestAsync(
				null,
				"/AuthenticateByTrustedServiceKey",
				authenticateByTrustedServiceKeyInfo,
				response => Encoding.UTF8.GetString(response));
		}

		[Obsolete("Use /AuthenticateByCertificate")]
		public Task<string> AuthenticateAsync(byte[] certificateBytes, bool useLocalSystemStorage = false)
		{
			return PerformHttpRequestAsync(
				null,
				"POST",
				"/Authenticate",
				certificateBytes,
				response => Convert.ToBase64String(crypt.Decrypt(response, useLocalSystemStorage)));
		}

		[Obsolete("Use /AuthenticateByCertificate")]
		public Task<string> AuthenticateAsync(string thumbprint, bool useLocalSystemStorage = false)
		{
			var userCert = crypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage);

			return AuthenticateAsync(userCert.RawData, useLocalSystemStorage);
		}

		public async Task<string> AuthenticateWithKeyAsync(byte[] certificateBytes, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true)
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

			var token = await PerformHttpRequestAsync(
					null,
					"/AuthenticateByCertificate",
					authenticateByCertificateInfo,
					response => Convert.ToBase64String(crypt.Decrypt(response, useLocalSystemStorage)))
				.ConfigureAwait(false);

			return autoConfirm
				? AuthenticateWithKeyConfirm(
					certificateBytes,
					token,
					!string.IsNullOrEmpty(key))
				: token;
		}

		public async Task<string> AuthenticateWithKeyAsync(string thumbprint, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true)
		{
			var userCert = crypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage);
			var token = await AuthenticateWithKeyAsync(userCert.RawData, useLocalSystemStorage, key, id, false).ConfigureAwait(false);

			return autoConfirm
				? await AuthenticateWithKeyConfirmAsync(
						userCert.Thumbprint,
						token,
						!string.IsNullOrEmpty(key))
					.ConfigureAwait(false)
				: token;
		}

		public Task<string> AuthenticateWithKeyConfirmAsync(byte[] certificateBytes, string token, bool saveBinding = false)
		{
			var confirmQsb = new PathAndQueryBuilder("/V2/AuthenticateConfirm");
			confirmQsb.AddParameter("token", token);
			confirmQsb.AddParameter("saveBinding", saveBinding.ToString());

			return PerformHttpRequestAsync(
				null,
				"POST",
				confirmQsb.BuildPathAndQuery(),
				certificateBytes,
				response => Encoding.UTF8.GetString(response));
		}

		public Task<string> AuthenticateWithKeyConfirmAsync(string thumbprint, string token, bool saveBinding = false)
		{
			var confirmQsb = new PathAndQueryBuilder("/V2/AuthenticateConfirm");
			confirmQsb.AddParameter("thumbprint", thumbprint);
			confirmQsb.AddParameter("token", token);
			confirmQsb.AddParameter("saveBinding", saveBinding.ToString());

			return PerformHttpRequestAsync(
				null,
				"POST",
				confirmQsb.BuildPathAndQuery(),
				null,
				response => Encoding.UTF8.GetString(response));
		}
	}
}
