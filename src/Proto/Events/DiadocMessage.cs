using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Diadoc.Api.Com;
using Diadoc.Api.Proto.Documents;

namespace Diadoc.Api.Proto.Events
{
	[ComVisible(true)]
	[Guid("ABF440A3-BC33-4DF4-80CF-6889F7B004A3")]
	public interface IMessage
	{
		string MessageId { get; }
		string FromBoxId { get; }
		string FromTitle { get; }
		string ToBoxId { get; }
		string ToTitle { get; }
		bool IsDraft { get; }
		bool IsDeleted { get; }
		bool IsTest { get; }
		bool IsInternal { get; }
		ReadonlyList EntitiesList { get; }
		bool DraftIsLocked { get; }
		bool DraftIsRecycled { get; }
		string CreatedFromDraftId { get; }
		string DraftIsTransformedToMessageId { get; }
		DateTime Timestamp { get; }
		DateTime LastPatchTimestamp { get; }
	}

	[ComVisible(true)]
	[Guid("26C32890-61DE-4FB9-9EED-B815E41050B7")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IMessage))]
	public partial class Message : SafeComObject, IMessage
	{
		public string DraftIsTransformedToMessageId
		{
			get { return DraftIsTransformedToMessageIdList.FirstOrDefault(); }
		}

		public DateTime Timestamp
		{
			get { return new DateTime(TimestampTicks, DateTimeKind.Utc); }
		}

		public DateTime LastPatchTimestamp
		{
			get { return new DateTime(LastPatchTimestampTicks, DateTimeKind.Utc); }
		}

		public ReadonlyList EntitiesList
		{
			get { return new ReadonlyList(Entities); }
		}
	}

	[ComVisible(true)]
	[Guid("DFF0AEBA-4DCC-4910-B34A-200F1B919F4E")]
	public interface IEntity
	{
		string EntityId { get; }
		string ParentEntityId { get; }
		Com.EntityType EntityTypeValue { get; }
		Com.AttachmentType AttachmentTypeValue { get; }
		string FileName { get; }
		bool NeedRecipientSignature { get; }
		bool NeedReceipt { get; }
		Content Content { get; }

		Document DocumentInfo { get; }
		ResolutionInfo ResolutionInfo { get; }

		string SignerBoxId { get; }
		string SignerDepartmentId { get; }
		DateTime CreationTime { get; }

		string NotDeliveredEventId { get; }
		ResolutionRouteAssignmentInfo ResolutionRouteAssignmentInfo { get; }
		ResolutionRouteRemovalInfo ResolutionRouteRemovalInfo { get; }
	}

	[ComVisible(true)]
	[Guid("E7D82A2C-A0BF-4D0B-9466-9E8DA15B99B7")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IEntity))]
	public partial class Entity : SafeComObject, IEntity
	{
		public DateTime CreationTime
		{
			get { return new DateTime(RawCreationDate, DateTimeKind.Utc); }
		}

		public Com.EntityType EntityTypeValue
		{
			get { return (Com.EntityType) ((int) EntityType); }
		}

		public Com.AttachmentType AttachmentTypeValue
		{
			get { return (Com.AttachmentType) ((int) AttachmentType); }
		}
	}

	[ComVisible(true)]
	[Guid("D3BD7130-16A6-4202-B1CE-5FB1A9AFD4EF")]
	public interface IEntityPatch
	{
		string EntityId { get; }
		bool DocumentIsDeleted { get; }
		string MovedToDepartment { get; }
		bool DocumentIsRestored { get; }
		bool ContentIsPatched { get; }
	}

	[ComVisible(true)]
	[Guid("4C05AB1C-5385-41A2-8C04-8855FC7E8341")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IEntityPatch))]
	public partial class EntityPatch : SafeComObject, IEntityPatch
	{
	}

	[ComVisible(true)]
	[Guid("AE4037C5-DA6E-4D69-A738-5E6B64490492")]
	public interface IBoxEvent
	{
		string EventId { get; }
		string MessageId { get; }
		DateTime Timestamp { get; }
		ReadonlyList EntitiesList { get; }
		bool HasMessage { get; }
		bool HasPatch { get; }
		Message Message { get; }
		MessagePatch Patch { get; }
	}

	[ComVisible(true)]
	[Guid("C59A22CF-9744-457A-8359-3569B19A31C8")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IBoxEvent))]
	public partial class BoxEvent : SafeComObject, IBoxEvent
	{
		public string MessageId
		{
			get
			{
				return (Message != null ? Message.MessageId : null)
					?? (Patch != null ? Patch.MessageId : null);
			}
		}

		public DateTime Timestamp
		{
			get
			{
				if (Message != null)
					return Message.Timestamp;
				if (Patch != null)
					return Patch.Timestamp;
				return default(DateTime);
			}
		}

		public ReadonlyList EntitiesList
		{
			get
			{
				var entities = (Message != null ? Message.Entities : null)
					?? (Patch != null ? Patch.Entities : null);
				return entities != null
					? new ReadonlyList(entities)
					: null;
			}
		}

		public List<Entity> Entities
		{
			get
			{
				var entities = (Message != null ? Message.Entities : null)
					?? (Patch != null ? Patch.Entities : null);
				return entities != null
					? new List<Entity>(entities)
					: new List<Entity>();
			}
		}

		public bool HasMessage
		{
			get { return Message != null; }
		}

		public bool HasPatch
		{
			get { return Patch != null; }
		}
	}

	[ComVisible(true)]
	[Guid("581C269C-67A5-4CDF-AC77-CDB2D05E03A0")]
	public interface IBoxEventList
	{
		int TotalCount { get; }
		ReadonlyList EventsList { get; }
	}

	[ComVisible(true)]
	[Guid("793E451E-3F4A-4A3E-BDBB-F47ED13305F5")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IBoxEventList))]
	public partial class BoxEventList : SafeComObject, IBoxEventList
	{
		public ReadonlyList EventsList
		{
			get { return new ReadonlyList(Events); }
		}
	}

	[ComVisible(true)]
	[Guid("527CD64F-A219-4B45-8219-CC48586D521B")]
	public interface IMessagePatch
	{
		string MessageId { get; }
		ReadonlyList EntitiesList { get; }
		ReadonlyList EntityPatchesList { get; }
		DateTime Timestamp { get; }
		bool ForDraft { get; }
		bool DraftIsRecycled { get; }
		string DraftIsTransformedToMessageId { get; }
		bool DraftIsLocked { get; }
		bool MessageIsDeleted { get; }
		bool MessageIsRestored { get; }
		bool MessageIsDelivered { get; }
		string DeliveredPatchId { get; }
		string PatchId { get; }
	}

	[ComVisible(true)]
	[Guid("949C9C09-DD1A-4787-A5AD-94D8677A5439")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IMessagePatch))]
	public partial class MessagePatch : SafeComObject, IMessagePatch
	{
		public DateTime Timestamp
		{
			get { return new DateTime(TimestampTicks, DateTimeKind.Utc); }
		}

		public string DraftIsTransformedToMessageId
		{
			get { return DraftIsTransformedToMessageIdList.FirstOrDefault(); }
		}

		public ReadonlyList EntitiesList
		{
			get { return new ReadonlyList(Entities); }
		}

		public ReadonlyList EntityPatchesList
		{
			get { return new ReadonlyList(EntityPatches); }
		}
	}

	[ComVisible(true)]
	[Guid("32176AA9-AF39-4E0C-A47B-0CEA3C1DA211")]
	public interface IGeneratedFile
	{
		string FileName { get; }
		byte[] Content { get; }
		void SaveContentToFile(string path);
	}

	[ComVisible(true)]
	[Guid("D4E7012E-1A64-42EC-AD5E-B27754AC24FE")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IGeneratedFile))]
	public class GeneratedFile : SafeComObject, IGeneratedFile
	{
		private readonly string fileName;
		private readonly byte[] content;

		public GeneratedFile(string fileName, byte[] content)
		{
			this.fileName = fileName;
			this.content = content;
		}

		public void SaveContentToFile(string path)
		{
			File.WriteAllBytes(path, Content);
		}

		public string FileName
		{
			get { return fileName; }
		}

		public byte[] Content
		{
			get { return content; }
		}
	}

	[ComVisible(true)]
	[Guid("34FD4B0B-CE38-41CE-AB76-4C6234A6C542")]
	public interface IMessageToPost
	{
		string FromBoxId { get; set; }
		string FromDepartmentId { get; set; }
		string ToBoxId { get; set; }
		string ToDepartmentId { get; set; }
		bool IsInternal { get; set; }
		bool IsDraft { get; set; }
		bool LockDraft { get; set; }
		bool LockPacket { get; set; }
		bool StrictDraftValidation { get; set; }
		bool DelaySend { get; set; }
		TrustConnectionRequestAttachment TrustConnectionRequest { get; set; }
		void AddInvoice([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddXmlTorg12SellerTitle([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddXmlAcceptanceCertificateSellerTitle([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddAttachment([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddTorg12([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddAcceptanceCertificate([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddProformaInvoice([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddStructuredData([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddPriceList([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddPriceListAgreement([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddCertificateRegistry([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddReconciliationAct([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddContract([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddTorg13([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddServiceDetails([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddSupplementaryAgreement([MarshalAs(UnmanagedType.IDispatch)] object supplementaryAgreement);
		void AddUniversalTransferDocumentSellerTitle([MarshalAs(UnmanagedType.IDispatch)] object attachment);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.MessageToPost")]
	[Guid("6EABD544-6DDC-49b4-95A1-0D7936C08C31")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IMessageToPost))]
	public partial class MessageToPost : SafeComObject, IMessageToPost
	{
		//That property was deletes from proto description, but we can't delete this property here because COM need continuous sequence of methods
		public TrustConnectionRequestAttachment TrustConnectionRequest
		{
			get { return null; }
			set { }
		}

		public void AddInvoice(object attachment)
		{
			Invoices.Add((XmlDocumentAttachment)attachment);
		}

		public void AddXmlTorg12SellerTitle(object attachment)
		{
			XmlTorg12SellerTitles.Add((XmlDocumentAttachment)attachment);
		}

		public void AddXmlAcceptanceCertificateSellerTitle(object attachment)
		{
			XmlAcceptanceCertificateSellerTitles.Add((XmlDocumentAttachment)attachment);
		}

		public void AddAttachment(object attachment)
		{
			NonformalizedDocuments.Add((NonformalizedAttachment)attachment);
		}

		public void AddTorg12(object attachment)
		{
			Torg12Documents.Add((BasicDocumentAttachment)attachment);
		}

		public void AddAcceptanceCertificate(object attachment)
		{
			AcceptanceCertificates.Add((AcceptanceCertificateAttachment) attachment);
		}

		public void AddProformaInvoice(object attachment)
		{
			ProformaInvoices.Add((BasicDocumentAttachment) attachment);
		}

		public void AddStructuredData(object attachment)
		{
			StructuredDataAttachments.Add((StructuredDataAttachment) attachment);
		}

		public void AddPriceList(object attachment)
		{
			PriceLists.Add((PriceListAttachment) attachment);
		}

		public void AddPriceListAgreement(object attachment)
		{
			PriceListAgreements.Add((NonformalizedAttachment) attachment);
		}

		public void AddCertificateRegistry(object attachment)
		{
			CertificateRegistries.Add((NonformalizedAttachment) attachment);
		}

		public void AddReconciliationAct(object attachment)
		{
			ReconciliationActs.Add((ReconciliationActAttachment) attachment);
		}

		public void AddContract(object attachment)
		{
			Contracts.Add((ContractAttachment) attachment);
		}

		public void AddTorg13(object attachment)
		{
			Torg13Documents.Add((Torg13Attachment) attachment);
		}

		public void AddServiceDetails(object attachment)
		{
			ServiceDetailsDocuments.Add((ServiceDetailsAttachment) attachment);
		}

		public void AddSupplementaryAgreement(object supplementaryAgreement)
		{
			SupplementaryAgreements.Add((SupplementaryAgreementAttachment)supplementaryAgreement);
		}

		public void AddUniversalTransferDocumentSellerTitle(object attachment)
		{
			UniversalTransferDocumentSellerTitles.Add((XmlDocumentAttachment)attachment);
		}
	}

	[ComVisible(true)]
	[Guid("A0C93B1F-5FD2-4738-B8F9-994AE05B5B63")]
	public interface ISupplementaryAgreementAttachment
	{
		SignedContent SignedContent { get; set; }
		string FileName { get; set; }
		string Comment { get; set; }
		void AddInitialDocumentId([MarshalAs(UnmanagedType.IDispatch)] object documentId);
		void AddSubordinateDocumentId([MarshalAs(UnmanagedType.IDispatch)] object documentId);
		string CustomDocumentId { get; set; }
		string DocumentDate { get; set; }
		string DocumentNumber { get; set; }
		string Total { get; set; }
		string ContractNumber { get; set; }
		string ContractDate { get; set; }
		string ContractType { get; set; }
		bool NeedReceipt { get; set; }
		void AddCustomDataItem([MarshalAs(UnmanagedType.IDispatch)] object customDataItem);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SupplementaryAgreementAttachment")]
	[Guid("9AA39127-85C9-4B40-A456-9C18D5BF8348")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISupplementaryAgreementAttachment))]
	public partial class SupplementaryAgreementAttachment : SafeComObject, ISupplementaryAgreementAttachment
	{
		public void AddInitialDocumentId(object documentId)
		{
			InitialDocumentIds.Add((DocumentId)documentId);
		}

		public void AddSubordinateDocumentId(object documentId)
		{
			SubordinateDocumentIds.Add((DocumentId)documentId);
		}

		public void AddCustomDataItem(object customDataItem)
		{
			CustomData.Add((CustomDataItem)customDataItem);
		}
	}

	[ComVisible(true)]
	[Guid("0D648002-53CC-4EAB-969B-68364BB4F3CE")]
	public interface IMessagePatchToPost
	{
		string BoxId { get; set; }
		string MessageId { get; set; }
		void AddReceipt([MarshalAs(UnmanagedType.IDispatch)] object receipt);
		void AddCorrectionRequest([MarshalAs(UnmanagedType.IDispatch)] object correctionRequest);
		void AddSignature([MarshalAs(UnmanagedType.IDispatch)] object signature);

		void AddRequestedSignatureRejection(
			[MarshalAs(UnmanagedType.IDispatch)] object signatureRejection);

		void AddXmlTorg12BuyerTitle([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddXmlAcceptanceCertificateBuyerTitle([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddResolution([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddRevocationRequestAttachment([MarshalAs(UnmanagedType.IDispatch)] object attachment);

		void AddXmlSignatureRejectionAttachment([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddUniversalTransferDocumentBuyerTitleAttachment([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddResolutionRouteAssignment([MarshalAs(UnmanagedType.IDispatch)] object attachment);
		void AddResolutoinRouteRemoval([MarshalAs(UnmanagedType.IDispatch)] object attachment);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.MessagePatchToPost")]
	[Guid("F917FD6D-AEE9-4b21-A79F-11981E805F5D")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IMessagePatchToPost))]
	public partial class MessagePatchToPost : SafeComObject, IMessagePatchToPost
	{
		public void AddReceipt(object receipt)
		{
			Receipts.Add((ReceiptAttachment)receipt);
		}

		public void AddCorrectionRequest(object correctionRequest)
		{
			CorrectionRequests.Add((CorrectionRequestAttachment)correctionRequest);
		}

		public void AddSignature(object signature)
		{
			Signatures.Add((DocumentSignature)signature);
		}

		public void AddRequestedSignatureRejection(object signatureRejection)
		{
			RequestedSignatureRejections.Add((RequestedSignatureRejection)signatureRejection);
		}

		public void AddXmlTorg12BuyerTitle(object attachment)
		{
			XmlTorg12BuyerTitles.Add((ReceiptAttachment)attachment);
		}

		public void AddXmlAcceptanceCertificateBuyerTitle(object attachment)
		{
			XmlAcceptanceCertificateBuyerTitles.Add((ReceiptAttachment)attachment);
		}

		public void AddUniversalTransferDocumentBuyerTitle(object attachment)
		{
			UniversalTransferDocumentBuyerTitles.Add((ReceiptAttachment)attachment);
		}

		public void AddResolution(object attachment)
		{
			Resolutions.Add((ResolutionAttachment)attachment);
		}

		public void AddResolutionRequestAttachment(object attachment)
		{
			ResolutionRequests.Add((ResolutionRequestAttachment)attachment);
		}

		public void AddResolutionRequestCancellationAttachment(object attachment)
		{
			ResolutionRequestCancellations.Add((ResolutionRequestCancellationAttachment)attachment);
		}

		public void AddResolutionRequestDenialAttachment(object attachment)
		{
			ResolutionRequestDenials.Add((ResolutionRequestDenialAttachment)attachment);
		}

		public void AddResolutionRequestDenialCancellationAttachment(object attachment)
		{
			ResolutionRequestDenialCancellations.Add((ResolutionRequestDenialCancellationAttachment)attachment);
		}

		public void AddRevocationRequestAttachment(object attachment)
		{
			RevocationRequests.Add((RevocationRequestAttachment)attachment);
		}

		public void AddXmlSignatureRejectionAttachment(object attachment)
		{
			XmlSignatureRejections.Add((XmlSignatureRejectionAttachment)attachment);
		}

		public void AddUniversalTransferDocumentBuyerTitleAttachment(object attachment)
		{
			UniversalTransferDocumentBuyerTitles.Add((ReceiptAttachment)attachment);
		}

		public void AddResolutionRouteAssignment(object attachment)
		{
			ResolutionRouteAssignments.Add((ResolutionRouteAssignment)attachment);
		}

		public void AddResolutoinRouteRemoval(object attachment)
		{
			ResolutionRouteRemovals.Add((ResolutionRouteRemoval)attachment);
		}
	}

	[ComVisible(true)]
	[Guid("10AC1159-A121-4F3E-9437-7CF22A1B60A1")]
	public interface IXmlDocumentAttachment
	{
		string CustomDocumentId { get; set; }
		SignedContent SignedContent { get; set; }
		string Comment { get; set; }
		void SetSignedContent([MarshalAs(UnmanagedType.IDispatch)] object signedContent);
		void AddInitialDocumentId([MarshalAs(UnmanagedType.IDispatch)] object documentId);
		void AddSubordinateDocumentId([MarshalAs(UnmanagedType.IDispatch)] object documentId);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.XmlDocumentAttachment")]
	[Guid("E6B32174-37DE-467d-A947-B88AEDC2ECEC")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IXmlDocumentAttachment))]
	public partial class XmlDocumentAttachment : SafeComObject, IXmlDocumentAttachment
	{
		public void SetSignedContent(object signedContent)
		{
			SignedContent = (SignedContent) signedContent;
		}

		public void AddInitialDocumentId(object documentId)
		{
			InitialDocumentIds.Add((DocumentId) documentId);
		}

		public void AddSubordinateDocumentId(object documentId)
		{
			SubordinateDocumentIds.Add((DocumentId)documentId);
		}
	}

	[ComVisible(true)]
	[Guid("B43D2BF4-E100-4F49-8F8F-D87EA3ECE453")]
	public interface INonformalizedAttachment
	{
		string CustomDocumentId { get; set; }
		string FileName { get; set; }
		string DocumentNumber { get; set; }
		string DocumentDate { get; set; }
		bool NeedRecipientSignature { get; set; }
		string Comment { get; set; }
		SignedContent SignedContent { get; set; }
		void SetSignedContent([MarshalAs(UnmanagedType.IDispatch)] object signedContent);
		void AddInitialDocumentId([MarshalAs(UnmanagedType.IDispatch)] object documentId);
		void AddSubordinateDocumentId([MarshalAs(UnmanagedType.IDispatch)] object documentId);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.NonformalizedAttachment")]
	[Guid("9904995A-1EF3-4182-8C3E-61DBEADC159C")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (INonformalizedAttachment))]
	public partial class NonformalizedAttachment : SafeComObject, INonformalizedAttachment
	{
		public void SetSignedContent(object signedContent)
		{
			SignedContent = (SignedContent) signedContent;
		}

		public void AddInitialDocumentId(object documentId)
		{
			InitialDocumentIds.Add((DocumentId) documentId);
		}

		public void AddSubordinateDocumentId(object documentId)
		{
			SubordinateDocumentIds.Add((DocumentId) documentId);
		}
	}

	[ComVisible(true)]
	[Guid("8FC16D12-EEE7-44F1-AD9C-ACD907EF286D")]
	public interface IBasicDocumentAttachment
	{
		string CustomDocumentId { get; set; }
		string FileName { get; set; }
		string DocumentNumber { get; set; }
		string DocumentDate { get; set; }
		string Total { get; set; }
		string Vat { get; set; }
		string Grounds { get; set; }
		string Comment { get; set; }
		SignedContent SignedContent { get; set; }
		void SetSignedContent([MarshalAs(UnmanagedType.IDispatch)] object signedContent);
		void AddInitialDocumentId([MarshalAs(UnmanagedType.IDispatch)] object documentId);
		void AddSubordinateDocumentId([MarshalAs(UnmanagedType.IDispatch)] object documentId);
	}

	[ComVisible(true)]
	[Guid("E7DA152C-B651-4F3A-B9D7-25F6E5BABC1D")]
	public interface IAcceptanceCertificateAttachment
	{
		string CustomDocumentId { get; set; }
		string FileName { get; set; }
		string DocumentNumber { get; set; }
		string DocumentDate { get; set; }
		string Total { get; set; }
		string Vat { get; set; }
		string Grounds { get; set; }
		string Comment { get; set; }
		SignedContent SignedContent { get; set; }
		bool NeedRecipientSignature { get; set; }
		void SetSignedContent([MarshalAs(UnmanagedType.IDispatch)] object signedContent);
		void AddInitialDocumentId([MarshalAs(UnmanagedType.IDispatch)] object documentId);
		void AddSubordinateDocumentId([MarshalAs(UnmanagedType.IDispatch)] object documentId);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.BasicDocumentAttachment")]
	[Guid("776261C4-361D-42BF-929C-8B368DEE917D")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IBasicDocumentAttachment))]
	public partial class BasicDocumentAttachment : SafeComObject, IBasicDocumentAttachment
	{
		public void SetSignedContent(object signedContent)
		{
			SignedContent = (SignedContent)signedContent;
		}

		public void AddInitialDocumentId(object documentId)
		{
			InitialDocumentIds.Add((DocumentId)documentId);
		}

		public void AddSubordinateDocumentId(object documentId)
		{
			SubordinateDocumentIds.Add((DocumentId)documentId);
		}
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.AcceptanceCertificateAttachment")]
	[Guid("9BCBE1E4-11C5-45BF-887A-FE63D074D71A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IAcceptanceCertificateAttachment))]
	public partial class AcceptanceCertificateAttachment : SafeComObject, IAcceptanceCertificateAttachment
	{
		public void SetSignedContent(object signedContent)
		{
			SignedContent = (SignedContent) signedContent;
		}

		public void AddInitialDocumentId(object documentId)
		{
			InitialDocumentIds.Add((DocumentId) documentId);
		}

		public void AddSubordinateDocumentId(object documentId)
		{
			SubordinateDocumentIds.Add((DocumentId) documentId);
		}
	}

	[ComVisible(true)]
	[Guid("2860C8D8-72C3-4D83-A3EB-DF36A2A8EB2E")]
	public interface ITrustConnectionRequestAttachment
	{
		string FileName { get; set; }
		string Comment { get; set; }
		SignedContent SignedContent { get; set; }
		void SetSignedContent([MarshalAs(UnmanagedType.IDispatch)] object signedContent);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.TrustConnectionRequestAttachment")]
	[Guid("139BB7DA-D92A-49F0-840A-3FB541323632")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (ITrustConnectionRequestAttachment))]
	public partial class TrustConnectionRequestAttachment : SafeComObject, ITrustConnectionRequestAttachment
	{
		public void SetSignedContent(object signedContent)
		{
			SignedContent = (SignedContent) signedContent;
		}
	}

	[ComVisible(true)]
	[Guid("DA50195B-0B15-42DB-AFE3-35C1280C9EA6")]
	public interface IStructuredDataAttachment
	{
		string ParentCustomDocumentId { get; set; }
		string FileName { get; set; }
		byte[] Content { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.StructuredDataAttachment")]
	[Guid("E35327B6-F774-476A-93B4-CC68DE7432D1")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IStructuredDataAttachment))]
	public partial class StructuredDataAttachment : SafeComObject, IStructuredDataAttachment
	{
	}

	[ComVisible(true)]
	[Guid("4D8D636A-91D8-4734-8371-913E144FED66")]
	public interface ISignedContent
	{
		byte[] Content { get; set; }
		byte[] Signature { get; set; }

		[Obsolete]
		bool SignByAttorney { get; set; }

		bool SignWithTestSignature { get; set; }
		string NameOnShelf { get; set; }

		void LoadContentFromFile(string fileName);
		void SaveContentToFile(string fileName);
		void LoadSignatureFromFile(string fileName);
		void SaveSignatureToFile(string fileName);
	}

	[ComVisible(true)]
	[Guid("A42D43EB-C083-4765-86C2-A5BD7DE58C3E")]
	public interface IPriceListAttachment
	{
		string CustomDocumentId { get; set; }
		string FileName { get; set; }
		string DocumentNumber { get; set; }
		string DocumentDate { get; set; }
		string PriceListEffectiveDate { get; set; }
		string ContractDocumentDate { get; set; }
		string ContractDocumentNumber { get; set; }
		string Comment { get; set; }
		SignedContent SignedContent { get; set; }
		void SetSignedContent([MarshalAs(UnmanagedType.IDispatch)] object signedContent);
		void AddInitialDocumentId([MarshalAs(UnmanagedType.IDispatch)] object documentId);
		void AddSubordinateDocumentId([MarshalAs(UnmanagedType.IDispatch)] object documentId);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PriceListAttachment")]
	[Guid("2D0A054F-E3FA-4FBD-A64F-9C4EB73901BB")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IPriceListAttachment))]
	public partial class PriceListAttachment : SafeComObject, IPriceListAttachment
	{
		public void SetSignedContent(object signedContent)
		{
			SignedContent = (SignedContent) signedContent;
		}

		public void AddInitialDocumentId(object documentId)
		{
			InitialDocumentIds.Add((DocumentId) documentId);
		}

		public void AddSubordinateDocumentId(object documentId)
		{
			SubordinateDocumentIds.Add((DocumentId) documentId);
		}
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SignedContent")]
	[Guid("0EC71E3F-F203-4c49-B1D6-4DA6BFDD279B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (ISignedContent))]
	public partial class SignedContent : SafeComObject, ISignedContent
	{
		public bool SignByAttorney
		{
			get { return false; }
			set { }
		}

		public void LoadContentFromFile(string fileName)
		{
			Content = File.ReadAllBytes(fileName);
		}

		public void SaveContentToFile(string fileName)
		{
			if (Content != null)
				File.WriteAllBytes(fileName, Content);
		}

		public void LoadSignatureFromFile(string fileName)
		{
			Signature = File.ReadAllBytes(fileName);
		}

		public void SaveSignatureToFile(string fileName)
		{
			File.WriteAllBytes(fileName, Signature);
		}
	}

	[ComVisible(true)]
	[Guid("BD1E9A9B-E74C-41AD-94CE-199601346DDB")]
	public interface IDocumentSignature
	{
		string ParentEntityId { get; set; }
		byte[] Signature { get; set; }

		[Obsolete]
		bool SignByAttorney { get; set; }

		bool SignWithTestSignature { get; set; }
		void LoadFromFile(string fileName);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentSignature")]
	[Guid("28373DE6-3147-4d8b-B166-6D653F50EED3")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IDocumentSignature))]
	public partial class DocumentSignature : SafeComObject, IDocumentSignature
	{
		public bool SignByAttorney
		{
			get { return false; }
			set { }
		}

		public void LoadFromFile(string fileName)
		{
			Signature = File.ReadAllBytes(fileName);
		}
	}

	[ComVisible(true)]
	[Guid("F60296C5-6981-48A7-9E93-72B819B81172")]
	public interface IRequestedSignatureRejection
	{
		string ParentEntityId { get; set; }
		SignedContent SignedContent { get; set; }
		void SetSignedContent([MarshalAs(UnmanagedType.IDispatch)] object signedContent);
		void LoadComment(string commentFileName, string signatureFileName);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RequestedSignatureRejection")]
	[Guid("C0106095-AF9F-462A-AEC0-A3E0B1ACAC6B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IRequestedSignatureRejection))]
	public partial class RequestedSignatureRejection : SafeComObject, IRequestedSignatureRejection
	{
		public void SetSignedContent(object signedContent)
		{
			SignedContent = (SignedContent) signedContent;
		}

		public void LoadComment(string commentFileName, string signatureFileName)
		{
			SignedContent = new SignedContent
			{
				Content = File.ReadAllBytes(commentFileName),
				Signature = File.ReadAllBytes(signatureFileName),
			};
		}
	}

	[ComVisible(true)]
	[Guid("8E18ECC8-18B4-4A9C-B322-1CF03DB3E07F")]
	public interface IReceiptAttachment
	{
		string ParentEntityId { get; set; }
		SignedContent SignedContent { get; set; }
		void SetSignedContent([MarshalAs(UnmanagedType.IDispatch)] object signedContent);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ReceiptAttachment")]
	[Guid("45053335-83C3-4e62-9973-D6CC1872A60A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IReceiptAttachment))]
	public partial class ReceiptAttachment : SafeComObject, IReceiptAttachment
	{
		public void SetSignedContent(object signedContent)
		{
			SignedContent = (SignedContent) signedContent;
		}
	}

	[ComVisible(true)]
	[Guid("5902A325-F31B-4B9B-978E-99C1CC7C9209")]
	public interface IResolutionAttachment
	{
		string InitialDocumentId { get; set; }
		Com.ResolutionType ResolutionTypeValue { get; set; }
		string Comment { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ResolutionAttachment")]
	[Guid("89D739B0-03F1-4E6C-8301-E3FEA9FD2AD6")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IResolutionAttachment))]
	public partial class ResolutionAttachment : SafeComObject, IResolutionAttachment
	{
		public Com.ResolutionType ResolutionTypeValue
		{
			get { return (Com.ResolutionType) ResolutionType; }
			set { ResolutionType = (ResolutionType) value; }
		}
	}

	[ComVisible(true)]
	[Guid("F19CEEBD-ECE5-49D2-A0FC-FE8C4B1B9E4C")]
	public interface IResolutionRequestAttachment
	{
		string InitialDocumentId { get; set; }
		Com.ResolutionRequestType RequestType { get; set; }
		string TargetUserId { get; set; }
		string TargetDepartmentId { get; set; }
		string Comment { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ResolutionRequest")]
	[Guid("6043455B-3087-4A63-9870-66D54E8E34FB")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IResolutionRequestAttachment))]
	public partial class ResolutionRequestAttachment : SafeComObject, IResolutionRequestAttachment
	{
		public Com.ResolutionRequestType RequestType
		{
			get { return (Com.ResolutionRequestType) Type; }
			set { Type = (ResolutionRequestType) value; }
		}
	}

	[ComVisible(true)]
	[Guid("F23FCA9D-4FD3-4AE7-A431-275CAB51C7A1")]
	public interface IResolutionRequestCancellationAttachment
	{
		string InitialResolutionRequestId { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ResolutionRequestCancellation")]
	[Guid("E80B8699-9883-4259-AA67-B84B03DD5F09")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IResolutionRequestCancellationAttachment))]
	public partial class ResolutionRequestCancellationAttachment : SafeComObject, IResolutionRequestCancellationAttachment
	{
	}

	[ComVisible(true)]
	[Guid("96F3613E-9DAA-4F2E-B8AB-7A8895D9E3AE")]
	public interface IResolutionRequestDenialAttachment
	{
		string InitialResolutionRequestId { get; set; }
		string Comment { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ResolutionRequestDenial")]
	[Guid("BD79962A-D6AC-4831-9498-162C36AFD6E1")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IResolutionRequestDenialAttachment))]
	public partial class ResolutionRequestDenialAttachment : SafeComObject, IResolutionRequestDenialAttachment
	{
	}

	[ComVisible(true)]
	[Guid("39F44156-E889-4EFB-9368-350D1ED40B2F")]
	public interface IResolutionRequestDenialCancellationAttachment
	{
		string InitialResolutionRequestDenialId { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ResolutionRequestDenialCancellation")]
	[Guid("4E8A0DEF-B6F7-4820-9CBE-D94A38E34DFC")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IResolutionRequestDenialCancellationAttachment))]
	public partial class ResolutionRequestDenialCancellationAttachment : SafeComObject,
		IResolutionRequestDenialCancellationAttachment
	{
	}

	[ComVisible(true)]
	[Guid("4BA8315A-FDAA-4D4F-BB14-9707E12DFA39")]
	public interface ICorrectionRequestAttachment
	{
		string ParentEntityId { get; set; }
		SignedContent SignedContent { get; set; }
		void SetSignedContent([MarshalAs(UnmanagedType.IDispatch)] object signedContent);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.CorrectionRequestAttachment")]
	[Guid("D78FC023-2BEF-49a4-BDBE-3B791168CE98")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (ICorrectionRequestAttachment))]
	public partial class CorrectionRequestAttachment : SafeComObject, ICorrectionRequestAttachment
	{
		public void SetSignedContent(object signedContent)
		{
			SignedContent = (SignedContent)signedContent;
		}
	}

	[ComVisible(true)]
	[Guid("D9B32F6D-C203-49DB-B224-6B30BB1F2BAA")]
	public interface IDocumentSenderSignature
	{
		string ParentEntityId { get; set; }
		byte[] Signature { get; set; }
		bool SignWithTestSignature { get; set; }
		void LoadFromFile(string fileName);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentSenderSignature")]
	[Guid("19CB816D-F518-4E91-94A2-F19B0CF7CC71")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentSenderSignature))]
	public partial class DocumentSenderSignature : SafeComObject, IDocumentSenderSignature
	{
		public void LoadFromFile(string fileName)
		{
			Signature = File.ReadAllBytes(fileName);
		}
	}

	[ComVisible(true)]
	[Guid("BDCD849E-F6A9-4EA4-8B84-482E10173DED")]
	public interface IDraftToSend
	{
		string BoxId { get; set; }
		string DraftId { get; set; }
		string ToBoxId { get; set; }
		string ToDepartmentId { get; set; }
		void AddDocumentSignature([MarshalAs(UnmanagedType.IDispatch)] object signature);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DraftToSend")]
	[Guid("37127975-95FC-4247-8393-052EF27D1575")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IDraftToSend))]
	public partial class DraftToSend : SafeComObject, IDraftToSend
	{
		public void AddDocumentSignature(object signature)
		{
			DocumentSignatures.Add((DocumentSenderSignature) signature);
		}
	}

	[ComVisible(true)]
	[Guid("0A28FEDA-8108-49CE-AC56-31B16B2D036B")]
	public interface IRevocationRequestAttachment
	{
		string ParentEntityId { get; set; }
		SignedContent SignedContent { get; set; }
		void SetSignedContent([MarshalAs(UnmanagedType.IDispatch)] object signedContent);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RevocationRequestAttachment")]
	[Guid("D2616BCD-A691-42B5-9707-6CE12742C5D3")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IRevocationRequestAttachment))]
	public partial class RevocationRequestAttachment : SafeComObject, IRevocationRequestAttachment
	{
		public void SetSignedContent(object signedContent)
		{
			SignedContent = (SignedContent)signedContent;
		}
	}

	[ComVisible(true)]
	[Guid("4EED1BE6-7136-4B46-86C7-44F9A0FFD530")]
	public interface IXmlSignatureRejectionAttachment
	{
		string ParentEntityId { get; set; }
		SignedContent SignedContent { get; set; }
		void SetSignedContent([MarshalAs(UnmanagedType.IDispatch)] object signedContent);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.XmlSignatureRejectionAttachment")]
	[Guid("553DC13F-81B4-4010-886C-260DBD60D486")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IXmlSignatureRejectionAttachment))]
	public partial class XmlSignatureRejectionAttachment : SafeComObject, IXmlSignatureRejectionAttachment
	{
		public void SetSignedContent(object signedContent)
		{
			SignedContent = (SignedContent) signedContent;
		}
	}

	[ComVisible(true)]
	[Guid("1B7169E9-455A-47C8-BD0F-5227B436CC61")]
	public interface IResolutionRouteAssignment
	{
		string InitialDocumentId { get; set; }
		string RouteId { get; set; }
		string Comment { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ResolutionRouteAssignment")]
	[Guid("B4C90587-3DB9-4BFB-84A0-E1A8E082978D")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IResolutionRouteAssignment))]
	public partial class ResolutionRouteAssignment : SafeComObject, IResolutionRouteAssignment
	{
	}

	[ComVisible(true)]
	[Guid("DBCE6766-25F7-4EAB-9C4F-C59AB0054E57")]
	public interface IResolutionRouteRemoval
	{
		string ParentEntityId { get; set; }
		string RouteId { get; set; }
		string Comment { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ResolutionRouteRemoval")]
	[Guid("F1356D51-625B-4A24-AF11-3402D72908C8")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IResolutionRouteRemoval))]
	public partial class ResolutionRouteRemoval : SafeComObject, IResolutionRouteRemoval
	{
	}

	[ComVisible(true)]
	[Guid("12A46076-9F0F-4E89-8E49-68E3C5FC8C04")]
	public interface IResolutionRouteAssignmentInfo
	{
		string RouteId { get; }
		string Author { get; }
	}

	[ComVisible(true)]
	[Guid("AF775D73-D1BB-40C7-9DF5-D0305B606DC7")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IResolutionRouteAssignmentInfo))]
	public partial class ResolutionRouteAssignmentInfo : SafeComObject, IResolutionRouteAssignmentInfo
	{
	}

	[ComVisible(true)]
	[Guid("180C7D5E-7E8B-48ED-BAA4-AAA29D9467CC")]
	public interface IResolutionRouteRemovalInfo
	{
		string RouteId { get; }
		string Author { get; }
	}

	[ComVisible(true)]
	[Guid("AAB466D3-6BF3-4DB4-B7C9-DA7BD8F74837")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IResolutionRouteRemovalInfo))]
	public partial class ResolutionRouteRemovalInfo : SafeComObject, IResolutionRouteRemovalInfo
	{
	}
}
