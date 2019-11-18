using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("7E3BF9AD-5C01-437C-904B-DFC180AEF5A8")]
	[XmlType(TypeName = "CertificateType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum CertificateType
	{
		Unknown = Proto.CertificateType.Unknown,
		Token = Proto.CertificateType.Token,
		Dss = Proto.CertificateType.Dss,
		KonturCertificate = Proto.CertificateType.KonturCertificate
	}
}