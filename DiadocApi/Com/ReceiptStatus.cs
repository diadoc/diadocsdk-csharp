using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("241AA0D0-41D6-4311-831B-5B7848FCBF25")]
	public enum ReceiptStatus
	{
		UnknownReceiptStatus = 0,
		ReceiptStatusNone = 1,
		ReceiptStatusFinished = 2,
		ReceiptStatusHaveToCreateReceipt = 3,
		ReceiptStatusWaitingForReceipt = 4
	}
}