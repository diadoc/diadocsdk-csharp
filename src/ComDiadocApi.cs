using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using Diadoc.Api.Com;
using Diadoc.Api.Cryptography;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Docflow;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Forwarding;
using Diadoc.Api.Proto.Invoicing;
using Diadoc.Api.Proto.Recognition;
using DocumentType = Diadoc.Api.Proto.DocumentType;

namespace Diadoc.Api
{
	[ComVisible(true)]
	[Guid("EE244CBB-1EEB-42EA-978C-EB6B195C3BC8")]
	public interface IComDiadocApi
	{
		void Initialize(string apiClientId, string serverUrl);
		void SetProxyUri(string uri);
		void SetProxyCredentials(string proxyUser, string proxyPassword);
		void SetProxyCredentialsSecure(string proxyUser, SecureString proxyPassword);
		void EnableSystemProxyUsage();
		void DisableSystemProxyUsage();
		string CreateNewId();
		string AuthenticateWithPassword(string login, string password);
		string AuthenticateWithCertificate(string thumbprint, bool useLocalSystemStorage = false);
		OrganizationUserPermissions GetMyPermissions(string authToken, string orgId);
		ReadonlyList GetMyOrganizations(string authToken, bool autoRegister = true);
		ReadonlyList GetOrganizationsByInnKpp(string inn, string kpp);
		Organization GetOrganizationById(string orgId);
		Organization GetOrganizationByInn(string inn);
		ReadonlyList GetOrganizationsByInnList([MarshalAs(UnmanagedType.IDispatch)] object innList);

		ReadonlyList GetOrganizationsByInnList(string authToken, string myOrgId,
			[MarshalAs(UnmanagedType.IDispatch)] object innList);

		Organization GetOrganizationByFnsParticipantId(string fnsParticipantId);
		Box GetBox(string boxId);
		Department GetDepartment(string authToken, string orgId, string departmentId);
		BoxEventList GetNewEvents(string authToken, string boxId, string afterEventId);
		BoxEvent GetEvent(string authToken, string boxId, string eventId);
		void SaveEntityContent(string authToken, string boxId, string messageId, string entityId, string filePath);
		Message PostMessage(string authToken, [MarshalAs(UnmanagedType.IDispatch)] object message);
		MessagePatch PostMessagePatch(string authToken, [MarshalAs(UnmanagedType.IDispatch)] object patch);

		GeneratedFile GenerateInvoiceDocumentReceiptXml(string authToken, string boxId, string messageId, string attachmentId,
			[MarshalAs(UnmanagedType.IDispatch)] object signer);

		GeneratedFile GenerateInvoiceCorrectionRequestXml(string authToken, string boxId, string messageId,
			string attachmentId, [MarshalAs(UnmanagedType.IDispatch)] object correctionInfo);

		GeneratedFile GenerateInvoiceXml(string authToken, [MarshalAs(UnmanagedType.IDispatch)] object invoiceInfo,
			bool disableValidation = false);

		GeneratedFile GenerateInvoiceRevisionXml(string authToken,
			[MarshalAs(UnmanagedType.IDispatch)] object invoiceRevisionInfo, bool disableValidation = false);

		GeneratedFile GenerateInvoiceCorrectionXml(string authToken,
			[MarshalAs(UnmanagedType.IDispatch)] object invoiceCorrectionInfo, bool disableValidation = false);

		GeneratedFile GenerateInvoiceCorrectionRevisionXml(string authToken,
			[MarshalAs(UnmanagedType.IDispatch)] object invoiceCorrectionRevisionInfo, bool disableValidation = false);

		GeneratedFile GenerateTorg12XmlForSeller(string authToken, [MarshalAs(UnmanagedType.IDispatch)] object sellerInfo,
			bool disableValidation = false);

		GeneratedFile GenerateTorg12XmlForBuyer(string authToken, [MarshalAs(UnmanagedType.IDispatch)] object buyerInfo,
			string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId);

