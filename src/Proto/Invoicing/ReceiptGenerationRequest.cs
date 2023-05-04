using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Invoicing
{
	[ComVisible(true)]
	[Guid("139036A5-5EA6-4E79-8359-842C4D8393D3")]
	public interface IReceiptGenerationRequestV2
	{
		string MessageId { get; set; }
		string AttachmentId { get; set; }
		byte[] SignerContent { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ReceiptGenerationRequestV2")]
	[Guid("5801B974-0551-4132-A505-D4B950C40EF4")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IReceiptGenerationRequestV2))]
	public partial class ReceiptGenerationRequestV2 : SafeComObject, IReceiptGenerationRequestV2
	{
	}
}
