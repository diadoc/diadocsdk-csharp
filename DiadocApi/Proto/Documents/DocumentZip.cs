using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Documents
{
	[ComVisible(true)]
	[Guid("304AE8FB-DFAE-4BAA-8321-67E334EC8E1D")]
	public interface IDocumentZipGenerationResult
	{
		int RetryAfter { get; set; }
		string ZipFileNameOnShelf { get; set; }
	}

	[ComVisible(true)]
	[Guid("79B5F934-E2AB-47EE-8724-2A5A5130A172")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentZipGenerationResult))]
	public partial class DocumentZipGenerationResult: SafeComObject, IDocumentZipGenerationResult
	{
		public int RetryAfter { get; set; }
	}
}