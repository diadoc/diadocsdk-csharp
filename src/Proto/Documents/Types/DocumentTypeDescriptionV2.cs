using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Documents.Types
{
	[ComVisible(true)]
	[Guid("F09DEE95-CD69-4FF8-B55D-5F452C9C46E5")]
	public interface IDocumentTypeDescriptionV2
	{
		string Name { get; }
		string Title { get; }
		ReadonlyList SupportedDocflowList { get; }
		bool RequiresFnsRegistration { get; }
		ReadonlyList FunctionList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentTypeDescriptionV2")]
	[Guid("ACF64CB5-B8E8-4261-B184-63266C9E0881")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentTypeDescriptionV2))]
	public partial class DocumentTypeDescriptionV2 : SafeComObject, IDocumentTypeDescriptionV2
	{
		public ReadonlyList SupportedDocflowList
		{
			get { return new ReadonlyList(SupportedDocflows); }
		}

		public ReadonlyList FunctionList
		{
			get { return new ReadonlyList(Functions); }
		}
	}

	[ComVisible(true)]
	[Guid("616D7FE4-6AAA-4461-AF8D-3B498AB078D7")]
	public interface IGetDocumentTypesResponseV2
	{
		ReadonlyList DocumentTypeList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.GetDocumentTypesResponseV2")]
	[Guid("7D43C6F5-9637-4715-805D-6E2CA46D511E")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IGetDocumentTypesResponseV2))]
	public partial class GetDocumentTypesResponseV2 : SafeComObject, IGetDocumentTypesResponseV2
	{
		public ReadonlyList DocumentTypeList
		{
			get { return new ReadonlyList(DocumentTypes); }
		}
	}

	[ComVisible(true)]
	[Guid("F57033C7-7D2D-496D-A3F5-95E2C0425522")]
	public interface IDocumentFunctionV2
	{
		string Name { get; }
		ReadonlyList VersionList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentFunctionV2")]
	[Guid("7E622A26-69D0-4F4A-B1B9-25C13FF9F7E2")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentFunctionV2))]
	public partial class DocumentFunctionV2 : SafeComObject, IDocumentFunctionV2
	{
		public ReadonlyList VersionList
		{
			get { return new ReadonlyList(Versions); }
		}
	}

	[ComVisible(true)]
	[Guid("965FB0FC-4CE2-4CC5-AF0A-712D73D6FA7B")]
	public interface IDocumentVersionV2
	{
		string Version { get; }
		bool SupportsContentPatching { get; }
		bool SupportsEncrypting { get; }
		ReadonlyList TitleList { get; }
		bool IsActual { get; }
		ReadonlyList WorkflowList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentVersionV2")]
	[Guid("0A541D66-899E-4697-9B4F-A4042D059A73")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentVersionV2))]
	public partial class DocumentVersionV2 : SafeComObject, IDocumentVersionV2
	{
		public ReadonlyList TitleList
		{
			get { return new ReadonlyList(Titles); }
		}

		public ReadonlyList WorkflowList
		{
			get { return new ReadonlyList(Workflows); }
		}
	}

	[ComVisible(true)]
	[Guid("13668879-D13E-4817-B5F9-5B448A695CF7")]
	public interface IDocumentWorkflowV2
	{
		int Id { get; }
		bool IsDefault { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentWorkflowV2")]
	[Guid("0B9D2971-DA18-4183-9BC9-0F36200E9FCA")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentWorkflowV2))]
	public partial class DocumentWorkflowV2 : SafeComObject, IDocumentWorkflowV2
	{
	}

	[ComVisible(true)]
	[Guid("5BFE5A2A-CB6D-46F7-B28A-CA43FCFC6049")]
	public interface IDocumentTitleV2
	{
		bool IsFormal { get; }
		string XsdUrl { get; }
		ReadonlyList MetadataItemList { get; }
		ReadonlyList EncryptedMetadataItemList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentTitleV2")]
	[Guid("D18D72BC-5497-413C-BBF2-88D1BED8A2C6")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentTitleV2))]
	public partial class DocumentTitleV2 : SafeComObject, IDocumentTitleV2
	{
		public ReadonlyList MetadataItemList
		{
			get { return new ReadonlyList(MetadataItems); }
		}

		public ReadonlyList EncryptedMetadataItemList
		{
			get { return new ReadonlyList(EncryptedMetadataItems); }
		}
	}

	public interface ISignerInfoV2
	{
		int SignerTypeValue { get; }
		int ExtendedDocumentTitleTypeValue { get; }
		string SignerUserDataXsdUrlValue { get; }
	}

	public partial class SignerInfoV2 : SafeComObject, ISignerInfoV2
	{
		public int SignerTypeValue
		{
			get { return SignerType; }
		}

		public int ExtendedDocumentTitleTypeValue
		{
			get { return ExtendedDocumentTitleType; }
		}

		public string SignerUserDataXsdUrlValue
		{
			get { return SignerUserDataXsdUrl; }
		}
	}

	[ComVisible(true)]
	[Guid("7AFE5716-5867-44A3-A0F2-72E70D37C961")]
	public interface IDocumentMetadataItemV2
	{
		string Id { get; }
		int ItemType { get; }
		bool IsRequired { get; }
		int MetadataSource { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentMetadataItemV2")]
	[Guid("A8A76549-5D6F-450F-9B37-46F19C3C304B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentMetadataItemV2))]
	public partial class DocumentMetadataItemV2 : SafeComObject, IDocumentMetadataItemV2
	{
		int IDocumentMetadataItemV2.ItemType
		{
			get { return Type; }
		}

		int IDocumentMetadataItemV2.MetadataSource
		{
			get { return Source; }
		}
	}
}
