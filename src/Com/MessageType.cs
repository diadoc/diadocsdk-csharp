using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("C8D68A5A-D137-4ACC-A5C4-E6AC86979862")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "MessageType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum MessageType
	{
		Unknown = Diadoc.Api.Proto.Documents.MessageType.Unknown,
		Message = Diadoc.Api.Proto.Documents.MessageType.Message,
		Draft = Diadoc.Api.Proto.Documents.MessageType.Draft,
		Template = Diadoc.Api.Proto.Documents.MessageType.Template,
	}
}
