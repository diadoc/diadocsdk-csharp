using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("ECB3ABEE-4383-4306-A598-A51AD6753356")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "FunctionType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum FunctionType
	{
		Invoice = 0,
		Basic = 1,
		InvoiceAndBasic = 2
	}
}