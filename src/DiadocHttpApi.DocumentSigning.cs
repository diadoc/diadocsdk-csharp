using Diadoc.Api.Proto.DocumentSigning;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public SigningResponse PostMessageToSign(string authToken, MessageToPost msg)
		{
			return PerformHttpRequest<MessageToPost, SigningResponse>(authToken, "/V1/PostMessage", msg);
		}

		public SigningResponse PostMessagePatchToSign(string authToken, MessagePatchToPostV2 patch)
		{
			return PerformHttpRequest<MessagePatchToPostV2, SigningResponse>(authToken, "/V1/PostMessagePatchToSign", patch);
		}
	}
}
