using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("6BC9D5FE-B0E5-4595-A420-23ECC87082DC")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "EntityType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum EntityType
	{
		UnknownEntityType = 0,
		Attachment = 1,
		Signature = 2
	}
}
