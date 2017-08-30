using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Events
{
	[ComVisible(true)]
	[Guid("07C9B40F-E0B4-44F6-B2DD-5CFCB5ECB58E")]
	public interface ICancellationInfo
	{
		string Author { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.CancellationInfo")]
	[Guid("621A5E43-B342-44DA-B0A5-33729949B654")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICancellationInfo))]
	public partial class CancellationInfo : SafeComObject, ICancellationInfo
	{
	}
}