using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("C83CC405-5F80-439C-8CD5-695D646D218E")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "SignerType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum SignerType
	{
		SignerTypeUnspecified = -1,
		LegalEntity = 1,
		IndividualEntity = 2,
		PhysicalPerson = 3
	}
}
