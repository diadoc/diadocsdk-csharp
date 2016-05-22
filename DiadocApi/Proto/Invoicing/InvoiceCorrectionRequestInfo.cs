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
}
