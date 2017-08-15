using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;
using Diadoc.Api.Proto.Invoicing.Signers;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public GeneratedFile GenerateInvoiceDocumentReceiptXml(string authToken, string boxId, string messageId, string attachmentId, Signer signer)
		{
			var queryString = string.Format("/GenerateInvoiceDocumentReceiptXml?boxId={0}&messageId={1}&attachmentId={2}", boxId, messageId, attachmentId);
			return PerformGenerateXmlHttpRequest(authToken, queryString, signer);
		}

		public GeneratedFile GenerateInvoiceCorrectionRequestXml(string authToken, string boxId, string messageId, string attachmentId, InvoiceCorrectionRequestInfo correctionInfo)
		{
			var queryString = string.Format("/GenerateInvoiceCorrectionRequestXml?boxId={0}&messageId={1}&attachmentId={2}", boxId, messageId, attachmentId);
			return PerformGenerateXmlHttpRequest(authToken, queryString, correctionInfo);
		}

		public GeneratedFile GenerateRevocationRequestXml(string authToken, string boxId, string messageId, string attachmentId, RevocationRequestInfo revocationRequestInfo)
		{
			var queryString = string.Format("/GenerateRevocationRequestXml?boxId={0}&messageId={1}&attachmentId={2}", boxId, messageId, attachmentId);
			return PerformGenerateXmlHttpRequest(authToken, queryString, revocationRequestInfo);
		}

		public GeneratedFile GenerateSignatureRejectionXml(string authToken, string boxId, string messageId, string attachmentId, SignatureRejectionInfo signatureRejectionInfo)
		{
			var queryString = string.Format("/GenerateSignatureRejectionXml?boxId={0}&messageId={1}&attachmentId={2}", boxId, messageId, attachmentId);
			return PerformGenerateXmlHttpRequest(authToken, queryString, signatureRejectionInfo);
		}

		public InvoiceCorrectionRequestInfo GetInvoiceCorrectionRequestInfo(string authToken, string boxId, string messageId, string entityId)
		{
			var queryString = string.Format("/GetInvoiceCorrectionRequestInfo?boxId={0}&messageId={1}&entityId={2}", boxId, messageId, entityId);
			return PerformHttpRequest<InvoiceCorrectionRequestInfo>(authToken, "GET", queryString);
		}

		public GeneratedFile GenerateInvoiceXml(string authToken, InvoiceInfo invoiceInfo, bool disableValidation = false)
		{
			return GenerateInvoiceXml(authToken, invoiceInfo, "Invoice", disableValidation);
		}

		public GeneratedFile GenerateInvoiceRevisionXml(string authToken, InvoiceInfo invoiceRevisionInfo, bool disableValidation = false)
		{
			return GenerateInvoiceXml(authToken, invoiceRevisionInfo, "InvoiceRevision", disableValidation);
		}

		public GeneratedFile GenerateInvoiceCorrectionXml(string authToken, InvoiceCorrectionInfo invoiceCorrectionInfo, bool disableValidation = false)
		{
			return GenerateInvoiceXml(authToken, invoiceCorrectionInfo, "InvoiceCorrection", disableValidation);
		}

		public GeneratedFile GenerateInvoiceCorrectionRevisionXml(string authToken, InvoiceCorrectionInfo invoiceCorrectionRevision, bool disableValidation = false)
		{
			return GenerateInvoiceXml(authToken, invoiceCorrectionRevision, "InvoiceCorrectionRevision", disableValidation);
		}

		private GeneratedFile GenerateInvoiceXml<T>(string authToken, T protoInvoice, string invoiceType, bool disableValidation = false) where T : class
		{
			var queryString = string.Format("/GenerateInvoiceXml?invoiceType={0}{1}", invoiceType, disableValidation ? "&disableValidation" : "");
			return PerformGenerateXmlHttpRequest(authToken, queryString, protoInvoice);
		}

		public GeneratedFile GenerateTorg12XmlForSeller(string authToken, Torg12SellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			var queryString = string.Format("/GenerateTorg12XmlForSeller{0}", disableValidation ? "?disableValidation" : "");
			return PerformGenerateXmlHttpRequest(authToken, queryString, sellerInfo);
		}

		public GeneratedFile GenerateTorg12XmlForBuyer(string authToken, Torg12BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			var queryString = string.Format("/GenerateTorg12XmlForBuyer?boxId={0}&sellerTitleMessageId={1}&sellerTitleAttachmentId={2}", boxId, sellerTitleMessageId, sellerTitleAttachmentId);
			return PerformGenerateXmlHttpRequest(authToken, queryString, buyerInfo);
		}

		public GeneratedFile GenerateAcceptanceCertificateXmlForSeller(string authToken, AcceptanceCertificateSellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			var queryString = string.Format("/GenerateAcceptanceCertificateXmlForSeller{0}", disableValidation ? "?disableValidation" : "");
			return PerformGenerateXmlHttpRequest(authToken, queryString, sellerInfo);
		}

		public GeneratedFile GenerateAcceptanceCertificateXmlForBuyer(string authToken, AcceptanceCertificateBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			var queryString = string.Format("/GenerateAcceptanceCertificateXmlForBuyer?boxId={0}&sellerTitleMessageId={1}&sellerTitleAttachmentId={2}", boxId, sellerTitleMessageId, sellerTitleAttachmentId);
			return PerformGenerateXmlHttpRequest(authToken, queryString, buyerInfo);
		}

		public bool CanSendInvoice(string authToken, string boxId, byte[] certificateBytes)
		{
			var queryString = string.Format("/CanSendInvoice?boxId={0}", boxId);
			var request = BuildHttpRequest(authToken, "POST", queryString, certificateBytes);
			var response = HttpClient.PerformHttpRequest(request, HttpStatusCode.Forbidden);
			return response.StatusCode == HttpStatusCode.OK;
		}

		public void SendFnsRegistrationMessage(string authToken, string boxId, FnsRegistrationMessageInfo fnsRegistrationMessageInfo)
		{
			var queryString = string.Format("/SendFnsRegistrationMessage?boxId={0}", boxId);
			var request = BuildHttpRequest(authToken, "POST", queryString, Serialize(fnsRegistrationMessageInfo));
			HttpClient.PerformHttpRequest(request);
		}

		public InvoiceInfo ParseInvoiceXml(byte[] invoiceXmlContent)
		{
			return PerformHttpRequest<InvoiceInfo>(null, "POST", "/ParseInvoiceXml", invoiceXmlContent);
		}

		public Torg12SellerTitleInfo ParseTorg12SellerTitleXml(byte[] xmlContent)
		{
			return PerformHttpRequest<Torg12SellerTitleInfo>(null, "POST", "/ParseTorg12SellerTitleXml", xmlContent);
		}

		public AcceptanceCertificateSellerTitleInfo ParseAcceptanceCertificateSellerTitleXml(byte[] xmlContent)
		{
			return PerformHttpRequest<AcceptanceCertificateSellerTitleInfo>(null, "POST", "/ParseAcceptanceCertificateSellerTitleXml", xmlContent);
		}

		public RevocationRequestInfo ParseRevocationRequestXml(byte[] xmlContent)
		{
			return PerformHttpRequest<RevocationRequestInfo>(null, "POST", "/ParseRevocationRequestXml", xmlContent);
		}

		public SignatureRejectionInfo ParseSignatureRejectionXml(byte[] xmlContent)
		{
			return PerformHttpRequest<SignatureRejectionInfo>(null, "POST", "/ParseSignatureRejectionXml", xmlContent);
		}

		[Obsolete("Use overload with DocumentTitleType parameter")]
		public ExtendedSignerDetails GetExtendedSignerDetails(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection)
		{
			var documentTitleType = CreateDocumentTitleType(forBuyer, forCorrection);
			return GetExtendedSignerDetails(token, boxId, thumbprint, documentTitleType);
		}

		[Obsolete("Use overload with DocumentTitleType parameter")]
		public ExtendedSignerDetails GetExtendedSignerDetails(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection)
		{
			var certificate = new X509Certificate2(certificateBytes);
			return GetExtendedSignerDetails(token, boxId, certificate.Thumbprint, forBuyer, forCorrection);
		}

		public ExtendedSignerDetails GetExtendedSignerDetails(string token, string boxId, string thumbprint, DocumentTitleType documentTitleType)
		{
			var queryBuilder = new PathAndQueryBuilder("/V2/ExtendedSignerDetails");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("thumbprint", thumbprint);
			queryBuilder.AddParameter("documentTitleType", ((int) documentTitleType).ToString());
			return PerformHttpRequest<ExtendedSignerDetails>(token, "GET", queryBuilder.ToString());
		}

		public ExtendedSignerDetails GetExtendedSignerDetails(string token, string boxId, byte[] certificateBytes, DocumentTitleType documentTitleType)
		{
			var certificate = new X509Certificate2(certificateBytes);
			return GetExtendedSignerDetails(token, boxId, certificate.Thumbprint, documentTitleType);
		}

		[Obsolete("Use overload with DocumentTitleType parameter")]
		public ExtendedSignerDetails PostExtendedSignerDetails(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails)
		{
			var documentTitleType = CreateDocumentTitleType(forBuyer, forCorrection);
			return PostExtendedSignerDetails(token, boxId, thumbprint, documentTitleType, signerDetails);
		}

		[Obsolete("Use overload with DocumentTitleType parameter")]
		public ExtendedSignerDetails PostExtendedSignerDetails(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails)
		{
			var certificate = new X509Certificate2(certificateBytes);
			return PostExtendedSignerDetails(token, boxId, certificate.Thumbprint, forBuyer, forCorrection, signerDetails);
		}

		public ExtendedSignerDetails PostExtendedSignerDetails(string token, string boxId, string thumbprint, DocumentTitleType documentTitleType, ExtendedSignerDetailsToPost signerDetails)
		{
			var queryBuilder = new PathAndQueryBuilder("/V2/ExtendedSignerDetails");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("thumbprint", thumbprint);
			queryBuilder.AddParameter("documentTitleType", ((int) documentTitleType).ToString());
			return PerformHttpRequest<ExtendedSignerDetails>(token, "POST", queryBuilder.ToString(), Serialize(signerDetails));
		}

		public ExtendedSignerDetails PostExtendedSignerDetails(string token, string boxId, byte[] certificateBytes, DocumentTitleType documentTitleType, ExtendedSignerDetailsToPost signerDetails)
		{
			var certificate = new X509Certificate2(certificateBytes);
			return PostExtendedSignerDetails(token, boxId, certificate.Thumbprint, documentTitleType, signerDetails);
		}

		private static DocumentTitleType CreateDocumentTitleType(bool forBuyer, bool forCorrection)
		{
			return forBuyer
				? (forCorrection ? DocumentTitleType.UcdBuyer : DocumentTitleType.UtdBuyer)
				: (forCorrection ? DocumentTitleType.UcdSeller : DocumentTitleType.UtdSeller);
		}
	}
}
