using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("FE21CF3E-D903-45A8-B7EF-3F646B9E0336")]
	public enum ResolutionType
	{
		UndefinedResolutionType = Proto.Events.ResolutionType.UndefinedResolutionType,
		Approve = Proto.Events.ResolutionType.Approve,
		Disapprove = Proto.Events.ResolutionType.Disapprove,
		UnknownResolutionType = Proto.Events.ResolutionType.UnknownResolutionType
	}
}