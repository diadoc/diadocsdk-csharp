using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("6C7C35F8-58D6-4890-B41F-0BEEDBC056DB")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "SenderSignatureStatus", Namespace = "https://diadoc-api.kontur.ru")]
	public enum SenderSignatureStatus
	{
		UnknownSenderSignatureStatus = Diadoc.Api.Proto.Documents.SenderSignatureStatus.UnknownSenderSignatureStatus,
		WaitingForSenderSignature = Diadoc.Api.Proto.Documents.SenderSignatureStatus.WaitingForSenderSignature,
		SenderSignatureUnchecked = Diadoc.Api.Proto.Documents.SenderSignatureStatus.SenderSignatureUnchecked,
		SenderSignatureCheckedAndValid = Diadoc.Api.Proto.Documents.SenderSignatureStatus.SenderSignatureCheckedAndValid,
		SenderSignatureCheckedAndInvalid = Diadoc.Api.Proto.Documents.SenderSignatureStatus.SenderSignatureCheckedAndInvalid,
	}
}
