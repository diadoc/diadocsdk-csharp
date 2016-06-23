﻿using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("4966C818-3101-42F4-8B40-8C8FAB546548")]
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
		UniversalTransferDocumentSellerTitle = 41,
		UniversalTransferDocumentBuyerTitle = 42,
	}
}