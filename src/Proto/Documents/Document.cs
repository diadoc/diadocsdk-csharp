using System;
using System.Runtime.InteropServices;
using Diadoc.Api.Com;
using Diadoc.Api.Proto.Documents.AcceptanceCertificateDocument;
using Diadoc.Api.Proto.Documents.BilateralDocument;
using Diadoc.Api.Proto.Documents.InvoiceDocument;
using Diadoc.Api.Proto.Documents.NonformalizedDocument;
using Diadoc.Api.Proto.Documents.UnilateralDocument;
using Diadoc.Api.Proto.Documents.UniversalTransferDocument;

namespace Diadoc.Api.Proto.Documents
{
	[ComVisible(true)]
	[Guid("77DE3BB1-3646-4219-98D7-7D2510AE3C46")]
	public interface IDocument
	{
		string MessageId { get; }
		string EntityId { get; }
		string CounteragentBoxId { get; }
		string FileName { get; }
		string DocumentNumber { get; }
		string DocumentDate { get; }
		Com.DocumentType Type { get; }
		string IndexKey { get; }
		DateTime CreationTimestamp { get; }

		ReadonlyList InitialDocumentIdsList { get; }
		ReadonlyList SubordinateDocumentIdsList { get; }
		ReadonlyList CustomDataList { get; }
		ReadonlyList ForwardDocumentEventsList { get; }

		Content Content { get; }
		NonformalizedDocumentMetadata NonformalizedDocumentMetadata { get; }
		InvoiceMetadata InvoiceMetadata { get; }
		TrustConnectionRequestMetadata TrustConnectionRequestMetadata { get; }
		BasicDocumentMetadata Torg12Metadata { get; }
		InvoiceRevisionMetadata InvoiceRevisionMetadata { get; }
		InvoiceCorrectionMetadata InvoiceCorrectionMetadata { get; }
		InvoiceCorrectionRevisionMetadata InvoiceCorrectionRevisionMetadata { get; }
		AcceptanceCertificateMetadata AcceptanceCertificateMetadata { get; }
		ProformaInvoiceMetadata ProformaInvoiceMetadata { get; }
		BasicDocumentMetadata XmlTorg12Metadata { get; }
		BasicDocumentMetadata XmlAcceptanceCertificateMetadata { get; }
		PriceListMetadata PriceListMetadata { get; }
		NonformalizedDocumentMetadata PriceListAgreementMetadata { get; }
		NonformalizedDocumentMetadata CertificateRegistryMetadata { get; }
		BilateralDocumentMetadata ReconciliationActMetadata { get; }
		ContractMetadata ContractMetadata { get; }
		BasicDocumentMetadata Torg13Metadata { get; }
		ServiceDetailsMetadata ServiceDetailsMetadata { get; }
		UniversalTransferDocumentMetadata UniversalTransferDocumentMetadata { get; }
		UniversalTransferDocumentRevisionMetadata UniversalTransferDocumentRevisionMetadata { get; }
		UniversalCorrectionDocumentMetadata UniversalCorrectionDocumentMetadata { get; }
		UniversalCorrectionDocumentRevisionMetadata UniversalCorrectionDocumentRevisionMetadata { get; }
		SupplementaryAgreementMetadata SupplementaryAgreementMetadata { get; }
		RecipientReceiptMetadata RecipientReceiptMetadata { get; }
		ConfirmationMetadata ConfirmationMetadata { get; }
		AmendmentRequestMetadata AmendmentRequestMetadata { get; }
		ReadonlyList MetadataList { get; }
		bool IsDeleted { get; }
		bool IsTest { get; }
		bool IsRead { get; }
		bool PacketIsLocked { get; }
		bool HasCustomPrintForm { get; }
		string PacketId { get; }
		bool IsEncryptedContent { get; }
		string DepartmentId { get; }
		string CustomDocumentId { get; }
		string FromDepartmentId { get; }
		string ToDepartmentId { get; }
		ResolutionStatus ResolutionStatus { get; }
		string ResolutionRouteId { get; }

		Com.RevocationStatus DocumentRevocationStatus { get; }

		DateTime SendTimestamp { get; }
		DateTime DeliveryTimestamp { get; }
		DateTime LastModificationTimestamp { get; }
		int WorkflowId { get; }
		string TypeNamedId { get; }
		string Function { get; }
		string Title { get; }
		string AttachmentVersion { get; set; }
		string Version { get; }
		Origin Origin { get; }

		Com.DocumentDirection DocumentDirectionValue { get; }
		Com.SenderSignatureStatus SenderSignatureStatusValue { get; }
		Com.ProxySignatureStatus ProxySignatureStatusValue { get; }
		Com.RecipientResponseStatus RecipientResponseStatusValue { get; }
		Com.RoamingNotificationStatus RoamingNotificationStatusValue { get; }
		string RoamingNotificationStatusDescription { get; }

		ReadonlyList LastOuterDocflowsList { get; }
		string ProxyBoxId { get; }
		string ProxyDepartmentId { get; }
	}

	[ComVisible(true)]
	[Guid("410FA2BA-94EC-4A60-B9A7-1B430AF4BF14")]
	public interface IResolutionStatus
	{
		Com.ResolutionStatusType StatusType { get; }
		ResolutionTarget Target { get; }
		string AuthorUserId { get; }
		string AuthorFIO { get; }
	}

	[ComVisible(true)]
	[Guid("289F0860-0CEF-4BC5-9E4E-399AC5131DA2")]
	public interface IRecipientReceiptMetadata
	{
		Com.GeneralReceiptStatus ReceiptStatusValue { get; }
		ConfirmationMetadata ConfirmationMetadata { get; set; }
	}

