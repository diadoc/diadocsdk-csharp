using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Invoicing
{
	[ComVisible(true)]
	[Guid("695822E1-BB8D-433A-8B02-AC038B1BEF02")]
	public interface IInvoiceCorrectionRequestInfo
	{
		Signer Signer { get; set; }
		string ErrorMessage { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.InvoiceCorrectionRequestInfo")]
	[Guid("E3473BDB-8F06-43cb-97D8-A5EDF7E505B6")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IInvoiceCorrectionRequestInfo))]
	public partial class InvoiceCorrectionRequestInfo : SafeComObject, IInvoiceCorrectionRequestInfo
	{
	}

	[ComVisible(true)]
	[Guid("A5A07BBA-1818-4F54-9E61-BD096842C41F")]
	public interface IInvoiceCorrectionRequestGenerationRequestV2
	{
		string ErrorMessage { get; set; }
		string MessageId { get; set; }
		string AttachmentId { get; set; }
		byte[] SignerContent { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.InvoiceCorrectionRequestGenerationRequestV2")]
	[Guid("BB970027-D15B-4BE6-A296-188F54E1E838")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IInvoiceCorrectionRequestGenerationRequestV2))]
	public partial class InvoiceCorrectionRequestGenerationRequestV2 : SafeComObject, IInvoiceCorrectionRequestGenerationRequestV2
	{
	}
}
