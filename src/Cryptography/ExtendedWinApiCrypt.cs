using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Diadoc.Api.Cryptography
{
	public interface IExtendedWinApiCrypt : ICrypt
	{
		byte[] Sign(byte[] content);
	}

	public sealed class ExtendedWinApiCrypt : IExtendedWinApiCrypt, IDisposable
	{
		private readonly WinApiCrypt winApiCrypt = new WinApiCrypt();
		private readonly IntPtr certificate;
		private GCHandle certificatesHandle;
		private readonly StreamCrypt streamCrypt;

		public ExtendedWinApiCrypt(byte[] certificateContent)
		{
			certificate = CertificateWithPrivateKeyFinder.GetCertificateWithPrivateKey(certificateContent);
			certificatesHandle = GCHandle.Alloc(new[] { certificate }, GCHandleType.Pinned);
			streamCrypt = new StreamCrypt(new X509Certificate2(certificate));
		}

		public byte[] Sign(byte[] content)
		{
			return streamCrypt.Encode(content);
		}

		public byte[] Sign(byte[] content, byte[] certificateContent)
		{
			return winApiCrypt.Sign(content, certificateContent);	
		}

		public List<byte[]> VerifySignature(byte[] content, byte[] signatures)
		{
			return winApiCrypt.VerifySignature(content, signatures);
		}

		public byte[] Decrypt(byte[] encryptedContent, bool userLocalSystemStorage = false)
		{
			return winApiCrypt.Decrypt(encryptedContent, userLocalSystemStorage);
		}

		public List<X509Certificate2> GetPersonalCertificates(bool onlyWithPrivateKey, bool useLocalSystemStorage = false)
		{
			return winApiCrypt.GetPersonalCertificates(onlyWithPrivateKey, useLocalSystemStorage);	
		}

		public X509Certificate2 GetCertificateWithPrivateKey(string thumbprint, bool useLocalSystemStorage = false)
		{
			return winApiCrypt.GetCertificateWithPrivateKey(thumbprint, useLocalSystemStorage);
		}

		public void Dispose()
		{
			certificatesHandle.Free();
			streamCrypt.Dispose();
			Api.CertFreeCertificateContext(certificate);
		}
	}
}