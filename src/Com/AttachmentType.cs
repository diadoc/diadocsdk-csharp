﻿using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("E6445859-B3E2-46FB-853A-AEB0E3F66207")]
	public enum AttachmentType
	{
		UnknownAttachmentType = -1,
		Nonformalized = 0,
		Invoice = 1,
		InvoiceReceipt = 2,
		InvoiceConfirmation = 3,
		InvoiceCorrectionRequest = 4,
		AttachmentComment = 5,
		DeliveryFailureNotification = 6,
		EancomInvoic = 7,
		SignatureRequestRejection = 8,
		EcrCatConformanceCertificateMetadata = 9,
		SignatureVerificationReport = 10,
		TrustConnectionRequest = 11,
		Torg12 = 12,
		InvoiceRevision = 13,
		InvoiceCorrection = 14,
		InvoiceCorrectionRevision = 15,
		AcceptanceCertificate = 16,
		StructuredData = 17,
		ProformaInvoice = 18,
		XmlTorg12 = 19,
		XmlAcceptanceCertificate = 20,
		XmlTorg12BuyerTitle = 21,
		XmlAcceptanceCertificateBuyerTitle = 22,
		Resolution = 23,
		ResolutionRequest = 24,
		ResolutionRequestDenial = 25,
		PriceList = 26,
		Receipt = 27,
		XmlSignatureRejection = 28,
		RevocationRequest = 29,
		PriceListAgreement = 30,
		CertificateRegistry = 34,
		ReconciliationAct = 35,
		Contract = 36,
		Torg13 = 37,
		ServiceDetails = 38,
		RoamingNotification = 39,
		SupplementaryAgreement = 40,
		UniversalTransferDocumentSellerTitle = 41,
		UniversalTransferDocumentBuyerTitle = 42,
		UniversalTransferDocumentReceipt = 43,
		UniversalTransferDocumentConfirmation = 44,
		UniversalTransferDocumentSellerTitleRevision = 45,
		CustomData = 64
	}
}