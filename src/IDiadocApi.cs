using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
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
	public interface IDiadocApi
	{
		/// <summary>
		/// The default value is true
		/// </summary>
		bool UsingSystemProxy { get; }

		void SetProxyUri([CanBeNull] string uri);
		void EnableSystemProxyUsage();
		void DisableSystemProxyUsage();
		void SetProxyCredentials([CanBeNull] NetworkCredential proxyCredentials);
		void SetProxyCredentials([NotNull] string user, [NotNull] string password);
		void SetProxyCredentials([NotNull] string user, [NotNull] SecureString password);
		string Authenticate(string login, string password);
		string AuthenticateByKey([NotNull] string key, [NotNull] string id);
		string Authenticate(byte[] certificateBytes, bool useLocalSystemStorage = false);
		string Authenticate(string thumbprint, bool useLocalSystemStorage = false);
		OrganizationUserPermissions GetMyPermissions(string authToken, string orgId);
		OrganizationList GetMyOrganizations(string authToken, bool autoRegister = true);
		OrganizationList GetOrganizationsByInnKpp(string inn, string kpp, bool includeRelations = false);
		Organization GetOrganizationById(string orgId);
		Organization GetOrganizationByBoxId(string boxId);
		Organization GetOrganizationByFnsParticipantId(string fnsParticipantId);
		Organization GetOrganizationByInnKpp(string inn, string kpp);
		Box GetBox(string boxId);
		Department GetDepartment(string authToken, string orgId, string departmentId);
		void UpdateOrganizationProperties(string authToken, OrganizationPropertiesToUpdate orgProps);
		BoxEventList GetNewEvents(string authToken, string boxId, string afterEventId = null);
		BoxEvent GetEvent(string authToken, string boxId, string eventId);
		Message PostMessage(string authToken, MessageToPost msg, string operationId = null);
		MessagePatch PostMessagePatch(string authToken, MessagePatchToPost patch, string operationId = null);
		void PostRoamingNotification(string authToken, RoamingNotificationToPost notification);
		void Delete(string authToken, string boxId, string messageId, string documentId);
		void Restore(string authToken, string boxId, string messageId, string documentId);
		void MoveDocuments(string authToken, DocumentsMoveOperation query);
		byte[] GetEntityContent(string authToken, string boxId, string messageId, string entityId);
		GeneratedFile GenerateDocumentReceiptXml(string authToken, string boxId, string messageId, string attachmentId, Signer signer);
		GeneratedFile GenerateInvoiceDocumentReceiptXml(string authToken, string boxId, string messageId, string attachmentId, Signer signer);
		GeneratedFile GenerateInvoiceCorrectionRequestXml(string authToken, string boxId, string messageId, string attachmentId, InvoiceCorrectionRequestInfo correctionInfo);
		GeneratedFile GenerateRevocationRequestXml(string authToken, string boxId, string messageId, string attachmentId, RevocationRequestInfo revocationRequestInfo);
		GeneratedFile GenerateSignatureRejectionXml(string authToken, string boxId, string messageId, string attachmentId, SignatureRejectionInfo signatureRejectionInfo);
		InvoiceCorrectionRequestInfo GetInvoiceCorrectionRequestInfo(string authToken, string boxId, string messageId, string entityId);
		GeneratedFile GenerateInvoiceXml(string authToken, InvoiceInfo invoiceInfo, bool disableValidation = false);
		GeneratedFile GenerateInvoiceRevisionXml(string authToken, InvoiceInfo invoiceRevisionInfo, bool disableValidation = false);
		GeneratedFile GenerateInvoiceCorrectionXml(string authToken, InvoiceCorrectionInfo invoiceCorrectionInfo, bool disableValidation = false);
		GeneratedFile GenerateInvoiceCorrectionRevisionXml(string authToken, InvoiceCorrectionInfo invoiceCorrectionRevision, bool disableValidation = false);
		GeneratedFile GenerateTorg12XmlForSeller(string authToken, Torg12SellerTitleInfo sellerInfo, bool disableValidation = false);
		GeneratedFile GenerateTorg12XmlForBuyer(string authToken, Torg12BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId);
		GeneratedFile GenerateAcceptanceCertificateXmlForSeller(string authToken, AcceptanceCertificateSellerTitleInfo sellerInfo, bool disableValidation = false);
		GeneratedFile GenerateAcceptanceCertificateXmlForBuyer(string authToken, AcceptanceCertificateBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId);
		GeneratedFile GenerateUniversalTransferDocumentXmlForSeller(string authToken, UniversalTransferDocumentSellerTitleInfo sellerInfo, bool disableValidation = false);
		GeneratedFile GenerateUniversalCorrectionDocumentXmlForSeller(string authToken, UniversalCorrectionDocumentSellerTitleInfo sellerInfo, bool disableValidation = false);
		GeneratedFile GenerateUniversalTransferDocumentXmlForBuyer(string authToken, UniversalTransferDocumentBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId);
		Message GetMessage(string authToken, string boxId, string messageId, bool withOriginalSignature = false);
		Message GetMessage(string authToken, string boxId, string messageId, string entityId, bool withOriginalSignature = false);
		void RecycleDraft(string authToken, string boxId, string draftId);
		Message SendDraft(string authToken, DraftToSend draftToSend, string operationId = null);
		PrintFormResult GeneratePrintForm(string authToken, string boxId, string messageId, string documentId);
		string GeneratePrintFormFromAttachment(string authToken, DocumentType documentType, byte[] content, string fromBoxId = null);
		PrintFormResult GetGeneratedPrintForm(string authToken, DocumentType documentType, string printFormId);
		string Recognize(string fileName, byte[] content);
		Recognized GetRecognized(string recognitionId);
		DocumentList GetDocuments(string authToken, string boxId, string filterCategory, string counteragentBoxId, DateTime? timestampFrom, DateTime? timestampTo, string fromDocumentDate, string toDocumentDate, string departmentId, bool excludeSubdepartments, string afterIndexKey);
		DocumentList GetDocuments(string authToken, DocumentsFilter filter);
		Document GetDocument(string authToken, string boxId, string messageId, string entityId);
		GetDocflowBatchResponse GetDocflows(string authToken, string boxId, GetDocflowBatchRequest request);
		GetDocflowEventsResponse GetDocflowEvents(string authToken, string boxId, GetDocflowEventsRequest request);
		SearchDocflowsResponse SearchDocflows(string authToken, string boxId, SearchDocflowsRequest request);
		GetDocflowsByPacketIdResponse GetDocflowsByPacketId(string authToken, string boxId, GetDocflowsByPacketIdRequest request);
		ForwardDocumentResponse ForwardDocument(string authToken, string boxId, ForwardDocumentRequest request);
		GetForwardedDocumentsResponse GetForwardedDocuments(string authToken, string boxId, GetForwardedDocumentsRequest request);
		GetForwardedDocumentEventsResponse GetForwardedDocumentEvents(string authToken, string boxId, GetForwardedDocumentEventsRequest request);
		byte[] GetForwardedEntityContent(string authToken, string boxId, ForwardedDocumentId forwardedDocumentId, string entityId);
		IDocumentProtocolResult GenerateForwardedDocumentProtocol(string authToken, string boxId, ForwardedDocumentId forwardedDocumentId);
		bool CanSendInvoice(string authToken, string boxId, byte[] certificateBytes);
		void SendFnsRegistrationMessage(string authToken, string boxId, FnsRegistrationMessageInfo fnsRegistrationMessageInfo);
		Counteragent GetCounteragent(string authToken, string myOrgId, string counteragentOrgId);
		CounteragentCertificateList GetCounteragentCertificates(string authToken, string myOrgId, string counteragentOrgId);
		CounteragentList GetCounteragents(string authToken, string myOrgId, string counteragentStatus, string afterIndexKey);
		void BreakWithCounteragent(string authToken, string myOrgId, string counteragentOrgId, string comment);
		string UploadFileToShelf(string authToken, byte[] data);
		byte[] GetFileFromShelf(string authToken, string nameOnShelf);
		RussianAddress ParseRussianAddress(string address);
		InvoiceInfo ParseInvoiceXml(byte[] invoiceXmlContent);
		Torg12SellerTitleInfo ParseTorg12SellerTitleXml(byte[] xmlContent);
		AcceptanceCertificateSellerTitleInfo ParseAcceptanceCertificateSellerTitleXml(byte[] xmlContent);
		UniversalTransferDocumentSellerTitleInfo ParseUniversalTransferDocumentSellerTitleXml(byte[] xmlContent);
		UniversalTransferDocumentBuyerTitleInfo ParseUniversalTransferDocumentBuyerTitleXml(byte[] xmlContent);
		OrganizationUsersList GetOrganizationUsers(string authToken, string orgId);
		List<Organization> GetOrganizationsByInnList(GetOrganizationsByInnListRequest innList);
		List<OrganizationWithCounteragentStatus> GetOrganizationsByInnList(string authToken, string myOrgId, GetOrganizationsByInnListRequest innList);
		RevocationRequestInfo ParseRevocationRequestXml(byte[] revocationRequestXmlContent);
		SignatureRejectionInfo ParseSignatureRejectionXml(byte[] signatureRejectionXmlContent);
		IDocumentProtocolResult GenerateDocumentProtocol(string authToken, string boxId, string messageId, string documentId);
		IDocumentZipGenerationResult GenerateDocumentZip(string authToken, string boxId, string messageId, string documentId, bool fullDocflow);
		DocumentList GetDocumentsByCustomId(string authToken, string boxId, string customDocumentId);
		PrepareDocumentsToSignResponse PrepareDocumentsToSign(string authToken, PrepareDocumentsToSignRequest request, bool excludeContent = false);
		User GetMyUser(string authToken);
		AsyncMethodResult CloudSign(string authToken, CloudSignRequest request, string certificateThumbprint);
		CloudSignResult WaitCloudSignResult(string authToken, string taskId, TimeSpan? timeout = null);
		AsyncMethodResult CloudSignConfirm(string authToken, string cloudSignToken, string confirmationCode, ContentLocationPreference? locationPreference = null);
		CloudSignConfirmResult WaitCloudSignConfirmResult(string authToken, string taskId, TimeSpan? timeout = null);

		AsyncMethodResult AcquireCounteragent(string authToken, string myOrgId, AcquireCounteragentRequest request, string myDepartmentId = null);
		AcquireCounteragentResult WaitAcquireCounteragentResult(string authToken, string taskId, TimeSpan? timeout = null);
		DocumentList GetDocumentsByMessageId(string authToken, string boxId, string messageId);
	}
}