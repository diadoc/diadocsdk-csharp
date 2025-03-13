using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("AEE03F93-D5D2-40EF-9502-78D27BD0755F")]
	[XmlType(TypeName = "DssSignDataSource", Namespace = "https://diadoc-api.kontur.ru")]
	public enum DssSignDataSource
	{
		CertificateSign = 0,
		GosKeySign = 1
	}
}
