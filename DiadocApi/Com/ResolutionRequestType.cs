using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("8B280FD8-0E8E-4FCC-8586-288DD94A7437")]
	public enum ResolutionRequestType
	{
		ApprovementRequest = Proto.Events.ResolutionRequestType.ApprovementRequest,
		SignatureRequest = Proto.Events.ResolutionRequestType.SignatureRequest
	}
}