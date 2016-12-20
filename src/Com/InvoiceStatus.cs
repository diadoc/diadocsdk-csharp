using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("82A17952-BA3A-4BD2-8B4B-0FC68AAB8F20")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "InvoiceStatus", Namespace = "https://diadoc-api.kontur.ru")]
	public enum InvoiceStatus
	{
		UnknownInvoiceStatus = 0,
		OutboundWaitingForInvoiceReceipt = 1,
		OutboundNotFinished = 2,
		OutboundFinished = 3,
		OutboundWaitingForSenderSignature = 6,
		OutboundInvalidSenderSignature = 7,
		InboundNotFinished = 4,
		InboundFinished = 5
	}
}