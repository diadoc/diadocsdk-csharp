using System.Threading.Tasks;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.DocumentSigning;


namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<SigningResponse> PostMessageToSignAsync(string authToken, MessageToPost msg)
		{
			return PerformHttpRequestAsync<MessageToPost, SigningResponse>(authToken, "/V1/PostMessageToSign", msg);
		}

		public Task<SigningResponse> PostMessagePatchToSignAsync(string authToken, MessagePatchToPostV2 patch)
		{
			return PerformHttpRequestAsync<MessagePatchToPostV2, SigningResponse>(authToken, "/V1/PostMessagePatchToSign", patch);
		}
	}
}
