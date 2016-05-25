using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("6D5CAF9B-AFFF-4B09-8C32-2D37178D5CB5")]
	public interface IContent
	{
		int Size { get; }
		byte[] Data { get; }
	}

	[ComVisible(true)]
	[Guid("C0CDAA2A-B201-4755-A5FC-63B42FD7EF4D")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IContent))]
	public partial class Content : SafeComObject, IContent
	{
	}
}
