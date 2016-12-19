using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("AEF49130-2AC4-4B2F-A8EE-67DF184D5788")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "SignerStatus", Namespace = "https://diadoc-api.kontur.ru")]
	public enum SignerStatus
	{
		SellerEmployee = 1,
		InformationCreatorEmployee = 2,
		OtherOrganizationEmployee = 3,
		AuthorizedPerson = 4
	}
}