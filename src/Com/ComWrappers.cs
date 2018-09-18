using System;
using System.Runtime.InteropServices;
using Diadoc.Api.Com;
using Diadoc.Api.Proto.Docflow;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;

namespace Diadoc.Api.Proto.Forwarding
{
	[ComVisible(true)]
	[Guid("1113F3EE-9492-4407-8A17-F0561B6EAD01")]
	public interface IGetForwardedDocumentsResponse
	{
		ReadonlyList ForwardedDocumentsList { get; }
	}

	[ComVisible(true)]
	[Guid("4794633C-A1F0-462E-A1DE-40E10E41F28A")]
	[ProgId("Diadoc.Api.GetForwardedDocumentsResponse")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IGetForwardedDocumentsResponse))]
	public partial class GetForwardedDocumentsResponse : SafeComObject, IGetForwardedDocumentsResponse
	{
		public ReadonlyList ForwardedDocumentsList
		{
			get { return new ReadonlyList(ForwardedDocuments); }
		}
	}
}

namespace Diadoc.Api.Proto.Forwarding
{
	[ComVisible(true)]
	[Guid("6B7B7B9E-EE23-4E39-BCC6-C0A720FDF9A2")]
	public interface IForwardedDocument
	{
		Timestamp ForwardTimestamp { get; }
		ForwardedDocumentId ForwardedDocumentId { get; }
		DocumentWithDocflow DocumentWithDocflow { get; }
	}

	[ComVisible(true)]
	[Guid("8AF406A7-1EC8-438F-B547-5F53153F2B69")]
	[ProgId("Diadoc.Api.ForwardedDocument")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IForwardedDocument))]
	public partial class ForwardedDocument : SafeComObject, IForwardedDocument
	{
	}
}

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("55644D7A-19C6-4D61-8059-262E8329B85E")]
	public interface ITimestamp
	{
		DateTime DateTime { get; set; }
	}

	[ComVisible(true)]
	[Guid("3C2DC1F3-00A1-4F40-9E50-12302E6F89AA")]
	[ProgId("Diadoc.Api.Timestamp")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (ITimestamp))]
	public partial class Timestamp : SafeComObject, ITimestamp
	{
		public DateTime DateTime
		{
			get { return new DateTime(Ticks, DateTimeKind.Utc); }
			set { Ticks = value.ToUniversalTime().Ticks; }
		}
	}
}

namespace Diadoc.Api.Proto.Forwarding
{
	[ComVisible(true)]
	[Guid("B8AB49D8-F3CE-44F8-9203-36FCA8F161ED")]
	public interface IForwardedDocumentId
	{
		string FromBoxId { get; set; }
		DocumentId DocumentId { get; set; }
		string ForwardEventId { get; set; }
	}

