using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("2246990F-984D-4D92-8FD7-F9F916503BF2")]
	public interface IOuterDocflowInfo
	{
		string DocflowNamedId { get; }
		string DocflowFriendlyName { get; }
		Status Status { get; }
	}

	[ComVisible(true)]
	[Guid("D6569917-F69C-4031-A33C-5955C4D7D735")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IOuterDocflowInfo))]
	public partial class OuterDocflowInfo : SafeComObject, IOuterDocflowInfo
	{
	}
}
