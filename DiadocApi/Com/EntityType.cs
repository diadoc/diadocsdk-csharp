using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("6BC9D5FE-B0E5-4595-A420-23ECC87082DC")]
	public enum EntityType
	{
		UnknownEntityType = 0,
		Attachment = 1,
		Signature = 2
	}
}