	[ComVisible(true)]
	[Guid("02033018-627C-4D07-B555-9482CAD0C2FB")]
	[ProgId("Diadoc.Api.ForwardedDocumentId")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IForwardedDocumentId))]
	public partial class ForwardedDocumentId : SafeComObject, IForwardedDocumentId
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("F26BCF18-20A1-48E4-ABC3-51B1B1B6A706")]
	public interface IDocumentWithDocflow
	{
		DocumentId DocumentId { get; }
		string LastEventId { get; }
		Timestamp LastEventTimestamp { get; }
		DocumentInfo DocumentInfo { get; }
		Docflow Docflow { get; }
		ReadonlyList InitialDocumentIdsList { get; }
		ReadonlyList SubordinateDocumentIdsList { get; }
		ReadonlyList ForwardDocumentEventsList { get; }
	}

	[ComVisible(true)]
	[Guid("B55FB545-D3B2-43FC-B720-AA84A7ACA51E")]
	[ProgId("Diadoc.Api.DocumentWithDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IDocumentWithDocflow))]
	public partial class DocumentWithDocflow : SafeComObject, IDocumentWithDocflow
	{
		public ReadonlyList InitialDocumentIdsList
		{
			get { return new ReadonlyList(InitialDocumentIds); }
		}

		public ReadonlyList SubordinateDocumentIdsList
		{
			get { return new ReadonlyList(SubordinateDocumentIds); }
		}

		public ReadonlyList ForwardDocumentEventsList
		{
			get { return new ReadonlyList(ForwardDocumentEvents); }
		}
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("DCBF2036-598C-4443-958D-508AE27B8F53")]
	public interface IDocumentInfo
	{
		Com.DocumentType DocumentTypeValue { get; }
		Com.DocumentDirection DocumentDirectionValue { get; }
		bool IsTest { get; }
		string CustomDocumentId { get; }
		string FromDepartmentId { get; }
		string ToDepartmentId { get; }
		string CounteragentBoxId { get; }
		DocumentDateAndNumber DocumentDateAndNumber { get; }
		BasicDocumentInfo BasicDocumentInfo { get; }
		InvoiceDocumentInfo InvoiceInfo { get; }
		InvoiceCorrectionDocumentInfo InvoiceCorrectionInfo { get; }
		PriceListDocumentInfo PriceListInfo { get; }
		ContractDocumentInfo ContractInfo { get; }
		SupplementaryAgreementDocumentInfo SupplementaryAgreementInfo { get; }
		UniversalTransferDocumentInfo UniversalTransferDocumentInfo { get; }
	}

	[ComVisible(true)]
	[Guid("7C393786-09A6-4586-9481-3158EC0867D8")]
	[ProgId("Diadoc.Api.DocumentInfo")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IDocumentInfo))]
	public partial class DocumentInfo : SafeComObject, IDocumentInfo
	{
		public Com.DocumentType DocumentTypeValue
		{
			get { return (Com.DocumentType) DocumentType; }
			set { DocumentType = (DocumentType) value; }
		}

		public Com.DocumentDirection DocumentDirectionValue
		{
			get { return (Com.DocumentDirection) DocumentDirection; }
			set { DocumentDirection = (DocumentDirection) value; }
		}
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("9D447F4E-E3AA-4900-B989-1518AD251353")]
	public interface IDocumentDateAndNumber
	{
		string DocumentDate { get; }
		string DocumentNumber { get; }
	}

	[ComVisible(true)]
	[Guid("094AD617-530F-4C76-8098-2C34A0C75B4E")]
	[ProgId("Diadoc.Api.DocumentDateAndNumber")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IDocumentDateAndNumber))]
	public partial class DocumentDateAndNumber : SafeComObject, IDocumentDateAndNumber
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("5F94C6F0-084B-44E4-9B56-E982011707D6")]
	public interface IBasicDocumentInfo
	{
		string Total { get; }
		bool NoVat { get; }
		string Vat { get; }
		string Grounds { get; }
		DocumentDateAndNumber RevisionDateAndNumber { get; }
	}

	[ComVisible(true)]
	[Guid("E16EDCDC-CEFA-4C86-B5A3-82AA7187B0F6")]
	[ProgId("Diadoc.Api.BasicDocumentInfo")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IBasicDocumentInfo))]
	public partial class BasicDocumentInfo : SafeComObject, IBasicDocumentInfo
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("79584194-FF7C-45EA-B6F7-689470833FA9")]
	public interface IInvoiceDocumentInfo
	{
		string Total { get; }
		string Vat { get; }
		int CurrencyCode { get; }
		DocumentDateAndNumber OriginalInvoiceDateAndNumber { get; }
	}

	[ComVisible(true)]
	[Guid("0B6F4885-B722-450C-A0C5-2BC580800423")]
	[ProgId("Diadoc.Api.InvoiceDocumentInfo")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IInvoiceDocumentInfo))]
	public partial class InvoiceDocumentInfo : SafeComObject, IInvoiceDocumentInfo
	{
	}

	[ComVisible(true)]
	[Guid("C3780B81-50E3-4AF3-9B0D-83A71BC052FE")]
	public interface IUniversalTransferDocumentInfo
	{
		string Total { get; }
		string Vat { get; }
		int CurrencyCode { get; }
		string Grounds { get; }
		Com.FunctionType Function { get; }
		DocumentDateAndNumber OriginalDocumentDateAndNumber { get; }
	}

	[ComVisible(true)]
	[Guid("E1BCADF2-7D17-444B-BEC3-6E01A30F00D1")]
	[ProgId("Diadoc.Api.UniversalTransferDocumentInfo")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IUniversalTransferDocumentInfo))]
	public partial class UniversalTransferDocumentInfo : SafeComObject, IUniversalTransferDocumentInfo
	{
		Com.FunctionType IUniversalTransferDocumentInfo.Function
		{
			get { return (Com.FunctionType) Function; }
		}
	}

	[ComVisible(true)]
	[Guid("41F28B5A-0B9D-4322-BA86-4D25F5307B94")]
	public interface IUniversalCorrectionDocumentInfo
	{
		string TotalInc { get; }
		string TotalDec { get; }
		string VatInc { get; }
		string VatDec { get; }
		int CurrencyCode { get; }
		string Grounds { get; }
		Com.FunctionType Function { get; }
		DocumentDateAndNumber OriginalDocumentDateAndNumber { get; }
		DocumentDateAndNumber OriginalDocumentRevisionDateAndNumber { get; }
		DocumentDateAndNumber OriginalDocumentCorrectionDateAndNumber { get; }
	}

	[ComVisible(true)]
	[Guid("CB5B366A-1074-4002-B925-D5B68CCD687B")]
	[ProgId("Diadoc.Api.UniversalCorrectionDocumentInfo")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUniversalCorrectionDocumentInfo))]
	public partial class UniversalCorrectionDocumentInfo : SafeComObject, IUniversalCorrectionDocumentInfo
	{
		Com.FunctionType IUniversalCorrectionDocumentInfo.Function
		{
			get { return (Com.FunctionType) Function; }
		}
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("185B0C2F-C73A-4A9F-9CE1-E7069BEF1B0A")]
	public interface IInvoiceCorrectionDocumentInfo
	{
		string TotalInc { get; }
		string TotalDec { get; }
		string VatInc { get; }
		string VatDec { get; }
		int CurrencyCode { get; }
		DocumentDateAndNumber OriginalInvoiceDateAndNumber { get; }
		DocumentDateAndNumber OriginalInvoiceRevisionDateAndNumber { get; }
		DocumentDateAndNumber OriginalInvoiceCorrectionDateAndNumber { get; }
	}

	[ComVisible(true)]
	[Guid("C0491E71-569B-4FEC-BCC4-F173B3498B49")]
	[ProgId("Diadoc.Api.InvoiceCorrectionDocumentInfo")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IInvoiceCorrectionDocumentInfo))]
	public partial class InvoiceCorrectionDocumentInfo : SafeComObject, IInvoiceCorrectionDocumentInfo
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("0213EAD2-E0E1-4D52-BE91-AF2D7175E0A1")]
	public interface IPriceListDocumentInfo
	{
		string PriceListEffectiveDate { get; }
		DocumentDateAndNumber ContractDocumentDateAndNumber { get; }
	}

	[ComVisible(true)]
	[Guid("6B888C27-7876-4AF2-925C-3974F2F1C038")]
	[ProgId("Diadoc.Api.PriceListDocumentInfo")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IPriceListDocumentInfo))]
	public partial class PriceListDocumentInfo : SafeComObject, IPriceListDocumentInfo
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("4C676D38-49DB-442F-8698-565CEFA48B09")]
	public interface IContractDocumentInfo
	{
		string ContractPrice { get; }
		string ContractType { get; }
	}

	[ComVisible(true)]
	[Guid("ED012FCB-82F2-4D14-B96D-1DE06AE1D061")]
	[ProgId("Diadoc.Api.ContractDocumentInfo")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IContractDocumentInfo))]
	public partial class ContractDocumentInfo : SafeComObject, IContractDocumentInfo
	{
	}

	[ComVisible(true)]
	[Guid("55063C2D-4049-4215-BEDE-E8FE964892A7")]
	public interface ISupplementaryAgreementDocumentInfo
	{
		string Total { get; }
		string ContractType { get; }
		string ContractNumber { get; }
		string ContractDate { get; }
	}

	[ComVisible(true)]
	[Guid("7FAB28EA-F73B-4B1B-9F59-269F354B80CD")]
	[ProgId("Diadoc.Api.SupplementaryAgreementDocumentInfo")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISupplementaryAgreementDocumentInfo))]
	public partial class SupplementaryAgreementDocumentInfo : SafeComObject, ISupplementaryAgreementDocumentInfo
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("647ED83E-00BE-4451-86E3-F4C2E0C8930B")]
	public interface IDocflow
	{
		bool IsFinished { get; }
		SignedAttachment DocumentAttachment { get; }
		string DepartmentId { get; }
		bool DocumentIsDeleted { get; }
		DocflowStatus DocflowStatus { get; }
		Timestamp SendTimestamp { get; }
		Timestamp DeliveryTimestamp { get; }
		InboundInvoiceDocflow InboundInvoiceDocflow { get; }
		OutboundInvoiceDocflow OutboundInvoiceDocflow { get; }
		XmlBilateralDocflow XmlBilateralDocflow { get; }
		BilateralDocflow BilateralDocflow { get; }
		UnilateralDocflow UnilateralDocflow { get; }
		RevocationDocflow RevocationDocflow { get; }
		ResolutionDocflow ResolutionDocflow { get; }
		bool CanDocumentBeRevokedUnilaterallyBySender { get; }
		string PacketId { get; }
		ReadonlyList CustomDataList { get; }
		InboundUniversalTransferDocumentDocflow InboundUniversalTransferDocumentDocflow { get; }
		OutboundUniversalTransferDocumentDocflow OutboundUniversalTransferDocumentDocflow { get; }
		RoamingNotification RoamingNotification { get; }
	}

	[ComVisible(true)]
	[Guid("ADF03347-3A84-4A6F-A997-1518AD08CE22")]
	[ProgId("Diadoc.Api.Docflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IDocflow))]
	public partial class Docflow : SafeComObject, IDocflow
	{
		public ReadonlyList CustomDataList
		{
			get { return new ReadonlyList(CustomData); }
		}
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("0AF10771-13CE-426B-8E69-59356CEB3FE3")]
	public interface ISignedAttachment
	{
		Attachment Attachment { get; }
		Signature Signature { get; }
		Entity Comment { get; }
	}

	[ComVisible(true)]
	[Guid("4ECB31F8-3CB3-4957-B304-1D989D52C5C6")]
	[ProgId("Diadoc.Api.SignedAttachment")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (ISignedAttachment))]
	public partial class SignedAttachment : SafeComObject, ISignedAttachment
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("328BCD8F-F2AC-493C-BA74-FD6CF9703309")]
	public interface IAttachment
	{
		Entity Entity { get; }
		string AttachmentFilename { get; }
		string DisplayFilename { get; }
	}

	[ComVisible(true)]
	[Guid("234E11E7-CDD5-4BB1-8C90-FE7CF274443B")]
	[ProgId("Diadoc.Api.Attachment")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IAttachment))]
	public partial class Attachment : SafeComObject, IAttachment
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("150A67C9-F3E6-4A6B-9E37-602A7EC25853")]
	public interface IEntity
	{
		string EntityId { get; }
		Timestamp CreationTimestamp { get; }
		Content Content { get; }
	}

	[ComVisible(true)]
	[Guid("0C3FF695-070C-40C5-BC1C-61E60EBDB691")]
	[ProgId("Diadoc.Api.Entity")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IEntity))]
	public partial class Entity : SafeComObject, IEntity
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("D94178C6-D939-4C84-997A-0E92D18159E5")]
	public interface ISignature
	{
		Entity Entity { get; }
		string SignerBoxId { get; }
		string SignerDepartmentId { get; }
		bool IsValid { get; }
		SignatureVerificationResult VerificationResult { get; }
	}

	[ComVisible(true)]
	[Guid("FD195FDF-7EBC-4C04-B381-536E11E39942")]
	[ProgId("Diadoc.Api.Signature")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (ISignature))]
	public partial class Signature : SafeComObject, ISignature
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("221D02AA-174B-41A8-995F-7A0537DCCE4F")]
	public interface IDocflowStatus
	{
		DocflowStatusModel PrimaryStatus { get; }
		DocflowStatusModel SecondaryStatus { get; }
	}

	[ComVisible(true)]
	[Guid("99350250-1589-43B1-A8D4-4B73435CA9EA")]
	[ProgId("Diadoc.Api.DocflowStatus")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IDocflowStatus))]
	public partial class DocflowStatus : SafeComObject, IDocflowStatus
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("C5410118-D81E-4147-B29B-06892BFEA627")]
	public interface IDocflowStatusModel
	{
		Com.DocflowStatusSeverity SeverityValue { get; }
		string StatusText { get; }
		string StatusHint { get; }
	}

	[ComVisible(true)]
	[Guid("1F5ACD28-A357-46DB-9F7E-812130F17E2E")]
	[ProgId("Diadoc.Api.DocflowStatusModel")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IDocflowStatusModel))]
	public partial class DocflowStatusModel : SafeComObject, IDocflowStatusModel
	{
		public Com.DocflowStatusSeverity SeverityValue
		{
			get { return (Com.DocflowStatusSeverity) Severity; }
			set { Severity = (DocflowStatusSeverity) value; }
		}
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("5AC1A212-0F14-4C89-BBB4-462F8168F11D")]
	public interface IInboundInvoiceDocflow
	{
		bool IsFinished { get; }
		InboundInvoiceReceiptDocflow ReceiptDocflow { get; }
		InvoiceConfirmationDocflow ConfirmationDocflow { get; }
		InvoiceCorrectionRequestDocflow CorrectionRequestDocflow { get; }
		Timestamp ConfirmationTimestamp { get; }
		bool IsAmendmentRequested { get; }
		bool IsRevised { get; }
		bool IsCorrected { get; }
	}

	[ComVisible(true)]
	[Guid("60F5617F-20AC-457B-A42B-D263AFDED386")]
	[ProgId("Diadoc.Api.InboundInvoiceDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IInboundInvoiceDocflow))]
	public partial class InboundInvoiceDocflow : SafeComObject, IInboundInvoiceDocflow
	{
	}

	[ComVisible(true)]
	[Guid("672B6A0B-D012-4A11-BCD2-919829AA10C7")]
	public interface IInboundUniversalTransferDocumentDocflow
	{
		bool IsFinished { get; }
		InboundInvoiceReceiptDocflow ReceiptDocflow { get; }
		InvoiceConfirmationDocflow ConfirmationDocflow { get; }
		InvoiceCorrectionRequestDocflow CorrectionRequestDocflow { get; }
		Timestamp ConfirmationTimestamp { get; }
		bool IsAmendmentRequested { get; }
		bool IsRevised { get; }
		bool IsCorrected { get; }
		BuyerTitleDocflow BuyerTitleDocflow { get; }
		RecipientSignatureRejectionDocflow RecipientSignatureRejectionDocflow { get; }
		bool IsReceiptRequested { get; }
		bool IsRecipientSignatureRequested { get; }
		bool IsDocumentSignedByRecipient { get; }
		bool IsDocumentRejectedByRecipient { get; }
		bool CanDocumentBeReceipted { get; }
		bool CanDocumentBeSignedOrRejectedByRecipient { get; }
	}

	[ComVisible(true)]
	[Guid("85EC89E0-C8BD-4AE1-916B-8D786D1AB817")]
	[ProgId("Diadoc.Api.InboundUniversalTransferDocumentDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IInboundUniversalTransferDocumentDocflow))]
	public partial class InboundUniversalTransferDocumentDocflow : SafeComObject, IInboundUniversalTransferDocumentDocflow
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("9FFA0649-22C7-43E8-923F-9C91C20C1E10")]
	public interface IInboundInvoiceReceiptDocflow
	{
		bool IsFinished { get; }
		SignedAttachment ReceiptAttachment { get; }
		InvoiceConfirmationDocflow ConfirmationDocflow { get; }
	}

	[ComVisible(true)]
	[Guid("C1B645F7-B8D7-46B5-B6B0-A3995495B4E7")]
	[ProgId("Diadoc.Api.InboundInvoiceReceiptDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IInboundInvoiceReceiptDocflow))]
	public partial class InboundInvoiceReceiptDocflow : SafeComObject, IInboundInvoiceReceiptDocflow
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("DC930D33-82C9-4993-8A91-B3EC025D509E")]
	public interface IInvoiceConfirmationDocflow
	{
		bool IsFinished { get; }
		SignedAttachment ConfirmationAttachment { get; }
		ReceiptDocflow ReceiptDocflow { get; }
	}

	[ComVisible(true)]
	[Guid("E158F62A-7D1F-454C-8295-F9F3B3AB5D51")]
	[ProgId("Diadoc.Api.InvoiceConfirmationDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IInvoiceConfirmationDocflow))]
	public partial class InvoiceConfirmationDocflow : SafeComObject, IInvoiceConfirmationDocflow
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("530EE618-D7D5-4D6D-A0DA-E820CA809A9E")]
	public interface IReceiptDocflow
	{
		bool IsFinished { get; }
		SignedAttachment ReceiptAttachment { get; }
	}

	[ComVisible(true)]
	[Guid("93A89C35-C44A-4DC2-886F-82895411F74F")]
	[ProgId("Diadoc.Api.ReceiptDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IReceiptDocflow))]
	public partial class ReceiptDocflow : SafeComObject, IReceiptDocflow
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("64DBA66A-7905-40E6-B59B-8D2B7C6C03A2")]
	public interface IInvoiceCorrectionRequestDocflow
	{
		bool IsFinished { get; }
		SignedAttachment CorrectionRequestAttachment { get; }
		ReceiptDocflow ReceiptDocflow { get; }
	}

	[ComVisible(true)]
	[Guid("F433C739-B7AF-4ABE-A8FF-D3B7378CB369")]
	[ProgId("Diadoc.Api.InvoiceCorrectionRequestDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IInvoiceCorrectionRequestDocflow))]
	public partial class InvoiceCorrectionRequestDocflow : SafeComObject, IInvoiceCorrectionRequestDocflow
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("4EB3927B-A688-4E3D-A3B7-30FC285AE3EC")]
	public interface IOutboundInvoiceDocflow
	{
		bool IsFinished { get; }
		ReceiptDocflow ReceiptDocflow { get; }
		InvoiceConfirmationDocflow ConfirmationDocflow { get; }
		InvoiceCorrectionRequestDocflow CorrectionRequestDocflow { get; }
		Timestamp ConfirmationTimestamp { get; }
		bool IsAmendmentRequested { get; }
		bool IsRevised { get; }
		bool IsCorrected { get; }
		bool CanDocumentBeSignedBySender { get; }
	}

	[ComVisible(true)]
	[Guid("EE16D82C-7047-47C9-A306-B01C3BC43563")]
	[ProgId("Diadoc.Api.OutboundInvoiceDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IOutboundInvoiceDocflow))]
	public partial class OutboundInvoiceDocflow : SafeComObject, IOutboundInvoiceDocflow
	{
	}

	[ComVisible(true)]
	[Guid("C734468F-EBD5-4975-A213-FC08C1F2A84E")]
	public interface IOutboundUniversalTransferDocumentDocflow
	{
		bool IsFinished { get; }
		ReceiptDocflow ReceiptDocflow { get; }
		InvoiceConfirmationDocflow ConfirmationDocflow { get; }
		InvoiceCorrectionRequestDocflow CorrectionRequestDocflow { get; }
		Timestamp ConfirmationTimestamp { get; }
		bool IsAmendmentRequested { get; }
		bool IsRevised { get; }
		bool IsCorrected { get; }
		bool CanDocumentBeSignedBySender { get; }
		BuyerTitleDocflow BuyerTitleDocflow { get; }
		RecipientSignatureRejectionDocflow RecipientSignatureRejectionDocflow { get; }
		bool IsReceiptRequested { get; }
		bool IsRecipientSignatureRequested { get; }
		bool IsDocumentSignedByRecipient { get; }
		bool IsDocumentRejectedByRecipient { get; }
		bool CanDocumentBeReceipted { get; }
		bool CanDocumentBeSignedOrRejectedByRecipient { get; }
	}

	[ComVisible(true)]
	[Guid("7E45E673-18D4-4DD4-88E2-5149B04F13C7")]
	[ProgId("Diadoc.Api.OutboundUniversalTransferDocumentDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IOutboundUniversalTransferDocumentDocflow))]
	public partial class OutboundUniversalTransferDocumentDocflow : SafeComObject, IOutboundUniversalTransferDocumentDocflow
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("98304281-EA03-4358-A8DF-ACB72AAE3522")]
	public interface IXmlBilateralDocflow
	{
		bool IsFinished { get; }
		ReceiptDocflow ReceiptDocflow { get; }
		BuyerTitleDocflow BuyerTitleDocflow { get; }
		RecipientSignatureRejectionDocflow RecipientSignatureRejectionDocflow { get; }
		bool IsReceiptRequested { get; }
		bool IsDocumentSignedByRecipient { get; }
		bool IsDocumentRejectedByRecipient { get; }
		bool CanDocumentBeReceipted { get; }
		bool CanDocumentBeSignedBySender { get; }
		bool CanDocumentBeSignedOrRejectedByRecipient { get; }
	}

	[ComVisible(true)]
	[Guid("40A50529-25FE-40FD-8373-E9B97C5C3AF7")]
	[ProgId("Diadoc.Api.XmlBilateralDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IXmlBilateralDocflow))]
	public partial class XmlBilateralDocflow : SafeComObject, IXmlBilateralDocflow
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("9C7450C9-7D9C-423B-BCC9-F3C7DB7DC53C")]
	public interface IBuyerTitleDocflow
	{
		bool IsFinished { get; }
		SignedAttachment BuyerTitleAttachment { get; }
		Timestamp SendTimestamp { get; }
		Timestamp DeliveryTimestamp { get; }
	}

	[ComVisible(true)]
	[Guid("5B6150A9-2C34-4FF8-82EA-EC51CED31F00")]
	[ProgId("Diadoc.Api.BuyerTitleDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IBuyerTitleDocflow))]
	public partial class BuyerTitleDocflow : SafeComObject, IBuyerTitleDocflow
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("CBDF4B8E-EDDD-4A19-A303-8DCA75E46A9F")]
	public interface IRecipientSignatureRejectionDocflow
	{
		bool IsFinished { get; }
		SignedAttachment RecipientSignatureRejectionAttachment { get; }
		Timestamp DeliveryTimestamp { get; }
	}

	[ComVisible(true)]
	[Guid("CE41659A-58E3-4288-BA60-AE6EBB0304FB")]
	[ProgId("Diadoc.Api.RecipientSignatureRejectionDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IRecipientSignatureRejectionDocflow))]
	public partial class RecipientSignatureRejectionDocflow : SafeComObject, IRecipientSignatureRejectionDocflow
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("73EBAD0A-A4AB-4CD2-AD7A-6BC263686BF2")]
	public interface IBilateralDocflow
	{
		bool IsFinished { get; }
		ReceiptDocflow ReceiptDocflow { get; }
		RecipientSignatureDocflow RecipientSignatureDocflow { get; }
		RecipientSignatureRejectionDocflow RecipientSignatureRejectionDocflow { get; }
		bool IsReceiptRequested { get; }
		bool IsRecipientSignatureRequested { get; }
		bool IsDocumentSignedByRecipient { get; }
		bool IsDocumentRejectedByRecipient { get; }
		bool CanDocumentBeReceipted { get; }
		bool CanDocumentBeSignedBySender { get; }
		bool CanDocumentBeSignedOrRejectedByRecipient { get; }
	}

	[ComVisible(true)]
	[Guid("9F6B2F8B-CB5A-4890-B341-8FE154BEEE41")]
	[ProgId("Diadoc.Api.BilateralDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IBilateralDocflow))]
	public partial class BilateralDocflow : SafeComObject, IBilateralDocflow
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("127D25A0-C744-4435-BB8D-1973B68AD70E")]
	public interface IRecipientSignatureDocflow
	{
		bool IsFinished { get; }
		Signature RecipientSignature { get; }
		Timestamp DeliveryTimestamp { get; }
	}

	[ComVisible(true)]
	[Guid("64EBD561-A98F-49E9-8BD8-36F9976D11D2")]
	[ProgId("Diadoc.Api.RecipientSignatureDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IRecipientSignatureDocflow))]
	public partial class RecipientSignatureDocflow : SafeComObject, IRecipientSignatureDocflow
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("32D76640-A78B-483C-88A9-714E995D4343")]
	public interface IUnilateralDocflow
	{
		bool IsFinished { get; }
		ReceiptDocflow ReceiptDocflow { get; }
		bool IsReceiptRequested { get; }
		bool CanDocumentBeReceipted { get; }
		bool CanDocumentBeSignedBySender { get; }
	}

	[ComVisible(true)]
	[Guid("68C206EE-32E4-4966-BF5C-90D3E13631FC")]
	[ProgId("Diadoc.Api.UnilateralDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IUnilateralDocflow))]
	public partial class UnilateralDocflow : SafeComObject, IUnilateralDocflow
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("44359480-E95E-4D85-B7CA-4CFC6E99B44D")]
	public interface IRevocationDocflow
	{
		bool IsFinished { get; }
		SignedAttachment RevocationRequestAttachment { get; }
		RecipientSignatureDocflow RecipientSignatureDocflow { get; }
		RecipientSignatureRejectionDocflow RecipientSignatureRejectionDocflow { get; }
		string InitiatorBoxId { get; }
		bool IsRevocationAccepted { get; }
		bool IsRevocationRejected { get; }
	}

	[ComVisible(true)]
	[Guid("7C3176DE-5B61-4D2D-84B6-95F20074FA25")]
	[ProgId("Diadoc.Api.RevocationDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IRevocationDocflow))]
	public partial class RevocationDocflow : SafeComObject, IRevocationDocflow
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("0C2EA854-0B47-4B80-A89F-21F1C80DA072")]
	public interface IResolutionDocflow
	{
	}

	[ComVisible(true)]
	[Guid("E2C1F246-B758-40ED-A0B7-1AC1C469ADEC")]
	[ProgId("Diadoc.Api.ResolutionDocflow")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IResolutionDocflow))]
	public partial class ResolutionDocflow : SafeComObject, IResolutionDocflow
	{
	}
}

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("A25F7B09-3936-4880-B9E6-7CB0130B03D7")]
	public interface ICustomDataItem
	{
		string Key { get; }
		string Value { get; }
	}

	[ComVisible(true)]
	[Guid("E660345A-6C3B-48F8-9E39-C25AE5A50200")]
	[ProgId("Diadoc.Api.CustomDataItem")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (ICustomDataItem))]
	public partial class CustomDataItem : SafeComObject, ICustomDataItem
	{
	}
}

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("9250A6A1-72BB-47C5-9F0F-688B8A236B37")]
	public interface IForwardDocumentEvent
	{
		Timestamp Timestamp { get; }
		string ToBoxId { get; }
	}

	[ComVisible(true)]
	[Guid("D6399AD9-4BFD-4A06-AC64-0501E2CBCEA4")]
	[ProgId("Diadoc.Api.ForwardDocumentEvent")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IForwardDocumentEvent))]
	public partial class ForwardDocumentEvent : SafeComObject, IForwardDocumentEvent
	{
	}
}

namespace Diadoc.Api.Proto.Forwarding
{
	[ComVisible(true)]
	[Guid("B1F88165-F635-433E-A793-DC5587E9F2C5")]
	public interface IGetForwardedDocumentsRequest
	{
		void AddForwardedDocumentIdsItem([MarshalAs(UnmanagedType.IDispatch)] object item);
		bool InjectEntityContent { get; set; }
	}

	[ComVisible(true)]
	[Guid("4D3463C5-1A18-44F4-A747-1BD44BB85F1C")]
	[ProgId("Diadoc.Api.GetForwardedDocumentsRequest")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IGetForwardedDocumentsRequest))]
	public partial class GetForwardedDocumentsRequest : SafeComObject, IGetForwardedDocumentsRequest
	{
		public void AddForwardedDocumentIdsItem(object item)
		{
			ForwardedDocumentIds.Add((ForwardedDocumentId) item);
		}
	}
}

namespace Diadoc.Api.Proto.Forwarding
{
	[ComVisible(true)]
	[Guid("7FDDD1F0-45FD-42AA-B923-DCDA6851446C")]
	public interface IForwardDocumentResponse
	{
		Timestamp ForwardTimestamp { get; }
		ForwardedDocumentId ForwardedDocumentId { get; }
	}

	[ComVisible(true)]
	[Guid("F95227DA-901D-49DA-9935-AA984C788BC1")]
	[ProgId("Diadoc.Api.ForwardDocumentResponse")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IForwardDocumentResponse))]
	public partial class ForwardDocumentResponse : SafeComObject, IForwardDocumentResponse
	{
	}
}

namespace Diadoc.Api.Proto.Forwarding
{
	[ComVisible(true)]
	[Guid("BABAF248-9899-4D8D-93C8-6B48D57A0C40")]
	public interface IForwardDocumentRequest
	{
		string ToBoxId { get; set; }
		DocumentId DocumentId { get; set; }
	}

	[ComVisible(true)]
	[Guid("8A48BDE1-DBF6-4CA3-9770-0D47E14C985F")]
	[ProgId("Diadoc.Api.ForwardDocumentRequest")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IForwardDocumentRequest))]
	public partial class ForwardDocumentRequest : SafeComObject, IForwardDocumentRequest
	{
	}
}

namespace Diadoc.Api.Proto.Forwarding
{
	[ComVisible(true)]
	[Guid("D030B8B1-9ED9-4D18-BDCD-072D83AB8E54")]
	public interface IGetForwardedDocumentEventsResponse
	{
		int TotalCount { get; }
		ReadonlyList EventsList { get; }
	}

	[ComVisible(true)]
	[Guid("8C1A949E-9ABF-47CC-B913-FBB910649449")]
	[ProgId("Diadoc.Api.GetForwardedDocumentEventsResponse")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IGetForwardedDocumentEventsResponse))]
	public partial class GetForwardedDocumentEventsResponse : SafeComObject, IGetForwardedDocumentEventsResponse
	{
		public ReadonlyList EventsList
		{
			get { return new ReadonlyList(Events); }
		}
	}
}

