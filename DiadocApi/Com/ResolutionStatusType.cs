using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("B0A83874-31C1-400F-931B-646551A2A2F1")]
	public enum ResolutionStatusType
	{
		None = 0,
		Approved = 1,
		Disapproved = 2,
		ApprovementRequested = 3,
		SignatureRequested = 4,
		SignatureDenied = 5
	}
}