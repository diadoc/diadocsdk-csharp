using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("ECB3ABEE-4383-4306-A598-A51AD6753356")]
	public enum FunctionType
	{
		Invoice = 0,
		Basic = 1,
		InvoiceAndBasic = 2
	}
}