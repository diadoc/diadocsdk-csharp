using System;
using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("342E90B6-0BE5-4C68-B0CF-6A2C0C1B4683")]
	public interface ICertificateVerificationResult
	{
		bool IsValid { get; }
		DateTime VerificationDateTime { get; }
		ReadonlyList CertificateChainList { get; }
	}

	[ComVisible(true)]
	[Guid("6CC49E57-04C1-41BB-B462-81D06ABE6143")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICertificateVerificationResult))]
	public partial class CertificateVerificationResult : SafeComObject, ICertificateVerificationResult
	{
		public DateTime VerificationDateTime { get { return new DateTime(VerificationTime.Ticks, DateTimeKind.Utc); } }
		public ReadonlyList CertificateChainList { get { return new ReadonlyList(CertificateChain); } }
	}

	[ComVisible(true)]
	[Guid("617634C9-A177-47E8-BA45-3AC34FC76DB0")]
	public interface ICertificateChainElement
	{
		byte[] DerCertificate { get; }
		Com.CertificateChainStatus CertificateChainStatus { get; }
	}

	[ComVisible(true)]
	[Guid("D2169599-03D5-4B4D-B2C3-A7B9E2E18321")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICertificateChainElement))]
	public partial class CertificateChainElement : SafeComObject, ICertificateChainElement
	{
		public CertificateChainStatus CertificateChainStatus { get { return (CertificateChainStatus)CertificateChainStatusFlags; } }
	}

	[ComVisible(true)]
	[Guid("BCE4CC1A-F217-4AD3-B39F-652486ACB805")]
	public interface ISignatureVerificationResult
	{
		bool IsValid { get; }
		CertificateVerificationResult CertificateStatus { get; }
	}

	[ComVisible(true)]
	[Guid("4592915E-E121-4EEB-A40D-A71D2E09C1BC")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISignatureVerificationResult))]
	public partial class SignatureVerificationResult : SafeComObject, ISignatureVerificationResult
	{
	}
}