namespace Diadoc.Api.Proto.Forwarding
{
	[ComVisible(true)]
	[Guid("4DD2087F-278C-4309-B3A1-2D6A9ABD79CB")]
	public interface IForwardedDocumentEvent
	{
		Timestamp Timestamp { get; }
		ForwardedDocumentId ForwardedDocumentId { get; }
		byte[] IndexKey { get; }
		ForwardedDocument ForwardedDocument { get; }
	}

	[ComVisible(true)]
	[Guid("189DE681-0D16-4DAF-9930-1BF7E11EE126")]
	[ProgId("Diadoc.Api.ForwardedDocumentEvent")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IForwardedDocumentEvent))]
	public partial class ForwardedDocumentEvent : SafeComObject, IForwardedDocumentEvent
	{
	}
}

namespace Diadoc.Api.Proto.Forwarding
{
	[ComVisible(true)]
	[Guid("D1259DEA-501E-4DE5-8572-6899F1C89C11")]
	public interface IGetForwardedDocumentEventsRequest
	{
		TimeBasedFilter Filter { get; set; }
		byte[] AfterIndexKey { get; set; }
		bool PopulateForwardedDocuments { get; set; }
		bool InjectEntityContent { get; set; }
	}

	[ComVisible(true)]
	[Guid("B0862442-5AC4-4580-B49F-96F1F332AD58")]
	[ProgId("Diadoc.Api.GetForwardedDocumentEventsRequest")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IGetForwardedDocumentEventsRequest))]
	public partial class GetForwardedDocumentEventsRequest : SafeComObject, IGetForwardedDocumentEventsRequest
	{
	}
}

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("433D80C8-4B3D-406F-A8F4-1358A1BE7FC3")]
	public interface ITimeBasedFilter
	{
		Timestamp FromTimestamp { get; set; }
		Timestamp ToTimestamp { get; set; }
		Com.SortDirection SortDirectionValue { get; set; }
	}

	[ComVisible(true)]
	[Guid("203095AA-CB81-462D-BC38-6D0417BB4D65")]
	[ProgId("Diadoc.Api.TimeBasedFilter")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (ITimeBasedFilter))]
	public partial class TimeBasedFilter : SafeComObject, ITimeBasedFilter
	{
		public Com.SortDirection SortDirectionValue
		{
			get { return (Com.SortDirection) SortDirection; }
			set { SortDirection = (SortDirection) value; }
		}
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("976FD0C8-8B3C-46E0-A4AD-67DFBBEFB58F")]
	public interface IGetDocflowBatchResponse
	{
		ReadonlyList DocumentsList { get; }
	}

	[ComVisible(true)]
	[Guid("B5C96AC5-8943-4D41-ABD8-06CC02FCEE9C")]
	[ProgId("Diadoc.Api.GetDocflowBatchResponse")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IGetDocflowBatchResponse))]
	public partial class GetDocflowBatchResponse : SafeComObject, IGetDocflowBatchResponse
	{
		public ReadonlyList DocumentsList
		{
			get { return new ReadonlyList(Documents); }
		}
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("C49FF834-4939-4F07-8BC1-D02EF7D70947")]
	public interface IGetDocflowBatchRequest
	{
		void AddRequestsItem([MarshalAs(UnmanagedType.IDispatch)] object item);
	}

	[ComVisible(true)]
	[Guid("1FCDA80E-6858-46AA-83FD-C71D2698CDEC")]
	[ProgId("Diadoc.Api.GetDocflowBatchRequest")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IGetDocflowBatchRequest))]
	public partial class GetDocflowBatchRequest : SafeComObject, IGetDocflowBatchRequest
	{
		public void AddRequestsItem(object item)
		{
			Requests.Add((GetDocflowRequest) item);
		}
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("1525CC0A-B22E-4624-9C49-393C8CCC2E03")]
	public interface IGetDocflowRequest
	{
		DocumentId DocumentId { get; set; }
		string LastEventId { get; set; }
		bool InjectEntityContent { get; set; }
	}

	[ComVisible(true)]
	[Guid("BB8A4A86-BE9D-418D-AC83-007AFA5ABE55")]
	[ProgId("Diadoc.Api.GetDocflowRequest")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IGetDocflowRequest))]
	public partial class GetDocflowRequest : SafeComObject, IGetDocflowRequest
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("829E2343-D0D1-4A9F-82C9-5E7D059D8CA7")]
	public interface IGetDocflowEventsResponse
	{
		int TotalCount { get; }
		ReadonlyList EventsList { get; }
	}

	[ComVisible(true)]
	[Guid("1D94433D-37C6-4504-A91D-1408783F0AD2")]
	[ProgId("Diadoc.Api.GetDocflowEventsResponse")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IGetDocflowEventsResponse))]
	public partial class GetDocflowEventsResponse : SafeComObject, IGetDocflowEventsResponse
	{
		public ReadonlyList EventsList
		{
			get { return new ReadonlyList(Events); }
		}
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("48DA813C-8E8F-4EAF-B39A-23A448A93127")]
	public interface IDocflowEvent
	{
		string EventId { get; }
		Timestamp Timestamp { get; }
		DocumentId DocumentId { get; }
		byte[] IndexKey { get; }
		DocumentWithDocflow Document { get; }
		string PreviousEventId { get; }
		DocumentWithDocflow PreviousDocumentState { get; }
	}

	[ComVisible(true)]
	[Guid("A7C8A9B5-E7E8-4A78-ADAA-B0FE2F6EADDD")]
	[ProgId("Diadoc.Api.DocflowEvent")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IDocflowEvent))]
	public partial class DocflowEvent : SafeComObject, IDocflowEvent
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("7B6E1E2D-69CC-43E3-9DAA-2A4848302BC0")]
	public interface IGetDocflowEventsRequest
	{
		TimeBasedFilter Filter { get; set; }
		byte[] AfterIndexKey { get; set; }
		bool PopulateDocuments { get; set; }
		bool InjectEntityContent { get; set; }
		bool PopulatePreviousDocumentStates { get; set; }
	}

	[ComVisible(true)]
	[Guid("1E46CEAF-98FF-4C17-AAB6-BDE6AB0B2D4F")]
	[ProgId("Diadoc.Api.GetDocflowEventsRequest")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IGetDocflowEventsRequest))]
	public partial class GetDocflowEventsRequest : SafeComObject, IGetDocflowEventsRequest
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("7ACE3BB9-F064-4712-A159-1AEF3B1D6264")]
	public interface ISearchDocflowsResponse
	{
		ReadonlyList DocumentsList { get; }
		bool HaveMoreDocuments { get; }
	}

	[ComVisible(true)]
	[Guid("1B8530CB-B699-4E04-9461-D71C78BE1841")]
	[ProgId("Diadoc.Api.SearchDocflowsResponse")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (ISearchDocflowsResponse))]
	public partial class SearchDocflowsResponse : SafeComObject, ISearchDocflowsResponse
	{
		public ReadonlyList DocumentsList
		{
			get { return new ReadonlyList(Documents); }
		}
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("69D5E9AC-67DA-4264-ADB7-043CF7F3FBE2")]
	public interface ISearchDocflowsRequest
	{
		string QueryString { get; set; }
		int Count { get; set; }
		int FirstIndex { get; set; }
		Com.SearchScope ScopeValue { get; set; }
		bool InjectEntityContent { get; set; }
	}

	[ComVisible(true)]
	[Guid("97607201-3A41-48E5-A000-E2B367B13308")]
	[ProgId("Diadoc.Api.SearchDocflowsRequest")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (ISearchDocflowsRequest))]
	public partial class SearchDocflowsRequest : SafeComObject, ISearchDocflowsRequest
	{
		public Com.SearchScope ScopeValue
		{
			get { return (Com.SearchScope) Scope; }
			set { Scope = (SearchScope) value; }
		}
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("BF61856F-C562-472D-9EF0-44B8A018770B")]
	public interface IGetDocflowsByPacketIdResponse
	{
		ReadonlyList DocumentsList { get; }
		byte[] NextPageIndexKey { get; }
	}

	[ComVisible(true)]
	[Guid("FD538009-6470-4D5F-B3D3-FC64F72FB2F5")]
	[ProgId("Diadoc.Api.GetDocflowsByPacketIdResponse")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IGetDocflowsByPacketIdResponse))]
	public partial class GetDocflowsByPacketIdResponse : SafeComObject, IGetDocflowsByPacketIdResponse
	{
		public ReadonlyList DocumentsList
		{
			get { return new ReadonlyList(Documents); }
		}
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("0207BCDE-076D-4D42-B07F-B6614A2F103C")]
	public interface IFetchedDocument
	{
		DocumentWithDocflow Document { get; }
		byte[] IndexKey { get; }
	}

	[ComVisible(true)]
	[Guid("80C988B1-8EDA-4F72-90EF-4748F01C2146")]
	[ProgId("Diadoc.Api.FetchedDocument")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IFetchedDocument))]
	public partial class FetchedDocument : SafeComObject, IFetchedDocument
	{
	}
}

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("6FF7892C-AD1D-45EA-B2F6-96A48780F00E")]
	public interface IGetDocflowsByPacketIdRequest
	{
		string PacketId { get; set; }
		int Count { get; set; }
		bool InjectEntityContent { get; set; }
		byte[] AfterIndexKey { get; set; }
	}

	[ComVisible(true)]
	[Guid("49D9EE81-0C32-439D-9D57-1A6529FA0F7B")]
	[ProgId("Diadoc.Api.GetDocflowsByPacketIdRequest")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IGetDocflowsByPacketIdRequest))]
	public partial class GetDocflowsByPacketIdRequest : SafeComObject, IGetDocflowsByPacketIdRequest
	{
	}
}

