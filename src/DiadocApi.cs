using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using Diadoc.Api.Cryptography;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Docflow;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Documents.Types;
using Diadoc.Api.Proto.Employees.Subscriptions;
using Diadoc.Api.Proto.Employees;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Forwarding;
using Diadoc.Api.Proto.Invoicing;
using Diadoc.Api.Proto.Invoicing.Signers;
using Diadoc.Api.Proto.KeyValueStorage;
using Diadoc.Api.Proto.Recognition;
using Diadoc.Api.Proto.Users;
using JetBrains.Annotations;
using DocumentType = Diadoc.Api.Proto.DocumentType;
using Employee = Diadoc.Api.Proto.Employees.Employee;

namespace Diadoc.Api
{
	public partial class DiadocApi : IDiadocApi
	{
		private readonly DiadocHttpApi diadocHttpApi;

		public DiadocApi(string apiClientId, string diadocApiBaseUrl, ICrypt crypt)
			: this(new DiadocHttpApi(apiClientId, new HttpClient(diadocApiBaseUrl), crypt))
		{
		}

		public DiadocApi([NotNull] DiadocHttpApi diadocHttpApi)
		{
			if (diadocHttpApi == null) throw new ArgumentNullException("diadocHttpApi");
			this.diadocHttpApi = diadocHttpApi;
			Docflow = new DocflowApi(diadocHttpApi.Docflow);
		}

		/// <summary>
		///   The default value is true
		/// </summary>
		public bool UsingSystemProxy
		{
			get { return diadocHttpApi.HttpClient.UseSystemProxy; }
		}

		public IDocflowApi Docflow { get; }

		public void SetProxyUri([CanBeNull] string uri)
		{
			diadocHttpApi.HttpClient.SetProxyUri(uri);
		}

		public void EnableSystemProxyUsage()
		{
			diadocHttpApi.HttpClient.UseSystemProxy = true;
		}

		public void DisableSystemProxyUsage()
		{
			diadocHttpApi.HttpClient.UseSystemProxy = false;
		}

		public void SetProxyCredentials([CanBeNull] NetworkCredential proxyCredentials)
		{
			diadocHttpApi.HttpClient.SetProxyCredentials(proxyCredentials);
		}

		public void SetProxyCredentials([NotNull] string user, [NotNull] string password)
		{
			diadocHttpApi.HttpClient.SetProxyCredentials(user, password);
		}

		public void SetProxyCredentials([NotNull] string user, [NotNull] SecureString password)
		{
			diadocHttpApi.HttpClient.SetProxyCredentials(user, password);
		}

		public string Authenticate(string login, string password, string key = null, string id = null)
		{
			return diadocHttpApi.Authenticate(login, password, key, id);
		}

		public string AuthenticateByKey([NotNull] string key, [NotNull] string id)
		{
			if (key == null) throw new ArgumentNullException("key");
			if (id == null) throw new ArgumentNullException("id");
			return diadocHttpApi.AuthenticateByKey(key, id);
		}

		public string AuthenticateBySid([NotNull] string sid)
		{
			if (sid == null) throw new ArgumentNullException("sid");
			return diadocHttpApi.AuthenticateBySid(sid);
		}

		public string Authenticate(byte[] certificateBytes, bool useLocalSystemStorage = false)
		{
			if (certificateBytes == null) throw new ArgumentNullException("certificateBytes");
			return diadocHttpApi.Authenticate(certificateBytes, useLocalSystemStorage);
		}

		public string Authenticate(string thumbprint, bool useLocalSystemStorage = false)
		{
			if (thumbprint == null) throw new ArgumentNullException("thumbprint");
			return diadocHttpApi.Authenticate(thumbprint, useLocalSystemStorage);
		}

		public string AuthenticateWithKey(string thumbprint, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true)
		{
			if (thumbprint == null) throw new ArgumentNullException("thumbprint");
			return diadocHttpApi.AuthenticateWithKey(thumbprint, useLocalSystemStorage, key, id, autoConfirm);
		}

		public string AuthenticateWithKey(byte[] certificateBytes, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true)
		{
			if (certificateBytes == null) throw new ArgumentNullException("certificateBytes");
			return diadocHttpApi.AuthenticateWithKey(certificateBytes, useLocalSystemStorage, key, id, autoConfirm);
		}

		public string AuthenticateWithKeyConfirm(byte[] certificateBytes, string token, bool saveBinding = false)
		{
			if (certificateBytes == null) throw new ArgumentNullException("certificateBytes");
			if (string.IsNullOrEmpty(token)) throw new ArgumentNullException("token");
			return diadocHttpApi.AuthenticateWithKeyConfirm(certificateBytes, token, saveBinding);
		}

		public string AuthenticateWithKeyConfirm(string thumbprint, string token, bool saveBinding = false)
		{
			if (thumbprint == null) throw new ArgumentNullException("thumbprint");
			if (string.IsNullOrEmpty(token)) throw new ArgumentNullException("token");
			return diadocHttpApi.AuthenticateWithKeyConfirm(thumbprint, token, saveBinding);
		}

		public OrganizationUserPermissions GetMyPermissions(string authToken, string orgId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (orgId == null) throw new ArgumentNullException("orgId");
			return diadocHttpApi.GetMyPermissions(authToken, orgId);
		}

