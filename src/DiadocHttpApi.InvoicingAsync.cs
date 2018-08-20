using System;
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
			var queryBuilder = new PathAndQueryBuilder("/GenerateTorg12XmlForSeller");
			if (disableValidation) queryBuilder.AddParameter("disableValidation");
			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), sellerInfo);
		}

		public Task<GeneratedFile> GenerateTovTorg551XmlForSellerAsync(string authToken, TovTorgSellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateTorg12XmlForSeller");
			if (disableValidation) queryBuilder.AddParameter("disableValidation");
			queryBuilder.AddParameter("documentVersion", "tovtorg_05_01_03");
			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), sellerInfo);
		}

		public Task<GeneratedFile> GenerateTorg12XmlForBuyerAsync(string authToken, Torg12BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateTorg12XmlForBuyer");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("sellerTitleMessageId", sellerTitleMessageId);
			queryBuilder.AddParameter("sellerTitleAttachmentId", sellerTitleAttachmentId);
			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), buyerInfo);
		}

		public Task<GeneratedFile> GenerateTovTorg551XmlForBuyerAsync(string authToken, TovTorgBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, string documentVersion = null)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateTorg12XmlForBuyer");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("sellerTitleMessageId", sellerTitleMessageId);
			queryBuilder.AddParameter("sellerTitleAttachmentId", sellerTitleAttachmentId);
			queryBuilder.AddParameter("documentVersion", documentVersion ?? "tovtorg_05_01_02");
			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), buyerInfo);
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

		public Task<GeneratedFile> GenerateAcceptanceCertificate552XmlForSellerAsync(string authToken, AcceptanceCertificate552SellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateAcceptanceCertificateXmlForSeller");
			if (disableValidation) queryBuilder.AddParameter("disableValidation");
			queryBuilder.AddParameter("documentVersion", "rezru_05_01_01");
			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), sellerInfo);
		}

		public Task<GeneratedFile> GenerateAcceptanceCertificate552XmlForBuyerAsync(string authToken, AcceptanceCertificate552BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateAcceptanceCertificateXmlForBuyer");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("sellerTitleMessageId", sellerTitleMessageId);
			queryBuilder.AddParameter("sellerTitleAttachmentId", sellerTitleAttachmentId);
			queryBuilder.AddParameter("documentVersion", "rezru_05_01_01");
			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), buyerInfo);
		}

		public async Task<GeneratedFile> GenerateSenderTitleXmlAsync(string authToken, string boxId, string documentTypeNamedId, string documentFunction, string documentVersion, byte[] userContractData, bool disableValidation = false, string editingSettingId = null)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateSenderTitleXml");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("documentTypeNamedId", documentTypeNamedId);
			queryBuilder.AddParameter("documentFunction", documentFunction);
			queryBuilder.AddParameter("documentVersion", documentVersion);
			queryBuilder.AddParameter("editingSettingId", editingSettingId);
			if (disableValidation) queryBuilder.AddParameter("disableValidation");

			var request = BuildHttpRequest(authToken, "POST", queryBuilder.BuildPathAndQuery(), userContractData);
			var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
			return new GeneratedFile(response.ContentDispositionFileName, response.Content);
		}

		public async Task<GeneratedFile> GenerateRecipientTitleXmlAsync(string authToken, string boxId, string senderTitleMessageId, string senderTitleAttachmentId, byte[] userContractData, string documentVersion = null)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateRecipientTitleXml");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("senderTitleMessageId", senderTitleMessageId);
			queryBuilder.AddParameter("senderTitleAttachmentId", senderTitleAttachmentId);
			queryBuilder.AddParameter("documentVersion", documentVersion);

			var request = BuildHttpRequest(authToken, "POST", queryBuilder.BuildPathAndQuery(), userContractData);
			var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
			return new GeneratedFile(response.ContentDispositionFileName, response.Content);
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

		public Task<Torg12BuyerTitleInfo> ParseTorg12BuyerTitleXmlAsync(byte[] xmlContent)
		{
			return PerformHttpRequestAsync<Torg12BuyerTitleInfo>(null, "POST", "/ParseTorg12BuyerTitleXml", xmlContent);
		}

		public Task<TovTorgSellerTitleInfo> ParseTovTorg551SellerTitleXmlAsync(byte[] xmlContent)
		{
			return PerformHttpRequestAsync<TovTorgSellerTitleInfo>(null, "POST", "/ParseTorg12SellerTitleXml?documentVersion=tovtorg_05_01_03", xmlContent);
		}

		public Task<TovTorgBuyerTitleInfo> ParseTovTorg551BuyerTitleXmlAsync(byte[] xmlContent)
		{
			return PerformHttpRequestAsync<TovTorgBuyerTitleInfo>(null, "POST", "/ParseTorg12BuyerTitleXml?documentVersion=tovtorg_05_01_02", xmlContent);
		}

		public Task<AcceptanceCertificateSellerTitleInfo> ParseAcceptanceCertificateSellerTitleXmlAsync(byte[] xmlContent)
		{
			return PerformHttpRequestAsync<AcceptanceCertificateSellerTitleInfo>(null, "POST", "/ParseAcceptanceCertificateSellerTitleXml", xmlContent);
		}

		public Task<AcceptanceCertificateBuyerTitleInfo> ParseAcceptanceCertificateBuyerTitleXmlAsync(byte[] xmlContent)
		{
			return PerformHttpRequestAsync<AcceptanceCertificateBuyerTitleInfo>(null, "POST", "/ParseAcceptanceCertificateBuyerTitleXml", xmlContent);
		}

		public Task<AcceptanceCertificate552SellerTitleInfo> ParseAcceptanceCertificate552SellerTitleXmlAsync(byte[] xmlContent)
		{
			return PerformHttpRequestAsync<AcceptanceCertificate552SellerTitleInfo>(null, "POST", "/ParseAcceptanceCertificateSellerTitleXml?documentVersion=rezru_05_01_01", xmlContent);
		}

		public Task<AcceptanceCertificate552BuyerTitleInfo> ParseAcceptanceCertificate552BuyerTitleXmlAsync(byte[] xmlContent)
		{
			return PerformHttpRequestAsync<AcceptanceCertificate552BuyerTitleInfo>(null, "POST", "/ParseAcceptanceCertificateBuyerTitleXml?documentVersion=rezru_05_01_01", xmlContent);
		}

		public Task<RevocationRequestInfo> ParseRevocationRequestXmlAsync(byte[] xmlContent)
		{
			return PerformHttpRequestAsync<RevocationRequestInfo>(null, "POST", "/ParseRevocationRequestXml", xmlContent);
		}

		public Task<SignatureRejectionInfo> ParseSignatureRejectionXmlAsync(byte[] xmlContent)
		{
			return PerformHttpRequestAsync<SignatureRejectionInfo>(null, "POST", "/ParseSignatureRejectionXml", xmlContent);
		}

		[Obsolete("Use overload with DocumentTitleType parameter")]
		public Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection)
		{
			var documentTitleType = CreateUtdDocumentTitleType(forBuyer, forCorrection);
			return GetExtendedSignerDetailsAsync(token, boxId, thumbprint, documentTitleType);
		}

		[Obsolete("Use overload with DocumentTitleType parameter")]
		public Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection)
		{
			var certificate = new X509Certificate2(certificateBytes);
			return GetExtendedSignerDetailsAsync(token, boxId, certificate.Thumbprint, forBuyer, forCorrection);
		}

		public Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, DocumentTitleType documentTitleType)
		{
			var queryBuilder = new PathAndQueryBuilder("/V2/ExtendedSignerDetails");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("thumbprint", thumbprint);
			queryBuilder.AddParameter("documentTitleType", ((int) documentTitleType).ToString());
			return PerformHttpRequestAsync<ExtendedSignerDetails>(token, "GET", queryBuilder.ToString());
		}

		public Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, DocumentTitleType documentTitleType)
		{
			var certificate = new X509Certificate2(certificateBytes);
			return GetExtendedSignerDetailsAsync(token, boxId, certificate.Thumbprint, documentTitleType);
		}

		[Obsolete("Use overload with DocumentTitleType parameter")]
		public Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails)
		{
			var documentTitleType = CreateUtdDocumentTitleType(forBuyer, forCorrection);
			return PostExtendedSignerDetailsAsync(token, boxId, thumbprint, documentTitleType, signerDetails);
		}

		[Obsolete("Use overload with DocumentTitleType parameter")]
		public Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails)
		{
			var certificate = new X509Certificate2(certificateBytes);
			return PostExtendedSignerDetailsAsync(token, boxId, certificate.Thumbprint, forBuyer, forCorrection, signerDetails);
		}

		public Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, DocumentTitleType documentTitleType, ExtendedSignerDetailsToPost signerDetails)
		{
			var queryBuilder = new PathAndQueryBuilder("/V2/ExtendedSignerDetails");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("thumbprint", thumbprint);
			queryBuilder.AddParameter("documentTitleType", ((int) documentTitleType).ToString());
			return PerformHttpRequestAsync<ExtendedSignerDetails>(token, "POST", queryBuilder.ToString(), Serialize(signerDetails));
		}

		public Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, DocumentTitleType documentTitleType, ExtendedSignerDetailsToPost signerDetails)
		{
			var certificate = new X509Certificate2(certificateBytes);
			return PostExtendedSignerDetailsAsync(token, boxId, certificate.Thumbprint, documentTitleType, signerDetails);
		}
	}
}