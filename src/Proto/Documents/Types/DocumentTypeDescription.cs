using System.Linq;
using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Documents.Types
{
	[ComVisible(true)]
	[Guid("DC526A35-DF41-4E26-A725-907C5CBB43A3")]
	public interface IDocumentTypeDescription
	{
		string Name { get; }
		string Title { get; }
		ReadonlyList SupportedDocflowList { get; }
		bool RequiresFnsRegistration { get; }
		ReadonlyList FunctionList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentTypeDescription")]
	[Guid("066E8F08-9A38-4DA7-879A-22C636E12065")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentTypeDescription))]
	public partial class DocumentTypeDescription : SafeComObject, IDocumentTypeDescription
	{
		ReadonlyList IDocumentTypeDescription.SupportedDocflowList
		{
			get { return new ReadonlyList(SupportedDocflows.Select(x => (Com.DocumentDocflow)(int)x).ToArray()); }
		}

		ReadonlyList IDocumentTypeDescription.FunctionList
		{
			get { return new ReadonlyList(Functions); }
		}
	}

	[ComVisible(true)]
	[Guid("CBA931B9-9E3F-4EAF-AA3B-1850A6AFCB49")]
	public interface IGetDocumentTypesResponse
	{
		ReadonlyList DocumentTypeList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.GetDocumentTypesResponse")]
	[Guid("EED1BB2E-626C-4CC7-9DB4-E3E1F34DF5CB")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IGetDocumentTypesResponse))]
	public partial class GetDocumentTypesResponse : SafeComObject, IGetDocumentTypesResponse
	{
		ReadonlyList IGetDocumentTypesResponse.DocumentTypeList
		{
			get { return new ReadonlyList(DocumentTypes); }
		}
	}

	[ComVisible(true)]
	[Guid("86A87404-9C37-4CE3-8F0C-8FA5FFC5760C")]
	public interface IDocumentFunction
	{
		string Name { get; }
		ReadonlyList VersionList { get; }
		ReadonlyList WorkflowList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentFunction")]
	[Guid("18437F9A-8B18-47E0-886F-C53D24E19605")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentFunction))]
	public partial class DocumentFunction : SafeComObject, IDocumentFunction
	{
		ReadonlyList IDocumentFunction.VersionList
		{
			get { return new ReadonlyList(Versions); }
		}

		ReadonlyList IDocumentFunction.WorkflowList
		{
			get { return new ReadonlyList(Workflows); }
		}
	}

	[ComVisible(true)]
	[Guid("8F13D26C-55A0-41F5-8BF3-4574E7FCBAE0")]
	public interface IDocumentVersion
	{
		string Version { get; }
		bool SupportsContentPatching { get; }
		bool SupportsEncrypting { get; }
		ReadonlyList TitleList { get; }
		bool IsActual { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentVersion")]
	[Guid("363C9AC7-52EA-4B5D-AE99-1B0F573D63EE")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentVersion))]
	public partial class DocumentVersion : SafeComObject, IDocumentVersion
	{
		ReadonlyList IDocumentVersion.TitleList
		{
			get { return new ReadonlyList(Titles); }
		}
	}

	[ComVisible(true)]
	[Guid("86DDBB71-A4E5-4586-A304-3EE1FFFB8CEC")]
	public interface IDocumentWorkflow
	{
		int Id { get; }
		bool IsDefault { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentWorkflow")]
	[Guid("B9485BE6-614F-4028-A170-9B720CEB7DAD")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentWorkflow))]
	public partial class DocumentWorkflow : SafeComObject, IDocumentWorkflow
	{
	}

	[ComVisible(true)]
	[Guid("E6F9BDD8-9813-4DED-8BC7-3CA747EF7446")]
	public interface IDocumentTitle
	{
		bool IsFormal { get; }
		string XsdUrl { get; }
		ReadonlyList MetadataItemList { get; }
		ReadonlyList EncryptedMetadataItemList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentTitle")]
	[Guid("07C1A293-101F-4691-A5B2-A0A9D29F80F8")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentTitle))]
	public partial class DocumentTitle : SafeComObject, IDocumentTitle
	{
		ReadonlyList IDocumentTitle.MetadataItemList
		{
			get { return new ReadonlyList(MetadataItems); }
		}

		ReadonlyList IDocumentTitle.EncryptedMetadataItemList
		{
			get { return new ReadonlyList(EncryptedMetadataItems); }
		}
	}

	[ComVisible(true)]
	[Guid("760F4134-378A-444C-96D3-233D43222A43")]
	public interface IDocumentMetadataItem
	{
		string Id { get; }
		Com.DocumentMetadataItemType ItemType { get; }
		bool IsRequired { get; }
		Com.DocumentMetadataSource MetadataSource { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentMetadataItem")]
	[Guid("C4D99441-9336-458B-BED7-2C0B63C739CD")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentMetadataItem))]
	public partial class DocumentMetadataItem : SafeComObject, IDocumentMetadataItem
	{
		Com.DocumentMetadataItemType IDocumentMetadataItem.ItemType
		{
			get { return (Com.DocumentMetadataItemType) (int) Type; }
		}

		Com.DocumentMetadataSource IDocumentMetadataItem.MetadataSource
		{
			get { return (Com.DocumentMetadataSource)(int)Source; }
		}
	}
}