		public OrganizationList GetMyOrganizations(string authToken, bool autoRegister = true)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			return diadocHttpApi.GetMyOrganizations(authToken, autoRegister);
		}

		public User GetMyUser(string authToken)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			return diadocHttpApi.GetMyUser(authToken);
		}

		public UserV2 GetMyUserV2(string authToken)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			return diadocHttpApi.GetMyUserV2(authToken);
		}

		public UserV2 UpdateMyUser(string authToken, UserToUpdate userToUpdate)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (userToUpdate == null) throw new ArgumentNullException("userToUpdate");
			return diadocHttpApi.UpdateMyUser(authToken, userToUpdate);
		}

		public OrganizationList GetOrganizationsByInnKpp(string inn, string kpp, bool includeRelations = false)
		{
			if (inn == null) throw new ArgumentNullException("inn");
			return diadocHttpApi.GetOrganizationsByInnKpp(inn, kpp, includeRelations);
		}

		public Organization GetOrganizationById(string orgId)
		{
			if (orgId == null) throw new ArgumentNullException("orgId");
			return diadocHttpApi.GetOrganizationById(orgId);
		}

		public Organization GetOrganizationByBoxId(string boxId)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetOrganizationByBoxId(boxId);
		}

		public Organization GetOrganizationByFnsParticipantId(string fnsParticipantId)
		{
			if (fnsParticipantId == null) throw new ArgumentException("fnsParticipantId");
			return diadocHttpApi.GetOrganizationByFnsParticipantId(fnsParticipantId);
		}

		public Organization GetOrganizationByInnKpp(string inn, string kpp)
		{
			if (inn == null) throw new ArgumentException("inn");
			return diadocHttpApi.GetOrganizationByInnKpp(inn, kpp);
		}

		public Box GetBox(string boxId)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetBox(boxId);
		}

		public Department GetDepartment(string authToken, string orgId, string departmentId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (orgId == null) throw new ArgumentNullException("orgId");
			if (departmentId == null) throw new ArgumentNullException("departmentId");
			return diadocHttpApi.GetDepartment(authToken, orgId, departmentId);
		}

		public void UpdateOrganizationProperties(string authToken, OrganizationPropertiesToUpdate orgProps)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (orgProps == null) throw new ArgumentNullException("orgProps");
			diadocHttpApi.UpdateOrganizationProperties(authToken, orgProps);
		}

		public BoxEventList GetNewEvents(string authToken, string boxId, string afterEventId = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetNewEvents(authToken, boxId, afterEventId);
		}

		public BoxEvent GetEvent(string authToken, string boxId, string eventId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (eventId == null) throw new ArgumentNullException("eventId");
			return diadocHttpApi.GetEvent(authToken, boxId, eventId);
		}

		public Message PostMessage(string authToken, MessageToPost msg, string operationId = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (msg == null) throw new ArgumentNullException("msg");
			return diadocHttpApi.PostMessage(authToken, msg, operationId);
		}

		public Template PostTemplate(string authToken, TemplateToPost template, string operationId = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (template == null) throw new ArgumentNullException("template");
			return diadocHttpApi.PostTemplate(authToken, template, operationId);
		}

		public Message TransformTemplateToMessage(string authToken, TemplateTransformationToPost templateTransformation, string operationId = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (templateTransformation == null) throw new ArgumentNullException("templateTransformation");
			return diadocHttpApi.TransformTemplateToMessage(authToken, templateTransformation, operationId);
		}

		public MessagePatch PostMessagePatch(string authToken, MessagePatchToPost patch, string operationId = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (patch == null) throw new ArgumentNullException("patch");
			return diadocHttpApi.PostMessagePatch(authToken, patch, operationId);
		}

		public void PostRoamingNotification(string authToken, RoamingNotificationToPost notification)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (notification == null) throw new ArgumentNullException("notification");
			diadocHttpApi.PostRoamingNotification(authToken, notification);
		}

		public void Delete(string authToken, string boxId, string messageId, string documentId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			diadocHttpApi.Delete(authToken, boxId, messageId, documentId);
		}

		public void Restore(string authToken, string boxId, string messageId, string documentId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			diadocHttpApi.Restore(authToken, boxId, messageId, documentId);
		}

		public void MoveDocuments(string authToken, DocumentsMoveOperation query)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (query == null) throw new ArgumentNullException("query");
			diadocHttpApi.MoveDocuments(authToken, query);
		}

		public byte[] GetEntityContent(string authToken, string boxId, string messageId, string entityId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (entityId == null) throw new ArgumentNullException("entityId");
			return diadocHttpApi.GetEntityContent(authToken, boxId, messageId, entityId);
		}

		public GeneratedFile GenerateDocumentReceiptXml(string authToken, string boxId, string messageId, string attachmentId, Signer signer)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			if (signer == null) throw new ArgumentNullException("signer");
			return diadocHttpApi.GenerateDocumentReceiptXml(authToken, boxId, messageId, attachmentId, signer);
		}

		public GeneratedFile GenerateInvoiceDocumentReceiptXml(string authToken, string boxId, string messageId, string attachmentId, Signer signer)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			if (signer == null) throw new ArgumentNullException("signer");
			return diadocHttpApi.GenerateInvoiceDocumentReceiptXml(authToken, boxId, messageId, attachmentId, signer);
		}

		public GeneratedFile GenerateInvoiceCorrectionRequestXml(string authToken, string boxId, string messageId,
			string attachmentId, InvoiceCorrectionRequestInfo correctionInfo)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			return diadocHttpApi.GenerateInvoiceCorrectionRequestXml(authToken, boxId, messageId, attachmentId, correctionInfo);
		}

		public GeneratedFile GenerateRevocationRequestXml(string authToken, string boxId, string messageId,
			string attachmentId, RevocationRequestInfo revocationRequestInfo)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			return diadocHttpApi.GenerateRevocationRequestXml(authToken, boxId, messageId, attachmentId, revocationRequestInfo);
		}

		public GeneratedFile GenerateSignatureRejectionXml(string authToken, string boxId, string messageId,
			string attachmentId, SignatureRejectionInfo signatureRejectionInfo)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			return diadocHttpApi.GenerateSignatureRejectionXml(authToken, boxId, messageId, attachmentId, signatureRejectionInfo);
		}

		public InvoiceCorrectionRequestInfo GetInvoiceCorrectionRequestInfo(string authToken, string boxId, string messageId,
			string entityId)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (entityId == null) throw new ArgumentNullException("entityId");
			return diadocHttpApi.GetInvoiceCorrectionRequestInfo(authToken, boxId, messageId, entityId);
		}

		public GeneratedFile GenerateInvoiceXml(string authToken, InvoiceInfo invoiceInfo, bool disableValidation = false)
		{
			if (invoiceInfo == null) throw new ArgumentNullException("invoiceInfo");
			return diadocHttpApi.GenerateInvoiceXml(authToken, invoiceInfo, disableValidation);
		}

		public GeneratedFile GenerateInvoiceRevisionXml(string authToken, InvoiceInfo invoiceRevisionInfo,
			bool disableValidation = false)
		{
			if (invoiceRevisionInfo == null) throw new ArgumentNullException("invoiceRevisionInfo");
			return diadocHttpApi.GenerateInvoiceRevisionXml(authToken, invoiceRevisionInfo, disableValidation);
		}

		public GeneratedFile GenerateInvoiceCorrectionXml(string authToken, InvoiceCorrectionInfo invoiceCorrectionInfo,
			bool disableValidation = false)
		{
			if (invoiceCorrectionInfo == null) throw new ArgumentNullException("invoiceCorrectionInfo");
			return diadocHttpApi.GenerateInvoiceCorrectionXml(authToken, invoiceCorrectionInfo, disableValidation);
		}

		public GeneratedFile GenerateInvoiceCorrectionRevisionXml(string authToken,
			InvoiceCorrectionInfo invoiceCorrectionRevision, bool disableValidation = false)
		{
			if (invoiceCorrectionRevision == null) throw new ArgumentNullException("invoiceCorrectionRevision");
			return diadocHttpApi.GenerateInvoiceCorrectionRevisionXml(authToken, invoiceCorrectionRevision, disableValidation);
		}

		public GeneratedFile GenerateTorg12XmlForSeller(string authToken, Torg12SellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateTorg12XmlForSeller(authToken, sellerInfo, disableValidation);
		}

		public GeneratedFile GenerateTovTorg551XmlForSeller(string authToken, TovTorgSellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateTovTorg551XmlForSeller(authToken, sellerInfo, disableValidation);
		}

		public GeneratedFile GenerateTorg12XmlForBuyer(string authToken, Torg12BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			if (buyerInfo == null) throw new ArgumentNullException("buyerInfo");
			return diadocHttpApi.GenerateTorg12XmlForBuyer(authToken, buyerInfo, boxId, sellerTitleMessageId, sellerTitleAttachmentId);
		}

		public GeneratedFile GenerateTovTorg551XmlForBuyer(string authToken, TovTorgBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, string documentVersion = null)
		{
			if (buyerInfo == null) throw new ArgumentNullException("buyerInfo");
			return diadocHttpApi.GenerateTovTorg551XmlForBuyer(authToken, buyerInfo, boxId, sellerTitleMessageId, sellerTitleAttachmentId, documentVersion);
		}

		public GeneratedFile GenerateAcceptanceCertificateXmlForSeller(string authToken,
			AcceptanceCertificateSellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateAcceptanceCertificateXmlForSeller(authToken, sellerInfo, disableValidation);
		}

		public GeneratedFile GenerateAcceptanceCertificateXmlForBuyer(string authToken,
			AcceptanceCertificateBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId,
			string sellerTitleAttachmentId)
		{
			if (buyerInfo == null) throw new ArgumentNullException("buyerInfo");
			return diadocHttpApi.GenerateAcceptanceCertificateXmlForBuyer(authToken, buyerInfo, boxId, sellerTitleMessageId,
				sellerTitleAttachmentId);
		}

		public GeneratedFile GenerateAcceptanceCertificate552XmlForSeller(string authToken,
			AcceptanceCertificate552SellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateAcceptanceCertificate552XmlForSeller(authToken, sellerInfo, disableValidation);
		}

		public GeneratedFile GenerateAcceptanceCertificate552XmlForBuyer(string authToken,
			AcceptanceCertificate552BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId,
			string sellerTitleAttachmentId)
		{
			if (buyerInfo == null) throw new ArgumentNullException("buyerInfo");
			return diadocHttpApi.GenerateAcceptanceCertificate552XmlForBuyer(authToken, buyerInfo, boxId, sellerTitleMessageId,
				sellerTitleAttachmentId);
		}

		public GeneratedFile GenerateUniversalTransferDocumentXmlForSeller(string authToken,
			UniversalTransferDocumentSellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateUniversalTransferDocumentXmlForSeller(authToken, sellerInfo, disableValidation);
		}

		public GeneratedFile GenerateUniversalCorrectionDocumentXmlForSeller(string authToken,
			UniversalCorrectionDocumentSellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateUniversalCorrectionDocumentXmlForSeller(authToken, sellerInfo, disableValidation);
		}

		public GeneratedFile GenerateUniversalTransferDocumentXmlForBuyer(string authToken, UniversalTransferDocumentBuyerTitleInfo buyerInfo,
			string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			if (buyerInfo == null) throw new ArgumentNullException("buyerInfo");
			return diadocHttpApi.GenerateUniversalTransferDocumentXmlForBuyer(authToken, buyerInfo, boxId, sellerTitleMessageId, sellerTitleAttachmentId);
		}

		public GeneratedFile GenerateSenderTitleXml(string authToken, string boxId, string documentTypeNamedId, string documentFunction, string documentVersion, byte[] userContractData, bool disableValidation = false, string editingSettingId = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (documentTypeNamedId == null) throw new ArgumentNullException("documentTypeNamedId");
			if (documentFunction == null) throw new ArgumentNullException("documentFunction");
			if (documentVersion == null) throw new ArgumentNullException("documentVersion");
			if (userContractData == null) throw new ArgumentNullException("userContractData");
			return diadocHttpApi.GenerateSenderTitleXml(authToken, boxId, documentTypeNamedId, documentFunction, documentVersion, userContractData, disableValidation, editingSettingId);
		}

		public GeneratedFile GenerateRecipientTitleXml(string authToken, string boxId, string senderTitleMessageId, string senderTitleAttachmentId, byte[] userContractData, string documentVersion = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (senderTitleMessageId == null) throw new ArgumentNullException("senderTitleMessageId");
			if (senderTitleAttachmentId == null) throw new ArgumentNullException("senderTitleAttachmentId");
			if (userContractData == null) throw new ArgumentNullException("userContractData");
			return diadocHttpApi.GenerateRecipientTitleXml(authToken, boxId, senderTitleMessageId, senderTitleAttachmentId, userContractData);
		}

		public Message GetMessage(string authToken, string boxId, string messageId, bool withOriginalSignature = false, bool injectEntityContent = false)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			return diadocHttpApi.GetMessage(authToken, boxId, messageId, withOriginalSignature, injectEntityContent);
		}

		public Message GetMessage(string authToken, string boxId, string messageId, string entityId, bool withOriginalSignature = false, bool injectEntityContent = false)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (entityId == null) throw new ArgumentNullException("entityId");
			return diadocHttpApi.GetMessage(authToken, boxId, messageId, entityId, withOriginalSignature, injectEntityContent);
		}

		public Template GetTemplate(string authToken, string boxId, string templateId, string entityId = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (templateId == null) throw new ArgumentNullException("templateId");
			return diadocHttpApi.GetTemplate(authToken, boxId, templateId, entityId);
		}

		public void RecycleDraft(string authToken, string boxId, string draftId)
		{
			diadocHttpApi.RecycleDraft(authToken, boxId, draftId);
		}

		public Message SendDraft(string authToken, DraftToSend draftToSend, string operationId = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (draftToSend == null) throw new ArgumentNullException("draftToSend");
			return diadocHttpApi.SendDraft(authToken, draftToSend, operationId);
		}

		public PrintFormResult GeneratePrintForm(string authToken, string boxId, string messageId, string documentId)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			if (string.IsNullOrEmpty(messageId)) throw new ArgumentNullException("messageId");
			if (string.IsNullOrEmpty(documentId)) throw new ArgumentNullException("documentId");
			return diadocHttpApi.GeneratePrintForm(authToken, boxId, messageId, documentId);
		}

		public string GeneratePrintFormFromAttachment(string authToken, DocumentType documentType, byte[] content,
			string fromBoxId = null)
		{
			return diadocHttpApi.GeneratePrintFormFromAttachment(authToken, documentType, content, fromBoxId);
		}

		[Obsolete("Use GetGeneratedPrintForm without `documentType` parameter")]
		public PrintFormResult GetGeneratedPrintForm(string authToken, DocumentType documentType, string printFormId)
		{
			if (string.IsNullOrEmpty(printFormId)) throw new ArgumentNullException("printFormId");
			return diadocHttpApi.GetGeneratedPrintForm(authToken, documentType, printFormId);
		}

		public PrintFormResult GetGeneratedPrintForm(string authToken, string printFormId)
		{
			if (string.IsNullOrEmpty(printFormId)) throw new ArgumentNullException("printFormId");
			return diadocHttpApi.GetGeneratedPrintForm(authToken, printFormId);
		}

		public string Recognize(string fileName, byte[] content)
		{
			if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");
			return diadocHttpApi.Recognize(fileName, content);
		}

		public Recognized GetRecognized(string recognitionId)
		{
			if (string.IsNullOrEmpty(recognitionId)) throw new ArgumentNullException("recognitionId");
			return diadocHttpApi.GetRecognized(recognitionId);
		}

		public DocumentList GetDocuments(string authToken, string boxId, string filterCategory, string counteragentBoxId,
			DateTime? timestampFrom, DateTime? timestampTo, string fromDocumentDate, string toDocumentDate, string departmentId,
			bool excludeSubdepartments, string afterIndexKey, int? count = null)
		{
			return diadocHttpApi.GetDocuments(authToken, boxId, filterCategory, counteragentBoxId, timestampFrom, timestampTo,
				fromDocumentDate, toDocumentDate, departmentId, excludeSubdepartments, afterIndexKey, count);
		}

		public DocumentList GetDocuments(string authToken, DocumentsFilter filter)
		{
			return diadocHttpApi.GetDocuments(authToken, filter);
		}

		public Document GetDocument(string authToken, string boxId, string messageId, string entityId)
		{
			return diadocHttpApi.GetDocument(authToken, boxId, messageId, entityId);
		}

		public SignatureInfo GetSignatureInfo(string authToken, string boxId, string messageId, string entityId)
		{
			return diadocHttpApi.GetSignatureInfo(authToken, boxId, messageId, entityId);
		}

		public ExtendedSignerDetails GetExtendedSignerDetails(string token, string boxId, string thumbprint, DocumentTitleType documentTitleType)
		{
			return diadocHttpApi.GetExtendedSignerDetails(token, boxId, thumbprint, documentTitleType);
		}

		public ExtendedSignerDetails GetExtendedSignerDetails(string token, string boxId, byte[] certificateBytes, DocumentTitleType documentTitleType)
		{
			return diadocHttpApi.GetExtendedSignerDetails(token, boxId, certificateBytes, documentTitleType);
		}

		public ExtendedSignerDetails PostExtendedSignerDetails(string token, string boxId, string thumbprint, DocumentTitleType documentTitleType, ExtendedSignerDetailsToPost signerDetails)
		{
			return diadocHttpApi.PostExtendedSignerDetails(token, boxId, thumbprint, documentTitleType, signerDetails);
		}

		public ExtendedSignerDetails PostExtendedSignerDetails(string token, string boxId, byte[] certificateBytes, DocumentTitleType documentTitleType, ExtendedSignerDetailsToPost signerDetails)
		{
			return diadocHttpApi.PostExtendedSignerDetails(token, boxId, certificateBytes, documentTitleType, signerDetails);
		}

		public GetDocflowBatchResponse GetDocflows(string authToken, string boxId, GetDocflowBatchRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetDocflows(authToken, boxId, request);
		}

		public GetDocflowEventsResponse GetDocflowEvents(string authToken, string boxId, GetDocflowEventsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetDocflowEvents(authToken, boxId, request);
		}

		public SearchDocflowsResponse SearchDocflows(string authToken, string boxId, SearchDocflowsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.SearchDocflows(authToken, boxId, request);
		}

		public GetDocflowsByPacketIdResponse GetDocflowsByPacketId(string authToken, string boxId,
			GetDocflowsByPacketIdRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetDocflowsByPacketId(authToken, boxId, request);
		}

		public ForwardDocumentResponse ForwardDocument(string authToken, string boxId, ForwardDocumentRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.ForwardDocument(authToken, boxId, request);
		}

		public GetForwardedDocumentsResponse GetForwardedDocuments(string authToken, string boxId,
			GetForwardedDocumentsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetForwardedDocuments(authToken, boxId, request);
		}

		public GetForwardedDocumentEventsResponse GetForwardedDocumentEvents(string authToken, string boxId,
			GetForwardedDocumentEventsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetForwardedDocumentEvents(authToken, boxId, request);
		}

		public byte[] GetForwardedEntityContent(string authToken, string boxId, ForwardedDocumentId forwardedDocumentId,
			string entityId)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetForwardedEntityContent(authToken, boxId, forwardedDocumentId, entityId);
		}

		public DocumentProtocolResult GenerateForwardedDocumentProtocol(string authToken, string boxId,
			ForwardedDocumentId forwardedDocumentId)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GenerateForwardedDocumentProtocol(authToken, boxId, forwardedDocumentId);
		}

		public PrintFormResult GenerateForwardedDocumentPrintForm(string authToken, string boxId, ForwardedDocumentId forwardedDocumentId)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GenerateForwardedDocumentPrintForm(authToken, boxId, forwardedDocumentId);
		}

		public bool CanSendInvoice(string authToken, string boxId, byte[] certificateBytes)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			if (certificateBytes == null || certificateBytes.Length == 0) throw new ArgumentNullException("certificateBytes");
			return diadocHttpApi.CanSendInvoice(authToken, boxId, certificateBytes);
		}

		public void SendFnsRegistrationMessage(string authToken, string boxId,
			FnsRegistrationMessageInfo fnsRegistrationMessageInfo)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			if (!fnsRegistrationMessageInfo.Certificates.Any()) throw new ArgumentException("fnsRegistrationMessageInfo");
			diadocHttpApi.SendFnsRegistrationMessage(authToken, boxId, fnsRegistrationMessageInfo);
		}

		public Counteragent GetCounteragent(string authToken, string myOrgId, string counteragentOrgId)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId)) throw new ArgumentNullException("myOrgId");
			if (string.IsNullOrEmpty(counteragentOrgId)) throw new ArgumentNullException("counteragentOrgId");
			return diadocHttpApi.GetCounteragent(authToken, myOrgId, counteragentOrgId);
		}

		public CounteragentList GetCounteragents(string authToken, string myOrgId, string counteragentStatus, string afterIndexKey, string query = null, int? pageSize = null)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId)) throw new ArgumentNullException("myOrgId");
			return diadocHttpApi.GetCounteragents(authToken, myOrgId, counteragentStatus, afterIndexKey, query, pageSize);
		}

		public CounteragentCertificateList GetCounteragentCertificates(string authToken, string myOrgId,
			string counteragentOrgId)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId)) throw new ArgumentNullException("myOrgId");
			if (string.IsNullOrEmpty(counteragentOrgId)) throw new ArgumentNullException("counteragentOrgId");
			return diadocHttpApi.GetCounteragentCertificates(authToken, myOrgId, counteragentOrgId);
		}

		public void BreakWithCounteragent(string authToken, string myOrgId, string counteragentOrgId, string comment)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId)) throw new ArgumentNullException("myOrgId");
			if (string.IsNullOrEmpty(counteragentOrgId)) throw new ArgumentNullException("counteragentOrgId");
			diadocHttpApi.BreakWithCounteragent(authToken, myOrgId, counteragentOrgId, comment);
		}

		public string UploadFileToShelf(string authToken, byte[] data)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (data == null) throw new ArgumentNullException("data");
			return diadocHttpApi.UploadFileToShelf(authToken, data);
		}

		public byte[] GetFileFromShelf(string authToken, string nameOnShelf)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(nameOnShelf)) throw new ArgumentNullException("nameOnShelf");
			return diadocHttpApi.GetFileFromShelf(authToken, nameOnShelf);
		}

		public RussianAddress ParseRussianAddress(string address)
		{
			return diadocHttpApi.ParseRussianAddress(address);
		}

		public InvoiceInfo ParseInvoiceXml(byte[] invoiceXmlContent)
		{
			return diadocHttpApi.ParseInvoiceXml(invoiceXmlContent);
		}

		public Torg12SellerTitleInfo ParseTorg12SellerTitleXml(byte[] xmlContent)
		{
			return diadocHttpApi.ParseTorg12SellerTitleXml(xmlContent);
		}

		public Torg12BuyerTitleInfo ParseTorg12BuyerTitleXml(byte[] xmlContent)
		{
			return diadocHttpApi.ParseTorg12BuyerTitleXml(xmlContent);
		}

		public TovTorgSellerTitleInfo ParseTovTorg551SellerTitleXml(byte[] xmlContent)
		{
			return diadocHttpApi.ParseTovTorg551SellerTitleXml(xmlContent);
		}

		public TovTorgBuyerTitleInfo ParseTovTorg551BuyerTitleXml(byte[] xmlContent)
		{
			return diadocHttpApi.ParseTovTorg551BuyerTitleXml(xmlContent);
		}

		public AcceptanceCertificateSellerTitleInfo ParseAcceptanceCertificateSellerTitleXml(byte[] xmlContent)
		{
			return diadocHttpApi.ParseAcceptanceCertificateSellerTitleXml(xmlContent);
		}

		public AcceptanceCertificateBuyerTitleInfo ParseAcceptanceCertificateBuyerTitleXml(byte[] xmlContent)
		{
			return diadocHttpApi.ParseAcceptanceCertificateBuyerTitleXml(xmlContent);
		}

		public AcceptanceCertificate552SellerTitleInfo ParseAcceptanceCertificate552SellerTitleXml(byte[] xmlContent)
		{
			return diadocHttpApi.ParseAcceptanceCertificate552SellerTitleXml(xmlContent);
		}

		public AcceptanceCertificate552BuyerTitleInfo ParseAcceptanceCertificate552BuyerTitleXml(byte[] xmlContent)
		{
			return diadocHttpApi.ParseAcceptanceCertificate552BuyerTitleXml(xmlContent);
		}

		public UniversalTransferDocumentSellerTitleInfo ParseUniversalTransferDocumentSellerTitleXml(byte[] xmlContent)
		{
			return diadocHttpApi.ParseUniversalTransferDocumentSellerTitleXml(xmlContent);
		}

		public UniversalTransferDocumentBuyerTitleInfo ParseUniversalTransferDocumentBuyerTitleXml(byte[] xmlContent)
		{
			return diadocHttpApi.ParseUniversalTransferDocumentBuyerTitleXml(xmlContent);
		}

		public UniversalCorrectionDocumentSellerTitleInfo ParseUniversalCorrectionDocumentSellerTitleXml(byte[] xmlContent)
		{
			return diadocHttpApi.ParseUniversalCorrectionDocumentSellerTitleXml(xmlContent);
		}

		public UniversalTransferDocumentBuyerTitleInfo ParseUniversalCorrectionDocumentBuyerTitleXml(byte[] xmlContent)
		{
			return diadocHttpApi.ParseUniversalCorrectionDocumentBuyerTitleXml(xmlContent);
		}

		public OrganizationUsersList GetOrganizationUsers(string authToken, string orgId)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(orgId)) throw new ArgumentNullException("orgId");
			return diadocHttpApi.GetOrganizationUsers(authToken, orgId);
		}

		public List<Organization> GetOrganizationsByInnList(GetOrganizationsByInnListRequest innList)
		{
			if (innList == null)
				throw new ArgumentNullException("innList");
			return diadocHttpApi.GetOrganizationsByInnList(innList);
		}

		public List<OrganizationWithCounteragentStatus> GetOrganizationsByInnList(string authToken, string myOrgId,
			GetOrganizationsByInnListRequest innList)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId))
				throw new ArgumentNullException("myOrgId");
			if (innList == null)
				throw new ArgumentNullException("innList");
			return diadocHttpApi.GetOrganizationsByInnList(authToken, myOrgId, innList);
		}

		public RevocationRequestInfo ParseRevocationRequestXml(byte[] revocationRequestXmlContent)
		{
			return diadocHttpApi.ParseRevocationRequestXml(revocationRequestXmlContent);
		}

		public SignatureRejectionInfo ParseSignatureRejectionXml(byte[] signatureRejectionXmlContent)
		{
			return diadocHttpApi.ParseSignatureRejectionXml(signatureRejectionXmlContent);
		}

		public DocumentProtocolResult GenerateDocumentProtocol(string authToken, string boxId, string messageId,
			string documentId)
		{
			return diadocHttpApi.GenerateDocumentProtocol(authToken, boxId, messageId, documentId);
		}

		public DocumentZipGenerationResult GenerateDocumentZip(string authToken, string boxId, string messageId,
			string documentId, bool fullDocflow)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (documentId == null) throw new ArgumentNullException("documentId");
			return diadocHttpApi.GenerateDocumentZip(authToken, boxId, messageId, documentId, fullDocflow);
		}

		public DocumentList GetDocumentsByCustomId(string authToken, string boxId, string customDocumentId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (customDocumentId == null) throw new ArgumentNullException("customDocumentId");
			return diadocHttpApi.GetDocumentsByCustomId(authToken, boxId, customDocumentId);
		}

		public PrepareDocumentsToSignResponse PrepareDocumentsToSign(string authToken, PrepareDocumentsToSignRequest request,
			bool excludeContent = false)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			return diadocHttpApi.PrepareDocumentsToSign(authToken, request, excludeContent);
		}

		public AsyncMethodResult CloudSign(string authToken, CloudSignRequest request, string certificateThumbprint)
		{
			if (request == null) throw new ArgumentNullException("request");
			return diadocHttpApi.CloudSign(authToken, request, certificateThumbprint);
		}

		public CloudSignResult WaitCloudSignResult(string authToken, string taskId, TimeSpan? timeout = null)
		{
			if (string.IsNullOrEmpty(taskId)) throw new ArgumentNullException("taskId");
			return diadocHttpApi.WaitCloudSignResult(authToken, taskId, timeout);
		}

		public AsyncMethodResult CloudSignConfirm(string authToken, string cloudSignToken, string confirmationCode,
			ContentLocationPreference? locationPreference = null)
		{
			if (string.IsNullOrEmpty(cloudSignToken)) throw new ArgumentNullException("cloudSignToken");
			if (string.IsNullOrEmpty(confirmationCode)) throw new ArgumentNullException("confirmationCode");
			return diadocHttpApi.CloudSignConfirm(authToken, cloudSignToken, confirmationCode, locationPreference);
		}

		public CloudSignConfirmResult WaitCloudSignConfirmResult(string authToken, string taskId, TimeSpan? timeout = null)
		{
			if (string.IsNullOrEmpty(taskId)) throw new ArgumentNullException("taskId");
			return diadocHttpApi.WaitCloudSignConfirmResult(authToken, taskId, timeout);
		}

		public AsyncMethodResult AcquireCounteragent(string authToken, string myOrgId, AcquireCounteragentRequest request,
			string myDepartmentId = null)
		{
			if (request == null) throw new ArgumentNullException("request");
			return diadocHttpApi.AcquireCounteragent(authToken, myOrgId, request, myDepartmentId);
		}

		public AcquireCounteragentResult WaitAcquireCounteragentResult(string authToken, string taskId, TimeSpan? timeout = null, TimeSpan? delay = null)
		{
			if (string.IsNullOrEmpty(taskId)) throw new ArgumentNullException("taskId");
			return diadocHttpApi.WaitAcquireCounteragentResult(authToken, taskId, timeout, delay);
		}

		public DocumentList GetDocumentsByMessageId(string authToken, string boxId, string messageId)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (string.IsNullOrEmpty(messageId))
				throw new ArgumentNullException("messageId");
			return diadocHttpApi.GetDocumentsByMessageId(authToken, boxId, messageId);
		}

		public List<KeyValueStorageEntry> GetOrganizationStorageEntries(string authToken, string orgId, IEnumerable<string> keys)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(orgId))
				throw new ArgumentNullException("orgId");
			if (keys == null)
				throw new ArgumentNullException("keys");
			return diadocHttpApi.GetOrganizationStorageEntries(authToken, orgId, keys);
		}

		public void PutOrganizationStorageEntries(string authToken, string orgId, IEnumerable<KeyValueStorageEntry> entries)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(orgId))
				throw new ArgumentNullException("orgId");
			if (entries == null)
				throw new ArgumentNullException("entries");
			diadocHttpApi.PutOrganizationStorageEntries(authToken, orgId, entries);
		}

		public AsyncMethodResult AutoSignReceipts(string authToken, string boxId, string certificateThumbprint, string batchKey)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			return diadocHttpApi.AutoSignReceipts(authToken, boxId, certificateThumbprint, batchKey);
		}

		public AutosignReceiptsResult WaitAutosignReceiptsResult(string authToken, string taskId, TimeSpan? timeout = null)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(taskId))
				throw new ArgumentNullException("taskId");
			return diadocHttpApi.WaitAutosignReceiptsResult(authToken, taskId, timeout);
		}

		public ExternalServiceAuthInfo GetExternalServiceAuthInfo(string key)
		{
			if (string.IsNullOrEmpty(key))
				throw new ArgumentNullException("key");
			return diadocHttpApi.GetExternalServiceAuthInfo(key);
		}

		public ExtendedSignerDetails GetExtendedSignerDetails(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection)
		{
			if (string.IsNullOrEmpty(token))
				throw new ArgumentNullException("token");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (string.IsNullOrEmpty(thumbprint))
				throw new ArgumentNullException("thumbprint");
			return diadocHttpApi.GetExtendedSignerDetails(token, boxId, thumbprint, forBuyer, forCorrection);
		}

		public ExtendedSignerDetails GetExtendedSignerDetails(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection)
		{
			if (string.IsNullOrEmpty(token))
				throw new ArgumentNullException("token");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (certificateBytes == null)
				throw new ArgumentNullException("certificateBytes");
			return diadocHttpApi.GetExtendedSignerDetails(token, boxId, certificateBytes, forBuyer, forCorrection);
		}

		public ExtendedSignerDetails PostExtendedSignerDetails(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails)
		{
			if (string.IsNullOrEmpty(token))
				throw new ArgumentNullException("token");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (certificateBytes == null)
				throw new ArgumentNullException("certificateBytes");
			if (signerDetails == null)
				throw new ArgumentNullException("signerDetails");
			return diadocHttpApi.PostExtendedSignerDetails(token, boxId, certificateBytes, forBuyer, forCorrection, signerDetails);
		}

		public ExtendedSignerDetails PostExtendedSignerDetails(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails)
		{
			if (string.IsNullOrEmpty(token))
				throw new ArgumentNullException("token");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (string.IsNullOrEmpty(thumbprint))
				throw new ArgumentNullException("thumbprint");
			if (signerDetails == null)
				throw new ArgumentNullException("signerDetails");
			return diadocHttpApi.PostExtendedSignerDetails(token, boxId, thumbprint, forBuyer, forCorrection, signerDetails);
		}

		public ResolutionRouteList GetResolutionRoutesForOrganization(string authToken, string orgId)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(orgId))
				throw new ArgumentNullException("orgId");
			return diadocHttpApi.GetResolutionRoutesForOrganization(authToken, orgId);
		}

		public GetDocumentTypesResponse GetDocumentTypes(string authToken, string boxId)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetDocumentTypes(authToken, boxId);
		}

		public DetectDocumentTypesResponse DetectDocumentTypes(string authToken, string boxId, string nameOnShelf)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (string.IsNullOrEmpty(nameOnShelf))
				throw new ArgumentNullException("nameOnShelf");
			return diadocHttpApi.DetectDocumentTypes(authToken, boxId, nameOnShelf);
		}

		public DetectDocumentTypesResponse DetectDocumentTypes(string authToken, string boxId, byte[] content)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			return diadocHttpApi.DetectDocumentTypes(authToken, boxId, content);
		}

		public FileContent GetContent(string authToken, string typeNamedId, string function, string version, int titleIndex)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(typeNamedId))
				throw new ArgumentNullException("typeNamedId");
			if (string.IsNullOrEmpty(function))
				throw new ArgumentNullException("function");
			if (string.IsNullOrEmpty(version))
				throw new ArgumentNullException("version");
			if (titleIndex < 0)
				throw new ArgumentOutOfRangeException("titleIndex", titleIndex, "Title index should be non-negative");
			return diadocHttpApi.GetContent(authToken, typeNamedId, function, version, titleIndex);
		}

		public Employee GetEmployee(string authToken, string boxId, string userId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (userId == null) throw new ArgumentNullException("userId");
			return diadocHttpApi.GetEmployee(authToken, boxId, userId);
		}

		public Employee CreateEmployee(string authToken, string boxId, EmployeeToCreate employeeToCreate)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (employeeToCreate == null) throw new ArgumentNullException("employeeToCreate");
			return diadocHttpApi.CreateEmployee(authToken, boxId, employeeToCreate);
		}

		public Employee UpdateEmployee(string authToken, string boxId, string userId, EmployeeToUpdate employeeToUpdate)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (userId == null) throw new ArgumentNullException("userId");
			if (employeeToUpdate == null) throw new ArgumentNullException("employeeToUpdate");
			return diadocHttpApi.UpdateEmployee(authToken, boxId, userId, employeeToUpdate);
		}

		public EmployeeSubscriptions GetSubscriptions(string authToken, string boxId, string userId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (userId == null) throw new ArgumentNullException("userId");
			return diadocHttpApi.GetSubscriptions(authToken, boxId, userId);
		}

		public EmployeeSubscriptions UpdateSubscriptions(string authToken, string boxId, string userId, SubscriptionsToUpdate subscriptionsToUpdate)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (userId == null) throw new ArgumentNullException("userId");
			return diadocHttpApi.UpdateSubscriptions(authToken, boxId, userId, subscriptionsToUpdate);
		}
	}
}