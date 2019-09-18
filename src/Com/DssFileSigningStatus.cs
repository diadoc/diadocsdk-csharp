using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("10384AB5-1EA7-4D61-9308-8AAC6059597E")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "DssFileSigningStatus", Namespace = "https://diadoc-api.kontur.ru")]
	public enum DssFileSigningStatus
	{
		UnknownSigningStatus = 0,
		SigningCompleted = 1,
		SigningError = 2
	}
}