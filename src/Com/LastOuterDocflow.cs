using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Documents
{
	[ComVisible(true)]
	[Guid("05E7C8A3-6D4D-49BD-ADA7-83118EC0843F")]
	public interface ILastOuterDocflow
	{
		string ParentEntityId { get; }
		OuterDocflowInfo OuterDocflow { get; }
	}

	[ComVisible(true)]
	[Guid("9B973BB4-E678-4E46-B585-D8F8BA67E4CF")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ILastOuterDocflow))]
	public partial class LastOuterDocflow : SafeComObject, ILastOuterDocflow
	{
	}
}
