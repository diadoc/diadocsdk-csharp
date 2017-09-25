using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("5A9942A2-25B1-49E1-BD5B-F4C9D34CEDA7")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "DocumentMetadataSource", Namespace = "https://diadoc-api.kontur.ru")]
	public enum DocumentMetadataSource
	{
		Xml = 0,
		User = 1
	}
}