namespace Diadoc.Api.Proto.Events
{
	[ComVisible(true)]
	[Guid("8D518D3A-2057-4D16-ADBC-BF858D7291DA")]
	public interface IPrepareDocumentsToSignResponse
	{
		ReadonlyList DocumentPatchedContentsList { get; }
	}

	[ComVisible(true)]
	[Guid("6569AF75-09C4-49DD-8D7B-CD86AD63975A")]
	[ProgId("Diadoc.Api.PrepareDocumentsToSignResponse")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IPrepareDocumentsToSignResponse))]
	public partial class PrepareDocumentsToSignResponse : SafeComObject, IPrepareDocumentsToSignResponse
	{
		public ReadonlyList DocumentPatchedContentsList
		{
			get { return new ReadonlyList(DocumentPatchedContents); }
		}
	}
}

namespace Diadoc.Api.Proto.Events
{
	[ComVisible(true)]
	[Guid("38107920-FF2A-475D-9A18-23C680C35860")]
	public interface IDocumentPatchedContent
	{
		DocumentId DocumentId { get; }
		string PatchedContentId { get; }
		byte[] Content { get; }
	}

	[ComVisible(true)]
	[Guid("579C229A-855E-45F3-96F5-3F7606DE35A3")]
	[ProgId("Diadoc.Api.DocumentPatchedContent")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IDocumentPatchedContent))]
	public partial class DocumentPatchedContent : SafeComObject, IDocumentPatchedContent
	{
	}
}

