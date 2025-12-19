using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("710DBFD8-D7B6-4377-B5F4-EB442A7483C5")]
	public interface IGetDocflowBatchResponseV4
	{
		ReadonlyList DocumentsList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.GetDocflowBatchResponseV4")]
	[Guid("8C8CB298-3746-430D-988F-762DE857B415")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IGetDocflowBatchResponseV4))]
	public partial class GetDocflowBatchResponseV4 : SafeComObject, IGetDocflowBatchResponseV4
	{
		public ReadonlyList DocumentsList => new ReadonlyList(Documents);
	}

	[ComVisible(true)]
	[Guid("B0D1CBDD-F2AC-49EA-B364-67FF46712E99")]
	public interface ISearchDocflowsResponseV4
	{
		ReadonlyList DocumentsList { get; }
		bool HaveMoreDocuments { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SearchDocflowsResponseV4")]
	[Guid("02163104-5651-44C1-843D-F05F73D0F954")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISearchDocflowsResponseV4))]
	public partial class SearchDocflowsResponseV4 : SafeComObject, ISearchDocflowsResponseV4
	{
		public ReadonlyList DocumentsList { get; }
	}

	[ComVisible(true)]
	[Guid("6DC5108A-00A5-44B3-9C0B-A942D93EB3CB")]
	public interface IFetchedDocumentV4
	{
		DocumentWithDocflowV4 Document { get; }
		byte[] IndexKey { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.FetchedDocumentV4")]
	[Guid("4ACD23BE-EE2C-4F82-803E-E3C9C7F74953")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IFetchedDocumentV4))]
	public partial class FetchedDocumentV4 : SafeComObject, IFetchedDocumentV4
	{
	}

	[ComVisible(true)]
	[Guid("D166E43E-86DB-43CF-BBFE-AA3B2D381D5B")]
	public interface IGetDocflowsByPacketIdResponseV4
	{
		ReadonlyList DocumentsList { get; }
		byte[] NextPageIndexKey { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.GetDocflowsByPacketIdResponseV4")]
	[Guid("210F035F-854C-4674-ABF8-59021FE4FBB0")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IGetDocflowsByPacketIdResponseV4))]
	public partial class GetDocflowsByPacketIdResponseV4 : SafeComObject, IGetDocflowsByPacketIdResponseV4
	{
		public ReadonlyList DocumentsList => new ReadonlyList(Documents);
	}

	[ComVisible(true)]
	[Guid("888C64D6-A897-4C39-8838-FE9FF529E9C1")]
	public interface IGetDocflowEventsResponseV4
	{
		int TotalCount { get; }
		ReadonlyList EventsList { get; }
		Com.TotalCountType TotalCountTypeValue { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.GetDocflowEventsResponseV4")]
	[Guid("A999FD4E-49B8-4F1B-AAF7-F6883F6353C7")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IGetDocflowEventsResponseV4))]
	public partial class GetDocflowEventsResponseV4 : SafeComObject, IGetDocflowEventsResponseV4
	{
		public ReadonlyList EventsList => new ReadonlyList(Events);

		public Com.TotalCountType TotalCountTypeValue => (Com.TotalCountType) TotalCountType;
	}

	[ComVisible(true)]
	[Guid("67D80925-F327-4DE6-9CB8-D5129C7C638A")]
	public interface IDocflowEventV4
	{
		string EventId { get; }
		Timestamp Timestamp { get; }
		DocumentId DocumentId { get; }
		byte[] IndexKey { get; }
		DocumentWithDocflowV4 Document { get; }
		string PreviousEventId { get; }
		DocumentWithDocflowV4 PreviousDocumentState { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocflowEventV4")]
	[Guid("0F13DDED-C2C9-4FD5-B39C-8C0704E6F03A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocflowEventV4))]
	public partial class DocflowEventV4 : SafeComObject, IDocflowEventV4
	{
	}

	[ComVisible(true)]
	[Guid("1899B59E-D255-4B3A-8C4F-ACF96FABEFB8")]
	public interface IDocumentWithDocflowV4
	{
		DocumentId DocumentId { get; }
		LastEvent LastEvent { get; }
		DocumentInfoV3 DocumentInfo { get; }
		DocflowV4 Docflow { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentWithDocflowV4")]
	[Guid("D069CF01-C81F-4423-A713-6DC79AA227F2")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentWithDocflowV4))]
	public partial class DocumentWithDocflowV4 : SafeComObject, IDocumentWithDocflowV4
	{
	}

	[ComVisible(true)]
	[Guid("667AE14B-FBDB-4AE5-98D4-45C55679BF25")]
	public interface IDocflowV4
	{
		SenderTitleDocflow SenderTitle { get; }
		ConfirmationDocflowV4 Confirmation { get; }
		ParticipantResponseDocflowV4 ProxyResponse { get; }
		ReceiptDocflowV4 RecipientReceipt { get; }
		ParticipantResponseDocflowV4 RecipientResponse { get; }
		AmendmentRequestDocflowV4 AmendmentRequest { get; }
		RevocationDocflowV4 Revocation { get; }
		ResolutionDocflowV3 Resolution { get; }
		ResolutionEntitiesV3 ResolutionEntities { get; }
		ReadonlyList OuterDocflowsList { get; }
		ReadonlyList OuterDocflowEntitiesList { get; }
		DocflowStatusV3 DocflowStatus { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocflowV4")]
	[Guid("3E6402F5-FC9E-45D2-B0A5-89A6F1708483")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocflowV4))]
	public partial class DocflowV4 : SafeComObject, IDocflowV4
	{
		public ReadonlyList OuterDocflowsList => new ReadonlyList(OuterDocflows);

		public ReadonlyList OuterDocflowEntitiesList => new ReadonlyList(OuterDocflowEntities);
	}

	[ComVisible(true)]
	[Guid("54EFBD02-436A-4D3C-9490-28499FBEA34A")]
	public interface IConfirmationDocflowV4
	{
		bool IsFinished { get; }
		SignedAttachmentV3 ConfirmationAttachment { get; }
		Timestamp ConfirmedAt { get; }
		ReceiptDocflowV4 Receipt { get; }
		OperatorConfirmationDocflow RoamingConfirmation { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ConfirmationDocflowV4")]
	[Guid("2B18E6D6-A80B-48FC-8288-42D38F04EB81")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IConfirmationDocflowV4))]
	public partial class ConfirmationDocflowV4 : SafeComObject, IConfirmationDocflowV4
	{
	}

	[ComVisible(true)]
	[Guid("FF801C98-BE41-401E-A7BC-562C32961B20")]
	public interface ISignatureRejectionDocflowV4
	{
		SignedAttachmentV3 SignatureRejection { get; }
		bool IsFormal { get; }
		Timestamp DeliveredAt { get; }
		string PlainText { get; }
		UniversalMessageAttachmentDocflow UniversalMessage { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SignatureRejectionDocflowV4")]
	[Guid("DF5A9183-A8A7-4E90-8AC0-0E1DDEC59470")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISignatureRejectionDocflowV4))]
	public partial class SignatureRejectionDocflowV4 : SafeComObject, ISignatureRejectionDocflowV4
	{
	}

	[ComVisible(true)]
	[Guid("0F14E1B9-50C8-48C2-AE27-7B3D430586EF")]
	public interface IParticipantResponseDocflowV4
	{
		bool IsFinished { get; }
		SignatureV3 Signature { get; }
		SignedAttachmentV3 Title { get; }
		SignatureRejectionDocflowV4 Rejection { get; }
		Timestamp SentAt { get; }
		Timestamp DeliveredAt { get; }
		Com.RecipientResponseStatus ResponseStatusValue { get; }
		ConfirmationDocflowV4 Confirmation { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RecipientResponseDocflowV4")]
	[Guid("23E0A6EC-52C5-4AAC-90DC-1C20DDE98417")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IParticipantResponseDocflowV4))]
	public partial class ParticipantResponseDocflowV4 : SafeComObject, IParticipantResponseDocflowV4
	{
		public Com.RecipientResponseStatus ResponseStatusValue => (Com.RecipientResponseStatus) ResponseStatus;
	}

	[ComVisible(true)]
	[Guid("804C9295-E746-47C6-8859-E85EB840898F")]
	public interface IAmendmentRequestDocflowV4
	{
		bool IsFinished { get; }
		SignedAttachmentV3 AmendmentRequest { get; }
		Timestamp SentAt { get; }
		Timestamp DeliveredAt { get; }
		ReceiptDocflowV4 Receipt { get; }
		int AmendmentFlags { get; }
		string PlainText { get; }
		ConfirmationDocflowV4 ConfirmationDocflow { get; }
		UniversalMessageAttachmentDocflow UniversalMessage { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.AmendmentRequestDocflowV4")]
	[Guid("064BF1FD-2909-4823-BA56-45FF58F453EE")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IAmendmentRequestDocflowV4))]
	public partial class AmendmentRequestDocflowV4 : SafeComObject, IAmendmentRequestDocflowV4
	{
	}

	[ComVisible(true)]
	[Guid("A52EE10B-657C-4DDC-AC01-55EC08D5477C")]
	public interface IRevocationDocflowV4
	{
		bool IsFinished { get; }
		RevocationRequestDocflow RevocationRequest { get; }
		RevocationResponseDocflowV4 RevocationResponse { get; }
		string InitiatorBoxId { get; }
		Com.RevocationStatus RevocationStatusValue { get; }
		ReadonlyList OuterDocflowEntitiesList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RevocationDocflowV4")]
	[Guid("08B824D1-214F-46D4-B562-F025F157D2D4")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRevocationDocflowV4))]
	public partial class RevocationDocflowV4 : SafeComObject, IRevocationDocflowV4
	{
		public Com.RevocationStatus RevocationStatusValue => (Com.RevocationStatus) RevocationStatus;

		public ReadonlyList OuterDocflowEntitiesList => new ReadonlyList(OuterDocflowEntities);
	}

	[ComVisible(true)]
	[Guid("8B2A3EA0-2CFA-45A7-8F5D-07DC24766DC9")]
	public interface IRevocationResponseDocflowV4
	{
		SignatureV3 RecipientSignature { get; }
		SignatureRejectionDocflowV4 SignatureRejection { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RevocationResponseDocflowV4")]
	[Guid("02908F43-2378-4C55-B9EA-9F3EBEEB4D24")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRevocationResponseDocflowV4))]
	public partial class RevocationResponseDocflowV4 : SafeComObject, IRevocationResponseDocflowV4
	{
	}

	[ComVisible(true)]
	[Guid("3614542A-0540-43A8-B3C3-2B6935B2AB0A")]
	public interface IReceiptDocflowV4
	{
		bool IsFinished { get; }
		SignedAttachmentV3 ReceiptAttachment { get; }
		Timestamp SentAt { get; }
		Timestamp DeliveredAt { get; }
		ConfirmationDocflowV4 Confirmation { get; }
		Com.GeneralReceiptStatus StatusValue { get; }
		UniversalMessageAttachmentDocflow UniversalMessage { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ReceiptDocflowV4")]
	[Guid("11AADAA5-C14C-4E77-803F-AAFDF2CD2D48")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IReceiptDocflowV4))]
	public partial class ReceiptDocflowV4 : SafeComObject, IReceiptDocflowV4
	{
		public Com.GeneralReceiptStatus StatusValue => (Com.GeneralReceiptStatus) Status;
	}
}
