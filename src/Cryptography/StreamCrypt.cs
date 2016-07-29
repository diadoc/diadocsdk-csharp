using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Diadoc.Api.Annotations;

namespace Diadoc.Api.Cryptography
{
	internal sealed class StreamCrypt : IDisposable
	{
		private const int BufferSize = 1024 * 1024;

		private readonly IntPtr hProv;
		private readonly IntPtr signerInfoPtr;
		private readonly IntPtr certBlobsPtr;
		private Api.CMSG_SIGNED_ENCODE_INFO signedInfo;
		private readonly byte[] buffer = new byte[BufferSize];

		public StreamCrypt(X509Certificate2 cert)
		{
			hProv = IntPtr.Zero;
			signerInfoPtr = IntPtr.Zero;
			certBlobsPtr = IntPtr.Zero;

			try
			{
				var chain = new X509Chain();
				chain.Build(cert);
				var chainElements = new X509ChainElement[chain.ChainElements.Count];
				chain.ChainElements.CopyTo(chainElements, 0);

				// Get certs in chain
				var certs = new X509Certificate2[chainElements.Length];
				for (var i = 0; i < chainElements.Length; i++)
				{
					certs[i] = chainElements[i].Certificate;
				}

				// Get context of all certs in chain
				var certContexts = new Api.CERT_CONTEXT[certs.Length];
				for (var i = 0; i < certs.Length; i++)
				{
					certContexts[i] = (Api.CERT_CONTEXT)Marshal.PtrToStructure(certs[i].Handle, typeof(Api.CERT_CONTEXT));
				}

				// Get cert blob of all certs
				var certBlobs = new Api.BLOB[certContexts.Length];
				for (var i = 0; i < certContexts.Length; i++)
				{
					certBlobs[i].cbData = certContexts[i].encodedCertificateSize;
					certBlobs[i].pbData = certContexts[i].encodedCertificate;
				}

				// Get CSP of client certificate

				Api.CRYPT_KEY_PROV_INFO csp;
				GetPrivateKeyInfo(GetCertContext(cert), out csp);

				var bResult = Api.CryptAcquireContext(
					ref hProv,
					csp.pwszContainerName,
					csp.pwszProvName,
					(int)csp.dwProvType,
					0);

				if (!bResult)
				{
					var lastWin32Error = Marshal.GetLastWin32Error();

					throw new Exception(
						"CryptAcquireContext error #" + lastWin32Error,
						new Win32Exception(lastWin32Error));
				}

				// Populate Signer Info struct
				var signerInfo = new Api.CMSG_SIGNER_ENCODE_INFO();
				signerInfo.cbSize = Marshal.SizeOf(signerInfo);
				signerInfo.pCertInfo = certContexts[0].certificateInformation;
				signerInfo.hCryptProvOrhNCryptKey = hProv;
				signerInfo.dwKeySpec = (int)csp.dwKeySpec;
				signerInfo.hashAlgorithm.objectIdAnsiString = Api.OID_GOST_34_11_94;

				// Populate Signed Info struct
				var localSignedInfo = new Api.CMSG_SIGNED_ENCODE_INFO();
				signerInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(signerInfo));
				Marshal.StructureToPtr(signerInfo, signerInfoPtr, false);
				localSignedInfo.cbSize = Marshal.SizeOf(localSignedInfo);
				localSignedInfo.cSigners = 1;
				localSignedInfo.rgSigners = signerInfoPtr;
				localSignedInfo.cCertEncoded = certBlobs.Length;

				certBlobsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(certBlobs[0]) * certBlobs.Length);
				for (var i = 0; i < certBlobs.Length; i++)
				{
					Marshal.StructureToPtr(certBlobs[i], new IntPtr(certBlobsPtr.ToInt64() + Marshal.SizeOf(certBlobs[i]) * i), false);
				}

