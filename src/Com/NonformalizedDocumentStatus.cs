using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("29B3F7EE-0B97-42B5-8723-7D5DB4097152")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "NonformalizedDocumentStatus", Namespace = "https://diadoc-api.kontur.ru")]
	public enum NonformalizedDocumentStatus
	{
		UnknownNonformalizedDocumentStatus = 0,
		OutboundNoRecipientSignatureRequest = 1,
		OutboundWaitingForRecipientSignature = 2,
		OutboundWithRecipientSignature = 3,
		OutboundRecipientSignatureRequestRejected = 4,
		OutboundWaitingForSenderSignature = 13,
		OutboundInvalidSenderSignature = 14,
		InboundNoRecipientSignatureRequest = 5,
		InboundWaitingForRecipientSignature = 6,
		InboundWithRecipientSignature = 7,
		InboundRecipientSignatureRequestRejected = 8,
		InboundInvalidRecipientSignature = 15,
		InternalNoRecipientSignatureRequest = 9,
		InternalWaitingForRecipientSignature = 10,
		InternalWithRecipientSignature = 11,
		InternalRecipientSignatureRequestRejected = 12,
		InternalWaitingForSenderSignature = 16,
		InternalInvalidSenderSignature = 17,
		InternalInvalidRecipientSignature = 18
	}
}