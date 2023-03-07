using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Workflows
{
	[ComVisible(true)]
	[Guid("424305EE-BD53-4F77-8610-26364700B100")]
	public interface IDocumentWorkflowSettingsListV2
	{
		ReadonlyList DocumentWorkflowSettingsList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentWorkflowSettingsListV2")]
	[Guid("F13F1996-AE17-45D4-B75D-CC65BD90B79F")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentWorkflowSettingsListV2))]
	public partial class DocumentWorkflowSettingsListV2 : SafeComObject, IDocumentWorkflowSettingsListV2
	{
		public ReadonlyList DocumentWorkflowSettingsList
		{
			get { return new ReadonlyList(DocumentWorkflowSettings); }
		}
	}

	[ComVisible(true)]
	[Guid("B4904434-9B7E-4497-8E3A-D1F47402F42B")]
	public interface IDocumentWorkflowSettingsV2
	{
		int Id { get; }
		ReadonlyList ParticipantsList { get; }
		Com.OperatorConfirmationReceiptBehavior OperatorConfirmationReceiptBehavior { get; }
		Com.ReceiptOperatorConfirmationReceiptBehavior ReceiptOperatorConfirmationReceiptBehavior { get; }
		Com.OperatorConfirmationBehavior ReceiptOperatorConfirmationBehavior { get; }
		Com.AmendmentRequestResponseBehavior AmendmentRequestResponseBehavior { get; }
		Com.RoamingConfirmationBehavior ReceiptRoamingConfirmationBehavior { get; }
		Com.InvitationBehavior InvitationBehavior { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentWorkflowSettingsV2")]
	[Guid("E339E532-402B-4593-9286-EC92120D4F68")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentWorkflowSettingsV2))]
	public partial class DocumentWorkflowSettingsV2 : SafeComObject, IDocumentWorkflowSettingsV2
	{
		public ReadonlyList ParticipantsList
		{
			get { return new ReadonlyList(Participants); }
		}

		Com.ReceiptOperatorConfirmationReceiptBehavior IDocumentWorkflowSettingsV2.ReceiptOperatorConfirmationReceiptBehavior
		{
			get { return (Com.ReceiptOperatorConfirmationReceiptBehavior) ReceiptOperatorConfirmationReceiptBehavior; }
		}

		Com.OperatorConfirmationBehavior IDocumentWorkflowSettingsV2.ReceiptOperatorConfirmationBehavior
		{
			get { return (Com.OperatorConfirmationBehavior) ReceiptOperatorConfirmationBehavior; }
		}

		Com.OperatorConfirmationReceiptBehavior IDocumentWorkflowSettingsV2.OperatorConfirmationReceiptBehavior
		{
			get { return (Com.OperatorConfirmationReceiptBehavior) OperatorConfirmationReceiptBehavior; }
		}

		Com.AmendmentRequestResponseBehavior IDocumentWorkflowSettingsV2.AmendmentRequestResponseBehavior
		{
			get { return (Com.AmendmentRequestResponseBehavior) AmendmentRequestResponseBehavior; }
		}

		Com.RoamingConfirmationBehavior IDocumentWorkflowSettingsV2.ReceiptRoamingConfirmationBehavior
		{
			get { return (Com.RoamingConfirmationBehavior) ReceiptRoamingConfirmationBehavior; }
		}

		Com.InvitationBehavior IDocumentWorkflowSettingsV2.InvitationBehavior
		{
			get { return (Com.InvitationBehavior) InvitationBehavior; }
		}
	}

	[ComVisible(true)]
	[Guid("4D6B776C-F640-4FC5-9EAD-480884FD9B21")]
	public interface IParticipantSettingV2
	{
		Com.ParticipantType Participant { get; }
		Com.ParticipantAction ParticipantAction { get; }
		Com.TitleReceiptBehavior TitleReceiptBehavior { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ParticipantSettingV2")]
	[Guid("79AAB5C9-89DC-404A-B8A4-3086189379A3")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IParticipantSettingV2))]
	public partial class ParticipantSettingV2 : SafeComObject, IParticipantSettingV2
	{
		Com.ParticipantType IParticipantSettingV2.Participant
		{
			get { return (Com.ParticipantType) Participant; }
		}

		Com.ParticipantAction IParticipantSettingV2.ParticipantAction
		{
			get { return (Com.ParticipantAction) ParticipantAction; }
		}

		Com.TitleReceiptBehavior IParticipantSettingV2.TitleReceiptBehavior
		{
			get { return (Com.TitleReceiptBehavior) TitleReceiptBehavior; }
		}
	}
}

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("6FE135A4-F9B9-4923-926C-48EC824A62BC")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "OperatorConfirmationReceiptBehavior", Namespace = "https://diadoc-api.kontur.ru")]
	public enum OperatorConfirmationReceiptBehavior
	{
		Unknown = 0,
		Never = 1,
		Always = 2
	}

	[ComVisible(true)]
	[Guid("A0218667-E335-470E-AA9A-2F515D6C8D1B")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "ReceiptOperatorConfirmationReceiptBehavior", Namespace = "https://diadoc-api.kontur.ru")]
	public enum ReceiptOperatorConfirmationReceiptBehavior
	{
		Unknown = 0,
		Never = 1,
		Always = 2
	}

	[ComVisible(true)]
	[Guid("56134338-5087-4C83-AEBF-EF46BB6601D9")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "OperatorConfirmationBehavior", Namespace = "https://diadoc-api.kontur.ru")]
	public enum OperatorConfirmationBehavior
	{
		Unknown = 0,
		Never = 1,
		Initiator = 2,
		InitiatorCounterpart = 3
	}

	[ComVisible(true)]
	[Guid("D32EB08E-97B2-45D3-9985-08A84322ED61")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "AmendmentRequestResponseBehavior", Namespace = "https://diadoc-api.kontur.ru")]
	public enum AmendmentRequestResponseBehavior
	{
		Unknown = 0,
		None = 1,
		Receipt = 2,
		OperatorConfirmation = 3,
		OperatorConfirmationOrReceipt = 4
	}

	[ComVisible(true)]
	[Guid("A4123FFA-E4DF-4D65-8786-7F36F9F16FC8")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "RoamingConfirmationBehavior", Namespace = "https://diadoc-api.kontur.ru")]
	public enum RoamingConfirmationBehavior
	{
		Unknown = 0,
		Never = 1,
		Always = 2
	}

	[ComVisible(true)]
	[Guid("217F4417-D89E-4193-9CD0-F8F9262F946A")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "InvitationBehavior", Namespace = "https://diadoc-api.kontur.ru")]
	public enum InvitationBehavior
	{
		Unknown = 0,
		Never = 1,
		DefineByUser = 2,
		Always = 3
	}

	[ComVisible(true)]
	[Guid("CC3FE0B3-930C-490A-983E-32384283B2BD")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "ParticipantType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum ParticipantType
	{
		Unknown = 0,
		Sender = 1,
		Proxy = 2,
		Recipient = 3
	}

	[ComVisible(true)]
	[Guid("2C3152A8-E7A2-4D84-835E-D86A481765D3")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "ParticipantAction", Namespace = "https://diadoc-api.kontur.ru")]
	public enum ParticipantAction
	{
		Unknown = 0,
		Title = 1,
		Signature = 2,
		OptionalSignature = 3
	}

	[ComVisible(true)]
	[Guid("03818A56-55D6-4B00-BDAF-4235044B8221")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "TitleReceiptBehavior", Namespace = "https://diadoc-api.kontur.ru")]
	public enum TitleReceiptBehavior
	{
		Unknown = 0,
		Never = 1,
		DefineByUser = 2,
		Always = 3
	}
}
