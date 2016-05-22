using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("E426A34C-85D6-44FB-97E0-2BBF4F1490E7")]
	public enum DocumentDirection
	{

		UnknownDocumentDirection = Diadoc.Api.Proto.DocumentDirection.UnknownDocumentDirection,
		Inbound = Diadoc.Api.Proto.DocumentDirection.Inbound,
		Outbound = Diadoc.Api.Proto.DocumentDirection.Outbound,
		Internal = Diadoc.Api.Proto.DocumentDirection.Internal
	}
}