using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
using Diadoc.Api.Proto.KeyValueStorage;
using Diadoc.Api.Proto.Organizations;
using Diadoc.Api.Proto.Recognition;
using Diadoc.Api.Proto.Registration;
using Diadoc.Api.Proto.Users;
using JetBrains.Annotations;
using DocumentType = Diadoc.Api.Proto.DocumentType;
using Employee = Diadoc.Api.Proto.Employees.Employee;
using Departments = Diadoc.Api.Proto.Departments;
using Diadoc.Api.Proto.Certificates;
using RevocationRequestInfo = Diadoc.Api.Proto.Invoicing.RevocationRequestInfo;

namespace Diadoc.Api
{
	public partial class DiadocApi
	{
		public Task<string> AuthenticateAsync(string login, string password, string key = null, string id = null, CancellationToken ct = default)
		{
			return diadocHttpApi.AuthenticateAsync(login, password, key, id, ct: ct);
		}

		public Task<string> AuthenticateByKeyAsync([NotNull] string key, [NotNull] string id, CancellationToken ct = default)
		{
			if (key == null) throw new ArgumentNullException("key");
			if (id == null) throw new ArgumentNullException("id");
			return diadocHttpApi.AuthenticateByKeyAsync(key, id, ct: ct);
		}

		public Task<string> AuthenticateBySidAsync([NotNull] string sid, CancellationToken ct = default)
		{
			if (sid == null) throw new ArgumentNullException("sid");
			return diadocHttpApi.AuthenticateBySidAsync(sid, ct: ct);
		}

		public Task<string> AuthenticateAsync(byte[] certificateBytes, bool useLocalSystemStorage = false, CancellationToken ct = default)
		{
			if (certificateBytes == null) throw new ArgumentNullException("certificateBytes");
			return diadocHttpApi.AuthenticateAsync(certificateBytes, useLocalSystemStorage, ct: ct);
		}

		public Task<string> AuthenticateAsync(string thumbprint, bool useLocalSystemStorage = false, CancellationToken ct = default)
		{
			if (thumbprint == null) throw new ArgumentNullException("thumbprint");
			return diadocHttpApi.AuthenticateAsync(thumbprint, useLocalSystemStorage, ct: ct);
		}

		public Task<string> AuthenticateWithKeyAsync(string thumbprint, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true, CancellationToken ct = default)
		{
			if (thumbprint == null) throw new ArgumentNullException("thumbprint");
			return diadocHttpApi.AuthenticateWithKeyAsync(thumbprint, useLocalSystemStorage, key, id, autoConfirm, ct: ct);
		}

		public Task<string> AuthenticateWithKeyAsync(byte[] certificateBytes, bool useLocalSystemStorage = false, string key = null, string id = null, bool autoConfirm = true, CancellationToken ct = default)
		{
			if (certificateBytes == null) throw new ArgumentNullException("certificateBytes");
			return diadocHttpApi.AuthenticateWithKeyAsync(certificateBytes, useLocalSystemStorage, key, id, autoConfirm, ct: ct);
		}

		public Task<string> AuthenticateWithKeyConfirmAsync(byte[] certificateBytes, string token, bool saveBinding = false, CancellationToken ct = default)
		{
			if (certificateBytes == null) throw new ArgumentNullException("certificateBytes");
			if (string.IsNullOrEmpty(token)) throw new ArgumentNullException("token");
			return diadocHttpApi.AuthenticateWithKeyConfirmAsync(certificateBytes, token, saveBinding, ct: ct);
		}

		public Task<string> AuthenticateWithKeyConfirmAsync(string thumbprint, string token, bool saveBinding = false, CancellationToken ct = default)
		{
			if (thumbprint == null) throw new ArgumentNullException("thumbprint");
			if (string.IsNullOrEmpty(token)) throw new ArgumentNullException("token");
			return diadocHttpApi.AuthenticateWithKeyConfirmAsync(thumbprint, token, saveBinding, ct: ct);
		}

		public Task<OrganizationUserPermissions> GetMyPermissionsAsync(string authToken, string orgId, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (orgId == null) throw new ArgumentNullException("orgId");
			return diadocHttpApi.GetMyPermissionsAsync(authToken, orgId, ct: ct);
		}

		public Task<OrganizationList> GetMyOrganizationsAsync(string authToken, bool autoRegister = true, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			return diadocHttpApi.GetMyOrganizationsAsync(authToken, autoRegister, ct: ct);
		}

		public Task<User> GetMyUserAsync(string authToken, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			return diadocHttpApi.GetMyUserAsync(authToken, ct: ct);
		}

		public Task<UserV2> GetMyUserV2Async(string authToken, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			return diadocHttpApi.GetMyUserV2Async(authToken, ct: ct);
		}

		public Task<UserV2> UpdateMyUserAsync(string authToken, UserToUpdate userToUpdate, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (userToUpdate == null) throw new ArgumentNullException("userToUpdate");
			return diadocHttpApi.UpdateMyUserAsync(authToken, userToUpdate, ct: ct);
		}
		
		public Task<CertificateList> GetMyCertificatesAsync(string authToken, string boxId, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetMyCertificatesAsync(authToken, boxId, ct: ct);
		}

		public Task<OrganizationList> GetOrganizationsByInnKppAsync(string inn, string kpp, bool includeRelations = false, CancellationToken ct = default)
		{
			if (inn == null) throw new ArgumentNullException("inn");
			return diadocHttpApi.GetOrganizationsByInnKppAsync(inn, kpp, includeRelations, ct: ct);
		}

		public Task<Organization> GetOrganizationByIdAsync(string orgId, CancellationToken ct = default)
		{
			if (orgId == null) throw new ArgumentNullException("orgId");
			return diadocHttpApi.GetOrganizationByIdAsync(orgId, ct: ct);
		}

		public Task<Organization> GetOrganizationByBoxIdAsync(string boxId, CancellationToken ct = default)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetOrganizationByBoxIdAsync(boxId, ct: ct);
		}

		public Task<Organization> GetOrganizationByFnsParticipantIdAsync(string fnsParticipantId, CancellationToken ct = default)
		{
			if (fnsParticipantId == null) throw new ArgumentException("fnsParticipantId");
			return diadocHttpApi.GetOrganizationByFnsParticipantIdAsync(fnsParticipantId, ct: ct);
		}

		public Task<Organization> GetOrganizationByInnKppAsync(string inn, string kpp, CancellationToken ct = default)
		{
			if (inn == null) throw new ArgumentException("inn");
			return diadocHttpApi.GetOrganizationByInnKppAsync(inn, kpp, ct: ct);
		}

