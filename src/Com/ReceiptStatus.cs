using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("241AA0D0-41D6-4311-831B-5B7848FCBF25")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "ReceiptStatus", Namespace = "https://diadoc-api.kontur.ru")]
	public enum ReceiptStatus
	{
		UnknownReceiptStatus = 0,
		ReceiptStatusNone = 1,
		ReceiptStatusFinished = 2,
		ReceiptStatusHaveToCreateReceipt = 3,
		ReceiptStatusWaitingForReceipt = 4
	}
}