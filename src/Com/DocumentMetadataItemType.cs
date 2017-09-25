using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("48122657-DA57-4740-BF39-FF2075DFB0CD")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "DocumentMetadataItemType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum DocumentMetadataItemType
	{
		String = 0,
		Integer = 1,
		Decimal = 2,
		Date = 3,
		Time = 4
	}
}