	[ComVisible(true)]
	[Guid("D02ADF7A-9C6F-4041-A87A-514BF6650C8B")]
	public interface IConfirmationMetadata
	{
		Com.GeneralReceiptStatus ReceiptStatusValue { get; }
		DateTime DateTime { get; }
	}

	[ComVisible(true)]
	[Guid("A26A8F7F-247B-4C5F-904B-AAF9C8C87B2E")]
	public interface IAmendmentRequestMetadata
	{
		Com.GeneralReceiptStatus ReceiptStatusValue { get; }
		int AmendmentFlags { get; set; }
	}

	[ComVisible(true)]
	[Guid("96E1D7C2-45A5-4669-8196-16645AA79094")]
	public interface IOrigin
	{
		Com.MessageType MessageTypeValue { get; }
		string MessageId { get; set; }
	}

	[ComVisible(true)]
	[Guid("B9FF9F48-765A-42F5-83A9-908A2EB8A83A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IResolutionStatus))]
	public partial class ResolutionStatus : SafeComObject, IResolutionStatus
	{
		public Com.ResolutionStatusType StatusType
		{
			get { return (Com.ResolutionStatusType)((int)Type); }
			set { Type = (ResolutionStatusType)((int)value); }
		}
	}

	[ComVisible(true)]
	[Guid("DDBF8B9C-7EB8-40C2-9F9F-69771CEBFA6D")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocument))]
	public partial class Document : SafeComObject, IDocument
	{
		public DateTime CreationTimestamp
		{
			get { return new DateTime(CreationTimestampTicks, DateTimeKind.Utc); }
		}

		public DateTime SendTimestamp
		{
			get { return new DateTime(SendTimestampTicks, DateTimeKind.Utc); }
		}

		public DateTime DeliveryTimestamp
		{
			get { return new DateTime(DeliveryTimestampTicks, DateTimeKind.Utc); }
		}

		public Com.DocumentDirection DocumentDirectionValue
		{
			get { return (Com.DocumentDirection)DocumentDirection; }
		}

		public Com.RoamingNotificationStatus RoamingNotificationStatusValue
		{
			get { return (Com.RoamingNotificationStatus)RoamingNotificationStatus; }
		}

		public ReadonlyList CustomDataList
		{
			get { return new ReadonlyList(CustomData); }
		}

		public ReadonlyList ForwardDocumentEventsList
		{
			get { return new ReadonlyList(ForwardDocumentEvents); }
		}

		public DateTime LastModificationTimestamp
		{
			get { return new DateTime(LastModificationTimestampTicks); }
		}

		public Com.SenderSignatureStatus SenderSignatureStatusValue
		{
			get { return (Com.SenderSignatureStatus)SenderSignatureStatus; }
		}

		public Com.ProxySignatureStatus ProxySignatureStatusValue
		{
			get { return (Com.ProxySignatureStatus)ProxySignatureStatus; }
		}

		public Com.RecipientResponseStatus RecipientResponseStatusValue
		{
			get { return (Com.RecipientResponseStatus)RecipientResponseStatus; }
		}

		public ReadonlyList InitialDocumentIdsList
		{
			get { return new ReadonlyList(InitialDocumentIds); }
		}

		public ReadonlyList SubordinateDocumentIdsList
		{
			get { return new ReadonlyList(SubordinateDocumentIds); }
		}

		public Com.DocumentType Type
		{
			get { return (Com.DocumentType)((int)DocumentType); }
			set { DocumentType = (DocumentType)((int)value); }
		}

		public Com.RevocationStatus DocumentRevocationStatus
		{
			get { return (Com.RevocationStatus)((int)RevocationStatus); }
			set { RevocationStatus = (RevocationStatus)((int)value); }
		}

		public ReadonlyList MetadataList
		{
			get { return new ReadonlyList(Metadata); }
		}

		public ReadonlyList LastOuterDocflowsList
		{
			get { return new ReadonlyList(LastOuterDocflows); }
		}
	}

	[ComVisible(true)]
	[Guid("242F2A55-E1FA-4D16-A916-15CD33F509C0")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRecipientReceiptMetadata))]
	public partial class RecipientReceiptMetadata : SafeComObject, IRecipientReceiptMetadata
	{
		public Com.GeneralReceiptStatus ReceiptStatusValue
		{
			get { return (Com.GeneralReceiptStatus)ReceiptStatus; }
		}
	}

	[ComVisible(true)]
	[Guid("02D8510E-50C4-4020-8C3D-D6926026CE8B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IConfirmationMetadata))]
	public partial class ConfirmationMetadata : SafeComObject, IConfirmationMetadata
	{
		public Com.GeneralReceiptStatus ReceiptStatusValue
		{
			get { return (Com.GeneralReceiptStatus)ReceiptStatus; }
		}

		public DateTime DateTime
		{
			get { return new DateTime(DateTimeTicks); }
		}
	}

	[ComVisible(true)]
	[Guid("40691B8D-46DF-40B7-88D5-66B6FF10A415")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IAmendmentRequestMetadata))]
	public partial class AmendmentRequestMetadata : SafeComObject, IAmendmentRequestMetadata
	{
		public Com.GeneralReceiptStatus ReceiptStatusValue
		{
			get { return (Com.GeneralReceiptStatus)ReceiptStatus; }
		}
	}

	[ComVisible(true)]
	[Guid("3DCE9776-CCFD-4089-902A-A7AC98E1CB1D")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IOrigin))]
	public partial class Origin : SafeComObject, IOrigin
	{
		public Com.MessageType MessageTypeValue
		{
			get { return (Com.MessageType)MessageType; }
		}
	}
}
