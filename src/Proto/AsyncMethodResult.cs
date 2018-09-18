using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("6ACE70FA-930A-429F-B3A3-EAE6A4926E91")]
	public interface IAsyncMethodResult
	{
		string TaskId { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.AsyncMethodResult")]
	[Guid("82F05289-47D4-4A45-9AAC-E19A935479EA")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IAsyncMethodResult))]
	public partial class AsyncMethodResult : SafeComObject, IAsyncMethodResult
	{
	}
}
