using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("9194048E-F513-4BC1-9DA1-2E17E89D910F")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "DssOperator", Namespace = "https://diadoc-api.kontur.ru")]
	public enum DssOperator
	{
		OperatorUnknown = 0,
		Megafon = 1,
		Kontur = 2
	}
}