		GeneratedFile GenerateAcceptanceCertificateXmlForSeller(string authToken,
			[MarshalAs(UnmanagedType.IDispatch)] object sellerInfo, bool disableValidation = false);

		GeneratedFile GenerateAcceptanceCertificateXmlForBuyer(string authToken,
			[MarshalAs(UnmanagedType.IDispatch)] object buyerInfo, string boxId, string sellerTitleMessageId,
			string sellerTitleAttachmentId);

		InvoiceCorrectionRequestInfo GetInvoiceCorrectionRequestInfo(string authToken, string boxId, string messageId,
			string entityId);

		Message GetMessage(string authToken, string boxId, string messageId);
		Message GetMessage(string authToken, string boxId, string messageId, string entityId);
		void RecycleDraft(string authToken, string boxId, string draftId);
		Message SendDraft(string authToken, [MarshalAs(UnmanagedType.IDispatch)] object draftToSend);
		void Delete(string authToken, string boxId, string messageId, string documentId);
		void Restore(string authToken, string boxId, string messageId, string documentId);
		void MoveDocuments(string authToken, [MarshalAs(UnmanagedType.IDispatch)] object query);
		string NewGuid();
		string Recognize(string fileName, string filePath);
		Recognized GetRecognized(string recognitionId);
		PrintFormResult GeneratePrintForm(string authToken, string boxId, string messageId, string documentId);
		PrintFormResult GetGeneratedPrintForm(string authToken, int documentType, string printFormId);
		string GeneratePrintFormFromAttachment(string authToken, int documentType, byte[] content);
		DateTime NullDateTime();
		DocumentList GetDocuments(string authToken, [MarshalAs(UnmanagedType.IDispatch)] object filter);

		DocumentList GetDocuments(string authToken, string boxId, string filterCategory, string counteragentBoxId,
			DateTime timestampFrom, DateTime timestampTo, string fromDocumentDate, string toDocumentDate, string departmentId,
			bool excludeSubdepartments, string afterIndexKey);

		Document GetDocument(string authToken, string boxId, string messageId, string entityId);
		Counteragent GetCounteragent(string authToken, string myOrgId, string counteragentOrgId);
		CounteragentCertificateList GetCounteragentCertificates(string authToken, string myOrgId, string counteragentOrgId);
		CounteragentList GetCounteragents(string authToken, string myOrgId, string counteragentStatus, string afterIndexKey);

		void AcquireCounteragent(string authToken, string myOrgId, string counteragentOrgId, string comment,
			string myDepartmentId = null);

		AsyncMethodResult AcquireCounteragent2(string authToken, string myOrgId,
			[MarshalAs(UnmanagedType.IDispatch)] object acquireCounteragentRequest, string myDepartmentId = null);

		AcquireCounteragentResult WaitAcquireCounteragentResult(string authToken, string taskId);
		void BreakWithCounteragent(string authToken, string myOrgId, string counteragentOrgId, string comment);
		string UploadFileToShelf(string authToken, string fileName);
		void GetFileFromShelf(string authToken, string nameOnShelf, string fileName);
		RussianAddress ParseRussianAddress(string address);
		InvoiceInfo ParseInvoiceXml(byte[] invoiceXmlContent);
		InvoiceInfo ParseInvoiceXmlFromFile(string fileName);

		GeneratedFile GenerateDocumentReceiptXml(string authToken, string boxId, string messageId, string attachmentId,
			[MarshalAs(UnmanagedType.IDispatch)] object signer);

		Torg12SellerTitleInfo ParseTorg12SellerTitleXml(byte[] torg12SellerTitleXmlContent);
		Torg12SellerTitleInfo ParseTorg12SellerTitleXmlFromFile(string fileName);
		AcceptanceCertificateSellerTitleInfo ParseAcceptanceCertificateSellerTitleXml(byte[] xmlContent);
		AcceptanceCertificateSellerTitleInfo ParseAcceptanceCertificateSellerTitleXmlFromFile(string fileName);

