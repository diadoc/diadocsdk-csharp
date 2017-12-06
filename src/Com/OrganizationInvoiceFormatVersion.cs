using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("91D18358-2D6E-4DCB-A4FC-543C67E4A1F6")]
	[XmlType(TypeName = "OrganizationInvoiceFormatVersion", Namespace = "https://diadoc-api.kontur.ru")]
	public enum OrganizationInvoiceFormatVersion
	{
		v5_01 = Proto.OrganizationInvoiceFormatVersion.v5_01, 
		v5_02 = Proto.OrganizationInvoiceFormatVersion.v5_02
	}
}