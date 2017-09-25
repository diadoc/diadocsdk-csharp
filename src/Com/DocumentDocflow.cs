using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("B3F89B33-1CF8-467D-A0C3-95263CE77008")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "DocumentDocflow", Namespace = "https://diadoc-api.kontur.ru")]
	public enum DocumentDocflow
	{
		External = 0,
		Internal = 1
	}
}