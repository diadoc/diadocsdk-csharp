﻿using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("37A089E0-01EC-420B-8DDC-0F85AC167449")]
	public interface ISignatureV3
	{
		Entity Cms { get; }
		Entity CadesT { get; }
		string SignerBoxId { get; }
		string SignerDepartmentId { get; }
		bool IsValid { get; }
		SignatureVerificationResult VerificationResult { get; }
		Timestamp DeliveredAt { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SignatureV3")]
	[Guid("CFE3D4AE-9634-4CC2-8810-21D4E652EDB2")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISignatureV3))]
	public partial class SignatureV3 : SafeComObject, ISignatureV3
	{
	}

	[ComVisible(true)]
	[Guid("CE7F6921-827D-4E00-B2A3-83F78C17687C")]
	public interface ISignedAttachmentV3
	{
		Attachment Attachment { get; }
		SignatureV3 Signature { get; }
		Entity Comment { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SignedAttachmentV3")]
	[Guid("7DB3355D-5093-4BE4-8E13-A9F850C6DFD9")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISignedAttachmentV3))]
	public partial class SignedAttachmentV3 : SafeComObject, ISignedAttachmentV3
	{
	}

	[ComVisible(true)]
	[Guid("D5D0DC8C-9687-4621-A33B-AD7D16F48147")]
	public interface IRoamingNotification
	{
		Entity Notification { get; }
		bool IsSuccess { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RoamingNotification")]
	[Guid("286E2BEF-DE64-422D-885F-27299FFD713C")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRoamingNotification))]
	public partial class RoamingNotification : SafeComObject, IRoamingNotification
	{
	}

	[ComVisible(true)]
	[Guid("E2A01178-2927-434B-AB6F-3A07F5B00D0E")]
	public interface IGetDocflowBatchResponseV3
	{
		ReadonlyList DocumentsList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.GetDocflowBatchResponseV3")]
	[Guid("B0ACDA68-9C8D-4A0F-B03A-E22DF657783A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IGetDocflowBatchResponseV3))]
	public partial class GetDocflowBatchResponseV3 : SafeComObject, IGetDocflowBatchResponseV3
	{
		public ReadonlyList DocumentsList
		{
			get { return new ReadonlyList(Documents); }
		}
	}

	[ComVisible(true)]
	[Guid("7202FA99-A041-4DC1-8DFF-D13B256ED5AD")]
	public interface ISearchDocflowsResponseV3
	{
		ReadonlyList DocumentsList { get; }
		bool HaveMoreDocuments { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SearchDocflowsResponseV3")]
	[Guid("6DC4AB0E-03D4-4BFC-AEFC-FE3AF3E2BA9B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISearchDocflowsResponseV3))]
	public partial class SearchDocflowsResponseV3 : SafeComObject, ISearchDocflowsResponseV3
	{
		public ReadonlyList DocumentsList
		{
			get { return new ReadonlyList(Documents); }
		}
	}

	[ComVisible(true)]
	[Guid("7E8CAF6D-375F-4812-91BC-10AC42E6796C")]
	public interface IFetchedDocumentV3
	{
		DocumentWithDocflowV3 Document { get; }
		byte[] IndexKey { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.FetchedDocumentV3")]
	[Guid("DBE7349E-CC8C-4855-BA18-7E435813D733")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IFetchedDocumentV3))]
	public partial class FetchedDocumentV3 : SafeComObject, IFetchedDocumentV3
	{
	}

	[ComVisible(true)]
	[Guid("E3AAC2A4-1F43-409A-9D1C-E5F62E01CBB4")]
	public interface IGetDocflowsByPacketIdResponseV3
	{
		ReadonlyList DocumentsList { get; }
		byte[] NextPageIndexKey { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.GetDocflowsByPacketIdResponseV3")]
	[Guid("1F9AA375-EAF6-4A4A-9119-C42119783BC0")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IGetDocflowsByPacketIdResponseV3))]
	public partial class GetDocflowsByPacketIdResponseV3 : SafeComObject, IGetDocflowsByPacketIdResponseV3
	{
		public ReadonlyList DocumentsList
		{
			get { return new ReadonlyList(Documents); }
		}
	}

	[ComVisible(true)]
	[Guid("DC182DE0-3329-43F6-93E5-2DDBECA4E2FA")]
	public interface IGetDocflowEventsResponseV3
	{
		int TotalCount { get; }
		ReadonlyList EventsList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.GetDocflowEventsResponseV3")]
	[Guid("61A604E4-8162-4239-85FE-8E8D18533EA4")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IGetDocflowEventsResponseV3))]
	public partial class GetDocflowEventsResponseV3 : SafeComObject, IGetDocflowEventsResponseV3
	{
		public ReadonlyList EventsList
		{
			get { return new ReadonlyList(Events); }
		}
	}

	[ComVisible(true)]
	[Guid("1EBD474B-B979-4B10-912F-85DC18755877")]
	public interface IDocflowEventV3
	{
		string EventId { get; }
		Timestamp Timestamp { get; }
		DocumentId DocumentId { get; }
		byte[] IndexKey { get; }
		DocumentWithDocflowV3 Document { get; }
		string PreviousEventId { get; }
		DocumentWithDocflowV3 PreviousDocumentState { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocflowEventV3")]
	[Guid("A4FBDA5E-3137-43FA-80A1-47A937327D72")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocflowEventV3))]
	public partial class DocflowEventV3 : SafeComObject, IDocflowEventV3
	{
	}

	[ComVisible(true)]
	[Guid("FADEB261-5B76-4D9B-820F-6F1DC958DBAB")]
	public interface IDocumentWithDocflowV3
	{
		DocumentId DocumentId { get; }
		LastEvent LastEvent { get; }
		DocumentInfoV3 DocumentInfo { get; }
		DocflowV3 Docflow { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentWithDocflowV3")]
	[Guid("320881E2-DB59-4556-BD9C-34CE3D76F572")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentWithDocflowV3))]
	public partial class DocumentWithDocflowV3 : SafeComObject, IDocumentWithDocflowV3
	{
	}

	[ComVisible(true)]
	[Guid("F9335222-9662-4AF8-9EA3-7C8F1435148D")]
	public interface ILastEvent
	{
		string EventId { get; }
		Timestamp Timestamp { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.LastEvent")]
	[Guid("42888D6C-2EAA-46F5-B198-30E511E99B49")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ILastEvent))]
	public partial class LastEvent : SafeComObject, ILastEvent
	{
	}

	[ComVisible(true)]
	[Guid("274E88F9-3FE0-4B5E-850E-DA1E2634F03A")]
	public interface IDocumentInfoV3
	{
		FullVersion FullVersion { get; }
		Documents.MessageType MessageType { get; }
		int WorkflowId { get; }
		DocumentParticipants Participants { get; }
		DocumentDirection DocumentDirection { get; }
		string DepartmentId { get; }
		string CustomDocumentId { get; }
		ReadonlyList MetadataList { get; }
		ReadonlyList CustomDataList { get; }
		DocumentLinks DocumentLinks { get; }
		PacketInfo PacketInfo { get; }
		bool IsRead { get; }
		bool IsDeleted { get; }
		bool IsInvitation { get; }
		DocumentLetterInfo LetterInfo { get; }
		DocumentDraftInfo DraftInfo { get; }
		DocumentTemplateInfo TemplateInfo { get; }
		Documents.Origin Origin { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentInfoV3")]
	[Guid("3E0E5D76-D6FC-4D81-8AF7-FD7996B82057")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentInfoV3))]
	public partial class DocumentInfoV3 : SafeComObject, IDocumentInfoV3
	{
		public ReadonlyList MetadataList
		{
			get { return new ReadonlyList(Metadata); }
		}

		public ReadonlyList CustomDataList
		{
			get { return new ReadonlyList(CustomData); }
		}
	}

	[ComVisible(true)]
	[Guid("B10E628A-4754-4BD6-AFEA-3A9BE2DA2D11")]
	public interface IDocumentParticipants
	{
		DocumentParticipant Sender { get; }
		DocumentParticipant Proxy { get; }
		DocumentParticipant Recipient { get; }
		bool IsInternal { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentParticipants")]
	[Guid("3FE74724-45F6-48C6-B535-A4B2572FB83F")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentParticipants))]
	public partial class DocumentParticipants : SafeComObject, IDocumentParticipants
	{
	}

	[ComVisible(true)]
	[Guid("D5140C3A-994D-4CDA-8CD5-08585002DAEA")]
	public interface IDocumentParticipant
	{
		string BoxId { get; }
		string DepartmentId { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentParticipant")]
	[Guid("CD4E43EB-4DB6-40ED-9E6B-1F5B468C6A21")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentParticipant))]
	public partial class DocumentParticipant : SafeComObject, IDocumentParticipant
	{
	}

	[ComVisible(true)]
	[Guid("DFE15A47-8369-443E-A798-E725FC298BE4")]
	public interface IDocumentLinks
	{
		ReadonlyList InitialIdsList { get; }
		ReadonlyList SubordinateIdsList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentLinks")]
	[Guid("21F147F2-370A-416F-B320-B52F8BBF29CC")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentLinks))]
	public partial class DocumentLinks : SafeComObject, IDocumentLinks
	{
		public ReadonlyList InitialIdsList
		{
			get { return new ReadonlyList(InitialIds); }
		}

		public ReadonlyList SubordinateIdsList
		{
			get { return new ReadonlyList(SubordinateIds); }
		}
	}

	[ComVisible(true)]
	[Guid("5825EC56-B5B3-4CD6-A274-232406A510FE")]
	public interface IPacketInfo
	{
		LockMode LockMode { get; }
		string PacketId { get; }
		Timestamp AddedAt { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PacketInfo")]
	[Guid("2AA11BAE-97BC-4969-A428-F65C9C588054")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPacketInfo))]
	public partial class PacketInfo : SafeComObject, IPacketInfo
	{
	}

	[ComVisible(true)]
	[Guid("14C2C957-AF79-4D36-B4C7-32608D27B20B")]
	public interface IDocumentLetterInfo
	{
		bool IsEncrypted { get; }
		ReadonlyList ForwardDocumentEventsList { get; }
		bool IsTest { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentLetterInfo")]
	[Guid("F847CC01-A92F-414B-88CB-489622DF7EC9")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentLetterInfo))]
	public partial class DocumentLetterInfo : SafeComObject, IDocumentLetterInfo
	{
		public ReadonlyList ForwardDocumentEventsList
		{
			get { return new ReadonlyList(ForwardDocumentEvents); }
		}
	}

	[ComVisible(true)]
	[Guid("E714B983-CD7D-4CA7-B53C-AAD791FDD83F")]
	public interface IDocumentDraftInfo
	{
		bool IsRecycled { get; }
		bool IsLocked { get; }
		ReadonlyList TransformedToLetterIdsList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentDraftInfo")]
	[Guid("DC04CA7E-6D81-4A0B-B220-2540CCCBDA2F")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentDraftInfo))]
	public partial class DocumentDraftInfo : SafeComObject, IDocumentDraftInfo
	{
		public ReadonlyList TransformedToLetterIdsList
		{
			get { return new ReadonlyList(TransformedToLetterIds); }
		}
	}

	[ComVisible(true)]
	[Guid("CE0C6448-7D22-45AA-BF41-693E305A5CD9")]
	public interface IDocumentTemplateInfo
	{
		DocumentParticipants LetterParticipants { get; }
		ReadonlyList TransformedToLetterIdsList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentTemplateInfo")]
	[Guid("6510B124-5805-434B-93F5-AE6CE13BAF43")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentTemplateInfo))]
	public partial class DocumentTemplateInfo : SafeComObject, IDocumentTemplateInfo
	{
		public ReadonlyList TransformedToLetterIdsList
		{
			get { return new ReadonlyList(TransformedToLetterIds); }
		}
	}

	[ComVisible(true)]
	[Guid("F87CC29D-FF93-488C-83B0-AB8F148409F9")]
	public interface IDocflowV3
	{
		SenderTitleDocflow SenderTitle { get; }
		ConfirmationDocflow Confirmation { get; }
		ProxyResponseDocflow ProxyResponse { get; }
		ReceiptDocflowV3 RecipientReceipt { get; }
		RecipientResponseDocflow RecipientResponse { get; }
		AmendmentRequestDocflow AmendmentRequest { get; }
		RevocationDocflowV3 Revocation { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocflowV3")]
	[Guid("0D12A1F5-0393-4B79-8BEB-5941ED735A35")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocflowV3))]
	public partial class DocflowV3 : SafeComObject, IDocflowV3
	{
	}

	[ComVisible(true)]
	[Guid("633812A1-9ED4-4EBF-9A4F-FD679A4BF34C")]
	public interface ISenderTitleDocflow
	{
		bool IsFinished { get; }
		SignedAttachmentV3 Attachment { get; }
		Timestamp SentAt { get; }
		Timestamp DeliveredAt { get; }
		RoamingNotification RoamingNotification { get; }
		Documents.SenderSignatureStatus SenderSignatureStatus { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SenderTitleDocflow")]
	[Guid("AE208873-519B-4549-9A3B-2DB3B2D0C9A9")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISenderTitleDocflow))]
	public partial class SenderTitleDocflow : SafeComObject, ISenderTitleDocflow
	{
	}

	[ComVisible(true)]
	[Guid("B0B65E42-4A8C-4ABE-A02E-2DFF8A1AA9C8")]
	public interface IConfirmationDocflow
	{
		bool IsFinished { get; }
		SignedAttachmentV3 ConfirmationAttachment { get; }
		Timestamp ConfirmedAt { get; }
		ReceiptDocflowV3 Receipt { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ConfirmationDocflow")]
	[Guid("7ACDAE39-698E-49A9-B80E-87BA2A63E218")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IConfirmationDocflow))]
	public partial class ConfirmationDocflow : SafeComObject, IConfirmationDocflow
	{
	}

	[ComVisible(true)]
	[Guid("059D44EA-5671-49A2-9392-150526E268D7")]
	public interface IProxyResponseDocflow
	{
		bool IsFinished { get; }
		SignatureV3 Signature { get; }
		SignatureRejectionDocflow Rejection { get; }
		Documents.ProxySignatureStatus ProxySignatureStatus { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ProxyResponseDocflow")]
	[Guid("A0668D9F-5F8A-49F4-B100-010759B256BA")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IProxyResponseDocflow))]
	public partial class ProxyResponseDocflow : SafeComObject, IProxyResponseDocflow
	{
	}

	[ComVisible(true)]
	[Guid("089ADC9F-1FD9-4414-B18E-BBDC0DAF7578")]
	public interface ISignatureRejectionDocflow
	{
		SignedAttachmentV3 SignatureRejection { get; }
		bool IsFormal { get; }
		Timestamp DeliveredAt { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SignatureRejectionDocflow")]
	[Guid("7A3CDC4D-DC94-45A5-9BA7-27693D2E3A27")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISignatureRejectionDocflow))]
	public partial class SignatureRejectionDocflow : SafeComObject, ISignatureRejectionDocflow
	{
	}

	[ComVisible(true)]
	[Guid("B26897EA-BC79-4B32-B9AB-EBAEB1794581")]
	public interface IRecipientResponseDocflow
	{
		bool IsFinished { get; }
		SignatureV3 Signature { get; }
		SignedAttachmentV3 RecipientTitle { get; }
		SignatureRejectionDocflow Rejection { get; }
		Timestamp SentAt { get; }
		Timestamp DeliveredAt { get; }
		Documents.RecipientResponseStatus RecipientResponseStatus { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RecipientResponseDocflow")]
	[Guid("BA4DE041-1B8D-4079-B910-A746A97B28AF")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRecipientResponseDocflow))]
	public partial class RecipientResponseDocflow : SafeComObject, IRecipientResponseDocflow
	{
	}

	[ComVisible(true)]
	[Guid("E64CA69A-B454-47F4-99D5-62670F98612E")]
	public interface IAmendmentRequestDocflow
	{
		bool IsFinished { get; }
		SignedAttachmentV3 AmendmentRequest { get; }
		Timestamp SentAt { get; }
		Timestamp DeliveredAt { get; }
		ReceiptDocflowV3 Receipt { get; }
		int AmendmentFlags { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.AmendmentRequestDocflow")]
	[Guid("27C7E271-EBA2-4EE2-A7B3-7DBE8B41D96E")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IAmendmentRequestDocflow))]
	public partial class AmendmentRequestDocflow : SafeComObject, IAmendmentRequestDocflow
	{
	}

	[ComVisible(true)]
	[Guid("54F2001F-8AC8-4E63-8424-54E301E30924")]
	public interface IRevocationDocflowV3
	{
		bool IsFinished { get; }
		RevocationRequestDocflow RevocationRequest { get; }
		RevocationResponseDocflow RevocationResponse { get; }
		string InitiatorBoxId { get; }
		Documents.RevocationStatus RevocationStatus { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RevocationDocflowV3")]
	[Guid("792A5ED3-FAD0-4CE1-9F80-EE977ECEA2E1")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRevocationDocflowV3))]
	public partial class RevocationDocflowV3 : SafeComObject, IRevocationDocflowV3
	{
	}

	[ComVisible(true)]
	[Guid("35134850-7F1B-4E2F-A753-A0D633EFA5A2")]
	public interface IRevocationRequestDocflow
	{
		SignedAttachmentV3 RevocationRequest { get; }
		Timestamp SentAt { get; }
		Timestamp DeliveredAt { get; }
		RoamingNotification RoamingNotification { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RevocationRequestDocflow")]
	[Guid("E4603866-5443-402C-B77D-5909858210E4")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRevocationRequestDocflow))]
	public partial class RevocationRequestDocflow : SafeComObject, IRevocationRequestDocflow
	{
	}

	[ComVisible(true)]
	[Guid("F1F9B5E7-04CF-4DEA-974B-D9CA7A17257A")]
	public interface IRevocationResponseDocflow
	{
		SignatureV3 RecipientSignature { get; }
		SignatureRejectionDocflow SignatureRejection { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RevocationResponseDocflow")]
	[Guid("7FCD4018-7ECE-4462-AC3E-9481B816D158")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRevocationResponseDocflow))]
	public partial class RevocationResponseDocflow : SafeComObject, IRevocationResponseDocflow
	{
	}

	[ComVisible(true)]
	[Guid("376FF342-2F60-4999-8B98-874F291DDD95")]
	public interface IReceiptDocflowV3
	{
		bool IsFinished { get; }
		SignedAttachmentV3 ReceiptAttachment { get; }
		Timestamp SentAt { get; }
		Timestamp DeliveredAt { get; }
		ConfirmationDocflow Confirmation { get; }
		Documents.GeneralReceiptStatus Status { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ReceiptDocflowV3")]
	[Guid("BC20D8BD-3351-4A81-B596-9849856F21B7")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IReceiptDocflowV3))]
	public partial class ReceiptDocflowV3 : SafeComObject, IReceiptDocflowV3
	{
	}
}