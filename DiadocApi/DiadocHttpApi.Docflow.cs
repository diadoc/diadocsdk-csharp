using Diadoc.Api.Annotations;
using Diadoc.Api.Proto.Docflow;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[NotNull]
		public GetDocflowBatchResponse GetDocflows([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetDocflowBatchRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetDocflows", boxId);
			return PerformHttpRequest<GetDocflowBatchRequest, GetDocflowBatchResponse>(authToken, queryString, request);
		}

		[NotNull]
		public GetDocflowEventsResponse GetDocflowEvents([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetDocflowEventsRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetDocflowEvents", boxId);
			return PerformHttpRequest<GetDocflowEventsRequest, GetDocflowEventsResponse>(authToken, queryString, request);
		}

		[NotNull]
		public SearchDocflowsResponse SearchDocflows([NotNull] string authToken, [NotNull] string boxId, [NotNull] SearchDocflowsRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/SearchDocflows", boxId);
			return PerformHttpRequest<SearchDocflowsRequest, SearchDocflowsResponse>(authToken, queryString, request);
		}

		[NotNull]
		public GetDocflowsByPacketIdResponse GetDocflowsByPacketId([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetDocflowsByPacketIdRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetDocflowsByPacketId", boxId);
			return PerformHttpRequest<GetDocflowsByPacketIdRequest, GetDocflowsByPacketIdResponse>(authToken, queryString, request);
		}
	}
}
