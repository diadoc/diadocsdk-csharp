using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("8D421F65-3D2E-4994-AB89-028222D3E9A5")]
	public enum RevocationStatus
	{
		UnknownRevocationStatus = 0,
		RevocationStatusNone = 1,
		RevocationIsRequestedByMe = 2,
		RequestsMyRevocation = 3,
		RevocationAccepted = 4,
		RevocationRejected = 5
	}
}