		public Task<Box> GetBoxAsync(string boxId, CancellationToken ct = default)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetBoxAsync(boxId, ct: ct);
		}

		public Task<Department> GetDepartmentAsync(string authToken, string orgId, string departmentId, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (orgId == null) throw new ArgumentNullException("orgId");
			if (departmentId == null) throw new ArgumentNullException("departmentId");
			return diadocHttpApi.GetDepartmentAsync(authToken, orgId, departmentId, ct: ct);
		}

		public Task UpdateOrganizationPropertiesAsync(string authToken, OrganizationPropertiesToUpdate orgProps, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (orgProps == null) throw new ArgumentNullException("orgProps");
			return diadocHttpApi.UpdateOrganizationPropertiesAsync(authToken, orgProps, ct: ct);
		}

		public Task<OrganizationFeatures> GetOrganizationFeaturesAsync(string authToken, string boxId, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetOrganizationFeaturesAsync(authToken, boxId, ct: ct);
		}

		public Task<BoxEventList> GetNewEventsAsync(string authToken, string boxId, string afterEventId = null, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetNewEventsAsync(authToken, boxId, afterEventId, ct: ct);
		}

		public Task<BoxEvent> GetEventAsync(string authToken, string boxId, string eventId, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (eventId == null) throw new ArgumentNullException("eventId");
			return diadocHttpApi.GetEventAsync(authToken, boxId, eventId, ct: ct);
		}

		public Task<Message> PostMessageAsync(string authToken, MessageToPost msg, string operationId = null, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (msg == null) throw new ArgumentNullException("msg");
			return diadocHttpApi.PostMessageAsync(authToken, msg, operationId, ct: ct);
		}

		public Task<Template> PostTemplateAsync(string authToken, TemplateToPost template, string operationId = null, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (template == null) throw new ArgumentNullException("template");
			return diadocHttpApi.PostTemplateAsync(authToken, template, operationId, ct: ct);
		}

		public Task<Message> TransformTemplateToMessageAsync(string authToken, TemplateTransformationToPost templateTransformation, string operationId = null, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (templateTransformation == null) throw new ArgumentNullException("templateTransformation");
			return diadocHttpApi.TransformTemplateToMessageAsync(authToken, templateTransformation, operationId, ct: ct);
		}

		public Task<MessagePatch> PostMessagePatchAsync(string authToken, MessagePatchToPost patch, string operationId = null, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (patch == null) throw new ArgumentNullException("patch");
			return diadocHttpApi.PostMessagePatchAsync(authToken, patch, operationId, ct: ct);
		}

		public Task<MessagePatch> PostTemplatePatchAsync(string authToken, string boxId, string templateId, TemplatePatchToPost patch, string operationId = null, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (templateId == null) throw new ArgumentNullException("templateId");
			if (patch == null) throw new ArgumentNullException("patch");
			return diadocHttpApi.PostTemplatePatchAsync(authToken, boxId, templateId, patch, operationId, ct: ct);
		}

		public Task PostRoamingNotificationAsync(string authToken, RoamingNotificationToPost notification, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (notification == null) throw new ArgumentNullException("notification");
			return diadocHttpApi.PostRoamingNotificationAsync(authToken, notification, ct: ct);
		}

		public Task DeleteAsync(string authToken, string boxId, string messageId, string documentId, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			return diadocHttpApi.DeleteAsync(authToken, boxId, messageId, documentId, ct: ct);
		}

		public Task RestoreAsync(string authToken, string boxId, string messageId, string documentId, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			return diadocHttpApi.RestoreAsync(authToken, boxId, messageId, documentId, ct: ct);
		}

		public Task MoveDocumentsAsync(string authToken, DocumentsMoveOperation query, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (query == null) throw new ArgumentNullException("query");
			return diadocHttpApi.MoveDocumentsAsync(authToken, query, ct: ct);
		}

		public Task<byte[]> GetEntityContentAsync(string authToken, string boxId, string messageId, string entityId, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (entityId == null) throw new ArgumentNullException("entityId");
			return diadocHttpApi.GetEntityContentAsync(authToken, boxId, messageId, entityId, ct: ct);
		}

		[Obsolete("Use GenerateReceiptXmlAsync()")]
		public Task<GeneratedFile> GenerateDocumentReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer, CancellationToken ct = default)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			if (signer == null) throw new ArgumentNullException("signer");
			return diadocHttpApi.GenerateReceiptXmlAsync(authToken, boxId, messageId, attachmentId, signer, ct: ct);
		}

		[Obsolete("Use GenerateReceiptXmlAsync()")]
		public Task<GeneratedFile> GenerateInvoiceDocumentReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer, CancellationToken ct = default)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			if (signer == null) throw new ArgumentNullException("signer");
			return diadocHttpApi.GenerateReceiptXmlAsync(authToken, boxId, messageId, attachmentId, signer, ct: ct);
		}

		public Task<GeneratedFile> GenerateReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer, CancellationToken ct = default)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			if (signer == null) throw new ArgumentNullException("signer");
			return diadocHttpApi.GenerateReceiptXmlAsync(authToken, boxId, messageId, attachmentId, signer, ct: ct);
		}

		public Task<GeneratedFile> GenerateInvoiceCorrectionRequestXmlAsync(string authToken, string boxId, string messageId,
			string attachmentId, InvoiceCorrectionRequestInfo correctionInfo, CancellationToken ct = default)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			return diadocHttpApi.GenerateInvoiceCorrectionRequestXmlAsync(authToken, boxId, messageId, attachmentId, correctionInfo, ct: ct);
		}

		public Task<GeneratedFile> GenerateRevocationRequestXmlAsync(string authToken, string boxId, string messageId,
			string attachmentId, RevocationRequestInfo revocationRequestInfo, CancellationToken ct = default)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			return diadocHttpApi.GenerateRevocationRequestXmlAsync(authToken, boxId, messageId, attachmentId, revocationRequestInfo, ct: ct);
		}

		public Task<GeneratedFile> GenerateSignatureRejectionXmlAsync(string authToken, string boxId, string messageId,
			string attachmentId, SignatureRejectionInfo signatureRejectionInfo, CancellationToken ct = default)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			return diadocHttpApi.GenerateSignatureRejectionXmlAsync(authToken, boxId, messageId, attachmentId, signatureRejectionInfo, ct: ct);
		}

		public Task<InvoiceCorrectionRequestInfo> GetInvoiceCorrectionRequestInfoAsync(string authToken, string boxId, string messageId,
			string entityId, CancellationToken ct = default)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (entityId == null) throw new ArgumentNullException("entityId");
			return diadocHttpApi.GetInvoiceCorrectionRequestInfoAsync(authToken, boxId, messageId, entityId, ct: ct);
		}

		public Task<GeneratedFile> GenerateInvoiceXmlAsync(string authToken, InvoiceInfo invoiceInfo, bool disableValidation = false, CancellationToken ct = default)
		{
			if (invoiceInfo == null) throw new ArgumentNullException("invoiceInfo");
			return diadocHttpApi.GenerateInvoiceXmlAsync(authToken, invoiceInfo, disableValidation, ct: ct);
		}

		public Task<GeneratedFile> GenerateInvoiceRevisionXmlAsync(string authToken, InvoiceInfo invoiceRevisionInfo,
			bool disableValidation = false, CancellationToken ct = default)
		{
			if (invoiceRevisionInfo == null) throw new ArgumentNullException("invoiceRevisionInfo");
			return diadocHttpApi.GenerateInvoiceRevisionXmlAsync(authToken, invoiceRevisionInfo, disableValidation, ct: ct);
		}

		public Task<GeneratedFile> GenerateInvoiceCorrectionXmlAsync(string authToken, InvoiceCorrectionInfo invoiceCorrectionInfo,
			bool disableValidation = false, CancellationToken ct = default)
		{
			if (invoiceCorrectionInfo == null) throw new ArgumentNullException("invoiceCorrectionInfo");
			return diadocHttpApi.GenerateInvoiceCorrectionXmlAsync(authToken, invoiceCorrectionInfo, disableValidation, ct: ct);
		}

		public Task<GeneratedFile> GenerateInvoiceCorrectionRevisionXmlAsync(string authToken,
			InvoiceCorrectionInfo invoiceCorrectionRevision, bool disableValidation = false, CancellationToken ct = default)
		{
			if (invoiceCorrectionRevision == null) throw new ArgumentNullException("invoiceCorrectionRevision");
			return diadocHttpApi.GenerateInvoiceCorrectionRevisionXmlAsync(authToken, invoiceCorrectionRevision, disableValidation, ct: ct);
		}

		public Task<GeneratedFile> GenerateTorg12XmlForSellerAsync(string authToken, Torg12SellerTitleInfo sellerInfo, bool disableValidation = false, CancellationToken ct = default)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateTorg12XmlForSellerAsync(authToken, sellerInfo, disableValidation, ct: ct);
		}

		public Task<GeneratedFile> GenerateTovTorg551XmlForSellerAsync(string authToken, TovTorgSellerTitleInfo sellerInfo, bool disableValidation = false, CancellationToken ct = default)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateTovTorg551XmlForSellerAsync(authToken, sellerInfo, disableValidation, ct: ct);
		}

		public Task<GeneratedFile> GenerateTorg12XmlForBuyerAsync(string authToken, Torg12BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, CancellationToken ct = default)
		{
			if (buyerInfo == null) throw new ArgumentNullException("buyerInfo");
			return diadocHttpApi.GenerateTorg12XmlForBuyerAsync(authToken, buyerInfo, boxId, sellerTitleMessageId, sellerTitleAttachmentId, ct: ct);
		}

		public Task<GeneratedFile> GenerateTovTorg551XmlForBuyerAsync(string authToken, TovTorgBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, string documentVersion = null, CancellationToken ct = default)
		{
			if (buyerInfo == null) throw new ArgumentNullException("buyerInfo");
			return diadocHttpApi.GenerateTovTorg551XmlForBuyerAsync(authToken, buyerInfo, boxId, sellerTitleMessageId, sellerTitleAttachmentId, documentVersion, ct: ct);
		}

		public Task<GeneratedFile> GenerateAcceptanceCertificateXmlForSellerAsync(string authToken,
			AcceptanceCertificateSellerTitleInfo sellerInfo, bool disableValidation = false, CancellationToken ct = default)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateAcceptanceCertificateXmlForSellerAsync(authToken, sellerInfo, disableValidation, ct: ct);
		}

		public Task<GeneratedFile> GenerateAcceptanceCertificateXmlForBuyerAsync(string authToken,
			AcceptanceCertificateBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId,
			string sellerTitleAttachmentId, CancellationToken ct = default)
		{
			if (buyerInfo == null) throw new ArgumentNullException("buyerInfo");
			return diadocHttpApi.GenerateAcceptanceCertificateXmlForBuyerAsync(authToken, buyerInfo, boxId, sellerTitleMessageId,
				sellerTitleAttachmentId, ct: ct);
		}

		public Task<GeneratedFile> GenerateAcceptanceCertificate552XmlForSellerAsync(string authToken, AcceptanceCertificate552SellerTitleInfo sellerInfo, bool disableValidation = false, CancellationToken ct = default)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateAcceptanceCertificate552XmlForSellerAsync(authToken, sellerInfo, disableValidation, ct: ct);
		}

		public Task<GeneratedFile> GenerateAcceptanceCertificate552XmlForBuyerAsync(string authToken, AcceptanceCertificate552BuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, CancellationToken ct = default)
		{
			if (buyerInfo == null) throw new ArgumentNullException("buyerInfo");
			return diadocHttpApi.GenerateAcceptanceCertificate552XmlForBuyerAsync(authToken, buyerInfo, boxId, sellerTitleMessageId,
				sellerTitleAttachmentId, ct: ct);
		}

		public Task<GeneratedFile> GenerateUniversalTransferDocumentXmlForSellerAsync(
			string authToken,
			UniversalTransferDocumentSellerTitleInfo sellerInfo,
			bool disableValidation = false,
			string documentVersion = null, 
			CancellationToken ct = default)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateUniversalTransferDocumentXmlForSellerAsync(authToken, sellerInfo, disableValidation, documentVersion, ct: ct);
		}

		public Task<GeneratedFile> GenerateUniversalCorrectionDocumentXmlForSellerAsync(
			string authToken,
			UniversalCorrectionDocumentSellerTitleInfo sellerInfo,
			bool disableValidation = false,
			string documentVersion = null,
			CancellationToken ct = default)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateUniversalCorrectionDocumentXmlForSellerAsync(authToken, sellerInfo, disableValidation, documentVersion, ct: ct);
		}

		public Task<GeneratedFile> GenerateUniversalTransferDocumentXmlForBuyerAsync(string authToken, UniversalTransferDocumentBuyerTitleInfo buyerInfo,
			string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId, CancellationToken ct = default)
		{
			if (buyerInfo == null) throw new ArgumentNullException("buyerInfo");
			return diadocHttpApi.GenerateUniversalTransferDocumentXmlForBuyerAsync(authToken, buyerInfo, boxId, sellerTitleMessageId, sellerTitleAttachmentId, ct: ct);
		}

		public Task<GeneratedFile> GenerateTitleXmlAsync(string authToken, string boxId, string documentTypeNamedId, string documentFunction, string documentVersion, int titleIndex, byte[] userContractData, bool disableValidation = false, string editingSettingId = null, string letterId = null, string documentId = null, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (documentTypeNamedId == null) throw new ArgumentNullException("documentTypeNamedId");
			if (documentFunction == null) throw new ArgumentNullException("documentFunction");
			if (documentVersion == null) throw new ArgumentNullException("documentVersion");
			if (userContractData == null) throw new ArgumentNullException("userContractData");

			return diadocHttpApi.GenerateTitleXmlAsync(
				authToken,
				boxId,
				documentTypeNamedId,
				documentFunction,
				documentVersion,
				titleIndex,
				userContractData,
				disableValidation,
				editingSettingId,
				letterId,
				documentId, 
				ct: ct);
		}

		public Task<GeneratedFile> GenerateSenderTitleXmlAsync(string authToken, string boxId, string documentTypeNamedId, string documentFunction, string documentVersion, byte[] userContractData, bool disableValidation = false, string editingSettingId = null, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (documentTypeNamedId == null) throw new ArgumentNullException("documentTypeNamedId");
			if (documentFunction == null) throw new ArgumentNullException("documentFunction");
			if (documentVersion == null) throw new ArgumentNullException("documentVersion");
			if (userContractData == null) throw new ArgumentNullException("userContractData");
			return diadocHttpApi.GenerateSenderTitleXmlAsync(authToken, boxId, documentTypeNamedId, documentFunction, documentVersion, userContractData, disableValidation, editingSettingId, ct: ct);
		}

		public Task<GeneratedFile> GenerateRecipientTitleXmlAsync(string authToken, string boxId, string senderTitleMessageId, string senderTitleAttachmentId, byte[] userContractData, string documentVersion = null, CancellationToken ct = default)
		{
			if (userContractData == null) throw new ArgumentNullException("userContractData");
			if (senderTitleMessageId == null) throw new ArgumentNullException("senderTitleMessageId");
			if (senderTitleAttachmentId == null) throw new ArgumentNullException("senderTitleAttachmentId");
			return diadocHttpApi.GenerateRecipientTitleXmlAsync(authToken, boxId, senderTitleMessageId, senderTitleAttachmentId, userContractData, ct: ct);
		}

		public Task<Message> GetMessageAsync(string authToken, string boxId, string messageId, bool withOriginalSignature = false, bool injectEntityContent = false, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			return diadocHttpApi.GetMessageAsync(authToken, boxId, messageId, withOriginalSignature, injectEntityContent, ct: ct);
		}

		public Task<Message> GetMessageAsync(string authToken, string boxId, string messageId, string entityId, bool withOriginalSignature = false, bool injectEntityContent = false, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (entityId == null) throw new ArgumentNullException("entityId");
			return diadocHttpApi.GetMessageAsync(authToken, boxId, messageId, entityId, withOriginalSignature, injectEntityContent, ct: ct);
		}

		public Task<Template> GetTemplateAsync(string authToken, string boxId, string templateId, string entityId = null, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (templateId == null) throw new ArgumentNullException("templateId");
			return diadocHttpApi.GetTemplateAsync(authToken, boxId, templateId, entityId, ct: ct);
		}

		public Task RecycleDraftAsync(string authToken, string boxId, string draftId, CancellationToken ct = default)
		{
			return diadocHttpApi.RecycleDraftAsync(authToken, boxId, draftId, ct: ct);
		}

		public Task<Message> SendDraftAsync(string authToken, DraftToSend draftToSend, string operationId = null, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (draftToSend == null) throw new ArgumentNullException("draftToSend");
			return diadocHttpApi.SendDraftAsync(authToken, draftToSend, operationId, ct: ct);
		}

		public Task<PrintFormResult> GeneratePrintFormAsync(string authToken, string boxId, string messageId, string documentId, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			if (string.IsNullOrEmpty(messageId)) throw new ArgumentNullException("messageId");
			if (string.IsNullOrEmpty(documentId)) throw new ArgumentNullException("documentId");
			return diadocHttpApi.GeneratePrintFormAsync(authToken, boxId, messageId, documentId, ct: ct);
		}

		public Task<string> GeneratePrintFormFromAttachmentAsync(string authToken, DocumentType documentType, byte[] content,
			string fromBoxId = null, CancellationToken ct = default)
		{
			return diadocHttpApi.GeneratePrintFormFromAttachmentAsync(authToken, documentType, content, fromBoxId, ct: ct);
		}

		[Obsolete("Use GetGeneratedPrintFormAsync without `documentType` parameter")]
		public Task<PrintFormResult> GetGeneratedPrintFormAsync(string authToken, DocumentType documentType, string printFormId, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(printFormId)) throw new ArgumentNullException("printFormId");
			return diadocHttpApi.GetGeneratedPrintFormAsync(authToken, documentType, printFormId, ct: ct);
		}

		public Task<PrintFormResult> GetGeneratedPrintFormAsync(string authToken, string printFormId, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(printFormId)) throw new ArgumentNullException("printFormId");
			return diadocHttpApi.GetGeneratedPrintFormAsync(authToken, printFormId, ct: ct);
		}

		public Task<string> RecognizeAsync(string fileName, byte[] content, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");
			return diadocHttpApi.RecognizeAsync(fileName, content, ct: ct);
		}

		public Task<Recognized> GetRecognizedAsync(string recognitionId, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(recognitionId)) throw new ArgumentNullException("recognitionId");
			return diadocHttpApi.GetRecognizedAsync(recognitionId, ct: ct);
		}

		public Task<DocumentList> GetDocumentsAsync(string authToken, string boxId, string filterCategory, string counteragentBoxId,
			DateTime? timestampFrom, DateTime? timestampTo, string fromDocumentDate, string toDocumentDate, string departmentId,
			bool excludeSubdepartments, string afterIndexKey, int? count = null, CancellationToken ct = default)
		{
			return diadocHttpApi.GetDocumentsAsync(authToken, boxId, filterCategory, counteragentBoxId, timestampFrom, timestampTo,
				fromDocumentDate, toDocumentDate, departmentId, excludeSubdepartments, afterIndexKey, count, ct: ct);
		}

		public Task<DocumentList> GetDocumentsAsync(string authToken, DocumentsFilter filter, CancellationToken ct = default)
		{
			return diadocHttpApi.GetDocumentsAsync(authToken, filter, ct: ct);
		}

		public Task<Document> GetDocumentAsync(string authToken, string boxId, string messageId, string entityId, CancellationToken ct = default)
		{
			return diadocHttpApi.GetDocumentAsync(authToken, boxId, messageId, entityId, ct: ct);
		}

		public Task<SignatureInfo> GetSignatureInfoAsync(string authToken, string boxId, string messageId, string entityId, CancellationToken ct = default)
		{
			return diadocHttpApi.GetSignatureInfoAsync(authToken, boxId, messageId, entityId, ct: ct);
		}

		public Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, DocumentTitleType documentTitleType, CancellationToken ct = default)
		{
			return diadocHttpApi.GetExtendedSignerDetailsAsync(token, boxId, thumbprint, documentTitleType, ct: ct);
		}

		public Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, DocumentTitleType documentTitleType, CancellationToken ct = default)
		{
			return diadocHttpApi.GetExtendedSignerDetailsAsync(token, boxId, certificateBytes, documentTitleType, ct: ct);
		}

		public Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, DocumentTitleType documentTitleType, ExtendedSignerDetailsToPost signerDetails, CancellationToken ct = default)
		{
			return diadocHttpApi.PostExtendedSignerDetailsAsync(token, boxId, thumbprint, documentTitleType, signerDetails, ct: ct);
		}

		public Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, DocumentTitleType documentTitleType, ExtendedSignerDetailsToPost signerDetails, CancellationToken ct = default)
		{
			return diadocHttpApi.PostExtendedSignerDetailsAsync(token, boxId, certificateBytes, documentTitleType, signerDetails, ct: ct);
		}

		public Task<GetDocflowBatchResponse> GetDocflowsAsync(string authToken, string boxId, GetDocflowBatchRequest request, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetDocflowsAsync(authToken, boxId, request, ct: ct);
		}

		public Task<GetDocflowEventsResponse> GetDocflowEventsAsync(string authToken, string boxId, GetDocflowEventsRequest request, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetDocflowEventsAsync(authToken, boxId, request, ct: ct);
		}

		public Task<SearchDocflowsResponse> SearchDocflowsAsync(string authToken, string boxId, SearchDocflowsRequest request, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.SearchDocflowsAsync(authToken, boxId, request, ct: ct);
		}

		public Task<GetDocflowsByPacketIdResponse> GetDocflowsByPacketIdAsync(string authToken, string boxId,
			GetDocflowsByPacketIdRequest request, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetDocflowsByPacketIdAsync(authToken, boxId, request, ct: ct);
		}

		public Task<ForwardDocumentResponse> ForwardDocumentAsync(string authToken, string boxId, ForwardDocumentRequest request, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.ForwardDocumentAsync(authToken, boxId, request, ct: ct);
		}

		public Task<GetForwardedDocumentsResponse> GetForwardedDocumentsAsync(string authToken, string boxId,
			GetForwardedDocumentsRequest request, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetForwardedDocumentsAsync(authToken, boxId, request, ct: ct);
		}

		public Task<GetForwardedDocumentEventsResponse> GetForwardedDocumentEventsAsync(string authToken, string boxId,
			GetForwardedDocumentEventsRequest request, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetForwardedDocumentEventsAsync(authToken, boxId, request, ct: ct);
		}

		public Task<byte[]> GetForwardedEntityContentAsync(string authToken, string boxId, ForwardedDocumentId forwardedDocumentId,
			string entityId, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetForwardedEntityContentAsync(authToken, boxId, forwardedDocumentId, entityId, ct: ct);
		}

		public Task<DocumentProtocolResult> GenerateForwardedDocumentProtocolAsync(string authToken, string boxId,
			ForwardedDocumentId forwardedDocumentId, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GenerateForwardedDocumentProtocolAsync(authToken, boxId, forwardedDocumentId, ct: ct);
		}

		public Task<PrintFormResult> GenerateForwardedDocumentPrintFormAsync(string authToken, string boxId, ForwardedDocumentId forwardedDocumentId, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GenerateForwardedDocumentPrintFormAsync(authToken, boxId, forwardedDocumentId, ct: ct);
		}

		public Task<bool> CanSendInvoiceAsync(string authToken, string boxId, byte[] certificateBytes, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			if (certificateBytes == null || certificateBytes.Length == 0) throw new ArgumentNullException("certificateBytes");
			return diadocHttpApi.CanSendInvoiceAsync(authToken, boxId, certificateBytes, ct: ct);
		}

		public Task SendFnsRegistrationMessageAsync(string authToken, string boxId,
			FnsRegistrationMessageInfo fnsRegistrationMessageInfo, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			if (!fnsRegistrationMessageInfo.Certificates.Any()) throw new ArgumentException("fnsRegistrationMessageInfo");
			return diadocHttpApi.SendFnsRegistrationMessageAsync(authToken, boxId, fnsRegistrationMessageInfo, ct: ct);
		}

		public Task<Counteragent> GetCounteragentAsync(string authToken, string myOrgId, string counteragentOrgId, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId)) throw new ArgumentNullException("myOrgId");
			if (string.IsNullOrEmpty(counteragentOrgId)) throw new ArgumentNullException("counteragentOrgId");
			return diadocHttpApi.GetCounteragentAsync(authToken, myOrgId, counteragentOrgId, ct: ct);
		}

		public Task<CounteragentList> GetCounteragentsAsync(string authToken, string myOrgId, string counteragentStatus, string afterIndexKey, string query = null, int? pageSize = null, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId)) throw new ArgumentNullException("myOrgId");
			return diadocHttpApi.GetCounteragentsAsync(authToken, myOrgId, counteragentStatus, afterIndexKey, query, pageSize, ct: ct);
		}

		public Task<CounteragentCertificateList> GetCounteragentCertificatesAsync(string authToken, string myOrgId, string counteragentOrgId, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId)) throw new ArgumentNullException("myOrgId");
			if (string.IsNullOrEmpty(counteragentOrgId)) throw new ArgumentNullException("counteragentOrgId");
			return diadocHttpApi.GetCounteragentCertificatesAsync(authToken, myOrgId, counteragentOrgId, ct: ct);
		}

		public Task BreakWithCounteragentAsync(string authToken, string myOrgId, string counteragentOrgId, string comment, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId)) throw new ArgumentNullException("myOrgId");
			if (string.IsNullOrEmpty(counteragentOrgId)) throw new ArgumentNullException("counteragentOrgId");
			return diadocHttpApi.BreakWithCounteragentAsync(authToken, myOrgId, counteragentOrgId, comment, ct: ct);
		}

		public Task<string> UploadFileToShelfAsync(string authToken, byte[] data, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (data == null) throw new ArgumentNullException("data");
			return diadocHttpApi.UploadFileToShelfAsync(authToken, data, ct: ct);
		}

		public Task<byte[]> GetFileFromShelfAsync(string authToken, string nameOnShelf, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(nameOnShelf)) throw new ArgumentNullException("nameOnShelf");
			return diadocHttpApi.GetFileFromShelfAsync(authToken, nameOnShelf, ct: ct);
		}

		public Task<RussianAddress> ParseRussianAddressAsync(string address, CancellationToken ct = default)
		{
			return diadocHttpApi.ParseRussianAddressAsync(address, ct: ct);
		}

		public Task<InvoiceInfo> ParseInvoiceXmlAsync(byte[] invoiceXmlContent, CancellationToken ct = default)
		{
			return diadocHttpApi.ParseInvoiceXmlAsync(invoiceXmlContent, ct: ct);
		}

		public Task<Torg12SellerTitleInfo> ParseTorg12SellerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return diadocHttpApi.ParseTorg12SellerTitleXmlAsync(xmlContent, ct: ct);
		}

		public Task<Torg12BuyerTitleInfo> ParseTorg12BuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return diadocHttpApi.ParseTorg12BuyerTitleXmlAsync(xmlContent, ct: ct);
		}

		public Task<TovTorgSellerTitleInfo> ParseTovTorg551SellerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return diadocHttpApi.ParseTovTorg551SellerTitleXmlAsync(xmlContent, ct: ct);
		}

		public Task<TovTorgBuyerTitleInfo> ParseTovTorg551BuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return diadocHttpApi.ParseTovTorg551BuyerTitleXmlAsync(xmlContent, ct: ct);
		}

		public Task<AcceptanceCertificateSellerTitleInfo> ParseAcceptanceCertificateSellerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return diadocHttpApi.ParseAcceptanceCertificateSellerTitleXmlAsync(xmlContent, ct: ct);
		}

		public Task<AcceptanceCertificateBuyerTitleInfo> ParseAcceptanceCertificateBuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return diadocHttpApi.ParseAcceptanceCertificateBuyerTitleXmlAsync(xmlContent, ct: ct);
		}

		public Task<AcceptanceCertificate552SellerTitleInfo> ParseAcceptanceCertificate552SellerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return diadocHttpApi.ParseAcceptanceCertificate552SellerTitleXmlAsync(xmlContent, ct: ct);
		}

		public Task<AcceptanceCertificate552BuyerTitleInfo> ParseAcceptanceCertificate552BuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return diadocHttpApi.ParseAcceptanceCertificate552BuyerTitleXmlAsync(xmlContent, ct: ct);
		}

		public Task<UniversalTransferDocumentSellerTitleInfo> ParseUniversalTransferDocumentSellerTitleXmlAsync(byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Utd, CancellationToken ct = default)
		{
			return diadocHttpApi.ParseUniversalTransferDocumentSellerTitleXmlAsync(xmlContent, documentVersion, ct: ct);
		}

		public Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalTransferDocumentBuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return diadocHttpApi.ParseUniversalTransferDocumentBuyerTitleXmlAsync(xmlContent, ct: ct);
		}

		public Task<UniversalCorrectionDocumentSellerTitleInfo> ParseUniversalCorrectionDocumentSellerTitleXmlAsync(byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Ucd, CancellationToken ct = default)
		{
			return diadocHttpApi.ParseUniversalCorrectionDocumentSellerTitleXmlAsync(xmlContent, documentVersion, ct: ct);
		}

		public Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalCorrectionDocumentBuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return diadocHttpApi.ParseUniversalCorrectionDocumentBuyerTitleXmlAsync(xmlContent, ct: ct);
		}

		public Task<byte[]> ParseTitleXmlAsync(
			string authToken,
			string boxId,
			string documentTypeNamedId,
			string documentFunction,
			string documentVersion,
			int titleIndex,
			byte[] content, 
			CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException(nameof(authToken));
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException(nameof(boxId));
			if (string.IsNullOrEmpty(documentTypeNamedId)) throw new ArgumentNullException(nameof(documentTypeNamedId));
			if (string.IsNullOrEmpty(documentFunction)) throw new ArgumentNullException(nameof(documentFunction));
			if (string.IsNullOrEmpty(documentVersion)) throw new ArgumentNullException(nameof(documentVersion));
			if (content == null) throw new ArgumentNullException(nameof(content));

			return diadocHttpApi.ParseTitleXmlAsync(
				authToken,
				boxId,
				documentTypeNamedId,
				documentFunction,
				documentVersion,
				titleIndex,
				content, 
				ct: ct);
		}

		public Task<OrganizationUsersList> GetOrganizationUsersAsync(string authToken, string orgId, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(orgId)) throw new ArgumentNullException("orgId");
			return diadocHttpApi.GetOrganizationUsersAsync(authToken, orgId, ct: ct);
		}

		public Task<List<Organization>> GetOrganizationsByInnListAsync(GetOrganizationsByInnListRequest innList, CancellationToken ct = default)
		{
			if (innList == null)
				throw new ArgumentNullException("innList");
			return diadocHttpApi.GetOrganizationsByInnListAsync(innList, ct: ct);
		}

		public Task<List<OrganizationWithCounteragentStatus>> GetOrganizationsByInnListAsync(string authToken, string myOrgId,
			GetOrganizationsByInnListRequest innList, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId))
				throw new ArgumentNullException("myOrgId");
			if (innList == null)
				throw new ArgumentNullException("innList");
			return diadocHttpApi.GetOrganizationsByInnListAsync(authToken, myOrgId, innList, ct: ct);
		}

		public Task<RevocationRequestInfo> ParseRevocationRequestXmlAsync(byte[] revocationRequestXmlContent, CancellationToken ct = default)
		{
			return diadocHttpApi.ParseRevocationRequestXmlAsync(revocationRequestXmlContent, ct: ct);
		}

		public Task<SignatureRejectionInfo> ParseSignatureRejectionXmlAsync(byte[] signatureRejectionXmlContent, CancellationToken ct = default)
		{
			return diadocHttpApi.ParseSignatureRejectionXmlAsync(signatureRejectionXmlContent, ct: ct);
		}

		public Task<DocumentProtocolResult> GenerateDocumentProtocolAsync(string authToken, string boxId, string messageId,
			string documentId, CancellationToken ct = default)
		{
			return diadocHttpApi.GenerateDocumentProtocolAsync(authToken, boxId, messageId, documentId, ct: ct);
		}

		public Task<DocumentZipGenerationResult> GenerateDocumentZipAsync(string authToken, string boxId, string messageId,
			string documentId, bool fullDocflow, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (documentId == null) throw new ArgumentNullException("documentId");
			return diadocHttpApi.GenerateDocumentZipAsync(authToken, boxId, messageId, documentId, fullDocflow, ct: ct);
		}

		public Task<DocumentList> GetDocumentsByCustomIdAsync(string authToken, string boxId, string customDocumentId, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (customDocumentId == null) throw new ArgumentNullException("customDocumentId");
			return diadocHttpApi.GetDocumentsByCustomIdAsync(authToken, boxId, customDocumentId, ct: ct);
		}

		public Task<PrepareDocumentsToSignResponse> PrepareDocumentsToSignAsync(string authToken, PrepareDocumentsToSignRequest request,
			bool excludeContent = false, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			return diadocHttpApi.PrepareDocumentsToSignAsync(authToken, request, excludeContent, ct: ct);
		}

		public Task<AsyncMethodResult> CloudSignAsync(string authToken, CloudSignRequest request, string certificateThumbprint, CancellationToken ct = default)
		{
			if (request == null) throw new ArgumentNullException("request");
			return diadocHttpApi.CloudSignAsync(authToken, request, certificateThumbprint, ct: ct);
		}

		public Task<CloudSignResult> WaitCloudSignResultAsync(string authToken, string taskId, TimeSpan? timeout = null, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(taskId)) throw new ArgumentNullException("taskId");
			return diadocHttpApi.WaitCloudSignResultAsync(authToken, taskId, timeout, ct: ct);
		}

		public Task<AsyncMethodResult> CloudSignConfirmAsync(string authToken, string cloudSignToken, string confirmationCode,
			ContentLocationPreference? locationPreference = null, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(cloudSignToken)) throw new ArgumentNullException("cloudSignToken");
			if (string.IsNullOrEmpty(confirmationCode)) throw new ArgumentNullException("confirmationCode");
			return diadocHttpApi.CloudSignConfirmAsync(authToken, cloudSignToken, confirmationCode, locationPreference, ct: ct);
		}

		public Task<CloudSignConfirmResult> WaitCloudSignConfirmResultAsync(string authToken, string taskId, TimeSpan? timeout = null, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(taskId)) throw new ArgumentNullException("taskId");
			return diadocHttpApi.WaitCloudSignConfirmResultAsync(authToken, taskId, timeout, ct: ct);
		}

		public Task<AsyncMethodResult> DssSignAsync(string authToken, string boxId, DssSignRequest request, string certificateThumbprint = null, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			if (request == null) throw new ArgumentNullException("request");
			return diadocHttpApi.DssSignAsync(authToken, boxId, request, certificateThumbprint, ct: ct);
		}

		public Task<DssSignResult> DssSignResultAsync(string authToken, string boxId, string taskId, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(taskId)) throw new ArgumentNullException("taskId");
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.DssSignResultAsync(authToken, boxId, taskId, ct: ct);
		}

		public Task<AsyncMethodResult> AcquireCounteragentAsync(string authToken, string myOrgId, AcquireCounteragentRequest request,
			string myDepartmentId = null, CancellationToken ct = default)
		{
			if (request == null) throw new ArgumentNullException("request");
			return diadocHttpApi.AcquireCounteragentAsync(authToken, myOrgId, request, myDepartmentId, ct: ct);
		}

		public Task<AcquireCounteragentResult> WaitAcquireCounteragentResultAsync(string authToken, string taskId, TimeSpan? timeout = null, TimeSpan? delay = null, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(taskId)) throw new ArgumentNullException("taskId");
			return diadocHttpApi.WaitAcquireCounteragentResultAsync(authToken, taskId, timeout, delay, ct: ct);
		}

		public Task<DocumentList> GetDocumentsByMessageIdAsync(string authToken, string boxId, string messageId, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (string.IsNullOrEmpty(messageId))
				throw new ArgumentNullException("messageId");
			return diadocHttpApi.GetDocumentsByMessageIdAsync(authToken, boxId, messageId, ct: ct);
		}

		public Task<List<KeyValueStorageEntry>> GetOrganizationStorageEntriesAsync(string authToken, string boxId, IEnumerable<string> keys, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (keys == null)
				throw new ArgumentNullException("keys");
			return diadocHttpApi.GetOrganizationStorageEntriesAsync(authToken, boxId, keys, ct: ct);
		}

		public Task PutOrganizationStorageEntriesAsync(string authToken, string boxId, IEnumerable<KeyValueStorageEntry> entries, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (entries == null)
				throw new ArgumentNullException("entries");
			return diadocHttpApi.PutOrganizationStorageEntriesAsync(authToken, boxId, entries, ct: ct);
		}

		public Task<AsyncMethodResult> AutoSignReceiptsAsync(string authToken, string boxId, string certificateThumbprint, string batchKey, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			return diadocHttpApi.AutoSignReceiptsAsync(authToken, boxId, certificateThumbprint, batchKey, ct: ct);
		}

		public Task<AutosignReceiptsResult> WaitAutosignReceiptsResultAsync(string authToken, string taskId, TimeSpan? timeout = null, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(taskId))
				throw new ArgumentNullException("taskId");
			return diadocHttpApi.WaitAutosignReceiptsResultAsync(authToken, taskId, timeout, ct: ct);
		}

		public Task<ExternalServiceAuthInfo> GetExternalServiceAuthInfoAsync(string key, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(key))
				throw new ArgumentNullException("key");
			return diadocHttpApi.GetExternalServiceAuthInfoAsync(key, ct: ct);
		}

		public Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(token))
				throw new ArgumentNullException("token");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (string.IsNullOrEmpty(thumbprint))
				throw new ArgumentNullException("thumbprint");
			return diadocHttpApi.GetExtendedSignerDetailsAsync(token, boxId, thumbprint, forBuyer, forCorrection, ct: ct);
		}

		public Task<ExtendedSignerDetails> GetExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(token))
				throw new ArgumentNullException("token");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (certificateBytes == null)
				throw new ArgumentNullException("certificateBytes");
			return diadocHttpApi.GetExtendedSignerDetailsAsync(token, boxId, certificateBytes, forBuyer, forCorrection, ct: ct);
		}

		public Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, byte[] certificateBytes, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(token))
				throw new ArgumentNullException("token");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (certificateBytes == null)
				throw new ArgumentNullException("certificateBytes");
			if (signerDetails == null)
				throw new ArgumentNullException("signerDetails");
			return diadocHttpApi.PostExtendedSignerDetailsAsync(token, boxId, certificateBytes, forBuyer, forCorrection, signerDetails, ct: ct);
		}

		public Task<ExtendedSignerDetails> PostExtendedSignerDetailsAsync(string token, string boxId, string thumbprint, bool forBuyer, bool forCorrection, ExtendedSignerDetailsToPost signerDetails, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(token))
				throw new ArgumentNullException("token");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (string.IsNullOrEmpty(thumbprint))
				throw new ArgumentNullException("thumbprint");
			if (signerDetails == null)
				throw new ArgumentNullException("signerDetails");
			return diadocHttpApi.PostExtendedSignerDetailsAsync(token, boxId, thumbprint, forBuyer, forCorrection, signerDetails, ct: ct);
		}

		public Task<ResolutionRouteList> GetResolutionRoutesForOrganizationAsync(string authToken, string orgId, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(orgId))
				throw new ArgumentNullException("orgId");
			return diadocHttpApi.GetResolutionRoutesForOrganizationAsync(authToken, orgId, ct: ct);
		}

		public Task<GetDocumentTypesResponse> GetDocumentTypesAsync(string authToken, string boxId, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetDocumentTypesAsync(authToken, boxId, ct: ct);
		}

		[Obsolete("Use DetectDocumentTitlesAsync")]
		public Task<DetectDocumentTypesResponse> DetectDocumentTypesAsync(string authToken, string boxId, string nameOnShelf, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (string.IsNullOrEmpty(nameOnShelf))
				throw new ArgumentNullException("nameOnShelf");
			return diadocHttpApi.DetectDocumentTypesAsync(authToken, boxId, nameOnShelf, ct: ct);
		}

		[Obsolete("Use DetectDocumentTitlesAsync")]
		public Task<DetectDocumentTypesResponse> DetectDocumentTypesAsync(string authToken, string boxId, byte[] content, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			return diadocHttpApi.DetectDocumentTypesAsync(authToken, boxId, content, ct: ct);
		}
		
		public Task<DetectTitleResponse> DetectDocumentTitlesAsync(string authToken, string boxId, string nameOnShelf, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (string.IsNullOrEmpty(nameOnShelf))
				throw new ArgumentNullException("nameOnShelf");
			return diadocHttpApi.DetectDocumentTitlesAsync(authToken, boxId, nameOnShelf, ct: ct);
		}

		public Task<DetectTitleResponse> DetectDocumentTitlesAsync(string authToken, string boxId, byte[] content, CancellationToken ct = default)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			return diadocHttpApi.DetectDocumentTitlesAsync(authToken, boxId, content, ct: ct);
		}

		public Task<FileContent> GetContentAsync(string authToken, string typeNamedId, string function, string version, int titleIndex, XsdContentType contentType = default(XsdContentType), CancellationToken ct = default)
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
			return diadocHttpApi.GetContentAsync(authToken, typeNamedId, function, version, titleIndex, contentType, ct: ct);
		}

		public Task<Employee> GetEmployeeAsync(string authToken, string boxId, string userId, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (userId == null) throw new ArgumentNullException("userId");
			return diadocHttpApi.GetEmployeeAsync(authToken, boxId, userId, ct: ct);
		}

		public Task<EmployeeList> GetEmployeesAsync(string authToken, string boxId, int? page, int? count, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (page < 1)
				throw new ArgumentOutOfRangeException("page", page, "page must be 1 or greater");
			if (count < 1)
				throw new ArgumentOutOfRangeException("count", count, "count must be 1 or greater");
			return diadocHttpApi.GetEmployeesAsync(authToken, boxId, page, count, ct: ct);
		}

		public Task<Employee> CreateEmployeeAsync(string authToken, string boxId, EmployeeToCreate employeeToCreate, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (employeeToCreate == null) throw new ArgumentNullException("employeeToCreate");
			return diadocHttpApi.CreateEmployeeAsync(authToken, boxId, employeeToCreate, ct: ct);
		}

		public Task<Employee> UpdateEmployeeAsync(string authToken, string boxId, string userId, EmployeeToUpdate employeeToUpdate, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (userId == null) throw new ArgumentNullException("userId");
			if (employeeToUpdate == null) throw new ArgumentNullException("employeeToUpdate");
			return diadocHttpApi.UpdateEmployeeAsync(authToken, boxId, userId, employeeToUpdate, ct: ct);
		}

		public Task DeleteEmployeeAsync(string authToken, string boxId, string userId, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (userId == null) throw new ArgumentNullException("userId");
			return diadocHttpApi.DeleteEmployeeAsync(authToken, boxId, userId, ct: ct);
		}

		public Task<Employee> GetMyEmployeeAsync(string authToken, string boxId, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetMyEmployeeAsync(authToken, boxId, ct: ct);
		}

		public Task<EmployeeSubscriptions> GetSubscriptionsAsync(string authToken, string boxId, string userId, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (userId == null) throw new ArgumentNullException("userId");
			return diadocHttpApi.GetSubscriptionsAsync(authToken, boxId, userId, ct: ct);
		}

		public Task<EmployeeSubscriptions> UpdateSubscriptionsAsync(string authToken, string boxId, string userId, SubscriptionsToUpdate subscriptionsToUpdate, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (userId == null) throw new ArgumentNullException("userId");
			return diadocHttpApi.UpdateSubscriptionsAsync(authToken, boxId, userId, subscriptionsToUpdate, ct: ct);
		}

		public Task<Departments.Department> GetDepartmentByFullIdAsync(string authToken, string boxId, string departmentId, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (departmentId == null) throw new ArgumentNullException("departmentId");
			return diadocHttpApi.GetDepartmentByFullIdAsync(authToken, boxId, departmentId, ct: ct);
		}

		public Task<Departments.DepartmentList> GetDepartmentsAsync(string authToken, string boxId, int? page = null, int? count = null, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetDepartmentsAsync(authToken, boxId, page, count, ct: ct);
		}

		public Task<Departments.Department> CreateDepartmentAsync(string authToken, string boxId, Departments.DepartmentToCreate departmentToCreate, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.CreateDepartmentAsync(authToken, boxId, departmentToCreate, ct: ct);
		}

		public Task<Departments.Department> UpdateDepartmentAsync(string authToken, string boxId, string departmentId, Departments.DepartmentToUpdate departmentToUpdate, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (departmentId == null) throw new ArgumentNullException("departmentId");
			return diadocHttpApi.UpdateDepartmentAsync(authToken, boxId, departmentId, departmentToUpdate, ct: ct);
		}

		public Task DeleteDepartmentAsync(string authToken, string boxId, string departmentId, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (departmentId == null) throw new ArgumentNullException("departmentId");
			return diadocHttpApi.DeleteDepartmentAsync(authToken, boxId, departmentId, ct: ct);
		}

		public Task<RegistrationResponse> RegisterAsync(string authToken, RegistrationRequest registrationRequest, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (registrationRequest == null) throw new ArgumentNullException("registrationRequest");
			return diadocHttpApi.RegisterAsync(authToken, registrationRequest, ct: ct);
		}

		public Task RegisterConfirmAsync(string authToken, RegistrationConfirmRequest registrationConfirmRequest, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (registrationConfirmRequest == null) throw new ArgumentNullException("registrationConfirmRequest");
			return diadocHttpApi.RegisterConfirmAsync(authToken, registrationConfirmRequest, ct: ct);
		}

		public Task<CustomPrintFormDetectionResult> DetectCustomPrintFormsAsync(string authToken,
			string boxId,
			CustomPrintFormDetectionRequest request, CancellationToken ct = default)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (request == null) throw new ArgumentNullException("request");
			return diadocHttpApi.DetectCustomPrintFormsAsync(authToken, boxId, request, ct: ct);
		}

		public Task<BoxEvent> GetLastEventAsync(string authToken, string boxId, CancellationToken ct = default)
		{
			if(authToken == null) throw new ArgumentNullException("authToken");
			if(boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetLastEventAsync(authToken, boxId, ct: ct);
		}
	}
}