				localSignedInfo.rgCertEncoded = certBlobsPtr;
				signedInfo = localSignedInfo;
			}
			catch (Exception)
			{
				if (certBlobsPtr != IntPtr.Zero)
					Marshal.FreeHGlobal(certBlobsPtr);

				if (signerInfoPtr != IntPtr.Zero)
					Marshal.FreeHGlobal(signerInfoPtr);

				if (hProv != IntPtr.Zero)
					Api.CryptReleaseContext(hProv, 0);

				throw;
			}
		}

		public byte[] Encode([NotNull] byte[] content)
		{
			if (content == null)
				throw new ArgumentNullException(nameof(content));
			if (content.Length == 0)
				throw new ArgumentOutOfRangeException(nameof(content), nameof(content) + " is empty");


			using (var memoryStream = new MemoryStream())
			using (var inputStream = new MemoryStream(content))
			{
				// Populate Stream Info struct
				var streamInfo = new Api.CMSG_STREAM_INFO
				{
					cbContent = content.Length,
					pfnStreamOutput = (pvArg, pbData, cbData, fFinal) =>
					{
						Marshal.Copy(pbData, buffer, 0, cbData);
						memoryStream.Write(buffer, 0, cbData);
						return true;
					}
				};

				// Open message to encode
				var hMsg = Api.CryptMsgOpenToEncode(
					Api.X509_ASN_ENCODING | Api.PKCS_7_ASN_ENCODING,
					Api.CMSG_DETACHED_FLAG,
					Api.CMSG_SIGNED,
					ref signedInfo,
					null,
					ref streamInfo);
				try
				{
					if (hMsg == IntPtr.Zero)
					{
						var lastWin32Error = Marshal.GetLastWin32Error();

						throw new Exception(
							"CryptMsgOpenToEncode error #" + lastWin32Error,
							new Win32Exception(lastWin32Error));
					}

					// Process the whole message
					ProcessMessage(hMsg, inputStream);
				}
				finally
				{
					if (hMsg != IntPtr.Zero)
						Api.CryptMsgClose(hMsg);
				}

				return memoryStream.ToArray();
			}
		}

		private static void ProcessMessage(IntPtr hMsg, Stream dataStream)
		{
			var streamSize = dataStream.Length;
			var gchandle = new GCHandle();
			var dwSize = (int)(streamSize < BufferSize ? streamSize : BufferSize);
			var pbData = new byte[dwSize];

			try
			{
				var dwRemaining = streamSize;
				gchandle = GCHandle.Alloc(pbData, GCHandleType.Pinned);
				var pbPtr = gchandle.AddrOfPinnedObject();

				while (dwRemaining > 0)
				{
					dataStream.Read(pbData, 0, dwSize);
					// Update message piece by piece
					var bResult = Api.CryptMsgUpdate(hMsg, pbPtr, dwSize, dwRemaining <= dwSize);
					if (!bResult)
					{
						var lastWin32Error = Marshal.GetLastWin32Error();

						throw new Exception(
							"CryptMsgUpdate error #" + lastWin32Error,
							new Win32Exception(lastWin32Error));
					}

					dwRemaining -= dwSize;
					if (dwRemaining < dwSize)
						dwSize = (int)dwRemaining;
				}
			}
			finally
			{
				if (gchandle.IsAllocated)
					gchandle.Free();
			}
		}

		private static Api.CertHandle GetCertContext(X509Certificate2 certificate)
		{
			var handle = Api.CertDuplicateCertificateContextEx(certificate.Handle);
			GC.KeepAlive(certificate);
			return handle;
		}

		private static void GetPrivateKeyInfo(Api.CertHandle safeCertContext, out Api.CRYPT_KEY_PROV_INFO parameters)
		{
			parameters = new Api.CRYPT_KEY_PROV_INFO();
			uint pcbData = 0;

			if (!Api.CertGetCertificateContextProperty(safeCertContext, Api.CERT_KEY_PROV_INFO_PROP_ID, IntPtr.Zero, ref pcbData))
			{
				var lastWin32Error = Marshal.GetLastWin32Error();

				if (lastWin32Error != Api.CRYPT_E_NOT_FOUND)
					throw new CryptographicException(lastWin32Error);

				return;
			}

			using (var pvData = Api.LocalAlloc(0, new IntPtr(pcbData)))
			{
				if (!Api.CertGetCertificateContextProperty(safeCertContext, Api.CERT_KEY_PROV_INFO_PROP_ID, pvData.DangerousGetHandle(), ref pcbData))
				{
					var lastWin32Error = Marshal.GetLastWin32Error();

					if (lastWin32Error != Api.CRYPT_E_NOT_FOUND)
						throw new CryptographicException(lastWin32Error);

					return;
				}

				parameters =
					(Api.CRYPT_KEY_PROV_INFO)
						Marshal.PtrToStructure(pvData.DangerousGetHandle(), typeof(Api.CRYPT_KEY_PROV_INFO));
			}
		}

		public void Dispose()
		{
			if (certBlobsPtr != IntPtr.Zero)
				Marshal.FreeHGlobal(certBlobsPtr);

			if (signerInfoPtr != IntPtr.Zero)
				Marshal.FreeHGlobal(signerInfoPtr);

			if (hProv != IntPtr.Zero)
				Api.CryptReleaseContext(hProv, 0);
		}
	}
}