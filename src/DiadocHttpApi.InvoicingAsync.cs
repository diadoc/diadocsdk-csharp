using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Diadoc.Api.Constants;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;
using Diadoc.Api.Proto.Invoicing.Signers;
using RevocationRequestInfo = Diadoc.Api.Proto.Invoicing.RevocationRequestInfo;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[Obsolete("Use GenerateReceiptXmlV2Async()")]
		public Task<GeneratedFile> GenerateInvoiceDocumentReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer)
		{
			return GenerateReceiptXmlAsync(authToken, boxId, messageId, attachmentId, signer);
		}

		[Obsolete("Use GenerateInvoiceCorrectionRequestXmlV2Async()")]
		public Task<GeneratedFile> GenerateInvoiceCorrectionRequestXmlAsync(
			string authToken,
			string boxId,
			string messageId,
			string attachmentId,
			InvoiceCorrectionRequestInfo correctionInfo)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateInvoiceCorrectionRequestXml");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("messageId", messageId);
			queryBuilder.AddParameter("attachmentId", attachmentId);

			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), correctionInfo);
		}

		public Task<GeneratedFile> GenerateInvoiceCorrectionRequestXmlV2Async(
			string authToken,
			string boxId,
			InvoiceCorrectionRequestGenerationRequestV2 invoiceCorrectionRequestGenerationRequest)
		{
			var queryBuilder = new PathAndQueryBuilder("/V2/GenerateInvoiceCorrectionRequestXml");
			queryBuilder.AddParameter("boxId", boxId);

			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), invoiceCorrectionRequestGenerationRequest);
		}

		public Task<GeneratedFile> GenerateRevocationRequestXmlAsync(string authToken, string boxId, string messageId, string attachmentId, RevocationRequestInfo revocationRequestInfo, string contentTypeId = null)
		{
			var queryBuilder = new PathAndQueryBuilder("/V2/GenerateRevocationRequestXml");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("messageId", messageId);
			queryBuilder.AddParameter("attachmentId", attachmentId);
			if (!string.IsNullOrEmpty(contentTypeId))
				queryBuilder.AddParameter("contentTypeId", contentTypeId);

			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), revocationRequestInfo);
		}

		[Obsolete("Use GenerateSignatureRejectionXmlV2Async")]
		public Task<GeneratedFile> GenerateSignatureRejectionXmlAsync(string authToken, string boxId, string messageId, string attachmentId, SignatureRejectionInfo signatureRejectionInfo)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateSignatureRejectionXml");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("messageId", messageId);
			queryBuilder.AddParameter("attachmentId", attachmentId);

			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), signatureRejectionInfo);
		}

		public Task<GeneratedFile> GenerateSignatureRejectionXmlV2Async(string authToken, string boxId, SignatureRejectionGenerationRequestV2 signatureRejectionGenerationRequest)
		{
			var queryBuilder = new PathAndQueryBuilder("/V2/GenerateSignatureRejectionXml");
			queryBuilder.AddParameter("boxId", boxId);

			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), signatureRejectionGenerationRequest);
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
			queryBuilder.AddParameter("documentVersion", DefaultDocumentVersions.TovTorg551);

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
			queryBuilder.AddParameter("documentVersion", documentVersion ?? DefaultDocumentVersions.TovTorg551);

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
			queryBuilder.AddParameter("documentVersion", DefaultDocumentVersions.AcceptanceCerttificate552);

			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), sellerInfo);
		}

		public Task<GeneratedFile> GenerateAcceptanceCertificate552XmlForBuyerAsync(string authToken, AcceptanceCertificate552BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateAcceptanceCertificateXmlForBuyer");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("sellerTitleMessageId", sellerTitleMessageId);
			queryBuilder.AddParameter("sellerTitleAttachmentId", sellerTitleAttachmentId);
			queryBuilder.AddParameter("documentVersion", DefaultDocumentVersions.AcceptanceCerttificate552);

			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), buyerInfo);
		}

		public async Task<GeneratedFile> GenerateTitleXmlAsync(
			string authToken,
			string boxId,
			string documentTypeNamedId,
			string documentFunction,
			string documentVersion,
			int titleIndex,
			byte[] userContractData,
			bool disableValidation = false,
			string editingSettingId = null,
			string letterId = null,
			string documentId = null)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateTitleXml");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("documentTypeNamedId", documentTypeNamedId);
			queryBuilder.AddParameter("documentFunction", documentFunction);
			queryBuilder.AddParameter("documentVersion", documentVersion);
			queryBuilder.AddParameter("titleIndex", titleIndex.ToString());
			queryBuilder.AddParameter("editingSettingId", editingSettingId);
			if (disableValidation) queryBuilder.AddParameter("disableValidation");
			queryBuilder.AddParameter("letterId", letterId);
			queryBuilder.AddParameter("documentId", documentId);

			var request = BuildHttpRequest(authToken, "POST", queryBuilder.BuildPathAndQuery(), userContractData);
			var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);

			return new GeneratedFile(response.ContentDispositionFileName, response.Content);
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

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<RevocationRequestInfo> ParseRevocationRequestXmlAsync(byte[] xmlContent)
		{
			return ParseRevocationRequestXmlAsync(null, xmlContent);
		}

		public Task<RevocationRequestInfo> ParseRevocationRequestXmlAsync(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequestAsync<RevocationRequestInfo>(authToken, "POST", "/ParseRevocationRequestXml", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<SignatureRejectionInfo> ParseSignatureRejectionXmlAsync(byte[] xmlContent)
		{
			return ParseSignatureRejectionXmlAsync(null, xmlContent);
		}

		public Task<SignatureRejectionInfo> ParseSignatureRejectionXmlAsync(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequestAsync<SignatureRejectionInfo>(authToken, "POST", "/ParseSignatureRejectionXml", xmlContent);
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

		[Obsolete("Use overload with DocumentTitleType parameter. This overload will be removed soon")]
		public Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails)
		{
			var documentTitleType = CreateUtdDocumentTitleType(forBuyer, forCorrection);

			return PostExtendedSignerDetailsAsync(token, boxId, thumbprint, documentTitleType, signerDetails);
		}

		[Obsolete("Use overload with DocumentTitleType parameter. This overload will be removed soon")]
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
