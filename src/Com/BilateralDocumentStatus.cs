using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("445699DE-6BB3-4D2E-9151-18EED629A3BA")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "BilateralDocumentStatus", Namespace = "https://diadoc-api.kontur.ru")]
	public enum BilateralDocumentStatus
	{
		UnknownBilateralDocumentStatus = 0,
		OutboundWaitingForRecipientSignature = 1,
		OutboundWithRecipientSignature = 2,
		OutboundWithRecipientPartiallySignature = 19,
		OutboundRecipientSignatureRequestRejected = 3,
		OutboundWaitingForSenderSignature = 10,
		OutboundInvalidSenderSignature = 11,
		InboundWaitingForRecipientSignature = 4,
		InboundWithRecipientSignature = 5,
		InboundWithRecipientPartiallySignature = 20,
		InboundRecipientSignatureRequestRejected = 6,
		InboundInvalidRecipientSignature = 12,
		InternalWaitingForRecipientSignature = 7,
		InternalWithRecipientSignature = 8,
		InternalWithRecipientPartiallySignature = 21,
		InternalRecipientSignatureRequestRejected = 9,
		InternalWaitingForSenderSignature = 13,
		InternalInvalidSenderSignature = 14,
		InternalInvalidRecipientSignature = 15
	}
}
