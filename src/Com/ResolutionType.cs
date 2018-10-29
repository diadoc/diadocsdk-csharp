using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("FE21CF3E-D903-45A8-B7EF-3F646B9E0336")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "ResolutionType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum ResolutionType
	{
		UndefinedResolutionType = Proto.ResolutionType.UndefinedResolutionType,
		Approve = Proto.ResolutionType.Approve,
		Disapprove = Proto.ResolutionType.Disapprove,
		UnknownResolutionType = Proto.ResolutionType.UnknownResolutionType
	}
}