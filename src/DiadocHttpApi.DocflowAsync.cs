using System.Threading.Tasks;
using Diadoc.Api.Proto.Docflow;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[NotNull]
		public Task<GetDocflowBatchResponse> GetDocflowsAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetDocflowBatchRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetDocflows", boxId);
			return PerformHttpRequestAsync<GetDocflowBatchRequest, GetDocflowBatchResponse>(authToken, queryString, request);
		}

		[NotNull]
		public Task<GetDocflowEventsResponse> GetDocflowEventsAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetDocflowEventsRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetDocflowEvents", boxId);
			return PerformHttpRequestAsync<GetDocflowEventsRequest, GetDocflowEventsResponse>(authToken, queryString, request);
		}

		[NotNull]
		public Task<SearchDocflowsResponse> SearchDocflowsAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] SearchDocflowsRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/SearchDocflows", boxId);
			return PerformHttpRequestAsync<SearchDocflowsRequest, SearchDocflowsResponse>(authToken, queryString, request);
		}

		[NotNull]
		public Task<GetDocflowsByPacketIdResponse> GetDocflowsByPacketIdAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetDocflowsByPacketIdRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetDocflowsByPacketId", boxId);
			return PerformHttpRequestAsync<GetDocflowsByPacketIdRequest, GetDocflowsByPacketIdResponse>(authToken, queryString, request);
		}
	}
}
