using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("42D1D718-6C89-4DF4-A9A1-715013162B71")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "TotalCountType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum TotalCountType
	{
		UnknownCountType = Proto.TotalCountType.UnknownCountType,
		Equal = Proto.TotalCountType.Equal,
		GreaterThanOrEqual = Proto.TotalCountType.GreaterThanOrEqual
	}
}