		GeneratedFile GenerateRevocationRequestXml(string authToken, string boxId, string messageId, string attachmentId,
			[MarshalAs(UnmanagedType.IDispatch)] object revocationRequestInfo);

		GeneratedFile GenerateSignatureRejectionXml(string authToken, string boxId, string messageId, string attachmentId,
			[MarshalAs(UnmanagedType.IDispatch)] object signatureRejectionInfo);

		RevocationRequestInfo ParseRevocationRequestXml(byte[] revocationRequestXmlContent);
		RevocationRequestInfo ParseRevocationRequestXmlFromFile(string fileName);
		SignatureRejectionInfo ParseSignatureRejectionXml(byte[] signatureRejectionXmlContent);
		SignatureRejectionInfo ParseSignatureRejectionXmlFromFile(string fileName);
		Organization GetOrganizationByBoxId(string boxId);
		Organization GetOrganizationByInnKpp(string inn, string kpp);
		IDocumentProtocolResult GenerateDocumentProtocol(string authToken, string boxId, string messageId, string documentId);

		GetForwardedDocumentsResponse GetForwardedDocuments(string authToken, string boxId,
			[MarshalAs(UnmanagedType.IDispatch)] object request);

		ForwardDocumentResponse ForwardDocument(string authToken, string boxId,
			[MarshalAs(UnmanagedType.IDispatch)] object request);

		GetForwardedDocumentEventsResponse GetForwardedDocumentEvents(string authToken, string boxId,
			[MarshalAs(UnmanagedType.IDispatch)] object request);

		byte[] GetForwardedEntityContent(string authToken, string boxId,
			[MarshalAs(UnmanagedType.IDispatch)] object forwardedDocumentId, string entityId);

		IDocumentProtocolResult GenerateForwardedDocumentProtocol(string authToken, string boxId,
			[MarshalAs(UnmanagedType.IDispatch)] object forwardedDocumentId);

		GetDocflowBatchResponse GetDocflows(string authToken, string boxId,
			[MarshalAs(UnmanagedType.IDispatch)] object request);

		GetDocflowEventsResponse GetDocflowEvents(string authToken, string boxId,
			[MarshalAs(UnmanagedType.IDispatch)] object request);

		SearchDocflowsResponse SearchDocflows(string authToken, string boxId,
			[MarshalAs(UnmanagedType.IDispatch)] object request);

		GetDocflowsByPacketIdResponse GetDocflowsByPacketId(string authToken, string boxId,
			[MarshalAs(UnmanagedType.IDispatch)] object request);

		bool CanSendInvoice(string authToken, string boxId, byte[] certificateBytes);

		IDocumentZipGenerationResult GenerateDocumentZip(string authToken, string boxId, string messageId, string documentId,
			bool fullDocflow);

		PrepareDocumentsToSignResponse PrepareDocumentsToSign(string authToken,
			[MarshalAs(UnmanagedType.IDispatch)] object request);

