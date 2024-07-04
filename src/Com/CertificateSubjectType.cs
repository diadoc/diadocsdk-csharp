using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("9F6D8CC0-826A-42F9-B3FA-32DC273935EA")]
	[XmlType(TypeName = "CertificateSubjectType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum CertificateSubjectType
	{
		UnknownCertificateSubjectType = Proto.Certificates.CertificateSubjectType.UnknownCertificateSubjectType,
		LegalEntity = Proto.Certificates.CertificateSubjectType.LegalEntity,
		IndividualEntity = Proto.Certificates.CertificateSubjectType.IndividualEntity,
		PhysicalPerson = Proto.Certificates.CertificateSubjectType.PhysicalPerson
	}
}
