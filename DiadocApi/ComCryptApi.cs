using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Diadoc.Api.Com;
using Diadoc.Api.Cryptography;

namespace Diadoc.Api
{
	[ComVisible(true)]
	[Guid("E3DE40BB-93AE-4609-8AE4-1EC8886915F0")]
	public interface IComCryptApi
	{
		ReadonlyList GetPersonalCertificates(bool onlyWithPrivateKey = false, bool useLocalSystemStorage = false);
		void Sign(string contentFilename, string thumbprint, string signatureFilename, bool useLocalSystemStorage = false);
		Certificate Verify(string contentFilename, string signatureFilename);
		string SignBase64(string contentBase64, string thumbprint, bool useLocalSystemStorage = false);
		Certificate VerifyBase64(string contentBase64, string signatureBase64);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ComCryptApi")]
	[Guid("17FF6D8A-130B-4551-B35E-1714EEEBE49E")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IComCryptApi))]
	public class ComCryptApi : SafeComObject, IComCryptApi
	{
		private readonly ICrypt crypt = new WinApiCrypt();

		public ReadonlyList GetPersonalCertificates(bool onlyWithPrivateKey = false, bool useLocalSystemStorage = false)
		{
			var certs = crypt.GetPersonalCertificates(onlyWithPrivateKey, useLocalSystemStorage);
			var personalCertificates = new ArrayList(certs.Count);
			foreach (X509Certificate2 cert in certs)
				personalCertificates.Add(new Certificate(cert));
			return new ReadonlyList(personalCertificates);
		}

		public void Sign(string contentFilename, string thumbprint, string signatureFilename, bool useLocalSystemStorage = false)
		{
			var signatureContent = crypt.Sign(File.ReadAllBytes(contentFilename),
				crypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage).RawData);
			File.Delete(signatureFilename);
			File.WriteAllBytes(signatureFilename, signatureContent);
		}

		public Certificate Verify(string contentFilename, string signatureFilename)
		{
			var certificates = crypt.VerifySignature(File.ReadAllBytes(contentFilename), File.ReadAllBytes(signatureFilename));
			return new Certificate(new X509Certificate2(certificates[0]));
		}

		public string SignBase64(string contentBase64, string thumbprint, bool useLocalSystemStorage = false)
		{
			var signatureContent = crypt.Sign(Convert.FromBase64String(contentBase64),
				crypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage).RawData);
			return Convert.ToBase64String(signatureContent);
		}

		public Certificate VerifyBase64(string contentBase64, string signatureBase64)
		{
			var certificates = crypt.VerifySignature(Convert.FromBase64String(contentBase64), Convert.FromBase64String(signatureBase64));
			return new Certificate(new X509Certificate2(certificates[0]));
		}
	}
}