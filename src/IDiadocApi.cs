using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Docflow;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Documents.Types;
using Diadoc.Api.Proto.Dss;
using Diadoc.Api.Proto.Employees.Subscriptions;
using Diadoc.Api.Proto.Employees;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Forwarding;
using Diadoc.Api.Proto.Invoicing;
using Diadoc.Api.Proto.Invoicing.Signers;
using Diadoc.Api.Proto.Recognition;
using JetBrains.Annotations;
using Diadoc.Api.Proto.KeyValueStorage;
using Diadoc.Api.Proto.Organizations;
using Diadoc.Api.Proto.Registration;
using Diadoc.Api.Proto.Users;
using Departments = Diadoc.Api.Proto.Departments;
using DocumentType = Diadoc.Api.Proto.DocumentType;
using Employee = Diadoc.Api.Proto.Employees.Employee;
using Diadoc.Api.Proto.Certificates;
using RevocationRequestInfo = Diadoc.Api.Proto.Invoicing.RevocationRequestInfo;

#if !NET35
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Diadoc.Api
{
	public interface IDiadocApi
	{
		/// <summary>
		/// The default value is true
		/// </summary>
		bool UsingSystemProxy { get; }

		IDocflowApi Docflow { get; }

		void SetProxyUri([CanBeNull] string uri);
		void EnableSystemProxyUsage();
		void DisableSystemProxyUsage();
		void SetProxyCredentials([CanBeNull] NetworkCredential proxyCredentials);
		void SetProxyCredentials([NotNull] string user, [NotNull] string password);
		void SetProxyCredentials([NotNull] string user, [NotNull] SecureString password);
		string Authenticate(string login, string password, string key = null, string id = null);
		string AuthenticateByKey([NotNull] string key, [NotNull] string id);
		string AuthenticateBySid([NotNull] string sid);
		string Authenticate(byte[] certificateBytes, bool useLocalSystemStorage = false);
		string Authenticate(string thumbprint, bool useLocalSystemStorage = false);
		string AuthenticateWithKey(string thumbprint, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true);
		string AuthenticateWithKey(byte[] certificateBytes, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true);
		string AuthenticateWithKeyConfirm(byte[] certificateBytes, string token, bool saveBinding = false);
		string AuthenticateWithKeyConfirm(string thumbprint, string token, bool saveBinding = false);
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
		OrganizationFeatures GetOrganizationFeatures(string authToken, string boxId);
		BoxEventList GetNewEvents(string authToken, string boxId, string afterEventId = null);
		BoxEvent GetEvent(string authToken, string boxId, string eventId);
		Message PostMessage(string authToken, MessageToPost msg, string operationId = null);
		Template PostTemplate(string authToken, TemplateToPost template, string operationId = null);
		Message TransformTemplateToMessage(string authToken, TemplateTransformationToPost templateTransformation, string operationId = null);
		MessagePatch PostMessagePatch(string authToken, MessagePatchToPost patch, string operationId = null);
		MessagePatch PostTemplatePatch(string authToken, string boxId, string templateId, TemplatePatchToPost patch, string operationId = null);
		void PostRoamingNotification(string authToken, RoamingNotificationToPost notification);
		void Delete(string authToken, string boxId, string messageId, string documentId);
		void Restore(string authToken, string boxId, string messageId, string documentId);
		void MoveDocuments(string authToken, DocumentsMoveOperation query);
		byte[] GetEntityContent(string authToken, string boxId, string messageId, string entityId);
		GeneratedFile GenerateDocumentReceiptXml(string authToken, string boxId, string messageId, string attachmentId, Signer signer);
		GeneratedFile GenerateInvoiceDocumentReceiptXml(string authToken, string boxId, string messageId, string attachmentId, Signer signer);
		GeneratedFile GenerateReceiptXml(string authToken, string boxId, string messageId, string attachmentId, Signer signer);
		GeneratedFile GenerateInvoiceCorrectionRequestXml(string authToken, string boxId, string messageId, string attachmentId, InvoiceCorrectionRequestInfo correctionInfo);
		GeneratedFile GenerateRevocationRequestXml(string authToken, string boxId, string messageId, string attachmentId, RevocationRequestInfo revocationRequestInfo);
		GeneratedFile GenerateSignatureRejectionXml(string authToken, string boxId, string messageId, string attachmentId, SignatureRejectionInfo signatureRejectionInfo);
		InvoiceCorrectionRequestInfo GetInvoiceCorrectionRequestInfo(string authToken, string boxId, string messageId, string entityId);
		GeneratedFile GenerateInvoiceXml(string authToken, InvoiceInfo invoiceInfo, bool disableValidation = false);
		GeneratedFile GenerateInvoiceRevisionXml(string authToken, InvoiceInfo invoiceRevisionInfo, bool disableValidation = false);
		GeneratedFile GenerateInvoiceCorrectionXml(string authToken, InvoiceCorrectionInfo invoiceCorrectionInfo, bool disableValidation = false);
		GeneratedFile GenerateInvoiceCorrectionRevisionXml(string authToken, InvoiceCorrectionInfo invoiceCorrectionRevision, bool disableValidation = false);
		GeneratedFile GenerateTorg12XmlForSeller(string authToken, Torg12SellerTitleInfo sellerInfo, bool disableValidation = false);
		GeneratedFile GenerateTovTorg551XmlForSeller(string authToken, TovTorgSellerTitleInfo sellerInfo, bool disableValidation = false);
		GeneratedFile GenerateTorg12XmlForBuyer(string authToken, Torg12BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId);
		GeneratedFile GenerateTovTorg551XmlForBuyer(string authToken, TovTorgBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, string documentVersion = null);
		GeneratedFile GenerateAcceptanceCertificateXmlForSeller(string authToken, AcceptanceCertificateSellerTitleInfo sellerInfo, bool disableValidation = false);
		GeneratedFile GenerateAcceptanceCertificateXmlForBuyer(string authToken, AcceptanceCertificateBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId);
		GeneratedFile GenerateAcceptanceCertificate552XmlForSeller(string authToken, AcceptanceCertificate552SellerTitleInfo sellerInfo, bool disableValidation = false);
		GeneratedFile GenerateAcceptanceCertificate552XmlForBuyer(string authToken, AcceptanceCertificate552BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId);
		GeneratedFile GenerateUniversalTransferDocumentXmlForSeller(
			string authToken,
			UniversalTransferDocumentSellerTitleInfo sellerInfo,
			bool disableValidation = false,
			string documentVersion = null);
		GeneratedFile GenerateUniversalCorrectionDocumentXmlForSeller(
			string authToken,
			UniversalCorrectionDocumentSellerTitleInfo sellerInfo,
			bool disableValidation = false,
			string documentVersion = null);
		GeneratedFile GenerateUniversalTransferDocumentXmlForBuyer(string authToken, UniversalTransferDocumentBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId);
		GeneratedFile GenerateTitleXml(string authToken, string boxId, string documentTypeNamedId, string documentFunction, string documentVersion, int titleIndex, byte[] userContractData, bool disableValidation = false, string editingSettingId = null, string letterId = null, string documentId = null);
		GeneratedFile GenerateSenderTitleXml(string authToken, string boxId, string documentTypeNamedId, string documentFunction, string documentVersion, byte[] userContractData, bool disableValidation = false, string editingSettingId = null);
		GeneratedFile GenerateRecipientTitleXml(string authToken, string boxId, string senderTitleMessageId, string senderTitleAttachmentId, byte[] userContractData, string documentVersion = null);
		Message GetMessage(string authToken, string boxId, string messageId, bool withOriginalSignature = false, bool injectEntityContent = false);
		Message GetMessage(string authToken, string boxId, string messageId, string documentId, bool withOriginalSignature = false, bool injectEntityContent = false);
		Template GetTemplate(string authToken, string boxId, string templateId, string entityId = null);
		void RecycleDraft(string authToken, string boxId, string draftId);
		Message SendDraft(string authToken, DraftToSend draftToSend, string operationId = null);
		PrintFormResult GeneratePrintForm(string authToken, string boxId, string messageId, string documentId);
		string GeneratePrintFormFromAttachment(string authToken, DocumentType documentType, byte[] content, string fromBoxId = null);
		[Obsolete("Use GetGeneratedPrintForm without `documentType` parameter")]
		PrintFormResult GetGeneratedPrintForm(string authToken, DocumentType documentType, string printFormId);
		PrintFormResult GetGeneratedPrintForm(string authToken, string printFormId);
		string Recognize(string fileName, byte[] content);
		Recognized GetRecognized(string recognitionId);
		DocumentList GetDocuments(string authToken, string boxId, string filterCategory, string counteragentBoxId, DateTime? timestampFrom, DateTime? timestampTo, string fromDocumentDate, string toDocumentDate, string departmentId, bool excludeSubdepartments, string afterIndexKey, int? count = null);
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
		DocumentProtocolResult GenerateForwardedDocumentProtocol(string authToken, string boxId, ForwardedDocumentId forwardedDocumentId);
		PrintFormResult GenerateForwardedDocumentPrintForm(string authToken, string boxId, ForwardedDocumentId forwardedDocumentId);
		bool CanSendInvoice(string authToken, string boxId, byte[] certificateBytes);
		void SendFnsRegistrationMessage(string authToken, string boxId, FnsRegistrationMessageInfo fnsRegistrationMessageInfo);
		Counteragent GetCounteragent(string authToken, string myOrgId, string counteragentOrgId);
		CounteragentCertificateList GetCounteragentCertificates(string authToken, string myOrgId, string counteragentOrgId);
		CounteragentList GetCounteragents(string authToken, string myOrgId, string counteragentStatus, string afterIndexKey, string query = null, int? pageSize = null);
		void BreakWithCounteragent(string authToken, string myOrgId, string counteragentOrgId, string comment);
		string UploadFileToShelf(string authToken, byte[] data);
		byte[] GetFileFromShelf(string authToken, string nameOnShelf);
		RussianAddress ParseRussianAddress(string address);

		InvoiceInfo ParseInvoiceXml(byte[] invoiceXmlContent);
		Torg12SellerTitleInfo ParseTorg12SellerTitleXml(byte[] xmlContent);
		Torg12BuyerTitleInfo ParseTorg12BuyerTitleXml(byte[] xmlContent);
		TovTorgSellerTitleInfo ParseTovTorg551SellerTitleXml(byte[] xmlContent);
		TovTorgBuyerTitleInfo ParseTovTorg551BuyerTitleXml(byte[] xmlContent);
		AcceptanceCertificateSellerTitleInfo ParseAcceptanceCertificateSellerTitleXml(byte[] xmlContent);
		AcceptanceCertificateBuyerTitleInfo ParseAcceptanceCertificateBuyerTitleXml(byte[] xmlContent);
		AcceptanceCertificate552SellerTitleInfo ParseAcceptanceCertificate552SellerTitleXml(byte[] xmlContent);
		AcceptanceCertificate552BuyerTitleInfo ParseAcceptanceCertificate552BuyerTitleXml(byte[] xmlContent);
		UniversalTransferDocumentSellerTitleInfo ParseUniversalTransferDocumentSellerTitleXml(byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Utd);
		UniversalTransferDocumentBuyerTitleInfo ParseUniversalTransferDocumentBuyerTitleXml(byte[] xmlContent);
		UniversalCorrectionDocumentSellerTitleInfo ParseUniversalCorrectionDocumentSellerTitleXml(byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Ucd);
		UniversalTransferDocumentBuyerTitleInfo ParseUniversalCorrectionDocumentBuyerTitleXml(byte[] xmlContent);

		byte[] ParseTitleXml(
			string authToken,
			string boxId,
			string documentTypeNamedId,
			string documentFunction,
			string documentVersion,
			int titleIndex,
			byte[] content);

		OrganizationUsersList GetOrganizationUsers(string authToken, string orgId);
		List<Organization> GetOrganizationsByInnList(GetOrganizationsByInnListRequest innList);
		List<OrganizationWithCounteragentStatus> GetOrganizationsByInnList(string authToken, string myOrgId, GetOrganizationsByInnListRequest innList);
		RevocationRequestInfo ParseRevocationRequestXml(byte[] revocationRequestXmlContent);
		SignatureRejectionInfo ParseSignatureRejectionXml(byte[] signatureRejectionXmlContent);
		DocumentProtocolResult GenerateDocumentProtocol(string authToken, string boxId, string messageId, string documentId);
		DocumentZipGenerationResult GenerateDocumentZip(string authToken, string boxId, string messageId, string documentId, bool fullDocflow);
		DocumentList GetDocumentsByCustomId(string authToken, string boxId, string customDocumentId);
		PrepareDocumentsToSignResponse PrepareDocumentsToSign(string authToken, PrepareDocumentsToSignRequest request, bool excludeContent = false);
		[Obsolete("Use GetMyUserV2")]
		User GetMyUser(string authToken);
		UserV2 GetMyUserV2(string authToken);
		UserV2 UpdateMyUser(string authToken, UserToUpdate userToUpdate);
		CertificateList GetMyCertificates(string authToken, string boxId);
		AsyncMethodResult CloudSign(string authToken, CloudSignRequest request, string certificateThumbprint);
		CloudSignResult WaitCloudSignResult(string authToken, string taskId, TimeSpan? timeout = null);
		AsyncMethodResult CloudSignConfirm(string authToken, string cloudSignToken, string confirmationCode, ContentLocationPreference? locationPreference = null);
		CloudSignConfirmResult WaitCloudSignConfirmResult(string authToken, string taskId, TimeSpan? timeout = null);
		AsyncMethodResult DssSign(string authToken, string boxId, DssSignRequest request, string certificateThumbprint = null);
		DssSignResult DssSignResult(string authToken, string boxId, string taskId);
		AsyncMethodResult AcquireCounteragent(string authToken, string myOrgId, AcquireCounteragentRequest request, string myDepartmentId = null);
		AcquireCounteragentResult WaitAcquireCounteragentResult(string authToken, string taskId, TimeSpan? timeout = null, TimeSpan? delay = null);
		DocumentList GetDocumentsByMessageId(string authToken, string boxId, string messageId);
		List<KeyValueStorageEntry> GetOrganizationStorageEntries(string authToken, string boxId, IEnumerable<string> keys);
		void PutOrganizationStorageEntries(string authToken, string boxId, IEnumerable<KeyValueStorageEntry> entries);
		AsyncMethodResult AutoSignReceipts(string authToken, string boxId, string certificateThumbprint, string batchKey);
		AutosignReceiptsResult WaitAutosignReceiptsResult(string authToken, string taskId, TimeSpan? timeout = null);
		ExternalServiceAuthInfo GetExternalServiceAuthInfo(string key);
		ExtendedSignerDetails GetExtendedSignerDetails(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection);
		ExtendedSignerDetails GetExtendedSignerDetails(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection);
		ExtendedSignerDetails PostExtendedSignerDetails(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails);
		ExtendedSignerDetails PostExtendedSignerDetails(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails);
		ResolutionRouteList GetResolutionRoutesForOrganization(string authToken, string orgId);
		SignatureInfo GetSignatureInfo(string authToken, string boxId, string messageId, string entityId);

		ExtendedSignerDetails GetExtendedSignerDetails(string token, string boxId, string thumbprint, DocumentTitleType documentTitleType);
		ExtendedSignerDetails GetExtendedSignerDetails(string token, string boxId, byte[] certificateBytes, DocumentTitleType documentTitleType);
		ExtendedSignerDetails PostExtendedSignerDetails(string token, string boxId, string thumbprint, DocumentTitleType documentTitleType, ExtendedSignerDetailsToPost signerDetails);
		ExtendedSignerDetails PostExtendedSignerDetails(string token, string boxId, byte[] certificateBytes, DocumentTitleType documentTitleType, ExtendedSignerDetailsToPost signerDetails);
		GetDocumentTypesResponse GetDocumentTypes(string authToken, string boxId);
		DetectDocumentTypesResponse DetectDocumentTypes(string authToken, string boxId, string nameOnShelf);
		DetectDocumentTypesResponse DetectDocumentTypes(string authToken, string boxId, byte[] content);
		DetectTitleResponse DetectDocumentTitles(string authToken, string boxId, string nameOnShelf);
		DetectTitleResponse DetectDocumentTitles(string authToken, string boxId, byte[] content);
		FileContent GetContent(string authToken, string typeNamedId, string function, string version, int titleIndex, XsdContentType contentType = default(XsdContentType));
		Employee GetEmployee(string authToken, string boxId, string userId);
		EmployeeList GetEmployees(string authToken, string boxId, int? page, int? count);
		Employee CreateEmployee(string authToken, string boxId, EmployeeToCreate employeeToCreate);
		Employee UpdateEmployee(string authToken, string boxId, string userId, EmployeeToUpdate employeeToUpdate);
		void DeleteEmployee(string authToken, string boxId, string userId);
		Employee GetMyEmployee(string authToken, string boxId);
		EmployeeSubscriptions GetSubscriptions(string authToken, string boxId, string userId);
		EmployeeSubscriptions UpdateSubscriptions(string authToken, string boxId, string userId, SubscriptionsToUpdate subscriptionsToUpdate);

		Departments.Department GetDepartmentByFullId(string authToken, string boxId, string departmentId);
		Departments.DepartmentList GetDepartments(string authToken, string boxId, int? page = null, int? count = null);
		Departments.Department CreateDepartment(string authToken, string boxId, Departments.DepartmentToCreate departmentToCreate);
		Departments.Department UpdateDepartment(string authToken, string boxId, string departmentId, Departments.DepartmentToUpdate departmentToUpdate);
		void DeleteDepartment(string authToken, string boxId, string departmentId);

		RegistrationResponse Register(string authToken, RegistrationRequest registrationRequest);
		void RegisterConfirm(string authToken, RegistrationConfirmRequest registrationConfirmRequest);
		CustomPrintFormDetectionResult DetectCustomPrintForms(string authToken, string boxId, CustomPrintFormDetectionRequest request);
		BoxEvent GetLastEvent(string authToken, string boxId);

#if !NET35

		Task<string> AuthenticateAsync(string login, string password, string key = null, string id = null, CancellationToken ct = default);
		Task<string> AuthenticateByKeyAsync([NotNull] string key, [NotNull] string id, CancellationToken ct = default);
		Task<string> AuthenticateBySidAsync([NotNull] string sid, CancellationToken ct = default);
		Task<string> AuthenticateAsync(byte[] certificateBytes, bool useLocalSystemStorage = false, CancellationToken ct = default);
		Task<string> AuthenticateAsync(string thumbprint, bool useLocalSystemStorage = false, CancellationToken ct = default);
		Task<string> AuthenticateWithKeyAsync(string thumbprint, bool useLocalSystemStorage = false, string key =
 null, string id = null, bool autoConfirm = true, CancellationToken ct = default);
		Task<string> AuthenticateWithKeyAsync(byte[] certificateBytes, bool useLocalSystemStorage = false, string key =
 null, string id = null, bool autoConfirm = true, CancellationToken ct = default);
		Task<string> AuthenticateWithKeyConfirmAsync(byte[] certificateBytes, string token, bool saveBinding = false, CancellationToken ct = default);
		Task<string> AuthenticateWithKeyConfirmAsync(string thumbprint, string token, bool saveBinding = false, CancellationToken ct = default);
		Task<OrganizationUserPermissions> GetMyPermissionsAsync(string authToken, string orgId, CancellationToken ct = default);
		Task<OrganizationList> GetMyOrganizationsAsync(string authToken, bool autoRegister = true, CancellationToken ct = default);
		[Obsolete("Use GetMyUserV2Async")]
		Task<User> GetMyUserAsync(string authToken, CancellationToken ct = default);
		Task<UserV2> GetMyUserV2Async(string authToken, CancellationToken ct = default);
		Task<UserV2> UpdateMyUserAsync(string authToken, UserToUpdate userToUpdate, CancellationToken ct = default);
		Task<CertificateList> GetMyCertificatesAsync(string authToken, string boxId, CancellationToken ct = default);
		Task<OrganizationList> GetOrganizationsByInnKppAsync(string inn, string kpp, bool includeRelations = false, CancellationToken ct = default);
		Task<Organization> GetOrganizationByIdAsync(string orgId, CancellationToken ct = default);
		Task<Organization> GetOrganizationByBoxIdAsync(string boxId, CancellationToken ct = default);
		Task<Organization> GetOrganizationByFnsParticipantIdAsync(string fnsParticipantId, CancellationToken ct = default);
		Task<Organization> GetOrganizationByInnKppAsync(string inn, string kpp, CancellationToken ct = default);
		Task<Box> GetBoxAsync(string boxId, CancellationToken ct = default);
		Task<Department> GetDepartmentAsync(string authToken, string orgId, string departmentId, CancellationToken ct = default);
		Task UpdateOrganizationPropertiesAsync(string authToken, OrganizationPropertiesToUpdate orgProps, CancellationToken ct = default);
		Task<OrganizationFeatures> GetOrganizationFeaturesAsync(string authToken, string boxId, CancellationToken ct = default);
		Task<BoxEventList> GetNewEventsAsync(string authToken, string boxId, string afterEventId = null, CancellationToken ct = default);
		Task<BoxEvent> GetEventAsync(string authToken, string boxId, string eventId, CancellationToken ct = default);
		Task<Message> PostMessageAsync(string authToken, MessageToPost msg, string operationId = null, CancellationToken ct = default);
		Task<Template> PostTemplateAsync(string authToken, TemplateToPost template, string operationId = null, CancellationToken ct = default);
		Task<Message> TransformTemplateToMessageAsync(string authToken, TemplateTransformationToPost templateTransformation, string operationId
 = null, CancellationToken ct = default);
		Task<MessagePatch> PostMessagePatchAsync(string authToken, MessagePatchToPost patch, string operationId = null, CancellationToken ct = default);
		Task<MessagePatch> PostTemplatePatchAsync(string authToken, string boxId, string templateId, TemplatePatchToPost patch, string operationId = null, CancellationToken ct = default);
		Task PostRoamingNotificationAsync(string authToken, RoamingNotificationToPost notification, CancellationToken ct = default);
		Task DeleteAsync(string authToken, string boxId, string messageId, string documentId, CancellationToken ct = default);
		Task RestoreAsync(string authToken, string boxId, string messageId, string documentId, CancellationToken ct = default);
		Task MoveDocumentsAsync(string authToken, DocumentsMoveOperation query, CancellationToken ct = default);
		Task<byte[]> GetEntityContentAsync(string authToken, string boxId, string messageId, string entityId, CancellationToken ct = default);
		Task<GeneratedFile> GenerateDocumentReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer, CancellationToken ct = default);
		Task<GeneratedFile> GenerateInvoiceDocumentReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer, CancellationToken ct = default);
		Task<GeneratedFile> GenerateReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer, CancellationToken ct = default);
		Task<GeneratedFile> GenerateInvoiceCorrectionRequestXmlAsync(string authToken, string boxId, string messageId, string attachmentId, InvoiceCorrectionRequestInfo correctionInfo, CancellationToken ct = default);
		Task<GeneratedFile> GenerateRevocationRequestXmlAsync(string authToken, string boxId, string messageId, string attachmentId, RevocationRequestInfo revocationRequestInfo, CancellationToken ct = default);
		Task<GeneratedFile> GenerateSignatureRejectionXmlAsync(string authToken, string boxId, string messageId, string attachmentId, SignatureRejectionInfo signatureRejectionInfo, CancellationToken ct = default);
		Task<InvoiceCorrectionRequestInfo> GetInvoiceCorrectionRequestInfoAsync(string authToken, string boxId, string messageId, string entityId, CancellationToken ct = default);
		Task<GeneratedFile> GenerateInvoiceXmlAsync(string authToken, InvoiceInfo invoiceInfo, bool disableValidation =
 false, CancellationToken ct = default);
		Task<GeneratedFile> GenerateInvoiceRevisionXmlAsync(string authToken, InvoiceInfo invoiceRevisionInfo, bool disableValidation
 = false, CancellationToken ct = default);
		Task<GeneratedFile> GenerateInvoiceCorrectionXmlAsync(string authToken, InvoiceCorrectionInfo invoiceCorrectionInfo, bool disableValidation
 = false, CancellationToken ct = default);
		Task<GeneratedFile> GenerateInvoiceCorrectionRevisionXmlAsync(string authToken, InvoiceCorrectionInfo invoiceCorrectionRevision, bool disableValidation
 = false, CancellationToken ct = default);
		Task<GeneratedFile> GenerateTorg12XmlForSellerAsync(string authToken, Torg12SellerTitleInfo sellerInfo, bool disableValidation
 = false, CancellationToken ct = default);
		Task<GeneratedFile> GenerateTovTorg551XmlForSellerAsync(string authToken, TovTorgSellerTitleInfo sellerInfo, bool disableValidation
 = false, CancellationToken ct = default);
		Task<GeneratedFile> GenerateTorg12XmlForBuyerAsync(string authToken, Torg12BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, CancellationToken ct = default);
		Task<GeneratedFile> GenerateTovTorg551XmlForBuyerAsync(string authToken, TovTorgBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, string documentVersion
 = null, CancellationToken ct = default);
		Task<GeneratedFile> GenerateAcceptanceCertificateXmlForSellerAsync(string authToken, AcceptanceCertificateSellerTitleInfo sellerInfo, bool disableValidation
 = false, CancellationToken ct = default);
		Task<GeneratedFile> GenerateAcceptanceCertificateXmlForBuyerAsync(string authToken, AcceptanceCertificateBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, CancellationToken ct = default);
		Task<GeneratedFile> GenerateAcceptanceCertificate552XmlForSellerAsync(string authToken, AcceptanceCertificate552SellerTitleInfo sellerInfo, bool disableValidation
 = false, CancellationToken ct = default);
		Task<GeneratedFile> GenerateAcceptanceCertificate552XmlForBuyerAsync(string authToken, AcceptanceCertificate552BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, CancellationToken ct = default);
		Task<GeneratedFile> GenerateUniversalTransferDocumentXmlForSellerAsync(
			string authToken,
			UniversalTransferDocumentSellerTitleInfo sellerInfo,
			bool disableValidation = false,
			string documentVersion = null, 
			CancellationToken ct = default);
		Task<GeneratedFile> GenerateUniversalCorrectionDocumentXmlForSellerAsync(
			string authToken,
			UniversalCorrectionDocumentSellerTitleInfo sellerInfo,
			bool disableValidation = false,
			string documentVersion = null, 
			CancellationToken ct = default);
		Task<GeneratedFile> GenerateUniversalTransferDocumentXmlForBuyerAsync(string authToken, UniversalTransferDocumentBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, CancellationToken ct = default);
		Task<GeneratedFile> GenerateTitleXmlAsync(string authToken, string boxId, string documentTypeNamedId, string documentFunction, string documentVersion, int titleIndex, byte[] userContractData, bool disableValidation = false, string editingSettingId = null, string letterId = null, string documentId = null, CancellationToken ct = default);
		Task<GeneratedFile> GenerateSenderTitleXmlAsync(string authToken, string boxId, string documentTypeNamedId, string documentFunction, string documentVersion, byte[] userContractData, bool disableValidation
 = false, string editingSettingId = null, CancellationToken ct = default);
		Task<GeneratedFile> GenerateRecipientTitleXmlAsync(string authToken, string boxId, string senderTitleMessageId, string senderTitleAttachmentId, byte[] userContractData, string documentVersion
 = null, CancellationToken ct = default);
		Task<Message> GetMessageAsync(string authToken, string boxId, string messageId, bool withOriginalSignature =
 false, bool injectEntityContent = false, CancellationToken ct = default);
		Task<Message> GetMessageAsync(string authToken, string boxId, string messageId, string entityId, bool withOriginalSignature
 = false, bool injectEntityContent = false, CancellationToken ct = default);
		Task<Template> GetTemplateAsync(string authToken, string boxId, string templateId, string entityId = null, CancellationToken ct = default);
		Task RecycleDraftAsync(string authToken, string boxId, string draftId, CancellationToken ct = default);
		Task<Message> SendDraftAsync(string authToken, DraftToSend draftToSend, string operationId = null, CancellationToken ct = default);
		Task<PrintFormResult> GeneratePrintFormAsync(string authToken, string boxId, string messageId, string documentId, CancellationToken ct = default);
		Task<string> GeneratePrintFormFromAttachmentAsync(string authToken, DocumentType documentType, byte[] content, string fromBoxId
 = null, CancellationToken ct = default);
		[Obsolete("Use GetGeneratedPrintFormAsync without `documentType` parameter")]
		Task<PrintFormResult> GetGeneratedPrintFormAsync(string authToken, DocumentType documentType, string printFormId, CancellationToken ct = default);
		Task<PrintFormResult> GetGeneratedPrintFormAsync(string authToken, string printFormId, CancellationToken ct = default);
		Task<string> RecognizeAsync(string fileName, byte[] content, CancellationToken ct = default);
		Task<Recognized> GetRecognizedAsync(string recognitionId, CancellationToken ct = default);
		Task<DocumentList> GetDocumentsAsync(string authToken, string boxId, string filterCategory, string counteragentBoxId, DateTime? timestampFrom, DateTime? timestampTo, string fromDocumentDate, string toDocumentDate, string departmentId, bool excludeSubdepartments, string afterIndexKey, int? count
 = null, CancellationToken ct = default);
		Task<DocumentList> GetDocumentsAsync(string authToken, DocumentsFilter filter, CancellationToken ct = default);
		Task<Document> GetDocumentAsync(string authToken, string boxId, string messageId, string entityId, CancellationToken ct = default);
		Task<SignatureInfo> GetSignatureInfoAsync(string authToken, string boxId, string messageId, string entityId, CancellationToken ct = default);
		Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, DocumentTitleType documentTitleType, CancellationToken ct = default);
		Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, DocumentTitleType documentTitleType, CancellationToken ct = default);
		Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, DocumentTitleType documentTitleType, ExtendedSignerDetailsToPost signerDetails, CancellationToken ct = default);
		Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, DocumentTitleType documentTitleType, ExtendedSignerDetailsToPost signerDetails, CancellationToken ct = default);
		Task<GetDocflowBatchResponse> GetDocflowsAsync(string authToken, string boxId, GetDocflowBatchRequest request, CancellationToken ct = default);
		Task<GetDocflowEventsResponse> GetDocflowEventsAsync(string authToken, string boxId, GetDocflowEventsRequest request, CancellationToken ct = default);
		Task<SearchDocflowsResponse> SearchDocflowsAsync(string authToken, string boxId, SearchDocflowsRequest request, CancellationToken ct = default);
		Task<GetDocflowsByPacketIdResponse> GetDocflowsByPacketIdAsync(string authToken, string boxId, GetDocflowsByPacketIdRequest request, CancellationToken ct = default);
		Task<ForwardDocumentResponse> ForwardDocumentAsync(string authToken, string boxId, ForwardDocumentRequest request, CancellationToken ct = default);
		Task<GetForwardedDocumentsResponse> GetForwardedDocumentsAsync(string authToken, string boxId, GetForwardedDocumentsRequest request, CancellationToken ct = default);
		Task<GetForwardedDocumentEventsResponse> GetForwardedDocumentEventsAsync(string authToken, string boxId, GetForwardedDocumentEventsRequest request, CancellationToken ct = default);
		Task<byte[]> GetForwardedEntityContentAsync(string authToken, string boxId, ForwardedDocumentId forwardedDocumentId, string entityId, CancellationToken ct = default);
		Task<DocumentProtocolResult> GenerateForwardedDocumentProtocolAsync(string authToken, string boxId, ForwardedDocumentId forwardedDocumentId, CancellationToken ct = default);
		Task<PrintFormResult> GenerateForwardedDocumentPrintFormAsync(string authToken, string boxId, ForwardedDocumentId forwardedDocumentId, CancellationToken ct = default);
		Task<bool> CanSendInvoiceAsync(string authToken, string boxId, byte[] certificateBytes, CancellationToken ct = default);
		Task SendFnsRegistrationMessageAsync(string authToken, string boxId, FnsRegistrationMessageInfo fnsRegistrationMessageInfo, CancellationToken ct = default);
		Task<Counteragent> GetCounteragentAsync(string authToken, string myOrgId, string counteragentOrgId, CancellationToken ct = default);
		Task<CounteragentCertificateList> GetCounteragentCertificatesAsync(string authToken, string myOrgId, string counteragentOrgId, CancellationToken ct = default);
		Task<CounteragentList> GetCounteragentsAsync(string authToken, string myOrgId, string counteragentStatus, string afterIndexKey, string query
 = null, int? pageSize = null, CancellationToken ct = default);
		Task BreakWithCounteragentAsync(string authToken, string myOrgId, string counteragentOrgId, string comment, CancellationToken ct = default);
		Task<string> UploadFileToShelfAsync(string authToken, byte[] data, CancellationToken ct = default);
		Task<byte[]> GetFileFromShelfAsync(string authToken, string nameOnShelf, CancellationToken ct = default);
		Task<RussianAddress> ParseRussianAddressAsync(string address, CancellationToken ct = default);

		Task<InvoiceInfo> ParseInvoiceXmlAsync(byte[] invoiceXmlContent, CancellationToken ct = default);
		Task<Torg12SellerTitleInfo> ParseTorg12SellerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default);
		Task<Torg12BuyerTitleInfo> ParseTorg12BuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default);
		Task<TovTorgSellerTitleInfo> ParseTovTorg551SellerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default);
		Task<TovTorgBuyerTitleInfo> ParseTovTorg551BuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default);
		Task<AcceptanceCertificateSellerTitleInfo> ParseAcceptanceCertificateSellerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default);
		Task<AcceptanceCertificateBuyerTitleInfo> ParseAcceptanceCertificateBuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default);
		Task<AcceptanceCertificate552SellerTitleInfo> ParseAcceptanceCertificate552SellerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default);
		Task<AcceptanceCertificate552BuyerTitleInfo> ParseAcceptanceCertificate552BuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default);
		Task<UniversalTransferDocumentSellerTitleInfo> ParseUniversalTransferDocumentSellerTitleXmlAsync(byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Utd, CancellationToken ct = default);
		Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalTransferDocumentBuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default);
		Task<UniversalCorrectionDocumentSellerTitleInfo> ParseUniversalCorrectionDocumentSellerTitleXmlAsync(byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Ucd, CancellationToken ct = default);
		Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalCorrectionDocumentBuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default);

		Task<byte[]> ParseTitleXmlAsync(
			string authToken,
			string boxId,
			string documentTypeNamedId,
			string documentFunction,
			string documentVersion,
			int titleIndex,
			byte[] content, 
			CancellationToken ct = default);

		Task<OrganizationUsersList> GetOrganizationUsersAsync(string authToken, string orgId, CancellationToken ct = default);
		Task<List<Organization>> GetOrganizationsByInnListAsync(GetOrganizationsByInnListRequest innList, CancellationToken ct = default);
		Task<List<OrganizationWithCounteragentStatus>> GetOrganizationsByInnListAsync(string authToken, string myOrgId, GetOrganizationsByInnListRequest innList, CancellationToken ct = default);
		Task<RevocationRequestInfo> ParseRevocationRequestXmlAsync(byte[] revocationRequestXmlContent, CancellationToken ct = default);
		Task<SignatureRejectionInfo> ParseSignatureRejectionXmlAsync(byte[] signatureRejectionXmlContent, CancellationToken ct = default);
		Task<DocumentProtocolResult> GenerateDocumentProtocolAsync(string authToken, string boxId, string messageId, string documentId, CancellationToken ct = default);
		Task<DocumentZipGenerationResult> GenerateDocumentZipAsync(string authToken, string boxId, string messageId, string documentId, bool fullDocflow, CancellationToken ct = default);
		Task<DocumentList> GetDocumentsByCustomIdAsync(string authToken, string boxId, string customDocumentId, CancellationToken ct = default);
		Task<PrepareDocumentsToSignResponse> PrepareDocumentsToSignAsync(string authToken, PrepareDocumentsToSignRequest request, bool excludeContent
 = false, CancellationToken ct = default);
		Task<AsyncMethodResult> CloudSignAsync(string authToken, CloudSignRequest request, string certificateThumbprint, CancellationToken ct = default);
		Task<CloudSignResult> WaitCloudSignResultAsync(string authToken, string taskId, TimeSpan? timeout = null, CancellationToken ct = default);
		Task<AsyncMethodResult> CloudSignConfirmAsync(string authToken, string cloudSignToken, string confirmationCode, ContentLocationPreference? locationPreference
 = null, CancellationToken ct = default);
		Task<CloudSignConfirmResult> WaitCloudSignConfirmResultAsync(string authToken, string taskId, TimeSpan? timeout
 = null, CancellationToken ct = default);
		Task<AsyncMethodResult> DssSignAsync(string authToken, string boxId, DssSignRequest request, string certificateThumbprint = null, CancellationToken ct = default);
		Task<DssSignResult> DssSignResultAsync(string authToken, string boxId, string taskId, CancellationToken ct = default);
		Task<AsyncMethodResult> AcquireCounteragentAsync(string authToken, string myOrgId, AcquireCounteragentRequest request, string myDepartmentId
 = null, CancellationToken ct = default);
		Task<AcquireCounteragentResult> WaitAcquireCounteragentResultAsync(string authToken, string taskId, TimeSpan? timeout
 = null, TimeSpan? delay = null, CancellationToken ct = default);
		Task<DocumentList> GetDocumentsByMessageIdAsync(string authToken, string boxId, string messageId, CancellationToken ct = default);
		Task<List<KeyValueStorageEntry>> GetOrganizationStorageEntriesAsync(string authToken, string boxId, IEnumerable<string> keys, CancellationToken ct = default);
		Task PutOrganizationStorageEntriesAsync(string authToken, string boxId, IEnumerable<KeyValueStorageEntry> entries, CancellationToken ct = default);
		Task<AsyncMethodResult> AutoSignReceiptsAsync(string authToken, string boxId, string certificateThumbprint, string batchKey, CancellationToken ct = default);
		Task<AutosignReceiptsResult> WaitAutosignReceiptsResultAsync(string authToken, string taskId, TimeSpan? timeout
 = null, CancellationToken ct = default);
		Task<ExternalServiceAuthInfo> GetExternalServiceAuthInfoAsync(string key, CancellationToken ct = default);
		Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection, CancellationToken ct = default);
		Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection, CancellationToken ct = default);
		Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails, CancellationToken ct = default);
		Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails, CancellationToken ct = default);
		Task<ResolutionRouteList> GetResolutionRoutesForOrganizationAsync(string authToken, string orgId, CancellationToken ct = default);
		Task<GetDocumentTypesResponse> GetDocumentTypesAsync(string authToken, string boxId, CancellationToken ct = default);
		Task<DetectDocumentTypesResponse> DetectDocumentTypesAsync(string authToken, string boxId, string nameOnShelf, CancellationToken ct = default);
		Task<DetectDocumentTypesResponse> DetectDocumentTypesAsync(string authToken, string boxId, byte[] content, CancellationToken ct = default);
		Task<DetectTitleResponse> DetectDocumentTitlesAsync(string authToken, string boxId, string nameOnShelf, CancellationToken ct = default);
		Task<DetectTitleResponse> DetectDocumentTitlesAsync(string authToken, string boxId, byte[] content, CancellationToken ct = default);
		[Obsolete("In order to download XSD schema use the link provided in DocumentTitle.XsdUrl")]
		Task<FileContent> GetContentAsync(string authToken, string typeNamedId, string function, string version, int titleIndex, XsdContentType contentType = default(XsdContentType), CancellationToken ct = default);
		Task<Employee> GetEmployeeAsync(string authToken, string boxId, string userId, CancellationToken ct = default);
		Task<EmployeeList> GetEmployeesAsync(string authToken, string boxId, int? page, int? count, CancellationToken ct = default);
		Task<Employee> CreateEmployeeAsync(string authToken, string boxId, EmployeeToCreate employeeToCreate, CancellationToken ct = default);
		Task<Employee> UpdateEmployeeAsync(string authToken, string boxId, string userId, EmployeeToUpdate employeeToUpdate, CancellationToken ct = default);
		Task DeleteEmployeeAsync(string authToken, string boxId, string userId, CancellationToken ct = default);
		Task<Employee> GetMyEmployeeAsync(string authToken, string boxId, CancellationToken ct = default);
		Task<EmployeeSubscriptions> GetSubscriptionsAsync(string authToken, string boxId, string userId, CancellationToken ct = default);
		Task<EmployeeSubscriptions> UpdateSubscriptionsAsync(string authToken, string boxId, string userId, SubscriptionsToUpdate subscriptionsToUpdate, CancellationToken ct = default);
		Task<Departments.Department> GetDepartmentByFullIdAsync(string authToken, string boxId, string departmentId, CancellationToken ct = default);
		Task<Departments.DepartmentList> GetDepartmentsAsync(string authToken, string boxId, int? page =
 null, int? count = null, CancellationToken ct = default);
		Task<Departments.Department> CreateDepartmentAsync(string authToken, string boxId, Departments.DepartmentToCreate departmentToCreate, CancellationToken ct = default);
		Task<Departments.Department> UpdateDepartmentAsync(string authToken, string boxId, string departmentId, Departments.DepartmentToUpdate departmentToUpdate, CancellationToken ct = default);
		Task DeleteDepartmentAsync(string authToken, string boxId, string departmentId, CancellationToken ct = default);
		Task<RegistrationResponse> RegisterAsync(string authToken, RegistrationRequest registrationRequest, CancellationToken ct = default);
		Task RegisterConfirmAsync(string authToken, RegistrationConfirmRequest registrationConfirmRequest, CancellationToken ct = default);
		Task<CustomPrintFormDetectionResult> DetectCustomPrintFormsAsync(string authToken, string boxId, CustomPrintFormDetectionRequest request, CancellationToken ct = default);
		Task<BoxEvent> GetLastEventAsync(string authToken, string boxId, CancellationToken ct = default);
#endif
	}
}