namespace Diadoc.Api.Proto.Events
{
	[ComVisible(true)]
	[Guid("651F386C-B000-454E-959C-DEF645CCFA75")]
	public interface IPrepareDocumentsToSignRequest
	{
		string BoxId { get; set; }
		void AddDraftDocumentsItem([MarshalAs(UnmanagedType.IDispatch)] object item);
		void AddDocumentsItem([MarshalAs(UnmanagedType.IDispatch)] object item);
	}

	[ComVisible(true)]
	[Guid("67114699-DB39-4F63-9CD3-8BA82223FDC3")]
	[ProgId("Diadoc.Api.PrepareDocumentsToSignRequest")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IPrepareDocumentsToSignRequest))]
	public partial class PrepareDocumentsToSignRequest : SafeComObject, IPrepareDocumentsToSignRequest
	{
		public void AddDraftDocumentsItem(object item)
		{
			DraftDocuments.Add((DraftDocumentToPatch) item);
		}

		public void AddDocumentsItem(object item)
		{
			Documents.Add((DocumentToPatch) item);
		}
	}
}

namespace Diadoc.Api.Proto.Events
{
	[ComVisible(true)]
	[Guid("788C1747-9782-4D38-8EB3-79F767912840")]
	public interface IDraftDocumentToPatch
	{
		DocumentId DocumentId { get; set; }
		string ToBoxId { get; set; }
		Signer Signer { get; set; }
	}

