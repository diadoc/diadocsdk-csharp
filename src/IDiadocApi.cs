using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using Diadoc.Api.Constants;
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
using JetBrains.Annotations;
using Diadoc.Api.Proto.KeyValueStorage;
using Diadoc.Api.Proto.Organizations;
using Diadoc.Api.Proto.Registration;
using Departments = Diadoc.Api.Proto.Departments;
using DocumentType = Diadoc.Api.Proto.DocumentType;
using Employee = Diadoc.Api.Proto.Employees.Employee;
using Diadoc.Api.Proto.Certificates;
using Diadoc.Api.Proto.CounteragentGroups;
using Diadoc.Api.Proto.Employees.PowersOfAttorney;
using Diadoc.Api.Proto.PowersOfAttorney;
using Diadoc.Api.Proto.Workflows;
using RevocationRequestInfo = Diadoc.Api.Proto.Invoicing.RevocationRequestInfo;

#if !NET35
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
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		OrganizationList GetOrganizationsByInnKpp(string inn, string kpp, bool includeRelations = false);
		OrganizationList GetOrganizationsByInnKpp(string authToken, string inn, string kpp, bool includeRelations = false);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Organization GetOrganizationById(string orgId);
		Organization GetOrganizationById(string authToken, string orgId);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Organization GetOrganizationByBoxId(string boxId);
		Organization GetOrganizationByBoxId(string authToken, string boxId);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Organization GetOrganizationByFnsParticipantId(string fnsParticipantId);
		Organization GetOrganizationByFnsParticipantId(string authToken, string fnsParticipantId);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Organization GetOrganizationByInnKpp(string inn, string kpp);
		Organization GetOrganizationByInnKpp(string authToken, string inn, string kpp);
		RoamingOperatorList GetRoamingOperators(string authToken, string boxId);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Box GetBox(string boxId);
		Box GetBox(string authToken, string boxId);
		[Obsolete("Use a similar method with boxId: GetDepartmentV2()")]
		Department GetDepartment(string authToken, string orgId, string departmentId);
		Department GetDepartmentV2(string authToken, string boxId, string departmentId);
		void UpdateOrganizationProperties(string authToken, OrganizationPropertiesToUpdate orgProps);
		OrganizationFeatures GetOrganizationFeatures(string authToken, string boxId);
		[Obsolete("Use GetNewEventsV8()")]
		BoxEventList GetNewEvents(
			string authToken,
			string boxId,
			string afterEventId = null,
			string afterIndexKey = null,
			string departmentId = null,
			string[] messageTypes = null,
			string[] typeNamedIds = null,
			string[] documentDirections = null,
			long? timestampFromTicks = null,
			long? timestampToTicks = null,
			string counteragentBoxId = null,
			string orderBy = null,
			int? limit = null);

		BoxEventList GetNewEventsV8(
			string authToken,
			string boxId,
			string afterEventId = null,
			string afterIndexKey = null,
			string departmentId = null,
			string[] messageTypes = null,
			string[] typeNamedIds = null,
			string[] documentDirections = null,
			long? timestampFromTicks = null,
			long? timestampToTicks = null,
			string counteragentBoxId = null,
			string orderBy = null,
			int? limit = null);
		[Obsolete("Use GetEventV3()")]
		BoxEvent GetEvent(string authToken, string boxId, string eventId);
		BoxEvent GetEventV3(string authToken, string boxId, string eventId);
		Message PostMessage(string authToken, MessageToPost msg, string operationId = null);
		Template PostTemplate(string authToken, TemplateToPost template, string operationId = null);
		Message TransformTemplateToMessage(string authToken, TemplateTransformationToPost templateTransformation, string operationId = null);
		[Obsolete("Use PostMessagePatchV4()")]
		MessagePatch PostMessagePatch(string authToken, MessagePatchToPost patch, string operationId = null);
		MessagePatch PostMessagePatchV4(string authToken, MessagePatchToPostV2 patch, string operationId = null);
		MessagePatch PostTemplatePatch(string authToken, string boxId, string templateId, TemplatePatchToPost patch, string operationId = null);
		void PostRoamingNotification(string authToken, RoamingNotificationToPost notification);
		void Delete(string authToken, string boxId, string messageId, string documentId);
		void Restore(string authToken, string boxId, string messageId, string documentId);
		void MoveDocuments(string authToken, DocumentsMoveOperation query);
		byte[] GetEntityContent(string authToken, string boxId, string messageId, string entityId);
		GeneratedFile GenerateDocumentReceiptXml(string authToken, string boxId, string messageId, string attachmentId, Signer signer);
		GeneratedFile GenerateInvoiceDocumentReceiptXml(string authToken, string boxId, string messageId, string attachmentId, Signer signer);
		[Obsolete("Use GenerateReceiptXmlV2()")]
		GeneratedFile GenerateReceiptXml(string authToken, string boxId, string messageId, string attachmentId, Signer signer);
		GeneratedFile GenerateReceiptXmlV2(string authToken, string boxId, ReceiptGenerationRequestV2 receiptGenerationRequest);
		[Obsolete("Use GenerateInvoiceCorrectionRequestXmlV2()")]
		GeneratedFile GenerateInvoiceCorrectionRequestXml(string authToken, string boxId, string messageId, string attachmentId, InvoiceCorrectionRequestInfo correctionInfo);
		GeneratedFile GenerateInvoiceCorrectionRequestXmlV2(string authToken, string boxId, InvoiceCorrectionRequestGenerationRequestV2 invoiceCorrectionRequestGenerationRequest);
		GeneratedFile GenerateRevocationRequestXml(string authToken, string boxId, string messageId, string attachmentId, RevocationRequestInfo revocationRequestInfo, string contentTypeId = null);
		[Obsolete("Use GenerateSignatureRejectionXmlV2()")]
		GeneratedFile GenerateSignatureRejectionXml(string authToken, string boxId, string messageId, string attachmentId, SignatureRejectionInfo signatureRejectionInfo);
		GeneratedFile GenerateSignatureRejectionXmlV2(string authToken, string boxId, SignatureRejectionGenerationRequestV2 signatureRejectionGenerationRequest);
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
		GeneratedFile GenerateSystemUniversalMessage(string authToken, string boxId, string messageId, string attachmentId, byte[] userContractData);
		[Obsolete("Use GetMessageV6()")]
		Message GetMessage(string authToken, string boxId, string messageId, bool withOriginalSignature = false, bool injectEntityContent = false);
		Message GetMessageV6(string authToken, string boxId, string messageId, bool withOriginalSignature = false, bool injectEntityContent = false);
		[Obsolete("Use GetMessageV6()")]
		Message GetMessage(string authToken, string boxId, string messageId, string documentId, bool withOriginalSignature = false, bool injectEntityContent = false);
		Message GetMessageV6(string authToken, string boxId, string messageId, string documentId, bool withOriginalSignature = false, bool injectEntityContent = false);
		Template GetTemplate(string authToken, string boxId, string templateId, string entityId = null);
		void RecycleDraft(string authToken, string boxId, string draftId);
		Message SendDraft(string authToken, DraftToSend draftToSend, string operationId = null);
		PrintFormResult GeneratePrintForm(string authToken, string boxId, string messageId, string documentId);
		string GeneratePrintFormFromAttachment(string authToken, DocumentType documentType, byte[] content, string fromBoxId = null);
		[Obsolete("Use GetGeneratedPrintForm without `documentType` parameter")]
		PrintFormResult GetGeneratedPrintForm(string authToken, DocumentType documentType, string printFormId);
		PrintFormResult GetGeneratedPrintForm(string authToken, string printFormId);
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

		[Obsolete("Use GetCounteragentV3()")]
		Counteragent GetCounteragent(string authToken, string myOrgId, string counteragentOrgId);
		Counteragent GetCounteragentV3(string authToken, string myBoxId, string counteragentBoxId);

		[Obsolete("Use GetCounteragentCertificatesV2()")]
		CounteragentCertificateList GetCounteragentCertificates(string authToken, string myOrgId, string counteragentOrgId);
		CounteragentCertificateList GetCounteragentCertificatesV2(string authToken, string myBoxId, string counteragentBoxId);

		[Obsolete("Use GetCounteragentsV3()")]
		CounteragentList GetCounteragents(string authToken, string myOrgId, string counteragentStatus, string afterIndexKey, string query = null, int? pageSize = null);
		CounteragentList GetCounteragentsV3(string authToken, string myBoxId, string counteragentStatus, string afterIndexKey, string query = null, int? pageSize = null);

		[Obsolete("Use BreakWithCounteragentV2()")]
		void BreakWithCounteragent(string authToken, string myOrgId, string counteragentOrgId, string comment);
		void BreakWithCounteragentV2(string authToken, string myBoxId, string counteragentBoxId, string comment);
		BoxCounteragentEventList GetCounteragentEvents(
			string authToken,
			string boxId,
			string afterIndexKey = null,
			long? timestampFromTicks = null,
			long? timestampToTicks = null,
			int? limit = null);
		[Obsolete("Use UploadFileToShelfV2 or UploadLargeFileToShelf")]
		string UploadFileToShelf(string authToken, byte[] data);
		string UploadFileToShelfV2(string authToken, byte[] content, [CanBeNull] string fileExtension);
		string UploadLargeFileToShelf(string authToken, byte[] content, [CanBeNull] string fileExtension);
		[Obsolete("Use GetFileFromShelfV2")]
		byte[] GetFileFromShelf(string authToken, string nameOnShelf);
		byte[] GetFileFromShelfV2(string authToken, string fileName);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		RussianAddress ParseRussianAddress(string address);
		RussianAddress ParseRussianAddress(string authToken, string address);

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		InvoiceInfo ParseInvoiceXml(byte[] invoiceXmlContent);
		InvoiceInfo ParseInvoiceXml(string authToken, byte[] invoiceXmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Torg12SellerTitleInfo ParseTorg12SellerTitleXml(byte[] xmlContent);
		Torg12SellerTitleInfo ParseTorg12SellerTitleXml(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Torg12BuyerTitleInfo ParseTorg12BuyerTitleXml(byte[] xmlContent);
		Torg12BuyerTitleInfo ParseTorg12BuyerTitleXml(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		TovTorgSellerTitleInfo ParseTovTorg551SellerTitleXml(byte[] xmlContent);
		TovTorgSellerTitleInfo ParseTovTorg551SellerTitleXml(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		TovTorgBuyerTitleInfo ParseTovTorg551BuyerTitleXml(byte[] xmlContent);
		TovTorgBuyerTitleInfo ParseTovTorg551BuyerTitleXml(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		AcceptanceCertificateSellerTitleInfo ParseAcceptanceCertificateSellerTitleXml(byte[] xmlContent);
		AcceptanceCertificateSellerTitleInfo ParseAcceptanceCertificateSellerTitleXml(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		AcceptanceCertificateBuyerTitleInfo ParseAcceptanceCertificateBuyerTitleXml(byte[] xmlContent);
		AcceptanceCertificateBuyerTitleInfo ParseAcceptanceCertificateBuyerTitleXml(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		AcceptanceCertificate552SellerTitleInfo ParseAcceptanceCertificate552SellerTitleXml(byte[] xmlContent);
		AcceptanceCertificate552SellerTitleInfo ParseAcceptanceCertificate552SellerTitleXml(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		AcceptanceCertificate552BuyerTitleInfo ParseAcceptanceCertificate552BuyerTitleXml(byte[] xmlContent);
		AcceptanceCertificate552BuyerTitleInfo ParseAcceptanceCertificate552BuyerTitleXml(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		UniversalTransferDocumentSellerTitleInfo ParseUniversalTransferDocumentSellerTitleXml(byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Utd);
		UniversalTransferDocumentSellerTitleInfo ParseUniversalTransferDocumentSellerTitleXml(string authToken, byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Utd);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		UniversalTransferDocumentBuyerTitleInfo ParseUniversalTransferDocumentBuyerTitleXml(byte[] xmlContent);
		UniversalTransferDocumentBuyerTitleInfo ParseUniversalTransferDocumentBuyerTitleXml(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		UniversalCorrectionDocumentSellerTitleInfo ParseUniversalCorrectionDocumentSellerTitleXml(byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Ucd);
		UniversalCorrectionDocumentSellerTitleInfo ParseUniversalCorrectionDocumentSellerTitleXml(string authToken, byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Ucd);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		UniversalTransferDocumentBuyerTitleInfo ParseUniversalCorrectionDocumentBuyerTitleXml(byte[] xmlContent);
		UniversalTransferDocumentBuyerTitleInfo ParseUniversalCorrectionDocumentBuyerTitleXml(string authToken, byte[] xmlContent);

		byte[] ParseTitleXml(
			string authToken,
			string boxId,
			string documentTypeNamedId,
			string documentFunction,
			string documentVersion,
			int titleIndex,
			byte[] content);

		[Obsolete("Use GetOrganizationUsersV2()")]
		OrganizationUsersList GetOrganizationUsers(string authToken, string orgId);
		OrganizationUsersList GetOrganizationUsersV2(string authToken, string boxId);

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		List<Organization> GetOrganizationsByInnList(GetOrganizationsByInnListRequest innList);

		[Obsolete("Use a similar method: GetOrganizationsByInnListV2()")]
		List<Organization> GetOrganizationsByInnList(string authToken, GetOrganizationsByInnListRequest innList);

		[Obsolete("Use a similar method with boxId: GetOrganizationsByInnListV2()")]
		List<OrganizationWithCounteragentStatus> GetOrganizationsByInnList(string authToken, string myOrgId, GetOrganizationsByInnListRequest innList);

		List<Organization> GetOrganizationsByInnListV2(string authToken, GetOrganizationsByInnListRequest innList);

		List<OrganizationWithCounteragentStatus> GetOrganizationsByInnListV2(string authToken, string myBoxId, GetOrganizationsByInnListRequest innList);

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		RevocationRequestInfo ParseRevocationRequestXml(byte[] revocationRequestXmlContent);
		RevocationRequestInfo ParseRevocationRequestXml(string authToken, byte[] revocationRequestXmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		SignatureRejectionInfo ParseSignatureRejectionXml(byte[] signatureRejectionXmlContent);
		SignatureRejectionInfo ParseSignatureRejectionXml(string authToken, byte[] signatureRejectionXmlContent);
		DocumentProtocolResult GenerateDocumentProtocol(string authToken, string boxId, string messageId, string documentId);
		DocumentZipGenerationResult GenerateDocumentZip(string authToken, string boxId, string messageId, string documentId, bool fullDocflow);
		DocumentList GetDocumentsByCustomId(string authToken, string boxId, string customDocumentId);
		PrepareDocumentsToSignResponse PrepareDocumentsToSign(string authToken, PrepareDocumentsToSignRequest request, bool excludeContent = false);
		[Obsolete("Use GetMyUserV2")]
		User GetMyUser(string authToken);
		UserV2 GetMyUserV2(string authToken);
		CertificateList GetMyCertificates(string authToken, string boxId);
		AsyncMethodResult CloudSign(string authToken, CloudSignRequest request, string certificateThumbprint);
		CloudSignResult WaitCloudSignResult(string authToken, string taskId, TimeSpan? timeout = null);
		AsyncMethodResult CloudSignConfirm(string authToken, string cloudSignToken, string confirmationCode, ContentLocationPreference? locationPreference = null);
		CloudSignConfirmResult WaitCloudSignConfirmResult(string authToken, string taskId, TimeSpan? timeout = null);
		AsyncMethodResult DssSign(string authToken, string boxId, DssSignRequest request, string certificateThumbprint = null);
		DssSignResult DssSignResult(string authToken, string boxId, string taskId);

		[Obsolete("Use AcquireCounteragentV3()")]
		AsyncMethodResult AcquireCounteragent(string authToken, string myOrgId, AcquireCounteragentRequest request, string myDepartmentId = null);
		AsyncMethodResult AcquireCounteragentV3(string authToken, string myBoxId, AcquireCounteragentRequest request, string myDepartmentId = null);

		[Obsolete("Use WaitAcquireCounteragentResultV2()")]
		AcquireCounteragentResult WaitAcquireCounteragentResult(string authToken, string taskId, TimeSpan? timeout = null, TimeSpan? delay = null);
		AcquireCounteragentResultV2 WaitAcquireCounteragentResultV2(string authToken, string taskId, TimeSpan? timeout = null, TimeSpan? delay = null);

		DocumentList GetDocumentsByMessageId(string authToken, string boxId, string messageId);
		DocumentWorkflowSettingsListV2 GetWorkflowsSettings(string authToken, string boxId);
		List<KeyValueStorageEntry> GetOrganizationStorageEntries(string authToken, string boxId, IEnumerable<string> keys);
		void PutOrganizationStorageEntries(string authToken, string boxId, IEnumerable<KeyValueStorageEntry> entries);
		AsyncMethodResult AutoSignReceipts(string authToken, string boxId, string certificateThumbprint, string batchKey);
		AutosignReceiptsResult WaitAutosignReceiptsResult(string authToken, string taskId, TimeSpan? timeout = null);
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
		GetDocumentTypesResponseV2 GetDocumentTypesV2(string authToken, string boxId);
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

		EmployeePowerOfAttorneyList GetEmployeePowersOfAttorney(string authToken, string boxId, [CanBeNull] string userId = null, bool onlyActual = false);

		EmployeePowerOfAttorney UpdateEmployeePowerOfAttorney(
			string authToken,
			string boxId,
			[CanBeNull] string userId,
			string registrationNumber,
			string issuerInn,
			EmployeePowerOfAttorneyToUpdate powerOfAttorneyToUpdate);

		EmployeePowerOfAttorney AddEmployeePowerOfAttorney(
			string authToken,
			string boxId,
			[CanBeNull] string userId,
			string registrationNumber,
			string issuerInn);

		void DeleteEmployeePowerOfAttorney(
			string authToken,
			string boxId,
			[CanBeNull] string userId,
			string registrationNumber,
			string issuerInn);

		Departments.Department GetDepartmentByFullId(string authToken, string boxId, string departmentId);
		Departments.DepartmentList GetDepartments(string authToken, string boxId, int? page = null, int? count = null);
		Departments.Department CreateDepartment(string authToken, string boxId, Departments.DepartmentToCreate departmentToCreate);
		Departments.Department UpdateDepartment(string authToken, string boxId, string departmentId, Departments.DepartmentToUpdate departmentToUpdate);
		void DeleteDepartment(string authToken, string boxId, string departmentId);

		RegistrationResponse Register(string authToken, RegistrationRequest registrationRequest);
		void RegisterConfirm(string authToken, RegistrationConfirmRequest registrationConfirmRequest);
		CustomPrintFormDetectionResult DetectCustomPrintForms(string authToken, string boxId, CustomPrintFormDetectionRequest request);
		[Obsolete("Use GetLastEventV2()")]
		BoxEvent GetLastEvent(string authToken, string boxId);
		BoxEvent GetLastEventV2(string authToken, string boxId);

		AsyncMethodResult RegisterPowerOfAttorney(string authToken, string boxId, PowerOfAttorneyToRegister powerOfAttorneyToRegister);
		PowerOfAttorneyRegisterResult RegisterPowerOfAttorneyResult(string authToken, string boxId, string taskId);

		PowerOfAttorneyPrevalidateResult PrevalidatePowerOfAttorney(
			string authToken,
			string boxId,
			string registrationNumber,
			string issuerInn,
			PowerOfAttorneyPrevalidateRequest request);

		PowerOfAttorney GetPowerOfAttorneyInfo(string authToken, string boxId, string messageId, string entityId);
		PowerOfAttorneyContent GetPowerOfAttorneyContent(string authToken, string boxId, string messageId, string entityId);
		PowerOfAttorneyContentResponse GetPowerOfAttorneyContentV2(string authToken, string boxId, string messageId, string entityId);

		CounteragentGroup CreateCounteragentGroup(string authToken, string boxId, CounteragentGroupToCreate counteragentGroupToCreate);
		CounteragentGroup UpdateCounteragentGroup(string authToken, string boxId, string counteragentGroupId, CounteragentGroupToUpdate counteragentGroupToUpdate);
		void DeleteCounteragentGroup(string authToken, string boxId, string counteragentGroupId);
		CounteragentGroup GetCounteragentGroup(string authToken, string boxId, string counteragentGroupId);
		CounteragentGroupsList GetCounteragentGroups(string authToken, string boxId, int? page = null, int? count = null);
		void AddCounteragentToGroup(string authToken, string boxId, string counteragentBoxId, string counteragentGroupId);
		CounteragentFromGroupResponse GetCounteragentsFromGroup(string authToken, string boxId, string counteragentGroupId, int? count = null, string afterIndexKey = null);

#if !NET35

		Task<string> AuthenticateAsync(string login, string password, string key = null, string id = null);
		Task<string> AuthenticateByKeyAsync([NotNull] string key, [NotNull] string id);
		Task<string> AuthenticateBySidAsync([NotNull] string sid);
		Task<string> AuthenticateAsync(byte[] certificateBytes, bool useLocalSystemStorage = false);
		Task<string> AuthenticateAsync(string thumbprint, bool useLocalSystemStorage = false);
		Task<string> AuthenticateWithKeyAsync(string thumbprint, bool useLocalSystemStorage = false, string key =
 null, string id = null, bool autoConfirm = true);
		Task<string> AuthenticateWithKeyAsync(byte[] certificateBytes, bool useLocalSystemStorage = false, string key =
 null, string id = null, bool autoConfirm = true);
		Task<string> AuthenticateWithKeyConfirmAsync(byte[] certificateBytes, string token, bool saveBinding = false);
		Task<string> AuthenticateWithKeyConfirmAsync(string thumbprint, string token, bool saveBinding = false);
		Task<OrganizationUserPermissions> GetMyPermissionsAsync(string authToken, string orgId);
		Task<OrganizationList> GetMyOrganizationsAsync(string authToken, bool autoRegister = true);
		[Obsolete("Use GetMyUserV2Async")]
		Task<User> GetMyUserAsync(string authToken);
		Task<UserV2> GetMyUserV2Async(string authToken);
		Task<CertificateList> GetMyCertificatesAsync(string authToken, string boxId);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<OrganizationList> GetOrganizationsByInnKppAsync(string inn, string kpp, bool includeRelations = false);
		Task<OrganizationList> GetOrganizationsByInnKppAsync(string authToken, string inn, string kpp, bool includeRelations = false);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<Organization> GetOrganizationByIdAsync(string orgId);
		Task<Organization> GetOrganizationByIdAsync(string authToken, string orgId);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<Organization> GetOrganizationByBoxIdAsync(string boxId);
		Task<Organization> GetOrganizationByBoxIdAsync(string authToken, string boxId);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<Organization> GetOrganizationByFnsParticipantIdAsync(string fnsParticipantId);
		Task<Organization> GetOrganizationByFnsParticipantIdAsync(string authToken, string fnsParticipantId);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<Organization> GetOrganizationByInnKppAsync(string inn, string kpp);
		Task<Organization> GetOrganizationByInnKppAsync(string authToken, string inn, string kpp);
		Task<RoamingOperatorList> GetRoamingOperatorsAsync(string authToken, string boxId);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<Box> GetBoxAsync(string boxId);
		Task<Box> GetBoxAsync(string authToken, string boxId);
		[Obsolete("Use a similar method with boxId: GetDepartmentV2Async()")]
		Task<Department> GetDepartmentAsync(string authToken, string orgId, string departmentId);
		Task<Department> GetDepartmentV2Async(string authToken, string boxId, string departmentId);
		Task UpdateOrganizationPropertiesAsync(string authToken, OrganizationPropertiesToUpdate orgProps);
		Task<OrganizationFeatures> GetOrganizationFeaturesAsync(string authToken, string boxId);
		[Obsolete("Use GetNewEventsV8Async()")]
		Task<BoxEventList> GetNewEventsAsync(
			string authToken,
			string boxId,
			string afterEventId = null,
			string afterIndexKey = null,
			string departmentId = null,
			string[] messageTypes = null,
			string[] typeNamedIds = null,
			string[] documentDirections = null,
			long? timestampFromTicks = null,
			long? timestampToTicks = null,
			string counteragentBoxId = null,
			string orderBy = null,
			int? limit = null);
		Task<BoxEventList> GetNewEventsV8Async(
			string authToken,
			string boxId,
			string afterEventId = null,
			string afterIndexKey = null,
			string departmentId = null,
			string[] messageTypes = null,
			string[] typeNamedIds = null,
			string[] documentDirections = null,
			long? timestampFromTicks = null,
			long? timestampToTicks = null,
			string counteragentBoxId = null,
			string orderBy = null,
			int? limit = null);
		[Obsolete("Use GetEventV3Async()")]
		Task<BoxEvent> GetEventAsync(string authToken, string boxId, string eventId);
		Task<BoxEvent> GetEventV3Async(string authToken, string boxId, string eventId);
		Task<Message> PostMessageAsync(string authToken, MessageToPost msg, string operationId = null);
		Task<Template> PostTemplateAsync(string authToken, TemplateToPost template, string operationId = null);
		Task<Message> TransformTemplateToMessageAsync(
			string authToken,
			TemplateTransformationToPost templateTransformation,
			string operationId = null);
		[Obsolete("Use PostMessagePatchV4Async()")]
		Task<MessagePatch> PostMessagePatchAsync(string authToken, MessagePatchToPost patch, string operationId = null);
		Task<MessagePatch> PostMessagePatchV4Async(string authToken, MessagePatchToPostV2 patch, string operationId = null);
		Task<MessagePatch> PostTemplatePatchAsync(string authToken, string boxId, string templateId, TemplatePatchToPost patch, string operationId = null);
		Task PostRoamingNotificationAsync(string authToken, RoamingNotificationToPost notification);
		Task DeleteAsync(string authToken, string boxId, string messageId, string documentId);
		Task RestoreAsync(string authToken, string boxId, string messageId, string documentId);
		Task MoveDocumentsAsync(string authToken, DocumentsMoveOperation query);
		Task<byte[]> GetEntityContentAsync(string authToken, string boxId, string messageId, string entityId);
		Task<GeneratedFile> GenerateDocumentReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer);
		Task<GeneratedFile> GenerateInvoiceDocumentReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer);
		[Obsolete("Use GenerateReceiptXmlV2Async()")]
		Task<GeneratedFile> GenerateReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer);
		Task<GeneratedFile> GenerateReceiptXmlV2Async(string authToken, string boxId, ReceiptGenerationRequestV2 receiptGenerationRequest);
		[Obsolete("Use GenerateInvoiceCorrectionRequestXmlV2Async()")]
		Task<GeneratedFile> GenerateInvoiceCorrectionRequestXmlAsync(string authToken, string boxId, string messageId, string attachmentId, InvoiceCorrectionRequestInfo correctionInfo);
		Task<GeneratedFile> GenerateInvoiceCorrectionRequestXmlV2Async(string authToken, string boxId, InvoiceCorrectionRequestGenerationRequestV2 invoiceCorrectionRequestGenerationRequest);
		Task<GeneratedFile> GenerateRevocationRequestXmlAsync(string authToken, string boxId, string messageId, string attachmentId, RevocationRequestInfo revocationRequestInfo, string contentTypeId = null);
		[Obsolete("Use GenerateSignatureRejectionXmlV2Async()")]
		Task<GeneratedFile> GenerateSignatureRejectionXmlAsync(string authToken, string boxId, string messageId, string attachmentId, SignatureRejectionInfo signatureRejectionInfo);
		Task<GeneratedFile> GenerateSignatureRejectionXmlV2Async(string authToken, string boxId, SignatureRejectionGenerationRequestV2 signatureRejectionGenerationRequest);
		Task<InvoiceCorrectionRequestInfo> GetInvoiceCorrectionRequestInfoAsync(string authToken, string boxId, string messageId, string entityId);
		Task<GeneratedFile> GenerateInvoiceXmlAsync(string authToken, InvoiceInfo invoiceInfo, bool disableValidation =
 false);
		Task<GeneratedFile> GenerateInvoiceRevisionXmlAsync(string authToken, InvoiceInfo invoiceRevisionInfo, bool disableValidation
 = false);
		Task<GeneratedFile> GenerateInvoiceCorrectionXmlAsync(string authToken, InvoiceCorrectionInfo invoiceCorrectionInfo, bool disableValidation
 = false);
		Task<GeneratedFile> GenerateInvoiceCorrectionRevisionXmlAsync(string authToken, InvoiceCorrectionInfo invoiceCorrectionRevision, bool disableValidation
 = false);
		Task<GeneratedFile> GenerateTorg12XmlForSellerAsync(string authToken, Torg12SellerTitleInfo sellerInfo, bool disableValidation
 = false);
		Task<GeneratedFile> GenerateTovTorg551XmlForSellerAsync(string authToken, TovTorgSellerTitleInfo sellerInfo, bool disableValidation
 = false);
		Task<GeneratedFile> GenerateTorg12XmlForBuyerAsync(string authToken, Torg12BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId);
		Task<GeneratedFile> GenerateTovTorg551XmlForBuyerAsync(string authToken, TovTorgBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, string documentVersion
 = null);
		Task<GeneratedFile> GenerateAcceptanceCertificateXmlForSellerAsync(string authToken, AcceptanceCertificateSellerTitleInfo sellerInfo, bool disableValidation
 = false);
		Task<GeneratedFile> GenerateAcceptanceCertificateXmlForBuyerAsync(string authToken, AcceptanceCertificateBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId);
		Task<GeneratedFile> GenerateAcceptanceCertificate552XmlForSellerAsync(string authToken, AcceptanceCertificate552SellerTitleInfo sellerInfo, bool disableValidation
 = false);
		Task<GeneratedFile> GenerateAcceptanceCertificate552XmlForBuyerAsync(string authToken, AcceptanceCertificate552BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId);
		Task<GeneratedFile> GenerateUniversalTransferDocumentXmlForSellerAsync(
			string authToken,
			UniversalTransferDocumentSellerTitleInfo sellerInfo,
			bool disableValidation = false,
			string documentVersion = null);
		Task<GeneratedFile> GenerateUniversalCorrectionDocumentXmlForSellerAsync(
			string authToken,
			UniversalCorrectionDocumentSellerTitleInfo sellerInfo,
			bool disableValidation = false,
			string documentVersion = null);
		Task<GeneratedFile> GenerateUniversalTransferDocumentXmlForBuyerAsync(string authToken, UniversalTransferDocumentBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId);
		Task<GeneratedFile> GenerateTitleXmlAsync(string authToken, string boxId, string documentTypeNamedId, string documentFunction, string documentVersion, int titleIndex, byte[] userContractData, bool disableValidation = false, string editingSettingId = null, string letterId = null, string documentId = null);
		Task<GeneratedFile> GenerateSenderTitleXmlAsync(string authToken, string boxId, string documentTypeNamedId, string documentFunction, string documentVersion, byte[] userContractData, bool disableValidation
 = false, string editingSettingId = null);
		Task<GeneratedFile> GenerateRecipientTitleXmlAsync(string authToken, string boxId, string senderTitleMessageId, string senderTitleAttachmentId, byte[] userContractData, string documentVersion
 = null);
		Task<GeneratedFile> GenerateSystemUniversalMessageAsync(string authToken, string boxId, string messageId, string attachmentId, byte[] userContractData);
		[Obsolete("Use GetMessageV6Async()")]
		Task<Message> GetMessageAsync(string authToken, string boxId, string messageId, bool withOriginalSignature = false, bool injectEntityContent = false);
		Task<Message> GetMessageV6Async(string authToken, string boxId, string messageId, bool withOriginalSignature = false, bool injectEntityContent = false);
		[Obsolete("Use GetMessageV6Async()")]
		Task<Message> GetMessageAsync(string authToken, string boxId, string messageId, string entityId, bool withOriginalSignature = false, bool injectEntityContent = false);
		Task<Message> GetMessageV6Async(string authToken, string boxId, string messageId, string entityId, bool withOriginalSignature = false, bool injectEntityContent = false);
		Task<Template> GetTemplateAsync(string authToken, string boxId, string templateId, string entityId = null);
		Task RecycleDraftAsync(string authToken, string boxId, string draftId);
		Task<Message> SendDraftAsync(string authToken, DraftToSend draftToSend, string operationId = null);
		Task<PrintFormResult> GeneratePrintFormAsync(string authToken, string boxId, string messageId, string documentId);
		Task<string> GeneratePrintFormFromAttachmentAsync(string authToken, DocumentType documentType, byte[] content, string fromBoxId = null);
		[Obsolete("Use GetGeneratedPrintFormAsync without `documentType` parameter")]
		Task<PrintFormResult> GetGeneratedPrintFormAsync(string authToken, DocumentType documentType, string printFormId);
		Task<PrintFormResult> GetGeneratedPrintFormAsync(string authToken, string printFormId);
		Task<DocumentList> GetDocumentsAsync(string authToken, string boxId, string filterCategory, string counteragentBoxId, DateTime? timestampFrom, DateTime? timestampTo, string fromDocumentDate, string toDocumentDate, string departmentId, bool excludeSubdepartments, string afterIndexKey, int? count
 = null);
		Task<DocumentList> GetDocumentsAsync(string authToken, DocumentsFilter filter);
		Task<Document> GetDocumentAsync(string authToken, string boxId, string messageId, string entityId);
		Task<SignatureInfo> GetSignatureInfoAsync(string authToken, string boxId, string messageId, string entityId);
		Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, DocumentTitleType documentTitleType);
		Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, DocumentTitleType documentTitleType);
		Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, DocumentTitleType documentTitleType, ExtendedSignerDetailsToPost signerDetails);
		Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, DocumentTitleType documentTitleType, ExtendedSignerDetailsToPost signerDetails);
		Task<GetDocflowBatchResponse> GetDocflowsAsync(string authToken, string boxId, GetDocflowBatchRequest request);
		Task<GetDocflowEventsResponse> GetDocflowEventsAsync(string authToken, string boxId, GetDocflowEventsRequest request);
		Task<SearchDocflowsResponse> SearchDocflowsAsync(string authToken, string boxId, SearchDocflowsRequest request);
		Task<GetDocflowsByPacketIdResponse> GetDocflowsByPacketIdAsync(string authToken, string boxId, GetDocflowsByPacketIdRequest request);
		Task<ForwardDocumentResponse> ForwardDocumentAsync(string authToken, string boxId, ForwardDocumentRequest request);
		Task<GetForwardedDocumentsResponse> GetForwardedDocumentsAsync(string authToken, string boxId, GetForwardedDocumentsRequest request);
		Task<GetForwardedDocumentEventsResponse> GetForwardedDocumentEventsAsync(string authToken, string boxId, GetForwardedDocumentEventsRequest request);
		Task<byte[]> GetForwardedEntityContentAsync(string authToken, string boxId, ForwardedDocumentId forwardedDocumentId, string entityId);
		Task<DocumentProtocolResult> GenerateForwardedDocumentProtocolAsync(string authToken, string boxId, ForwardedDocumentId forwardedDocumentId);
		Task<PrintFormResult> GenerateForwardedDocumentPrintFormAsync(string authToken, string boxId, ForwardedDocumentId forwardedDocumentId);
		Task<bool> CanSendInvoiceAsync(string authToken, string boxId, byte[] certificateBytes);
		Task SendFnsRegistrationMessageAsync(string authToken, string boxId, FnsRegistrationMessageInfo fnsRegistrationMessageInfo);

		[Obsolete("Use GetCounteragentV3Async()")]
		Task<Counteragent> GetCounteragentAsync(string authToken, string myOrgId, string counteragentOrgId);
		Task<Counteragent> GetCounteragentV3Async(string authToken, string myBoxId, string counteragentBoxId);

		[Obsolete("Use GetCounteragentCertificatesV2Async()")]
		Task<CounteragentCertificateList> GetCounteragentCertificatesAsync(string authToken, string myOrgId, string counteragentOrgId);
		Task<CounteragentCertificateList> GetCounteragentCertificatesV2Async(string authToken, string myBoxId, string counteragentBoxId);

		[Obsolete("Use GetCounteragentsV3Async()")]
		Task<CounteragentList> GetCounteragentsAsync(
			string authToken, 
			string myOrgId, 
			string counteragentStatus,
			string afterIndexKey, 
			string query = null, 
			int? pageSize = null);
		Task<CounteragentList> GetCounteragentsV3Async(
			string authToken, 
			string myBoxId, 
			string counteragentStatus, 
			string afterIndexKey, 
			string query = null, 
			int? pageSize = null);

		[Obsolete("Use BreakWithCounteragentV2Async()")]
		Task BreakWithCounteragentAsync(string authToken, string myOrgId, string counteragentOrgId, string comment);
		Task BreakWithCounteragentV2Async(string authToken, string myBoxId, string counteragentBoxId, string comment);

		Task<BoxCounteragentEventList> GetCounteragentEventsAsync(
			string authToken,
			string boxId,
			string afterIndexKey = null,
			long? timestampFromTicks = null,
			long? timestampToTicks = null,
			int? limit = null);
		[Obsolete("Use UploadFileToShelfV2Async or UploadLargeFileToShelfAsync")]
		Task<string> UploadFileToShelfAsync(string authToken, byte[] data);
		Task<string> UploadFileToShelfV2Async(string authToken, byte[] content, [CanBeNull] string fileExtension);
		Task<string> UploadLargeFileToShelfAsync(string authToken, byte[] content, [CanBeNull] string fileExtension);
		[Obsolete("Use GetFileFromShelfV2Async")]
		Task<byte[]> GetFileFromShelfAsync(string authToken, string nameOnShelf);
		Task<byte[]> GetFileFromShelfV2Async(string authToken, string fileName);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<RussianAddress> ParseRussianAddressAsync(string address);
		Task<RussianAddress> ParseRussianAddressAsync(string authToken, string address);

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<InvoiceInfo> ParseInvoiceXmlAsync(byte[] invoiceXmlContent);
		Task<InvoiceInfo> ParseInvoiceXmlAsync(string authToken, byte[] invoiceXmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<Torg12SellerTitleInfo> ParseTorg12SellerTitleXmlAsync(byte[] xmlContent);
		Task<Torg12SellerTitleInfo> ParseTorg12SellerTitleXmlAsync(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<Torg12BuyerTitleInfo> ParseTorg12BuyerTitleXmlAsync(byte[] xmlContent);
		Task<Torg12BuyerTitleInfo> ParseTorg12BuyerTitleXmlAsync(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<TovTorgSellerTitleInfo> ParseTovTorg551SellerTitleXmlAsync(byte[] xmlContent);
		Task<TovTorgSellerTitleInfo> ParseTovTorg551SellerTitleXmlAsync(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<TovTorgBuyerTitleInfo> ParseTovTorg551BuyerTitleXmlAsync(byte[] xmlContent);
		Task<TovTorgBuyerTitleInfo> ParseTovTorg551BuyerTitleXmlAsync(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<AcceptanceCertificateSellerTitleInfo> ParseAcceptanceCertificateSellerTitleXmlAsync(byte[] xmlContent);
		Task<AcceptanceCertificateSellerTitleInfo> ParseAcceptanceCertificateSellerTitleXmlAsync(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<AcceptanceCertificateBuyerTitleInfo> ParseAcceptanceCertificateBuyerTitleXmlAsync(byte[] xmlContent);
		Task<AcceptanceCertificateBuyerTitleInfo> ParseAcceptanceCertificateBuyerTitleXmlAsync(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<AcceptanceCertificate552SellerTitleInfo> ParseAcceptanceCertificate552SellerTitleXmlAsync(byte[] xmlContent);
		Task<AcceptanceCertificate552SellerTitleInfo> ParseAcceptanceCertificate552SellerTitleXmlAsync(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<AcceptanceCertificate552BuyerTitleInfo> ParseAcceptanceCertificate552BuyerTitleXmlAsync(byte[] xmlContent);
		Task<AcceptanceCertificate552BuyerTitleInfo> ParseAcceptanceCertificate552BuyerTitleXmlAsync(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<UniversalTransferDocumentSellerTitleInfo> ParseUniversalTransferDocumentSellerTitleXmlAsync(byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Utd);
		Task<UniversalTransferDocumentSellerTitleInfo> ParseUniversalTransferDocumentSellerTitleXmlAsync(string authToken, byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Utd);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalTransferDocumentBuyerTitleXmlAsync(byte[] xmlContent);
		Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalTransferDocumentBuyerTitleXmlAsync(string authToken, byte[] xmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<UniversalCorrectionDocumentSellerTitleInfo> ParseUniversalCorrectionDocumentSellerTitleXmlAsync(byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Ucd);
		Task<UniversalCorrectionDocumentSellerTitleInfo> ParseUniversalCorrectionDocumentSellerTitleXmlAsync(string authToken, byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Ucd);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalCorrectionDocumentBuyerTitleXmlAsync(byte[] xmlContent);
		Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalCorrectionDocumentBuyerTitleXmlAsync(string authToken, byte[] xmlContent);

		Task<byte[]> ParseTitleXmlAsync(
			string authToken,
			string boxId,
			string documentTypeNamedId,
			string documentFunction,
			string documentVersion,
			int titleIndex,
			byte[] content);

		[Obsolete("Use GetOrganizationUsersV2Async()")]
		Task<OrganizationUsersList> GetOrganizationUsersAsync(string authToken, string orgId);
		Task<OrganizationUsersList> GetOrganizationUsersV2Async(string authToken, string boxId);

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<List<Organization>> GetOrganizationsByInnListAsync(GetOrganizationsByInnListRequest innList);

		[Obsolete("Use a similar method: GetOrganizationsByInnListV2Async()")]
		Task<List<Organization>> GetOrganizationsByInnListAsync(string authToken, GetOrganizationsByInnListRequest innList);

		[Obsolete("Use a similar method with boxId: GetOrganizationsByInnListV2Async()")]
		Task<List<OrganizationWithCounteragentStatus>> GetOrganizationsByInnListAsync(string authToken, string myOrgId, GetOrganizationsByInnListRequest innList);

		Task<List<Organization>> GetOrganizationsByInnListV2Async(string authToken, GetOrganizationsByInnListRequest innList);
		Task<List<OrganizationWithCounteragentStatus>> GetOrganizationsByInnListV2Async(string authToken, string myBoxId, GetOrganizationsByInnListRequest innList);

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<RevocationRequestInfo> ParseRevocationRequestXmlAsync(byte[] revocationRequestXmlContent);
		Task<RevocationRequestInfo> ParseRevocationRequestXmlAsync(string authToken, byte[] revocationRequestXmlContent);
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		Task<SignatureRejectionInfo> ParseSignatureRejectionXmlAsync(byte[] signatureRejectionXmlContent);
		Task<SignatureRejectionInfo> ParseSignatureRejectionXmlAsync(string authToken, byte[] signatureRejectionXmlContent);
		Task<DocumentProtocolResult> GenerateDocumentProtocolAsync(string authToken, string boxId, string messageId, string documentId);
		Task<DocumentZipGenerationResult> GenerateDocumentZipAsync(string authToken, string boxId, string messageId, string documentId, bool fullDocflow);
		Task<DocumentList> GetDocumentsByCustomIdAsync(string authToken, string boxId, string customDocumentId);
		Task<PrepareDocumentsToSignResponse> PrepareDocumentsToSignAsync(string authToken, PrepareDocumentsToSignRequest request, bool excludeContent
 = false);
		Task<AsyncMethodResult> CloudSignAsync(string authToken, CloudSignRequest request, string certificateThumbprint);
		Task<CloudSignResult> WaitCloudSignResultAsync(string authToken, string taskId, TimeSpan? timeout = null);
		Task<AsyncMethodResult> CloudSignConfirmAsync(string authToken, string cloudSignToken, string confirmationCode, ContentLocationPreference? locationPreference
 = null);
		Task<CloudSignConfirmResult> WaitCloudSignConfirmResultAsync(string authToken, string taskId, TimeSpan? timeout
 = null);
		Task<AsyncMethodResult> DssSignAsync(string authToken, string boxId, DssSignRequest request, string certificateThumbprint = null);
		Task<DssSignResult> DssSignResultAsync(string authToken, string boxId, string taskId);

		[Obsolete("Use AcquireCounteragentV3Async()")]
		Task<AsyncMethodResult> AcquireCounteragentAsync(string authToken, string myOrgId, AcquireCounteragentRequest request, string myDepartmentId = null);
		Task<AsyncMethodResult> AcquireCounteragentV3Async(string authToken, string myBoxId, AcquireCounteragentRequest request, string myDepartmentId = null);

		[Obsolete("Use WaitAcquireCounteragentResultV2Async()")]
		Task<AcquireCounteragentResult> WaitAcquireCounteragentResultAsync(
			string authToken, 
			string taskId, 
			TimeSpan? timeout = null, 
			TimeSpan? delay = null);
		Task<AcquireCounteragentResultV2> WaitAcquireCounteragentResultV2Async(
			string authToken, 
			string taskId, 
			TimeSpan? timeout = null, 
			TimeSpan? delay = null);

		Task<DocumentList> GetDocumentsByMessageIdAsync(string authToken, string boxId, string messageId);
		Task<DocumentWorkflowSettingsListV2> GetWorkflowsSettingsAsync(string authToken, string boxId);
		Task<List<KeyValueStorageEntry>> GetOrganizationStorageEntriesAsync(string authToken, string boxId, IEnumerable<string> keys);
		Task PutOrganizationStorageEntriesAsync(string authToken, string boxId, IEnumerable<KeyValueStorageEntry> entries);
		Task<AsyncMethodResult> AutoSignReceiptsAsync(string authToken, string boxId, string certificateThumbprint, string batchKey);
		Task<AutosignReceiptsResult> WaitAutosignReceiptsResultAsync(string authToken, string taskId, TimeSpan? timeout
 = null);
		Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection);
		Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection);
		Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails);
		Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails);
		Task<ResolutionRouteList> GetResolutionRoutesForOrganizationAsync(string authToken, string orgId);
		Task<GetDocumentTypesResponseV2> GetDocumentTypesV2Async(string authToken, string boxId);
		Task<DetectDocumentTypesResponse> DetectDocumentTypesAsync(string authToken, string boxId, string nameOnShelf);
		Task<DetectDocumentTypesResponse> DetectDocumentTypesAsync(string authToken, string boxId, byte[] content);
		Task<DetectTitleResponse> DetectDocumentTitlesAsync(string authToken, string boxId, string nameOnShelf);
		Task<DetectTitleResponse> DetectDocumentTitlesAsync(string authToken, string boxId, byte[] content);
		[Obsolete("In order to download XSD schema use the link provided in DocumentTitle.XsdUrl")]
		Task<FileContent> GetContentAsync(string authToken, string typeNamedId, string function, string version, int titleIndex, XsdContentType contentType = default(XsdContentType));
		Task<Employee> GetEmployeeAsync(string authToken, string boxId, string userId);
		Task<EmployeeList> GetEmployeesAsync(string authToken, string boxId, int? page, int? count);
		Task<Employee> CreateEmployeeAsync(string authToken, string boxId, EmployeeToCreate employeeToCreate);
		Task<Employee> UpdateEmployeeAsync(string authToken, string boxId, string userId, EmployeeToUpdate employeeToUpdate);
		Task DeleteEmployeeAsync(string authToken, string boxId, string userId);
		Task<Employee> GetMyEmployeeAsync(string authToken, string boxId);

		Task<EmployeePowerOfAttorneyList> GetEmployeePowersOfAttorneyAsync(string authToken, string boxId, [CanBeNull] string userId = null, bool onlyActual = false);

		Task<EmployeePowerOfAttorney> UpdateEmployeePowerOfAttorneyAsync(
			string authToken,
			string boxId,
			[CanBeNull] string userId,
			string registrationNumber,
			string issuerInn,
			EmployeePowerOfAttorneyToUpdate powerOfAttorneyToUpdate);

		Task<EmployeePowerOfAttorney> AddEmployeePowerOfAttorneyAsync(
			string authToken,
			string boxId,
			[CanBeNull] string userId,
			string registrationNumber,
			string issuerInn);

		Task DeleteEmployeePowerOfAttorneyAsync(
			string authToken,
			string boxId,
			[CanBeNull] string userId,
			string registrationNumber,
			string issuerInn);

		Task<EmployeeSubscriptions> GetSubscriptionsAsync(string authToken, string boxId, string userId);
		Task<EmployeeSubscriptions> UpdateSubscriptionsAsync(string authToken, string boxId, string userId, SubscriptionsToUpdate subscriptionsToUpdate);
		Task<Departments.Department> GetDepartmentByFullIdAsync(string authToken, string boxId, string departmentId);
		Task<Departments.DepartmentList> GetDepartmentsAsync(string authToken, string boxId, int? page =
 null, int? count = null);
		Task<Departments.Department> CreateDepartmentAsync(string authToken, string boxId, Departments.DepartmentToCreate departmentToCreate);
		Task<Departments.Department> UpdateDepartmentAsync(string authToken, string boxId, string departmentId, Departments.DepartmentToUpdate departmentToUpdate);
		Task DeleteDepartmentAsync(string authToken, string boxId, string departmentId);
		Task<RegistrationResponse> RegisterAsync(string authToken, RegistrationRequest registrationRequest);
		Task RegisterConfirmAsync(string authToken, RegistrationConfirmRequest registrationConfirmRequest);
		Task<CustomPrintFormDetectionResult> DetectCustomPrintFormsAsync(string authToken, string boxId, CustomPrintFormDetectionRequest request);
		[Obsolete("Use GetLastEventV2Async()")]
		Task<BoxEvent> GetLastEventAsync(string authToken, string boxId);
		Task<BoxEvent> GetLastEventV2Async(string authToken, string boxId);

		Task<AsyncMethodResult> RegisterPowerOfAttorneyAsync(string authToken, string boxId, PowerOfAttorneyToRegister powerOfAttorneyToRegister);
		Task<PowerOfAttorneyRegisterResult> RegisterPowerOfAttorneyResultAsync(string authToken, string boxId, string taskId);

		Task<PowerOfAttorneyPrevalidateResult> PrevalidatePowerOfAttorneyAsync(
			string authToken,
			string boxId,
			string registrationNumber,
			string issuerInn,
			PowerOfAttorneyPrevalidateRequest request);

		Task<PowerOfAttorney> GetPowerOfAttorneyInfoAsync(string authToken, string boxId, string messageId, string entityId);
		Task<PowerOfAttorneyContent> GetPowerOfAttorneyContentAsync(string authToken, string boxId, string messageId, string entityId);
		Task<PowerOfAttorneyContentResponse> GetPowerOfAttorneyContentV2Async(string authToken, string boxId, string messageId, string entityId);
		Task<CounteragentGroup> CreateCounteragentGroupAsync(string authToken, string boxId, CounteragentGroupToCreate counteragentGroupToCreate);
		Task<CounteragentGroup> UpdateCounteragentGroupAsync(string authToken, string boxId, string counteragentGroupId, CounteragentGroupToUpdate counteragentGroupToUpdate);
		Task DeleteCounteragentGroupAsync(string authToken, string boxId, string counteragentGroupId);
		Task<CounteragentGroup> GetCounteragentGroupAsync(string authToken, string boxId, string counteragentGroupId);
		Task<CounteragentGroupsList> GetCounteragentGroupsAsync(string authToken, string boxId, int? page = null, int? count = null);
		Task AddCounteragentToGroupAsync(string authToken, string boxId, string counteragentBoxId, string counteragentGroupId);
		Task<CounteragentFromGroupResponse> GetCounteragentsFromGroupAsync(string authToken, string boxId, string counteragentGroupId, int? count = null, string afterIndexKey = null);
#endif
	}
}
