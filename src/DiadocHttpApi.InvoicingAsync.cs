using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;
using Diadoc.Api.Proto.Invoicing.Signers;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[Obsolete("Use GenerateReceiptXmlAsync()")]
		public Task<GeneratedFile> GenerateInvoiceDocumentReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer, CancellationToken ct = default)
		{
			var queryString = $"/GenerateInvoiceDocumentReceiptXml?boxId={boxId}&messageId={messageId}&attachmentId={attachmentId}";
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, signer, ct: ct);
		}

		public Task<GeneratedFile> GenerateInvoiceCorrectionRequestXmlAsync(string authToken, string boxId, string messageId, string attachmentId, InvoiceCorrectionRequestInfo correctionInfo, CancellationToken ct = default)
		{
			var queryString = $"/GenerateInvoiceCorrectionRequestXml?boxId={boxId}&messageId={messageId}&attachmentId={attachmentId}";
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, correctionInfo, ct: ct);
		}

		public Task<GeneratedFile> GenerateRevocationRequestXmlAsync(string authToken, string boxId, string messageId, string attachmentId, RevocationRequestInfo revocationRequestInfo, CancellationToken ct = default)
		{
			var queryString = $"/GenerateRevocationRequestXml?boxId={boxId}&messageId={messageId}&attachmentId={attachmentId}";
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, revocationRequestInfo, ct: ct);
		}

		public Task<GeneratedFile> GenerateSignatureRejectionXmlAsync(string authToken, string boxId, string messageId, string attachmentId, SignatureRejectionInfo signatureRejectionInfo, CancellationToken ct = default)
		{
			var queryString = $"/GenerateSignatureRejectionXml?boxId={boxId}&messageId={messageId}&attachmentId={attachmentId}";
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, signatureRejectionInfo, ct: ct);
		}

		public Task<InvoiceCorrectionRequestInfo> GetInvoiceCorrectionRequestInfoAsync(string authToken, string boxId, string messageId, string entityId, CancellationToken ct = default)
		{
			var queryString = $"/GetInvoiceCorrectionRequestInfo?boxId={boxId}&messageId={messageId}&entityId={entityId}";
			return PerformHttpRequestAsync<InvoiceCorrectionRequestInfo>(authToken, "GET", queryString, ct: ct);
		}

		public Task<GeneratedFile> GenerateInvoiceXmlAsync(string authToken, InvoiceInfo invoiceInfo, bool disableValidation = false, CancellationToken ct = default)
		{
			return GenerateInvoiceXmlAsync(authToken, invoiceInfo, "Invoice", disableValidation, ct: ct);
		}

		public Task<GeneratedFile> GenerateInvoiceRevisionXmlAsync(string authToken, InvoiceInfo invoiceRevisionInfo, bool disableValidation = false, CancellationToken ct = default)
		{
			return GenerateInvoiceXmlAsync(authToken, invoiceRevisionInfo, "InvoiceRevision", disableValidation, ct: ct);
		}

		public Task<GeneratedFile> GenerateInvoiceCorrectionXmlAsync(string authToken, InvoiceCorrectionInfo invoiceCorrectionInfo, bool disableValidation = false, CancellationToken ct = default)
		{
			return GenerateInvoiceXmlAsync(authToken, invoiceCorrectionInfo, "InvoiceCorrection", disableValidation, ct: ct);
		}

		public Task<GeneratedFile> GenerateInvoiceCorrectionRevisionXmlAsync(string authToken, InvoiceCorrectionInfo invoiceCorrectionRevision, bool disableValidation = false, CancellationToken ct = default)
		{
			return GenerateInvoiceXmlAsync(authToken, invoiceCorrectionRevision, "InvoiceCorrectionRevision", disableValidation, ct: ct);
		}

		private Task<GeneratedFile> GenerateInvoiceXmlAsync<T>(string authToken, T protoInvoice, string invoiceType, bool disableValidation = false, CancellationToken ct = default) where T : class
		{
			var qsb = new PathAndQueryBuilder("/GenerateInvoiceXml").With("invoiceType", invoiceType);
			if (disableValidation)
				qsb.AddParameter("disableValidation");
			return PerformGenerateXmlHttpRequestAsync(authToken, qsb.BuildPathAndQuery(), protoInvoice, ct: ct);
		}

		public Task<GeneratedFile> GenerateTorg12XmlForSellerAsync(string authToken, Torg12SellerTitleInfo sellerInfo, bool disableValidation = false, CancellationToken ct = default)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateTorg12XmlForSeller");
			if (disableValidation) queryBuilder.AddParameter("disableValidation");
			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), sellerInfo, ct: ct);
		}

		public Task<GeneratedFile> GenerateTovTorg551XmlForSellerAsync(string authToken, TovTorgSellerTitleInfo sellerInfo, bool disableValidation = false, CancellationToken ct = default)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateTorg12XmlForSeller");
			if (disableValidation) queryBuilder.AddParameter("disableValidation");
			queryBuilder.AddParameter("documentVersion", DefaultDocumentVersions.TovTorg551);
			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), sellerInfo, ct: ct);
		}

		public Task<GeneratedFile> GenerateTorg12XmlForBuyerAsync(string authToken, Torg12BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, CancellationToken ct = default)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateTorg12XmlForBuyer");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("sellerTitleMessageId", sellerTitleMessageId);
			queryBuilder.AddParameter("sellerTitleAttachmentId", sellerTitleAttachmentId);
			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), buyerInfo, ct: ct);
		}

		public Task<GeneratedFile> GenerateTovTorg551XmlForBuyerAsync(string authToken, TovTorgBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, string documentVersion = null, CancellationToken ct = default)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateTorg12XmlForBuyer");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("sellerTitleMessageId", sellerTitleMessageId);
			queryBuilder.AddParameter("sellerTitleAttachmentId", sellerTitleAttachmentId);
			queryBuilder.AddParameter("documentVersion", documentVersion ?? DefaultDocumentVersions.TovTorg551);
			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), buyerInfo, ct: ct);
		}

		public Task<GeneratedFile> GenerateAcceptanceCertificateXmlForSellerAsync(string authToken, AcceptanceCertificateSellerTitleInfo sellerInfo, bool disableValidation = false, CancellationToken ct = default)
		{
			var queryString = string.Format("/GenerateAcceptanceCertificateXmlForSeller{0}", disableValidation ? "?disableValidation" : "");
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, sellerInfo, ct: ct);
		}

		public Task<GeneratedFile> GenerateAcceptanceCertificateXmlForBuyerAsync(string authToken, AcceptanceCertificateBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, CancellationToken ct = default)
		{
			var queryString = string.Format("/GenerateAcceptanceCertificateXmlForBuyer?boxId={0}&sellerTitleMessageId={1}&sellerTitleAttachmentId={2}", boxId, sellerTitleMessageId, sellerTitleAttachmentId);
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, buyerInfo, ct: ct);
		}

		public Task<GeneratedFile> GenerateAcceptanceCertificate552XmlForSellerAsync(string authToken, AcceptanceCertificate552SellerTitleInfo sellerInfo, bool disableValidation = false, CancellationToken ct = default)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateAcceptanceCertificateXmlForSeller");
			if (disableValidation) queryBuilder.AddParameter("disableValidation");
			queryBuilder.AddParameter("documentVersion", DefaultDocumentVersions.AcceptanceCerttificate552);
			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), sellerInfo, ct: ct);
		}

		public Task<GeneratedFile> GenerateAcceptanceCertificate552XmlForBuyerAsync(string authToken, AcceptanceCertificate552BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, CancellationToken ct = default)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateAcceptanceCertificateXmlForBuyer");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("sellerTitleMessageId", sellerTitleMessageId);
			queryBuilder.AddParameter("sellerTitleAttachmentId", sellerTitleAttachmentId);
			queryBuilder.AddParameter("documentVersion", DefaultDocumentVersions.AcceptanceCerttificate552);
			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), buyerInfo, ct: ct);
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
			string documentId = null,
			CancellationToken ct = default)
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
			var response = await HttpClient.PerformHttpRequestAsync(request, ct: ct).ConfigureAwait(false);
			return new GeneratedFile(response.ContentDispositionFileName, response.Content);
		}

		public async Task<GeneratedFile> GenerateSenderTitleXmlAsync(string authToken, string boxId, string documentTypeNamedId, string documentFunction, string documentVersion, byte[] userContractData, bool disableValidation = false, string editingSettingId = null, CancellationToken ct = default)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateSenderTitleXml");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("documentTypeNamedId", documentTypeNamedId);
			queryBuilder.AddParameter("documentFunction", documentFunction);
			queryBuilder.AddParameter("documentVersion", documentVersion);
			queryBuilder.AddParameter("editingSettingId", editingSettingId);
			if (disableValidation) queryBuilder.AddParameter("disableValidation");

			var request = BuildHttpRequest(authToken, "POST", queryBuilder.BuildPathAndQuery(), userContractData);
			var response = await HttpClient.PerformHttpRequestAsync(request, ct: ct).ConfigureAwait(false);
			return new GeneratedFile(response.ContentDispositionFileName, response.Content);
		}

		public async Task<GeneratedFile> GenerateRecipientTitleXmlAsync(string authToken, string boxId, string senderTitleMessageId, string senderTitleAttachmentId, byte[] userContractData, string documentVersion = null, CancellationToken ct = default)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateRecipientTitleXml");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("senderTitleMessageId", senderTitleMessageId);
			queryBuilder.AddParameter("senderTitleAttachmentId", senderTitleAttachmentId);
			queryBuilder.AddParameter("documentVersion", documentVersion);

			var request = BuildHttpRequest(authToken, "POST", queryBuilder.BuildPathAndQuery(), userContractData);
			var response = await HttpClient.PerformHttpRequestAsync(request, ct: ct).ConfigureAwait(false);
			return new GeneratedFile(response.ContentDispositionFileName, response.Content);
		}

		public async Task<bool> CanSendInvoiceAsync(string authToken, string boxId, byte[] certificateBytes, CancellationToken ct = default)
		{
			var queryString = string.Format("/CanSendInvoice?boxId={0}", boxId);
			var request = BuildHttpRequest(authToken, "POST", queryString, certificateBytes);
			var response = await HttpClient.PerformHttpRequestAsync(request, ct: ct, HttpStatusCode.Forbidden).ConfigureAwait(false);
			return response.StatusCode == HttpStatusCode.OK;
		}

		public Task SendFnsRegistrationMessageAsync(string authToken, string boxId, FnsRegistrationMessageInfo fnsRegistrationMessageInfo, CancellationToken ct = default)
		{
			var queryString = string.Format("/SendFnsRegistrationMessage?boxId={0}", boxId);
			var request = BuildHttpRequest(authToken, "POST", queryString, Serialize(fnsRegistrationMessageInfo));
			return HttpClient.PerformHttpRequestAsync(request, ct: ct);
		}

		public Task<RevocationRequestInfo> ParseRevocationRequestXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<RevocationRequestInfo>(null, "POST", "/ParseRevocationRequestXml", xmlContent, ct: ct);
		}

		public Task<SignatureRejectionInfo> ParseSignatureRejectionXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<SignatureRejectionInfo>(null, "POST", "/ParseSignatureRejectionXml", xmlContent, ct: ct);
		}

		[Obsolete("Use overload with DocumentTitleType parameter")]
		public Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection, CancellationToken ct = default)
		{
			var documentTitleType = CreateUtdDocumentTitleType(forBuyer, forCorrection);
			return GetExtendedSignerDetailsAsync(token, boxId, thumbprint, documentTitleType, ct: ct);
		}

		[Obsolete("Use overload with DocumentTitleType parameter")]
		public Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection, CancellationToken ct = default)
		{
			var certificate = new X509Certificate2(certificateBytes);
			return GetExtendedSignerDetailsAsync(token, boxId, certificate.Thumbprint, forBuyer, forCorrection, ct: ct);
		}

		public Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, DocumentTitleType documentTitleType, CancellationToken ct = default)
		{
			var queryBuilder = new PathAndQueryBuilder("/V2/ExtendedSignerDetails");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("thumbprint", thumbprint);
			queryBuilder.AddParameter("documentTitleType", ((int) documentTitleType).ToString());
			return PerformHttpRequestAsync<ExtendedSignerDetails>(token, "GET", queryBuilder.ToString(), ct: ct);
		}

		public Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, DocumentTitleType documentTitleType, CancellationToken ct = default)
		{
			var certificate = new X509Certificate2(certificateBytes);
			return GetExtendedSignerDetailsAsync(token, boxId, certificate.Thumbprint, documentTitleType, ct: ct);
		}

		[Obsolete("Use overload with DocumentTitleType parameter")]
		public Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails, CancellationToken ct = default)
		{
			var documentTitleType = CreateUtdDocumentTitleType(forBuyer, forCorrection);
			return PostExtendedSignerDetailsAsync(token, boxId, thumbprint, documentTitleType, signerDetails, ct: ct);
		}

		[Obsolete("Use overload with DocumentTitleType parameter")]
		public Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails, CancellationToken ct = default)
		{
			var certificate = new X509Certificate2(certificateBytes);
			return PostExtendedSignerDetailsAsync(token, boxId, certificate.Thumbprint, forBuyer, forCorrection, signerDetails, ct: ct);
		}

		public Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, DocumentTitleType documentTitleType, ExtendedSignerDetailsToPost signerDetails, CancellationToken ct = default)
		{
			var queryBuilder = new PathAndQueryBuilder("/V2/ExtendedSignerDetails");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("thumbprint", thumbprint);
			queryBuilder.AddParameter("documentTitleType", ((int) documentTitleType).ToString());
			return PerformHttpRequestAsync<ExtendedSignerDetails>(token, "POST", queryBuilder.ToString(), Serialize(signerDetails), ct: ct);
		}

		public Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, DocumentTitleType documentTitleType, ExtendedSignerDetailsToPost signerDetails, CancellationToken ct = default)
		{
			var certificate = new X509Certificate2(certificateBytes);
			return PostExtendedSignerDetailsAsync(token, boxId, certificate.Thumbprint, documentTitleType, signerDetails, ct: ct);
		}
	}
}