	[ComVisible(true)]
	[Guid("6F53996E-1D99-4934-B026-55D24E8E2434")]
	[ProgId("Diadoc.Api.DraftDocumentToPatch")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IDraftDocumentToPatch))]
	public partial class DraftDocumentToPatch : SafeComObject, IDraftDocumentToPatch
	{
	}
}

namespace Diadoc.Api.Proto.Events
{
	[ComVisible(true)]
	[Guid("E2C853E5-EFD7-4BD5-AC0D-C48B8B3FDC0D")]
	public interface IDocumentToPatch
	{
		DocumentId DocumentId { get; set; }
		Signer Signer { get; set; }
	}

	[ComVisible(true)]
	[Guid("7A2CF092-042C-4492-A577-9A816ED330A7")]
	[ProgId("Diadoc.Api.DocumentToPatch")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IDocumentToPatch))]
	public partial class DocumentToPatch : SafeComObject, IDocumentToPatch
	{
	}
}

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("1550BF67-A1C4-4B5E-B126-75347B98FB1E")]
	public interface IUser
	{
		string Id { get; }
		string LastName { get; }
		string FirstName { get; }
		string MiddleName { get; }
		ReadonlyList CloudCertificatesList { get; }
	}

	[ComVisible(true)]
	[Guid("3F868982-609E-4742-9D1B-222670349AFB")]
	[ProgId("Diadoc.Api.User")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IUser))]
	public partial class User : SafeComObject, IUser
	{
		public ReadonlyList CloudCertificatesList
		{
			get { return new ReadonlyList(CloudCertificates); }
		}
	}

	[ComVisible(true)]
	[Guid("13652788-44BC-43F0-939A-35A4B598D68E")]
	public interface IUserV2
	{
		string UserId { get; }
		string Login { get; }
		FullName FullName { get; }
		bool IsRegistered { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.UserV2")]
	[Guid("E46BDA02-92A9-459F-866F-E38D488F3933")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUserV2))]
	public partial class UserV2 : SafeComObject, IUserV2
	{
	}

	[ComVisible(true)]
	[Guid("B4B591A6-3B03-4B3A-83D0-F57734F4EE9B")]
	public interface IFullName
	{
		string LastName { get; set; }
		string FirstName { get; set; }
		string MiddleName { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.FullName")]
	[Guid("54793D4C-02D2-4F8D-B13B-0BB8A88A0907")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IFullName))]
	public partial class FullName : SafeComObject, IFullName
	{
	}
}

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("5F913CA8-8791-443E-A6BC-05265848184A")]
	public interface ICertificateInfo
	{
		string Thumbprint { get; }
		DateTime ValidFromDateTime { get; }
		DateTime ValidToDateTime { get; }
	}

	[ComVisible(true)]
	[Guid("5E7C74F3-E1CA-4B07-B711-20AE2772C332")]
	[ProgId("Diadoc.Api.CertificateInfo")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (ICertificateInfo))]
	public partial class CertificateInfo : SafeComObject, ICertificateInfo
	{
		public DateTime ValidFromDateTime
		{
			get { return new DateTime(ValidFrom, DateTimeKind.Utc); }
		}

		public DateTime ValidToDateTime
		{
			get { return new DateTime(ValidTo, DateTimeKind.Utc); }
		}
	}
}

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("A729FCEC-16C2-4F76-B73E-CB9E1DD282EC")]
	public interface IAcquireCounteragentRequest
	{
		string OrgId { get; set; }
		string Inn { get; set; }
		string MessageToCounteragent { get; set; }
		InvitationDocument InvitationDocument { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.AcquireCounteragentRequest")]
	[Guid("1B48C883-8494-4EE7-984C-FCCA9BAE200B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IAcquireCounteragentRequest))]
	public partial class AcquireCounteragentRequest : SafeComObject, IAcquireCounteragentRequest
	{
	}

	[ComVisible(true)]
	[Guid("46FC5024-1BB5-414B-9C9E-3005C2441766")]
	public interface IInvitationDocument
	{
		SignedContent SignedContent { get; set; }
		string FileName { get; set; }
		bool SignatureRequested { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.InvitationDocument")]
	[Guid("713BBB50-5009-473F-8A3B-1F5194F77A2B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IInvitationDocument))]
	public partial class InvitationDocument : SafeComObject, IInvitationDocument
	{
	}

	[ComVisible(true)]
	[Guid("6CEE1BC1-9BD0-4F05-9AB2-9E5093903430")]
	public interface IAcquireCounteragentResult
	{
		string OrgId { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.AsyncMethodResult")]
	[Guid("545F328C-8CB7-41A6-8B85-E2FAF271F995")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IAcquireCounteragentResult))]
	public partial class AcquireCounteragentResult : SafeComObject, IAcquireCounteragentResult
	{
	}

	[ComVisible(true)]
	[Guid("2AEC9CF5-1D41-4127-8252-C2DA82085BC1")]
	public interface IFullVersion
	{
		string TypeNamedId { get; }
		string Function { get; }
		string Version { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.FullVersion")]
	[Guid("701A11AC-8919-4000-A591-542A680C3EEE")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IFullVersion))]
	public partial class FullVersion : SafeComObject, IFullVersion
	{
	}
}
