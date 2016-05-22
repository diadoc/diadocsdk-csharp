using System;
using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[Flags]
	[ComVisible(true)]
	[Guid("5198883B-F68A-480B-A4D2-1AFF7BA50A01")]
	public enum CertificateChainStatus
	{
		NoError = 0,
		NotTimeValid = 1,
		NotTimeNested = 2,
		Revoked = 4,
		NotSignatureValid = 8,
		NotValidForUsage = 16,
		UntrustedRoot = 32,
		RevocationStatusUnknown = 64,
		Cyclic = 128,
		InvalidExtension = 256,
		InvalidPolicyConstraints = 512,
		InvalidBasicConstraints = 1024,
		InvalidNameConstraints = 2048,
		HasNotSupportedNameConstraint = 4096,
		HasNotDefinedNameConstraint = 8192,
		HasNotPermittedNameConstraint = 16384,
		HasExcludedNameConstraint = 32768,
		PartialChain = 65536,
		CtlNotTimeValid = 131072,
		CtlNotSignatureValid = 262144,
		CtlNotValidForUsage = 524288,
		OfflineRevocation = 16777216,
		NoIssuanceChainPolicy = 33554432,
	}
}