using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("9F31FF5F-0D30-496E-98A0-5A52EFCEDA26")]
	public interface IUniversalMessageAttachmentDocflow
	{
		Attachment Attachment { get; }
		Diadoc.Api.Proto.UniversalMessageInfo MessageInfo { get; }
		string ContentTypeId { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.UniversalMessageAttachmentDocflow")]
	[Guid("69FCC7A5-A0C7-4419-88E2-CFE389B034B3")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUniversalMessageAttachmentDocflow))]
	public partial class UniversalMessageAttachmentDocflow : SafeComObject, IUniversalMessageAttachmentDocflow
	{
	}
}
