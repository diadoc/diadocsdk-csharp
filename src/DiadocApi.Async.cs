using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Docflow;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Forwarding;
using Diadoc.Api.Proto.Invoicing;
using Diadoc.Api.Proto.Recognition;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocApi
	{
		public Task<string> AuthenticateAsync(string login, string password)
		{
			return diadocHttpApi.AuthenticateAsync(login, password);
		}

		public Task<string> AuthenticateByKeyAsync([NotNull] string key, [NotNull] string id)
		{
			if (key == null) throw new ArgumentNullException("key");
			if (id == null) throw new ArgumentNullException("id");
			return diadocHttpApi.AuthenticateByKeyAsync(key, id);
		}

		public Task<string> AuthenticateAsync(byte[] certificateBytes, bool useLocalSystemStorage = false)
		{
			if (certificateBytes == null) throw new ArgumentNullException("certificateBytes");
			return diadocHttpApi.AuthenticateAsync(certificateBytes, useLocalSystemStorage);
		}

		public Task<string> AuthenticateAsync(string thumbprint, bool useLocalSystemStorage = false)
		{
			if (thumbprint == null) throw new ArgumentNullException("thumbprint");
			return diadocHttpApi.AuthenticateAsync(thumbprint, useLocalSystemStorage);
		}

		public Task<OrganizationUserPermissions> GetMyPermissionsAsync(string authToken, string orgId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (orgId == null) throw new ArgumentNullException("orgId");
			return diadocHttpApi.GetMyPermissionsAsync(authToken, orgId);
		}

		public Task<OrganizationList> GetMyOrganizationsAsync(string authToken, bool autoRegister = true)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			return diadocHttpApi.GetMyOrganizationsAsync(authToken, autoRegister);
		}

		public Task<User> GetMyUserAsync(string authToken)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			return diadocHttpApi.GetMyUserAsync(authToken);
		}

		public Task<OrganizationList> GetOrganizationsByInnKppAsync(string inn, string kpp, bool includeRelations = false)
		{
			if (inn == null) throw new ArgumentNullException("inn");
			return diadocHttpApi.GetOrganizationsByInnKppAsync(inn, kpp, includeRelations);
		}

		public Task<Organization> GetOrganizationByIdAsync(string orgId)
		{
			if (orgId == null) throw new ArgumentNullException("orgId");
			return diadocHttpApi.GetOrganizationByIdAsync(orgId);
		}

		public Task<Organization> GetOrganizationByBoxIdAsync(string boxId)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetOrganizationByBoxIdAsync(boxId);
		}

		public Task<Organization> GetOrganizationByFnsParticipantIdAsync(string fnsParticipantId)
		{
			if (fnsParticipantId == null) throw new ArgumentException("fnsParticipantId");
			return diadocHttpApi.GetOrganizationByFnsParticipantIdAsync(fnsParticipantId);
		}

		public Task<Organization> GetOrganizationByInnKppAsync(string inn, string kpp)
		{
			if (inn == null) throw new ArgumentException("inn");
			return diadocHttpApi.GetOrganizationByInnKppAsync(inn, kpp);
		}

		public Task<Box> GetBoxAsync(string boxId)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetBoxAsync(boxId);
		}

		public Task<Department> GetDepartmentAsync(string authToken, string orgId, string departmentId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (orgId == null) throw new ArgumentNullException("orgId");
			if (departmentId == null) throw new ArgumentNullException("departmentId");
			return diadocHttpApi.GetDepartmentAsync(authToken, orgId, departmentId);
		}

		public Task UpdateOrganizationPropertiesAsync(string authToken, OrganizationPropertiesToUpdate orgProps)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (orgProps == null) throw new ArgumentNullException("orgProps");
			return diadocHttpApi.UpdateOrganizationPropertiesAsync(authToken, orgProps);
		}

		public Task<BoxEventList> GetNewEventsAsync(string authToken, string boxId, string afterEventId = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetNewEventsAsync(authToken, boxId, afterEventId);
		}

		public Task<BoxEvent> GetEventAsync(string authToken, string boxId, string eventId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (eventId == null) throw new ArgumentNullException("eventId");
			return diadocHttpApi.GetEventAsync(authToken, boxId, eventId);
		}

		public Task<Message> PostMessageAsync(string authToken, MessageToPost msg, string operationId = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (msg == null) throw new ArgumentNullException("msg");
			return diadocHttpApi.PostMessageAsync(authToken, msg, operationId);
		}

		public Task<MessagePatch> PostMessagePatchAsync(string authToken, MessagePatchToPost patch, string operationId = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (patch == null) throw new ArgumentNullException("patch");
			return diadocHttpApi.PostMessagePatchAsync(authToken, patch, operationId);
		}

		public Task PostRoamingNotificationAsync(string authToken, RoamingNotificationToPost notification)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (notification == null) throw new ArgumentNullException("notification");
			return diadocHttpApi.PostRoamingNotificationAsync(authToken, notification);
		}

		public Task DeleteAsync(string authToken, string boxId, string messageId, string documentId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			return diadocHttpApi.DeleteAsync(authToken, boxId, messageId, documentId);
		}

		public Task RestoreAsync(string authToken, string boxId, string messageId, string documentId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			return diadocHttpApi.RestoreAsync(authToken, boxId, messageId, documentId);
		}

		public Task MoveDocumentsAsync(string authToken, DocumentsMoveOperation query)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (query == null) throw new ArgumentNullException("query");
			return diadocHttpApi.MoveDocumentsAsync(authToken, query);
		}

		public Task<byte[]> GetEntityContentAsync(string authToken, string boxId, string messageId, string entityId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (entityId == null) throw new ArgumentNullException("entityId");
			return diadocHttpApi.GetEntityContentAsync(authToken, boxId, messageId, entityId);
		}

		public Task<GeneratedFile> GenerateDocumentReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			if (signer == null) throw new ArgumentNullException("signer");
			return diadocHttpApi.GenerateDocumentReceiptXmlAsync(authToken, boxId, messageId, attachmentId, signer);
		}

		public Task<GeneratedFile> GenerateInvoiceDocumentReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			if (signer == null) throw new ArgumentNullException("signer");
			return diadocHttpApi.GenerateInvoiceDocumentReceiptXmlAsync(authToken, boxId, messageId, attachmentId, signer);
		}

		public Task<GeneratedFile> GenerateInvoiceCorrectionRequestXmlAsync(string authToken, string boxId, string messageId,
			string attachmentId, InvoiceCorrectionRequestInfo correctionInfo)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			return diadocHttpApi.GenerateInvoiceCorrectionRequestXmlAsync(authToken, boxId, messageId, attachmentId, correctionInfo);
		}

		public Task<GeneratedFile> GenerateRevocationRequestXmlAsync(string authToken, string boxId, string messageId,
			string attachmentId, RevocationRequestInfo revocationRequestInfo)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			return diadocHttpApi.GenerateRevocationRequestXmlAsync(authToken, boxId, messageId, attachmentId, revocationRequestInfo);
		}

		public Task<GeneratedFile> GenerateSignatureRejectionXmlAsync(string authToken, string boxId, string messageId,
			string attachmentId, SignatureRejectionInfo signatureRejectionInfo)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			return diadocHttpApi.GenerateSignatureRejectionXmlAsync(authToken, boxId, messageId, attachmentId, signatureRejectionInfo);
		}

		public Task<InvoiceCorrectionRequestInfo> GetInvoiceCorrectionRequestInfoAsync(string authToken, string boxId, string messageId,
			string entityId)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (entityId == null) throw new ArgumentNullException("entityId");
			return diadocHttpApi.GetInvoiceCorrectionRequestInfoAsync(authToken, boxId, messageId, entityId);
		}

		public Task<GeneratedFile> GenerateInvoiceXmlAsync(string authToken, InvoiceInfo invoiceInfo, bool disableValidation = false)
		{
			if (invoiceInfo == null) throw new ArgumentNullException("invoiceInfo");
			return diadocHttpApi.GenerateInvoiceXmlAsync(authToken, invoiceInfo, disableValidation);
		}

		public Task<GeneratedFile> GenerateInvoiceRevisionXmlAsync(string authToken, InvoiceInfo invoiceRevisionInfo,
			bool disableValidation = false)
		{
			if (invoiceRevisionInfo == null) throw new ArgumentNullException("invoiceRevisionInfo");
			return diadocHttpApi.GenerateInvoiceRevisionXmlAsync(authToken, invoiceRevisionInfo, disableValidation);
		}

		public Task<GeneratedFile> GenerateInvoiceCorrectionXmlAsync(string authToken, InvoiceCorrectionInfo invoiceCorrectionInfo,
			bool disableValidation = false)
		{
			if (invoiceCorrectionInfo == null) throw new ArgumentNullException("invoiceCorrectionInfo");
			return diadocHttpApi.GenerateInvoiceCorrectionXmlAsync(authToken, invoiceCorrectionInfo, disableValidation);
		}

		public Task<GeneratedFile> GenerateInvoiceCorrectionRevisionXmlAsync(string authToken,
			InvoiceCorrectionInfo invoiceCorrectionRevision, bool disableValidation = false)
		{
			if (invoiceCorrectionRevision == null) throw new ArgumentNullException("invoiceCorrectionRevision");
			return diadocHttpApi.GenerateInvoiceCorrectionRevisionXmlAsync(authToken, invoiceCorrectionRevision, disableValidation);
		}

		public Task<GeneratedFile> GenerateTorg12XmlForSellerAsync(string authToken, Torg12SellerTitleInfo sellerInfo,
			bool disableValidation = false)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateTorg12XmlForSellerAsync(authToken, sellerInfo, disableValidation);
		}

		public Task<GeneratedFile> GenerateTorg12XmlForBuyerAsync(string authToken, Torg12BuyerTitleInfo buyerInfo, string boxId,
			string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			if (buyerInfo == null) throw new ArgumentNullException("buyerInfo");
			return diadocHttpApi.GenerateTorg12XmlForBuyerAsync(authToken, buyerInfo, boxId, sellerTitleMessageId,
				sellerTitleAttachmentId);
		}

		public Task<GeneratedFile> GenerateAcceptanceCertificateXmlForSellerAsync(string authToken,
			AcceptanceCertificateSellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateAcceptanceCertificateXmlForSellerAsync(authToken, sellerInfo, disableValidation);
		}

		public Task<GeneratedFile> GenerateAcceptanceCertificateXmlForBuyerAsync(string authToken,
			AcceptanceCertificateBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId,
			string sellerTitleAttachmentId)
		{
			if (buyerInfo == null) throw new ArgumentNullException("buyerInfo");
			return diadocHttpApi.GenerateAcceptanceCertificateXmlForBuyerAsync(authToken, buyerInfo, boxId, sellerTitleMessageId,
				sellerTitleAttachmentId);
		}

		public Task<GeneratedFile> GenerateUniversalTransferDocumentXmlForSellerAsync(string authToken,
			UniversalTransferDocumentSellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateUniversalTransferDocumentXmlForSellerAsync(authToken, sellerInfo, disableValidation);
		}

		public Task<GeneratedFile> GenerateUniversalCorrectionDocumentXmlForSellerAsync(string authToken,
			UniversalCorrectionDocumentSellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateUniversalCorrectionDocumentXmlForSellerAsync(authToken, sellerInfo, disableValidation);
		}

		public Task<GeneratedFile> GenerateUniversalTransferDocumentXmlForBuyerAsync(string authToken, UniversalTransferDocumentBuyerTitleInfo buyerInfo,
			string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			if (buyerInfo == null) throw new ArgumentNullException("buyerInfo");
			return diadocHttpApi.GenerateUniversalTransferDocumentXmlForBuyerAsync(authToken, buyerInfo, boxId, sellerTitleMessageId, sellerTitleAttachmentId);
		}

		public Task<Message> GetMessageAsync(string authToken, string boxId, string messageId, bool withOriginalSignature = false)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			return diadocHttpApi.GetMessageAsync(authToken, boxId, messageId, withOriginalSignature);
		}

		public Task<Message> GetMessageAsync(string authToken, string boxId, string messageId, string entityId, bool withOriginalSignature = false)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (entityId == null) throw new ArgumentNullException("entityId");
			return diadocHttpApi.GetMessageAsync(authToken, boxId, messageId, entityId, withOriginalSignature);
		}

		public Task RecycleDraftAsync(string authToken, string boxId, string draftId)
		{
			return diadocHttpApi.RecycleDraftAsync(authToken, boxId, draftId);
		}

		public Task<Message> SendDraftAsync(string authToken, DraftToSend draftToSend, string operationId = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (draftToSend == null) throw new ArgumentNullException("draftToSend");
			return diadocHttpApi.SendDraftAsync(authToken, draftToSend, operationId);
		}

		public Task<PrintFormResult> GeneratePrintFormAsync(string authToken, string boxId, string messageId, string documentId)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			if (string.IsNullOrEmpty(messageId)) throw new ArgumentNullException("messageId");
			if (string.IsNullOrEmpty(documentId)) throw new ArgumentNullException("documentId");
			return diadocHttpApi.GeneratePrintFormAsync(authToken, boxId, messageId, documentId);
		}

		public Task<string> GeneratePrintFormFromAttachmentAsync(string authToken, DocumentType documentType, byte[] content,
			string fromBoxId = null)
		{
			return diadocHttpApi.GeneratePrintFormFromAttachmentAsync(authToken, documentType, content, fromBoxId);
		}

		public Task<PrintFormResult> GetGeneratedPrintFormAsync(string authToken, DocumentType documentType, string printFormId)
		{
			if (string.IsNullOrEmpty(printFormId)) throw new ArgumentNullException("printFormId");
			return diadocHttpApi.GetGeneratedPrintFormAsync(authToken, documentType, printFormId);
		}

		public Task<string> RecognizeAsync(string fileName, byte[] content)
		{
			if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");
			return diadocHttpApi.RecognizeAsync(fileName, content);
		}

		public Task<Recognized> GetRecognizedAsync(string recognitionId)
		{
			if (string.IsNullOrEmpty(recognitionId)) throw new ArgumentNullException("recognitionId");
			return diadocHttpApi.GetRecognizedAsync(recognitionId);
		}

		public Task<DocumentList> GetDocumentsAsync(string authToken, string boxId, string filterCategory, string counteragentBoxId,
			DateTime? timestampFrom, DateTime? timestampTo, string fromDocumentDate, string toDocumentDate, string departmentId,
			bool excludeSubdepartments, string afterIndexKey)
		{
			return diadocHttpApi.GetDocumentsAsync(authToken, boxId, filterCategory, counteragentBoxId, timestampFrom, timestampTo,
				fromDocumentDate, toDocumentDate, departmentId, excludeSubdepartments, afterIndexKey);
		}

		public Task<DocumentList> GetDocumentsAsync(string authToken, DocumentsFilter filter)
		{
			return diadocHttpApi.GetDocumentsAsync(authToken, filter);
		}

		public Task<Document> GetDocumentAsync(string authToken, string boxId, string messageId, string entityId)
		{
			return diadocHttpApi.GetDocumentAsync(authToken, boxId, messageId, entityId);
		}

		public Task<GetDocflowBatchResponse> GetDocflowsAsync(string authToken, string boxId, GetDocflowBatchRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetDocflowsAsync(authToken, boxId, request);
		}

		public Task<GetDocflowEventsResponse> GetDocflowEventsAsync(string authToken, string boxId, GetDocflowEventsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetDocflowEventsAsync(authToken, boxId, request);
		}

		public Task<SearchDocflowsResponse> SearchDocflowsAsync(string authToken, string boxId, SearchDocflowsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.SearchDocflowsAsync(authToken, boxId, request);
		}

		public Task<GetDocflowsByPacketIdResponse> GetDocflowsByPacketIdAsync(string authToken, string boxId,
			GetDocflowsByPacketIdRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetDocflowsByPacketIdAsync(authToken, boxId, request);
		}

		public Task<ForwardDocumentResponse> ForwardDocumentAsync(string authToken, string boxId, ForwardDocumentRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.ForwardDocumentAsync(authToken, boxId, request);
		}

		public Task<GetForwardedDocumentsResponse> GetForwardedDocumentsAsync(string authToken, string boxId,
			GetForwardedDocumentsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetForwardedDocumentsAsync(authToken, boxId, request);
		}

		public Task<GetForwardedDocumentEventsResponse> GetForwardedDocumentEventsAsync(string authToken, string boxId,
			GetForwardedDocumentEventsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetForwardedDocumentEventsAsync(authToken, boxId, request);
		}

		public Task<byte[]> GetForwardedEntityContentAsync(string authToken, string boxId, ForwardedDocumentId forwardedDocumentId,
			string entityId)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetForwardedEntityContentAsync(authToken, boxId, forwardedDocumentId, entityId);
		}

		public Task<DocumentProtocolResult> GenerateForwardedDocumentProtocolAsync(string authToken, string boxId,
			ForwardedDocumentId forwardedDocumentId)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GenerateForwardedDocumentProtocolAsync(authToken, boxId, forwardedDocumentId);
		}

		public Task<bool> CanSendInvoiceAsync(string authToken, string boxId, byte[] certificateBytes)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			if (certificateBytes == null || certificateBytes.Length == 0) throw new ArgumentNullException("certificateBytes");
			return diadocHttpApi.CanSendInvoiceAsync(authToken, boxId, certificateBytes);
		}

		public Task SendFnsRegistrationMessageAsync(string authToken, string boxId,
			FnsRegistrationMessageInfo fnsRegistrationMessageInfo)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			if (!fnsRegistrationMessageInfo.Certificates.Any()) throw new ArgumentException("fnsRegistrationMessageInfo");
			return diadocHttpApi.SendFnsRegistrationMessageAsync(authToken, boxId, fnsRegistrationMessageInfo);
		}

		public Task<Counteragent> GetCounteragentAsync(string authToken, string myOrgId, string counteragentOrgId)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId)) throw new ArgumentNullException("myOrgId");
			if (string.IsNullOrEmpty(counteragentOrgId)) throw new ArgumentNullException("counteragentOrgId");
			return diadocHttpApi.GetCounteragentAsync(authToken, myOrgId, counteragentOrgId);
		}

		public Task<CounteragentList> GetCounteragentsAsync(string authToken, string myOrgId, string counteragentStatus,
			string afterIndexKey)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId)) throw new ArgumentNullException("myOrgId");
			return diadocHttpApi.GetCounteragentsAsync(authToken, myOrgId, counteragentStatus, afterIndexKey);
		}

		public Task<CounteragentCertificateList> GetCounteragentCertificatesAsync(string authToken, string myOrgId,
			string counteragentOrgId)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId)) throw new ArgumentNullException("myOrgId");
			if (string.IsNullOrEmpty(counteragentOrgId)) throw new ArgumentNullException("counteragentOrgId");
			return diadocHttpApi.GetCounteragentCertificatesAsync(authToken, myOrgId, counteragentOrgId);
		}

		public Task BreakWithCounteragentAsync(string authToken, string myOrgId, string counteragentOrgId, string comment)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId)) throw new ArgumentNullException("myOrgId");
			if (string.IsNullOrEmpty(counteragentOrgId)) throw new ArgumentNullException("counteragentOrgId");
			return diadocHttpApi.BreakWithCounteragentAsync(authToken, myOrgId, counteragentOrgId, comment);
		}

		public Task<string> UploadFileToShelfAsync(string authToken, byte[] data)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (data == null) throw new ArgumentNullException("data");
			return diadocHttpApi.UploadFileToShelfAsync(authToken, data);
		}

		public Task<byte[]> GetFileFromShelfAsync(string authToken, string nameOnShelf)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(nameOnShelf)) throw new ArgumentNullException("nameOnShelf");
			return diadocHttpApi.GetFileFromShelfAsync(authToken, nameOnShelf);
		}

		public Task<RussianAddress> ParseRussianAddressAsync(string address)
		{
			return diadocHttpApi.ParseRussianAddressAsync(address);
		}

		public Task<InvoiceInfo> ParseInvoiceXmlAsync(byte[] invoiceXmlContent)
		{
			return diadocHttpApi.ParseInvoiceXmlAsync(invoiceXmlContent);
		}

		public Task<Torg12SellerTitleInfo> ParseTorg12SellerTitleXmlAsync(byte[] xmlContent)
		{
			return diadocHttpApi.ParseTorg12SellerTitleXmlAsync(xmlContent);
		}

		public Task<AcceptanceCertificateSellerTitleInfo> ParseAcceptanceCertificateSellerTitleXmlAsync(byte[] xmlContent)
		{
			return diadocHttpApi.ParseAcceptanceCertificateSellerTitleXmlAsync(xmlContent);
		}

		public Task<UniversalTransferDocumentSellerTitleInfo> ParseUniversalTransferDocumentSellerTitleXmlAsync(byte[] xmlContent)
		{
			return diadocHttpApi.ParseUniversalTransferDocumentSellerTitleXmlAsync(xmlContent);
		}

		public Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalTransferDocumentBuyerTitleXmlAsync(byte[] xmlContent)
		{
			return diadocHttpApi.ParseUniversalTransferDocumentBuyerTitleXmlAsync(xmlContent);
		}
		
		public Task<UniversalCorrectionDocumentSellerTitleInfo> ParseUniversalCorrectionDocumentSellerTitleXmlAsync(byte[] xmlContent)
		{
			return diadocHttpApi.ParseUniversalCorrectionDocumentSellerTitleXmlAsync(xmlContent);
		}

		public Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalCorrectionDocumentBuyerTitleXmlAsync(byte[] xmlContent)
		{
			return diadocHttpApi.ParseUniversalCorrectionDocumentBuyerTitleXmlAsync(xmlContent);
		}

		public Task<OrganizationUsersList> GetOrganizationUsersAsync(string authToken, string orgId)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(orgId)) throw new ArgumentNullException("orgId");
			return diadocHttpApi.GetOrganizationUsersAsync(authToken, orgId);
		}

		public Task<List<Organization>> GetOrganizationsByInnListAsync(GetOrganizationsByInnListRequest innList)
		{
			if (innList == null)
				throw new ArgumentNullException("innList");
			return diadocHttpApi.GetOrganizationsByInnListAsync(innList);
		}

		public Task<List<OrganizationWithCounteragentStatus>> GetOrganizationsByInnListAsync(string authToken, string myOrgId,
			GetOrganizationsByInnListRequest innList)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId))
				throw new ArgumentNullException("myOrgId");
			if (innList == null)
				throw new ArgumentNullException("innList");
			return diadocHttpApi.GetOrganizationsByInnListAsync(authToken, myOrgId, innList);
		}

		public Task<RevocationRequestInfo> ParseRevocationRequestXmlAsync(byte[] revocationRequestXmlContent)
		{
			return diadocHttpApi.ParseRevocationRequestXmlAsync(revocationRequestXmlContent);
		}

		public Task<SignatureRejectionInfo> ParseSignatureRejectionXmlAsync(byte[] signatureRejectionXmlContent)
		{
			return diadocHttpApi.ParseSignatureRejectionXmlAsync(signatureRejectionXmlContent);
		}

		public Task<DocumentProtocolResult> GenerateDocumentProtocolAsync(string authToken, string boxId, string messageId,
			string documentId)
		{
			return diadocHttpApi.GenerateDocumentProtocolAsync(authToken, boxId, messageId, documentId);
		}

		public Task<DocumentZipGenerationResult> GenerateDocumentZipAsync(string authToken, string boxId, string messageId,
			string documentId, bool fullDocflow)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (documentId == null) throw new ArgumentNullException("documentId");
			return diadocHttpApi.GenerateDocumentZipAsync(authToken, boxId, messageId, documentId, fullDocflow);
		}

		public Task<DocumentList> GetDocumentsByCustomIdAsync(string authToken, string boxId, string customDocumentId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (customDocumentId == null) throw new ArgumentNullException("customDocumentId");
			return diadocHttpApi.GetDocumentsByCustomIdAsync(authToken, boxId, customDocumentId);
		}

		public Task<PrepareDocumentsToSignResponse> PrepareDocumentsToSignAsync(string authToken, PrepareDocumentsToSignRequest request,
			bool excludeContent = false)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			return diadocHttpApi.PrepareDocumentsToSignAsync(authToken, request, excludeContent);
		}

		public Task<AsyncMethodResult> CloudSignAsync(string authToken, CloudSignRequest request, string certificateThumbprint)
		{
			if (request == null) throw new ArgumentNullException("request");
			return diadocHttpApi.CloudSignAsync(authToken, request, certificateThumbprint);
		}

		public Task<CloudSignResult> WaitCloudSignResultAsync(string authToken, string taskId, TimeSpan? timeout = null)
		{
			if (string.IsNullOrEmpty(taskId)) throw new ArgumentNullException("taskId");
			return diadocHttpApi.WaitCloudSignResultAsync(authToken, taskId, timeout);
		}

		public Task<AsyncMethodResult> CloudSignConfirmAsync(string authToken, string cloudSignToken, string confirmationCode,
			ContentLocationPreference? locationPreference = null)
		{
			if (string.IsNullOrEmpty(cloudSignToken)) throw new ArgumentNullException("cloudSignToken");
			if (string.IsNullOrEmpty(confirmationCode)) throw new ArgumentNullException("confirmationCode");
			return diadocHttpApi.CloudSignConfirmAsync(authToken, cloudSignToken, confirmationCode, locationPreference);
		}

		public Task<CloudSignConfirmResult> WaitCloudSignConfirmResultAsync(string authToken, string taskId, TimeSpan? timeout = null)
		{
			if (string.IsNullOrEmpty(taskId)) throw new ArgumentNullException("taskId");
			return diadocHttpApi.WaitCloudSignConfirmResultAsync(authToken, taskId, timeout);
		}

		public Task<AsyncMethodResult> AcquireCounteragentAsync(string authToken, string myOrgId, AcquireCounteragentRequest request,
			string myDepartmentId = null)
		{
			if (request == null) throw new ArgumentNullException("request");
			return diadocHttpApi.AcquireCounteragentAsync(authToken, myOrgId, request, myDepartmentId);
		}

		public Task<AcquireCounteragentResult> WaitAcquireCounteragentResultAsync(string authToken, string taskId,
			TimeSpan? timeout = null)
		{
			if (string.IsNullOrEmpty(taskId)) throw new ArgumentNullException("taskId");
			return diadocHttpApi.WaitAcquireCounteragentResultAsync(authToken, taskId, timeout);
		}

		public Task<DocumentList> GetDocumentsByMessageIdAsync(string authToken, string boxId, string messageId)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (string.IsNullOrEmpty(messageId))
				throw new ArgumentNullException("messageId");
			return diadocHttpApi.GetDocumentsByMessageIdAsync(authToken, boxId, messageId);
		}
	}
}