using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;
using Diadoc.Api.Proto.Invoicing.Signers;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<GeneratedFile> GenerateInvoiceDocumentReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer)
		{
			var queryString = $"/GenerateInvoiceDocumentReceiptXml?boxId={boxId}&messageId={messageId}&attachmentId={attachmentId}";
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, signer);
		}

		public Task<GeneratedFile> GenerateInvoiceCorrectionRequestXmlAsync(string authToken, string boxId, string messageId, string attachmentId, InvoiceCorrectionRequestInfo correctionInfo)
		{
			var queryString = $"/GenerateInvoiceCorrectionRequestXml?boxId={boxId}&messageId={messageId}&attachmentId={attachmentId}";
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, correctionInfo);
		}

		public Task<GeneratedFile> GenerateRevocationRequestXmlAsync(string authToken, string boxId, string messageId, string attachmentId, RevocationRequestInfo revocationRequestInfo)
		{
			var queryString = $"/GenerateRevocationRequestXml?boxId={boxId}&messageId={messageId}&attachmentId={attachmentId}";
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, revocationRequestInfo);
		}

		public Task<GeneratedFile> GenerateSignatureRejectionXmlAsync(string authToken, string boxId, string messageId, string attachmentId, SignatureRejectionInfo signatureRejectionInfo)
		{
			var queryString = $"/GenerateSignatureRejectionXml?boxId={boxId}&messageId={messageId}&attachmentId={attachmentId}";
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, signatureRejectionInfo);
		}

		public Task<InvoiceCorrectionRequestInfo> GetInvoiceCorrectionRequestInfoAsync(string authToken, string boxId, string messageId, string entityId)
		{
			var queryString = $"/GetInvoiceCorrectionRequestInfo?boxId={boxId}&messageId={messageId}&entityId={entityId}";
			return PerformHttpRequestAsync<InvoiceCorrectionRequestInfo>(authToken, "GET", queryString);
		}

		public Task<GeneratedFile> GenerateInvoiceXmlAsync(string authToken, InvoiceInfo invoiceInfo, bool disableValidation = false)
		{
			return GenerateInvoiceXmlAsync(authToken, invoiceInfo, "Invoice", disableValidation);
		}

		public Task<GeneratedFile> GenerateInvoiceRevisionXmlAsync(string authToken, InvoiceInfo invoiceRevisionInfo, bool disableValidation = false)
		{
			return GenerateInvoiceXmlAsync(authToken, invoiceRevisionInfo, "InvoiceRevision", disableValidation);
		}

		public Task<GeneratedFile> GenerateInvoiceCorrectionXmlAsync(string authToken, InvoiceCorrectionInfo invoiceCorrectionInfo, bool disableValidation = false)
		{
			return GenerateInvoiceXmlAsync(authToken, invoiceCorrectionInfo, "InvoiceCorrection", disableValidation);
		}

		public Task<GeneratedFile> GenerateInvoiceCorrectionRevisionXmlAsync(string authToken, InvoiceCorrectionInfo invoiceCorrectionRevision, bool disableValidation = false)
		{
			return GenerateInvoiceXmlAsync(authToken, invoiceCorrectionRevision, "InvoiceCorrectionRevision", disableValidation);
		}

		private Task<GeneratedFile> GenerateInvoiceXmlAsync<T>(string authToken, T protoInvoice, string invoiceType, bool disableValidation = false) where T : class
		{
			var qsb = new PathAndQueryBuilder("/GenerateInvoiceXml").With("invoiceType", invoiceType);
			if (disableValidation)
				qsb.AddParameter("disableValidation");
			return PerformGenerateXmlHttpRequestAsync(authToken, qsb.BuildPathAndQuery(), protoInvoice);
		}

		public Task<GeneratedFile> GenerateTorg12XmlForSellerAsync(string authToken, Torg12SellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			var queryString = string.Format("/GenerateTorg12XmlForSeller{0}", disableValidation ? "?disableValidation" : "");
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, sellerInfo);
		}

		public Task<GeneratedFile> GenerateTorg12XmlForBuyerAsync(string authToken, Torg12BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			var queryString = string.Format("/GenerateTorg12XmlForBuyer?boxId={0}&sellerTitleMessageId={1}&sellerTitleAttachmentId={2}", boxId, sellerTitleMessageId, sellerTitleAttachmentId);
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, buyerInfo);
		}

		public Task<GeneratedFile> GenerateAcceptanceCertificateXmlForSellerAsync(string authToken, AcceptanceCertificateSellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			var queryString = string.Format("/GenerateAcceptanceCertificateXmlForSeller{0}", disableValidation ? "?disableValidation" : "");
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, sellerInfo);
		}

		public Task<GeneratedFile> GenerateAcceptanceCertificateXmlForBuyerAsync(string authToken, AcceptanceCertificateBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			var queryString = string.Format("/GenerateAcceptanceCertificateXmlForBuyer?boxId={0}&sellerTitleMessageId={1}&sellerTitleAttachmentId={2}", boxId, sellerTitleMessageId, sellerTitleAttachmentId);
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, buyerInfo);
		}

		public async Task<bool> CanSendInvoiceAsync(string authToken, string boxId, byte[] certificateBytes)
		{
			var queryString = string.Format("/CanSendInvoice?boxId={0}", boxId);
			var request = BuildHttpRequest(authToken, "POST", queryString, certificateBytes);
			var response = await HttpClient.PerformHttpRequestAsync(request, HttpStatusCode.Forbidden).ConfigureAwait(false);
			return response.StatusCode == HttpStatusCode.OK;
		}

		public Task SendFnsRegistrationMessageAsync(string authToken, string boxId, FnsRegistrationMessageInfo fnsRegistrationMessageInfo)
		{
			var queryString = string.Format("/SendFnsRegistrationMessage?boxId={0}", boxId);
			var request = BuildHttpRequest(authToken, "POST", queryString, Serialize(fnsRegistrationMessageInfo));
			return HttpClient.PerformHttpRequestAsync(request);
		}

		public Task<InvoiceInfo> ParseInvoiceXmlAsync(byte[] invoiceXmlContent)
		{
			return PerformHttpRequestAsync<InvoiceInfo>(null, "POST", "/ParseInvoiceXml", invoiceXmlContent);
		}

		public Task<Torg12SellerTitleInfo> ParseTorg12SellerTitleXmlAsync(byte[] xmlContent)
		{
			return PerformHttpRequestAsync<Torg12SellerTitleInfo>(null, "POST", "/ParseTorg12SellerTitleXml", xmlContent);
		}

		public Task<AcceptanceCertificateSellerTitleInfo> ParseAcceptanceCertificateSellerTitleXmlAsync(byte[] xmlContent)
		{
			return PerformHttpRequestAsync<AcceptanceCertificateSellerTitleInfo>(null, "POST", "/ParseAcceptanceCertificateSellerTitleXml", xmlContent);
		}

		public Task<RevocationRequestInfo> ParseRevocationRequestXmlAsync(byte[] xmlContent)
		{
			return PerformHttpRequestAsync<RevocationRequestInfo>(null, "POST", "/ParseRevocationRequestXml", xmlContent);
		}

		public Task<SignatureRejectionInfo> ParseSignatureRejectionXmlAsync(byte[] xmlContent)
		{
			return PerformHttpRequestAsync<SignatureRejectionInfo>(null, "POST", "/ParseSignatureRejectionXml", xmlContent);
		}

		public Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection)
		{
			var queryBuilder = new PathAndQueryBuilder("/ExtendedSignerDetails");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("thumbprint", thumbprint);
			if (forBuyer)
				queryBuilder.AddParameter("buyer");
			if (forCorrection)
				queryBuilder.AddParameter("correction");
			return PerformHttpRequestAsync<ExtendedSignerDetails>(token, "GET", queryBuilder.ToString());
		}

		public Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection)
		{
			var certificate = new X509Certificate2(certificateBytes);
			return GetExtendedSignerDetailsAsync(token, boxId, certificate.Thumbprint, forBuyer, forCorrection);
		}

		public Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails)
		{
			var queryBuilder = new PathAndQueryBuilder("/ExtendedSignerDetails");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("thumbprint", thumbprint);
			if (forBuyer)
				queryBuilder.AddParameter("buyer");
			if (forCorrection)
				queryBuilder.AddParameter("correction");
			return PerformHttpRequestAsync<ExtendedSignerDetails>(token, "POST", queryBuilder.ToString(), Serialize(signerDetails));
		}

		public Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails)
		{
			var certificate = new X509Certificate2(certificateBytes);
			return PostExtendedSignerDetailsAsync(token, boxId, certificate.Thumbprint, forBuyer, forCorrection, signerDetails);
		}
	}
}