		User GetMyUser(string authToken);
		DocumentList GetDocumentsByMessageId(string authToken, string boxId, string messageId);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ComDiadocApi2")]
	[Guid("78FC377A-09AE-4053-AA41-4A943CEAEDEE")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IComDiadocApi))]
	public class ComDiadocApi : SafeComObject, IComDiadocApi
	{
		private DiadocApi diadoc;

		public void Initialize(string apiClientId, string serverUrl)
		{
			diadoc = new DiadocApi(apiClientId, serverUrl, new WinApiCrypt());
		}

		public void SetProxyUri(string uri)
		{
			diadoc.SetProxyUri(uri);
		}

		public void SetProxyCredentials(string proxyUser, string proxyPassword)
		{
			diadoc.SetProxyCredentials(proxyUser, proxyPassword);
		}

		public void SetProxyCredentialsSecure(string proxyUser, SecureString proxyPassword)
		{
			diadoc.SetProxyCredentials(proxyUser, proxyPassword);
		}

		public void EnableSystemProxyUsage()
		{
			diadoc.EnableSystemProxyUsage();
		}

		public void DisableSystemProxyUsage()
		{
			diadoc.DisableSystemProxyUsage();
		}

		public string CreateNewId()
		{
			return Guid.NewGuid().ToString("N");
		}

		public string AuthenticateWithPassword(string login, string password)
		{
			return diadoc.Authenticate(login, password);
		}

		public string AuthenticateWithCertificate(string thumbprint, bool useLocalSystemStorage = false)
		{
			return diadoc.Authenticate(thumbprint, useLocalSystemStorage);
		}

		public OrganizationUserPermissions GetMyPermissions(string authToken, string orgId)
		{
			return diadoc.GetMyPermissions(authToken, orgId);
		}

		public ReadonlyList GetOrganizationUsers(string authToken, string orgId)
		{
			return new ReadonlyList(diadoc.GetOrganizationUsers(authToken, orgId).Users);
		}

		public ReadonlyList GetMyOrganizations(string authToken, bool autoRegister = true)
		{
			return new ReadonlyList(diadoc.GetMyOrganizations(authToken, autoRegister).Organizations);
		}

		public ReadonlyList GetOrganizationsByInnKpp(string inn, string kpp)
		{
			return new ReadonlyList(diadoc.GetOrganizationsByInnKpp(inn, kpp).Organizations);
		}

		public Organization GetOrganizationById(string orgId)
		{
			return diadoc.GetOrganizationById(orgId);
		}

		public Organization GetOrganizationByInn(string inn)
		{
			return diadoc.GetOrganizationByInnKpp(inn, null);
		}

		public Organization GetOrganizationByBoxId(string boxId)
		{
			return diadoc.GetOrganizationByBoxId(boxId);
		}

		public Organization GetOrganizationByFnsParticipantId(string fnsParticipantId)
		{
			return diadoc.GetOrganizationByFnsParticipantId(fnsParticipantId);
		}

		public Organization GetOrganizationByInnKpp(string inn, string kpp)
		{
			return diadoc.GetOrganizationByInnKpp(inn, kpp);
		}

		public IDocumentProtocolResult GenerateDocumentProtocol(string authToken, string boxId, string messageId,
			string documentId)
		{
			return diadoc.GenerateDocumentProtocol(authToken, boxId, messageId, documentId);
		}

		public ReadonlyList GetOrganizationsByInnList(object innList)
		{
			return new ReadonlyList(diadoc.GetOrganizationsByInnList((GetOrganizationsByInnListRequest) innList));
		}

		public ReadonlyList GetOrganizationsByInnList(string authToken, string myOrgId, object innList)
		{
			return
				new ReadonlyList(diadoc.GetOrganizationsByInnList(authToken, myOrgId, (GetOrganizationsByInnListRequest) innList));
		}

		public Box GetBox(string boxId)
		{
			return diadoc.GetBox(boxId);
		}

		public Department GetDepartment(string authToken, string orgId, string departmentId)
		{
			return diadoc.GetDepartment(authToken, orgId, departmentId);
		}

		public BoxEventList GetNewEvents(string authToken, string boxId, string afterEventId)
		{
			return diadoc.GetNewEvents(authToken, boxId, afterEventId);
		}

		public BoxEvent GetEvent(string authToken, string boxId, string eventId)
		{
			return diadoc.GetEvent(authToken, boxId, eventId);
		}

		public void SaveEntityContent(string authToken, string boxId, string messageId, string entityId, string filePath)
		{
			File.WriteAllBytes(filePath, diadoc.GetEntityContent(authToken, boxId, messageId, entityId));
		}

		public Message PostMessage(string authToken, object message)
		{
			return diadoc.PostMessage(authToken, (MessageToPost) message);
		}

		public MessagePatch PostMessagePatch(string authToken, object patch)
		{
			return diadoc.PostMessagePatch(authToken, (MessagePatchToPost) patch);
		}

		public GeneratedFile GenerateDocumentReceiptXml(string authToken, string boxId, string messageId, string attachmentId,
			object signer)
		{
			return diadoc.GenerateDocumentReceiptXml(authToken, boxId, messageId, attachmentId, (Signer) signer);
		}

		public GeneratedFile GenerateInvoiceDocumentReceiptXml(string authToken, string boxId, string messageId,
			string attachmentId, object signer)
		{
			return diadoc.GenerateInvoiceDocumentReceiptXml(authToken, boxId, messageId, attachmentId, (Signer) signer);
		}

		public GeneratedFile GenerateInvoiceCorrectionRequestXml(string authToken, string boxId, string messageId,
			string attachmentId, object correctionInfo)
		{
			return diadoc.GenerateInvoiceCorrectionRequestXml(authToken, boxId, messageId, attachmentId,
				(InvoiceCorrectionRequestInfo) correctionInfo);
		}

		public GeneratedFile GenerateRevocationRequestXml(string authToken, string boxId, string messageId,
			string attachmentId, object revocationRequestInfo)
		{
			return diadoc.GenerateRevocationRequestXml(authToken, boxId, messageId, attachmentId,
				(RevocationRequestInfo) revocationRequestInfo);
		}

		public GeneratedFile GenerateSignatureRejectionXml(string authToken, string boxId, string messageId,
			string attachmentId, object signatureRejectionInfo)
		{
			return diadoc.GenerateSignatureRejectionXml(authToken, boxId, messageId, attachmentId,
				(SignatureRejectionInfo) signatureRejectionInfo);
		}

		public RevocationRequestInfo ParseRevocationRequestXml(byte[] revocationRequestXmlContent)
		{
			return diadoc.ParseRevocationRequestXml(revocationRequestXmlContent);
		}

		public RevocationRequestInfo ParseRevocationRequestXmlFromFile(string fileName)
		{
			return ParseRevocationRequestXml(File.ReadAllBytes(fileName));
		}

		public SignatureRejectionInfo ParseSignatureRejectionXml(byte[] signatureRejectionXmlContent)
		{
			return diadoc.ParseSignatureRejectionXml(signatureRejectionXmlContent);
		}

		public SignatureRejectionInfo ParseSignatureRejectionXmlFromFile(string fileName)
		{
			return ParseSignatureRejectionXml(File.ReadAllBytes(fileName));
		}

		public GeneratedFile GenerateInvoiceXml(string authToken, object invoiceInfo, bool disableValidation = false)
		{
			return diadoc.GenerateInvoiceXml(authToken, (InvoiceInfo) invoiceInfo, disableValidation);
		}

		public GeneratedFile GenerateInvoiceRevisionXml(string authToken, object invoiceRevisionInfo,
			bool disableValidation = false)
		{
			return diadoc.GenerateInvoiceRevisionXml(authToken, (InvoiceInfo) invoiceRevisionInfo, disableValidation);
		}

		public GeneratedFile GenerateInvoiceCorrectionXml(string authToken, object invoiceCorrectionInfo,
			bool disableValidation = false)
		{
			return diadoc.GenerateInvoiceCorrectionXml(authToken, (InvoiceCorrectionInfo) invoiceCorrectionInfo,
				disableValidation);
		}

		public GeneratedFile GenerateInvoiceCorrectionRevisionXml(string authToken, object invoiceCorrectionRevisionInfo,
			bool disableValidation = false)
		{
			return diadoc.GenerateInvoiceCorrectionRevisionXml(authToken, (InvoiceCorrectionInfo) invoiceCorrectionRevisionInfo,
				disableValidation);
		}

		public GeneratedFile GenerateTorg12XmlForSeller(string authToken, object sellerInfo, bool disableValidation = false)
		{
			return diadoc.GenerateTorg12XmlForSeller(authToken, (Torg12SellerTitleInfo) sellerInfo, disableValidation);
		}

		public GeneratedFile GenerateTorg12XmlForBuyer(string authToken, object buyerInfo, string boxId,
			string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			return diadoc.GenerateTorg12XmlForBuyer(authToken, (Torg12BuyerTitleInfo) buyerInfo, boxId, sellerTitleMessageId,
				sellerTitleAttachmentId);
		}

		public GeneratedFile GenerateAcceptanceCertificateXmlForSeller(string authToken, object sellerInfo,
			bool disableValidation = false)
		{
			return diadoc.GenerateAcceptanceCertificateXmlForSeller(authToken, (AcceptanceCertificateSellerTitleInfo) sellerInfo,
				disableValidation);
		}

		public GeneratedFile GenerateAcceptanceCertificateXmlForBuyer(string authToken, object buyerInfo, string boxId,
			string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			return diadoc.GenerateAcceptanceCertificateXmlForBuyer(authToken, (AcceptanceCertificateBuyerTitleInfo) buyerInfo,
				boxId, sellerTitleMessageId, sellerTitleAttachmentId);
		}

		public InvoiceCorrectionRequestInfo GetInvoiceCorrectionRequestInfo(string authToken, string boxId, string messageId,
			string entityId)
		{
			return diadoc.GetInvoiceCorrectionRequestInfo(authToken, boxId, messageId, entityId);
		}

		public Message GetMessage(string authToken, string boxId, string messageId)
		{
			return diadoc.GetMessage(authToken, boxId, messageId);
		}

		public Message GetMessage(string authToken, string boxId, string messageId, string entityId)
		{
			return diadoc.GetMessage(authToken, boxId, messageId, entityId);
		}

		public void RecycleDraft(string authToken, string boxId, string draftId)
		{
			diadoc.RecycleDraft(authToken, boxId, draftId);
		}

		public Message SendDraft(string authToken, object draftToSend)
		{
			return diadoc.SendDraft(authToken, (DraftToSend) draftToSend);
		}

		public void Delete(string authToken, string boxId, string messageId, string documentId)
		{
			diadoc.Delete(authToken, boxId, messageId, documentId);
		}

		public void Restore(string authToken, string boxId, string messageId, string documentId)
		{
			diadoc.Restore(authToken, boxId, messageId, documentId);
		}

		public void MoveDocuments(string authToken, object query)
		{
			diadoc.MoveDocuments(authToken, (DocumentsMoveOperation) query);
		}

		public string NewGuid()
		{
			return Guid.NewGuid().ToString();
		}

		public string Recognize(string fileName, string filePath)
		{
			return diadoc.Recognize(fileName, File.ReadAllBytes(filePath));
		}

		public Recognized GetRecognized(string recognitionId)
		{
			return diadoc.GetRecognized(recognitionId);
		}

		public PrintFormResult GeneratePrintForm(string authToken, string boxId, string messageId, string documentId)
		{
			return diadoc.GeneratePrintForm(authToken, boxId, messageId, documentId);
		}

		public PrintFormResult GetGeneratedPrintForm(string authToken, int documentType, string printFormId)
		{
			return diadoc.GetGeneratedPrintForm(authToken, (DocumentType) documentType, printFormId);
		}

		public string GeneratePrintFormFromAttachment(string authToken, int documentType, byte[] content)
		{
			return diadoc.GeneratePrintFormFromAttachment(authToken, (DocumentType) documentType, content);
		}

		public DateTime NullDateTime()
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		}

		public DateTime? GetNullable(DateTime dateTime)
		{
			return dateTime == NullDateTime() ? (DateTime?) null : dateTime;
		}

		public DocumentList GetDocuments(string authToken, object filter)
		{
			return diadoc.GetDocuments(authToken, (DocumentsFilter) filter);
		}

		public DocumentList GetDocuments(string authToken, string boxId, string filterCategory, string counteragentBoxId,
			DateTime timestampFrom, DateTime timestampTo, string fromDocumentDate, string toDocumentDate, string departmentId,
			bool excludeSubdepartments, string afterIndexKey)
		{
			return diadoc.GetDocuments(authToken, boxId, filterCategory, counteragentBoxId, GetNullable(timestampFrom),
				GetNullable(timestampTo), fromDocumentDate, toDocumentDate, departmentId, excludeSubdepartments, afterIndexKey);
		}

		public Document GetDocument(string authToken, string boxId, string messageId, string entityId)
		{
			return diadoc.GetDocument(authToken, boxId, messageId, entityId);
		}

		public GetDocflowBatchResponse GetDocflows(string authToken, string boxId, object request)
		{
			return diadoc.GetDocflows(authToken, boxId, (GetDocflowBatchRequest) request);
		}

		public GetDocflowEventsResponse GetDocflowEvents(string authToken, string boxId, object request)
		{
			return diadoc.GetDocflowEvents(authToken, boxId, (GetDocflowEventsRequest) request);
		}

		public SearchDocflowsResponse SearchDocflows(string authToken, string boxId, object request)
		{
			return diadoc.SearchDocflows(authToken, boxId, (SearchDocflowsRequest) request);
		}

		public GetDocflowsByPacketIdResponse GetDocflowsByPacketId(string authToken, string boxId, object request)
		{
			return diadoc.GetDocflowsByPacketId(authToken, boxId, (GetDocflowsByPacketIdRequest) request);
		}

		public ForwardDocumentResponse ForwardDocument(string authToken, string boxId, object request)
		{
			return diadoc.ForwardDocument(authToken, boxId, (ForwardDocumentRequest) request);
		}

		public GetForwardedDocumentsResponse GetForwardedDocuments(string authToken, string boxId, object request)
		{
			return diadoc.GetForwardedDocuments(authToken, boxId, (GetForwardedDocumentsRequest) request);
		}

		public GetForwardedDocumentEventsResponse GetForwardedDocumentEvents(string authToken, string boxId,
			object request)
		{
			return diadoc.GetForwardedDocumentEvents(authToken, boxId, (GetForwardedDocumentEventsRequest) request);
		}

		public byte[] GetForwardedEntityContent(string authToken, string boxId, object forwardedDocumentId,
			string entityId)
		{
			return diadoc.GetForwardedEntityContent(authToken, boxId, (ForwardedDocumentId) forwardedDocumentId, entityId);
		}

		public IDocumentProtocolResult GenerateForwardedDocumentProtocol(string authToken, string boxId,
			object forwardedDocumentId)
		{
			return diadoc.GenerateForwardedDocumentProtocol(authToken, boxId, (ForwardedDocumentId) forwardedDocumentId);
		}

		public bool CanSendInvoice(string authToken, string boxId, byte[] certificateBytes)
		{
			return diadoc.CanSendInvoice(authToken, boxId, certificateBytes);
		}

		public IDocumentZipGenerationResult GenerateDocumentZip(string authToken, string boxId, string messageId,
			string documentId,
			bool fullDocflow)
		{
			return diadoc.GenerateDocumentZip(authToken, boxId, messageId, documentId, fullDocflow);
		}

		public PrepareDocumentsToSignResponse PrepareDocumentsToSign(string authToken, object request)
		{
			return diadoc.PrepareDocumentsToSign(authToken, (PrepareDocumentsToSignRequest) request);
		}

		public User GetMyUser(string authToken)
		{
			return diadoc.GetMyUser(authToken);
		}

		public DocumentList GetDocumentsByMessageId(string authToken, string boxId, string messageId)
		{
			return diadoc.GetDocumentsByMessageId(authToken, boxId, messageId);
		}

		#region Counteragents

		public Counteragent GetCounteragent(string authToken, string myOrgId, string counteragentOrgId)
		{
			return diadoc.GetCounteragent(authToken, myOrgId, counteragentOrgId);
		}

		public CounteragentCertificateList GetCounteragentCertificates(string authToken, string myOrgId,
			string counteragentOrgId)
		{
			return diadoc.GetCounteragentCertificates(authToken, myOrgId, counteragentOrgId);
		}

		public CounteragentList GetCounteragents(string authToken, string myOrgId, string counteragentStatus,
			string afterIndexKey)
		{
			return diadoc.GetCounteragents(authToken, myOrgId, counteragentStatus, afterIndexKey);
		}

		public void AcquireCounteragent(string authToken, string myOrgId, string counteragentOrgId, string comment,
			string myDepartmentId = null)
		{
			diadoc.AcquireCounteragent(authToken, myOrgId, new AcquireCounteragentRequest
			{
				OrgId = counteragentOrgId,
				MessageToCounteragent = comment
			}, myDepartmentId);
		}

		public AsyncMethodResult AcquireCounteragent2(string authToken, string myOrgId,
			[MarshalAs(UnmanagedType.IDispatch)] object acquireCounteragentRequeststring, string myDepartmentId = null)
		{
			return diadoc.AcquireCounteragent(authToken, myOrgId,
				(AcquireCounteragentRequest) acquireCounteragentRequeststring, myDepartmentId);
		}

		public AcquireCounteragentResult WaitAcquireCounteragentResult(string authToken, string taskId)
		{
			return diadoc.WaitAcquireCounteragentResult(authToken, taskId);
		}

		public void BreakWithCounteragent(string authToken, string myOrgId, string counteragentOrgId, string comment)
		{
			diadoc.BreakWithCounteragent(authToken, myOrgId, counteragentOrgId, comment);
		}

		#endregion

		#region Shelf

		public string UploadFileToShelf(string authToken, string fileName)
		{
			return diadoc.UploadFileToShelf(authToken, File.ReadAllBytes(fileName));
		}

		public void GetFileFromShelf(string authToken, string nameOnShelf, string fileName)
		{
			File.WriteAllBytes(fileName, diadoc.GetFileFromShelf(authToken, nameOnShelf));
		}

		#endregion

		#region Parse...

		public RussianAddress ParseRussianAddress(string address)
		{
			return diadoc.ParseRussianAddress(address);
		}

		public InvoiceInfo ParseInvoiceXml(byte[] invoiceXmlContent)
		{
			return diadoc.ParseInvoiceXml(invoiceXmlContent);
		}

		public InvoiceInfo ParseInvoiceXmlFromFile(string fileName)
		{
			return ParseInvoiceXml(File.ReadAllBytes(fileName));
		}

		public Torg12SellerTitleInfo ParseTorg12SellerTitleXml(byte[] torg12SellerTitleXmlContent)
		{
			return diadoc.ParseTorg12SellerTitleXml(torg12SellerTitleXmlContent);
		}

		public Torg12SellerTitleInfo ParseTorg12SellerTitleXmlFromFile(string fileName)
		{
			return ParseTorg12SellerTitleXml(File.ReadAllBytes(fileName));
		}

		public AcceptanceCertificateSellerTitleInfo ParseAcceptanceCertificateSellerTitleXml(byte[] xmlContent)
		{
			return diadoc.ParseAcceptanceCertificateSellerTitleXml(xmlContent);
		}

		public AcceptanceCertificateSellerTitleInfo ParseAcceptanceCertificateSellerTitleXmlFromFile(string fileName)
		{
			return ParseAcceptanceCertificateSellerTitleXml(File.ReadAllBytes(fileName));
		}

		#endregion
	}
}