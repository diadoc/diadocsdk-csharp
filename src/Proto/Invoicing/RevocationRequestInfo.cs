using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Invoicing
{
	[ComVisible(true)]
	[Guid("2B6031D4-94BD-4441-8690-3C715498D126")]
	public interface IRevocationRequestInfo
	{
		Signer Signer { get; set; }
		string Comment { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RevocationRequestInfo")]
	[Guid("9DEE6DAA-6698-495C-B9E3-20CD9588BC2D")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRevocationRequestInfo))]
	public partial class RevocationRequestInfo : SafeComObject, IRevocationRequestInfo
	{
	}
}