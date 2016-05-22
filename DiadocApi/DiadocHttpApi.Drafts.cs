using System.Text;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public void RecycleDraft(string authToken, string boxId, string draftId)
		{
			var sb = new StringBuilder(string.Format("/RecycleDraft?boxId={0}", boxId));
			if (!string.IsNullOrEmpty(draftId)) sb.AppendFormat("&draftId={0}", draftId);
			PerformHttpRequest(authToken, "POST", sb.ToString());
		}

		public Message SendDraft(string authToken, DraftToSend draft, string operationId = null)
		{
			var queryString = string.Format("/SendDraft?operationId={0}", operationId);
			return PerformHttpRequest<DraftToSend, Message>(authToken, queryString, draft);
		}
	}
}
