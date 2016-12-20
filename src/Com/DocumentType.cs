using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("4966C818-3101-42F4-8B40-8C8FAB546548")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "DocumentType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum DocumentType
	{
		UnknownDocumentType = -1,
		Nonformalized = 0,
		Invoice = 1,
		TrustConnectionRequest = 11,
		Torg12 = 12,
		InvoiceRevision = 13,
		InvoiceCorrection = 14,
		InvoiceCorrectionRevision = 15,
		AcceptanceCertificate = 16,
		ProformaInvoice = 18,
		XmlTorg12 = 19,
		XmlAcceptanceCertificate = 20,
		PriceList = 26,
		PriceListAgreement = 30,
		CertificateRegistry = 34,
		ReconciliationAct = 35,
		Contract = 36,
		Torg13 = 37,
		ServiceDetails = 38,
		SupplementaryAgreement = 40,
		UniversalTransferDocument = 41,
		UniversalTransferDocumentRevision = 45,
		UniversalCorrectionDocument = 49,
		UniversalCorrectionDocumentRevision = 50,
	}
}