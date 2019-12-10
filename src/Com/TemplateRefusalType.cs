using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("67ED41D3-8E2C-4BF5-9FEA-FB0DBF97AB9E")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "TemplateRefusalType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum TemplateRefusalType
	{
		UnknownTemplateRefusalType = 0,
		Refusal = 1,
		Withdrawal = 2
	}
}