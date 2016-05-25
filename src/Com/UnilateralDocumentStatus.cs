using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("B2611D21-88BE-49C9-8DFE-CA4151CBE347")]
	public enum UnilateralDocumentStatus
	{
		UnknownUnilateralDocumentStatus = 0,
		Outbound = 1,
		OutboundWaitingForSenderSignature = 4,
		OutboundInvalidSenderSignature = 5,
		Inbound = 2,
		Internal = 3,
		InternalWaitingForSenderSignature = 6,
		InternalInvalidSenderSignature = 7
	}
}