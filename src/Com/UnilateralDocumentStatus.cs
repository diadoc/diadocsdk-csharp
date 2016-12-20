using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("B2611D21-88BE-49C9-8DFE-CA4151CBE347")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "UnilateralDocumentStatus", Namespace = "https://diadoc-api.kontur.ru")]
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