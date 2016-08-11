using System;
using System.Runtime.InteropServices;

namespace Diadoc.Api.Cryptography
{
	public interface IExtendedWinApiCrypt : ICrypt, IDisposable
	{
		byte[] Sign(byte[] content);
	}

	public sealed class ExtendedWinApiCrypt : WinApiCrypt, IExtendedWinApiCrypt
	{
		private readonly IntPtr certificate;
		private GCHandle certificatesHandle;

		private bool disposed;

		public ExtendedWinApiCrypt(byte[] certificateContent)
		{
			certificate = CertificateWithPrivateKeyFinder.GetCertificateWithPrivateKey(certificateContent);
			certificatesHandle = GCHandle.Alloc(new[] { certificate }, GCHandleType.Pinned);
		}

		public byte[] Sign(byte[] content)
		{
			return Sign(content, certificate, certificatesHandle);
		}

		public void Dispose()
		{
			if (disposed)
				return;

			disposed = true;

			certificatesHandle.Free();
			Api.CertFreeCertificateContext(certificate);
		}
	}
}