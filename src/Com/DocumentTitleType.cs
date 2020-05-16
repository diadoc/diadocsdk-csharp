using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("D096C16C-8C6F-4177-8362-54A5F09503B1")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "SignerType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum DocumentTitleType
	{
		Absent = -1,
		UtdSeller = 0,
		UtdBuyer = 1,
		UcdSeller = 2,
		UcdBuyer = 3,
		TovTorg551Seller = 4,
		TovTorg551Buyer = 5,
		AccCert552Seller = 6,
		AccCert552Buyer = 7,
		Utd820Buyer = 8,
		PriRasxBuyer = 9,
		PriRasxAddInformation = 10
	}
}