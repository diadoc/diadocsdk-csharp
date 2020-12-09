using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("652F9E2C-19C2-4E7C-BBC6-6D19A9EE2419")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "RecipientResponseStatus", Namespace = "https://diadoc-api.kontur.ru")]
	public enum RecipientResponseStatus
	{
		RecipientResponseStatusUnknown = Diadoc.Api.Proto.Documents.RecipientResponseStatus.RecipientResponseStatusUnknown,
		RecipientResponseStatusNotAcceptable = Diadoc.Api.Proto.Documents.RecipientResponseStatus.RecipientResponseStatusNotAcceptable,
		WaitingForRecipientSignature = Diadoc.Api.Proto.Documents.RecipientResponseStatus.WaitingForRecipientSignature,
		WithRecipientSignature = Diadoc.Api.Proto.Documents.RecipientResponseStatus.WithRecipientSignature,
		RecipientSignatureRequestRejected = Diadoc.Api.Proto.Documents.RecipientResponseStatus.RecipientSignatureRequestRejected,
		InvalidRecipientSignature = Diadoc.Api.Proto.Documents.RecipientResponseStatus.InvalidRecipientSignature,
		WithRecipientPartiallySignature = Diadoc.Api.Proto.Documents.RecipientResponseStatus.WithRecipientPartiallySignature
	}
}
