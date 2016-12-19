using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("8B38C941-0227-4A92-8469-6845B81ACF11")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "OrgType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum OrgType
	{
		LegalEntity = 1,
		IndividualEntity = 2,
		ForeignEntity = 3
	}
}