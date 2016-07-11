using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Diadoc.Api.Cryptography
{
	//������ � ��������� ��� ������ � Api
	//�������� ������� � �������� ����� ����� � MSDN
	public static class Api
	{
		[DllImport("Crypt32.dll", SetLastError = true)]
		public static extern bool CryptDecryptMessage(ref CRYPT_DECRYPT_MESSAGE_PARA parameters, IntPtr encryptedData, Int32 encryptedDataSize, IntPtr decryptedDataBuffer, ref int bufferLength, IntPtr certificate);

		[DllImport("Crypt32.dll", SetLastError = true)]
		public static extern bool CryptSignMessage(ref CRYPT_SIGN_MESSAGE_PARA parameters, Boolean detachedSignature, Int32 contentsCount, IntPtr[] contents, Int32[] contentsSizes, IntPtr buffer, ref Int32 signatureSize);

		[DllImport("Crypt32.dll", SetLastError = true)]
		public static extern bool CryptVerifyDetachedMessageSignature(ref CRYPT_VERIFY_MESSAGE_PARA parameters, Int32 signerIndex, Byte[] signature, Int32 signatureSize, Int32 contentsCount, IntPtr[] contents, Int32[] contentsSizes, [Out] out IntPtr certificate);

		[DllImport("Crypt32.dll", SetLastError = true)]
		public static extern Boolean CertCloseStore(IntPtr store, Int32 flags);

		[DllImport("Crypt32.dll", SetLastError = true)]
		public static extern IntPtr CertCreateCertificateContext(Int32 encoding, Byte[] certificateData, Int32 certificateDataSize);

		[DllImport("Crypt32.dll")]
		public static extern IntPtr CertDuplicateCertificateContext(IntPtr certificate);

		[DllImport("Crypt32.dll", SetLastError = true)]
		public static extern IntPtr CertEnumCertificatesInStore(IntPtr store, IntPtr previousCertificate);

		[DllImport("Crypt32.dll")]
		public static extern Boolean CertFreeCertificateContext(IntPtr certificate);

		[DllImport("Crypt32.dll", SetLastError = true)]
		public static extern bool CertGetCertificateContextProperty(IntPtr certificate, int propertyId, IntPtr buffer, ref int bufferSize);

		[DllImport("Crypt32.dll", SetLastError = true)]
		public static extern IntPtr CertOpenStore(IntPtr storeProvider, Int32 encoding, IntPtr cryptoProvider, Int32 flags, Byte[] parameters);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool CryptAcquireContext(ref IntPtr hProv, string pszContainer, string pszProvider, int dwProvType, int dwFlags);

		[DllImport("Crypt32.dll", SetLastError = true)]
		public static extern IntPtr CryptMsgOpenToEncode(int dwMsgEncodingType, int dwFlags, int dwMsgType, ref CMSG_SIGNED_ENCODE_INFO pvMsgEncodeInfo, string pszInnerContentObjID, ref CMSG_STREAM_INFO pStreamInfo);

		[DllImport("Crypt32.dll", SetLastError = true)]
		public static extern bool CryptMsgClose(IntPtr hCryptMsg);

		[DllImport("Crypt32.dll", SetLastError = true)]
		public static extern bool CryptMsgUpdate(IntPtr hCryptMsg, IntPtr pbData, int cbData, bool fFinal);

		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool CryptReleaseContext(IntPtr hProv, int dwFlags);

		[DllImport("crypt32.dll", EntryPoint = "CertDuplicateCertificateContext", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern CertHandle CertDuplicateCertificateContextEx([In] IntPtr pCertContext);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr LocalFree(IntPtr handle);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeHandle LocalAlloc([In] uint uFlags, [In] IntPtr sizetdwBytes);

		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CertGetCertificateContextProperty([In] CertHandle pCertContext, [In] uint dwPropId, [In, Out] IntPtr pvData, [In, Out] ref uint pcbData);

		[StructLayout(LayoutKind.Sequential)]
		public struct CRYPT_SIGN_MESSAGE_PARA
		{
			internal Int32 size;
			internal Int32 encoding;
			internal IntPtr signerCertificate;
			internal CRYPT_ALGORITHM_IDENTIFIER hashAlgorithm;
			internal IntPtr hashAlgorithmAdditionalParameters;
			internal Int32 certificatesCount;
			internal IntPtr certificates;
			internal Int32 revocationListsCount;
			internal IntPtr revocationLists;
			internal Int32 authenticatedAttributesCount;
			internal IntPtr authenticatedAttributes;
			internal Int32 unauthenticatedAttributesCount;
			internal IntPtr unauthenticatedAttributes;
			internal Int32 flags;
			internal Int32 innerContentType;
			internal CRYPT_ALGORITHM_IDENTIFIER hashEncryptionAlgorithm;
			internal IntPtr hashEncryptionAlgorithmAdditionalParameters;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CRYPT_VERIFY_MESSAGE_PARA
		{
			internal Int32 size;
			internal Int32 encoding;
			internal IntPtr cryptoProvider;
			internal IntPtr getSignerCertificateCallback;
			internal IntPtr callbackCookie;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CRYPT_DECRYPT_MESSAGE_PARA
		{
			internal Int32 size;
			internal Int32 encoding;
			internal Int32 storesCount;
			internal IntPtr stores;
			internal Int32 flags;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CERT_CONTEXT
		{
			public Int32 encoding;
			public IntPtr encodedCertificate;
			public Int32 encodedCertificateSize;
			public IntPtr certificateInformation;
			public IntPtr certificateStore;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CRYPT_ALGORITHM_IDENTIFIER
		{
			public string objectIdAnsiString;
			public CRYPT_BLOB parameters;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CRYPT_BLOB
		{
			public CRYPT_BLOB(IntPtr data, int dataSize)
			{
				this.dataSize = dataSize;
				this.data = data;
			}

			public byte[] ReadBytes()
			{
				var result = new byte[dataSize];
				Marshal.Copy(data, result, 0, dataSize);
				return result;
			}

			public override bool Equals(object otherObject)
			{
				if(GetType() != otherObject.GetType()) return false;
				var otherBlob = (CRYPT_BLOB)otherObject;
				return Bytes.Compare(ReadBytes(), otherBlob.ReadBytes()) == 0;
			}

			public override int GetHashCode()
			{
				throw new NotSupportedException();
			}

			public int dataSize;
			public IntPtr data;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CERT_ID
		{
			public int dwIdChoice;
			public BLOB issuerSerialNumberOrKeyIdOrHashId;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CMSG_SIGNER_ENCODE_INFO
		{
			public int cbSize;
			public IntPtr pCertInfo;
			public IntPtr hCryptProvOrhNCryptKey;
			public int dwKeySpec;
			public CRYPT_ALGORITHM_IDENTIFIER hashAlgorithm;
			public IntPtr pvHashAuxInfo;
			public int cAuthAttr;
			public IntPtr rgAuthAttr;
			public int cUnauthAttr;
			public IntPtr rgUnauthAttr;
			public CERT_ID signerId;
			public CRYPT_ALGORITHM_IDENTIFIER hashEncryptionAlgorithm;
			public IntPtr pvHashEncryptionAuxInfo;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct BLOB
		{
			public int cbData;
			public IntPtr pbData;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CMSG_SIGNED_ENCODE_INFO
		{
			public int cbSize;
			public int cSigners;
			public IntPtr rgSigners;
			public int cCertEncoded;
			public IntPtr rgCertEncoded;
			public int cCrlEncoded;
			public IntPtr rgCrlEncoded;
			public int cAttrCertEncoded;
			public IntPtr rgAttrCertEncoded;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CMSG_STREAM_INFO
		{
			public int cbContent;
			public StreamOutputCallbackDelegate pfnStreamOutput;
			public IntPtr pvArg;
		}

		public delegate bool StreamOutputCallbackDelegate(IntPtr pvArg, IntPtr pbData, int cbData, bool fFinal);

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		[ComVisible(false)]
		internal struct CRYPT_KEY_PROV_INFO
		{
			internal string pwszContainerName;
			internal string pwszProvName;
			internal uint dwProvType;
			internal uint dwFlags;
			internal uint cProvParam;
			internal IntPtr rgProvParam;
			internal uint dwKeySpec;
		}



		public const int CERT_KEY_PROV_INFO_PROP_ID = 2;
		public const int CERT_HASH_PROP_ID = 3;
		public const int CERT_STORE_PROV_SYSTEM = 10;
		public const int CERT_STORE_READONLY_FLAG = 0x00008000;
		public const int CERT_SYSTEM_STORE_CURRENT_USER = 0x00010000;
		public const int CERT_SYSTEM_STORE_LOCAL_MACHINE = 0x00020000;
		public const int CRYPT_E_NO_SIGNER = -2146885618;
		public const int CRYPT_E_NOT_FOUND = -2146885628;
		public const int ENCODING = X509_ASN_ENCODING | PKCS_7_ASN_ENCODING;
		public const int ERROR_NO_MORE_FILES = 18;
		public const int PKCS_7_ASN_ENCODING = 0x00010000;
		public const int X509_ASN_ENCODING = 0x00000001;
		public const int NTE_BAD_SIGNATURE = unchecked((int)0x80090006);
		public const int CMSG_SIGNED = 2;
		public const int CMSG_DETACHED_FLAG = 0x00000004;
		public const string OID_GOST_34_11_94 = "1.2.643.2.2.9";

		public class CertHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			public CertHandle()
				: base(true)
			{}

			public CertHandle(bool ownsHandle)
				: base(ownsHandle)
			{}

			protected override bool ReleaseHandle()
			{
				return CertFreeCertificateContext(handle);
			}
		}


		public class SafeHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			public SafeHandle(IntPtr handle)
				: base(true)
			{
				SetHandle(handle);
			}


			public SafeHandle()
				: base(true)
			{}

			protected override bool ReleaseHandle()
			{
				return LocalFree(handle) == IntPtr.Zero;
			}
		}
	}
}