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

	[ComVisible(true)]
	[Guid("D8ABB247-B272-4E1C-87B2-1FDD571CBA26")]
	public interface ISignatureRejectionGenerationRequestV2
	{
		string ErrorMessage { get; set; }
		string MessageId { get; set; }
		string AttachmentId { get; set; }
		byte[] SignerContent { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SignatureRejectionGenerationRequestV2")]
	[Guid("24AB7CDD-E239-46EF-8342-07A42558C024")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISignatureRejectionGenerationRequestV2))]
	public partial class SignatureRejectionGenerationRequestV2 : SafeComObject, ISignatureRejectionGenerationRequestV2
	{
	}
}
