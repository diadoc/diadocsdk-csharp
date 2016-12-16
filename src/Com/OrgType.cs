using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("8B38C941-0227-4A92-8469-6845B81ACF11")]
	public enum OrgType
	{
		LegalEntity = 1,
		IndividualEntity = 2,
		ForeignEntity = 3
	}
}