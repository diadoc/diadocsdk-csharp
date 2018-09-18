using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("9CF9E7FC-20B5-4FC5-9A1F-F1D01C9BC46C")]
	public interface IContent_v2
	{
		byte[] Content { get; set; }
		string NameOnShelf { get; set; }
		string PatchedContentId { get; set; }
	}

	[ComVisible(true)]
	[Guid("B7EE010C-0B39-461A-802F-42D2E2611081")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IContent_v2))]
	public partial class Content_v2 : SafeComObject, IContent_v2
	{
	}
}