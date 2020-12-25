using System.Threading;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task RecycleDraftAsync(string authToken, string boxId, string draftId, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/RecycleDraft");
			qsb.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(draftId))
				qsb.AddParameter("draftId", draftId);
			return PerformHttpRequestAsync(authToken, "POST", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<Message> SendDraftAsync(string authToken, DraftToSend draft, string operationId = null, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/SendDraft");
			qsb.AddParameter("operationId", operationId);
			return PerformHttpRequestAsync<DraftToSend, Message>(authToken, qsb.BuildPathAndQuery(), draft, ct: ct);
		}
	}
}
