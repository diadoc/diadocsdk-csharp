using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("AEF49130-2AC4-4B2F-A8EE-67DF184D5788")]
	public enum SignerStatus
	{
		SellerEmployee = 1,
		InformationCreatorEmployee = 2,
		OtherOrganizationEmployee = 3,
		AuthorizedPerson = 4
	}
}