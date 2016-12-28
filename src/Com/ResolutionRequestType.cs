using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("8B280FD8-0E8E-4FCC-8586-288DD94A7437")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "ResolutionRequestType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum ResolutionRequestType
	{
		UnknownResolutionRequestType = Proto.Events.ResolutionRequestType.UnknownResolutionRequestType,
		ApprovementRequest = Proto.Events.ResolutionRequestType.ApprovementRequest,
		SignatureRequest = Proto.Events.ResolutionRequestType.SignatureRequest,
		ApprovementSignatureRequest = Proto.Events.ResolutionRequestType.ApprovementSignatureRequest
	}
}