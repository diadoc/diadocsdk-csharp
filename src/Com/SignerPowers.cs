using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("A5F72179-B865-4FA0-86CF-085C961A3F62")]
	public enum SignerPowers
	{
		InvoiceSigner = 0,
		PersonMadeOperation = 1,
		MadeAndSignOperation = 2,
		PersonDocumentedOperation = 3,
		MadeOperationAndSignedInvoice = 4,
		MadeAndResponsibleForOperationAndSignedInvoice = 5,
		ResponsibleForOperationAndSignerForInvoice = 6
	}
}