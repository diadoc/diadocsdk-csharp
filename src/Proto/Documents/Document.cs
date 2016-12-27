using System;
using System.Runtime.InteropServices;
using Diadoc.Api.Com;
using Diadoc.Api.Proto.Documents.AcceptanceCertificateDocument;

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

		Content Content { get; }
		NonformalizedDocument.NonformalizedDocumentMetadata NonformalizedDocumentMetadata { get; }
		InvoiceDocument.InvoiceMetadata InvoiceMetadata { get; }
		BilateralDocument.TrustConnectionRequestMetadata TrustConnectionRequestMetadata { get; }
		BilateralDocument.BasicDocumentMetadata Torg12Metadata { get; }
		InvoiceDocument.InvoiceRevisionMetadata InvoiceRevisionMetadata { get; }
		InvoiceDocument.InvoiceCorrectionMetadata InvoiceCorrectionMetadata { get; }
		InvoiceDocument.InvoiceCorrectionRevisionMetadata InvoiceCorrectionRevisionMetadata { get; }
		AcceptanceCertificateMetadata AcceptanceCertificateMetadata { get; }
		UnilateralDocument.ProformaInvoiceMetadata ProformaInvoiceMetadata { get; }
		BilateralDocument.BasicDocumentMetadata XmlTorg12Metadata { get; }
		BilateralDocument.BasicDocumentMetadata XmlAcceptanceCertificateMetadata { get; }
		BilateralDocument.PriceListMetadata PriceListMetadata { get; }
		NonformalizedDocument.NonformalizedDocumentMetadata PriceListAgreementMetadata { get; }
		NonformalizedDocument.NonformalizedDocumentMetadata CertificateRegistryMetadata { get; }
		BilateralDocument.BilateralDocumentMetadata ReconciliationActMetadata { get; }
		BilateralDocument.ContractMetadata ContractMetadata { get; }
		BilateralDocument.BasicDocumentMetadata Torg13Metadata { get; }
		UnilateralDocument.ServiceDetailsMetadata ServiceDetailsMetadata { get; }
		UniversalTransferDocument.UniversalTransferDocumentMetadata UniversalTransferDocumentMetadata { get; }
		UniversalTransferDocument.UniversalTransferDocumentRevisionMetadata UniversalTransferDocumentRevisionMetadata { get; }
		UniversalTransferDocument.UniversalCorrectionDocumentMetadata UniversalCorrectionDocumentMetadata { get; }
		UniversalTransferDocument.UniversalCorrectionDocumentRevisionMetadata UniversalCorrectionDocumentRevisionMetadata { get; }
		bool IsDeleted { get; }
		bool IsTest { get; }
		string DepartmentId { get; }
		string CustomDocumentId { get; }
		string FromDepartmentId { get; }
		string ToDepartmentId { get; }
		ResolutionStatus ResolutionStatus { get; }

		Com.RevocationStatus DocumentRevocationStatus { get; }

		DateTime SendTimestamp { get; }
		DateTime DeliveryTimestamp { get; }
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
	}
}
