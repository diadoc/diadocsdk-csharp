using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Invoicing
{
	[ComVisible(true)]
	[Guid("846B6232-839A-4D8B-80E2-1E902776B438")]
	public interface ISignatureRejectionInfo
	{
		Signer Signer { get; set; }
		string ErrorMessage { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SignatureRejectionInfo")]
	[Guid("D18755A6-DF53-4642-8F68-A916939A1015")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISignatureRejectionInfo))]
	public partial class SignatureRejectionInfo : SafeComObject, ISignatureRejectionInfo
	{
	}
}