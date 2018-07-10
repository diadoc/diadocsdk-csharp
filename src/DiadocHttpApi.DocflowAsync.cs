using System.Threading.Tasks;
using Diadoc.Api.Proto.Docflow;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[ItemNotNull]
		public Task<GetDocflowBatchResponse> GetDocflowsAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetDocflowBatchRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetDocflows", boxId);
			return PerformHttpRequestAsync<GetDocflowBatchRequest, GetDocflowBatchResponse>(authToken, queryString, request);
		}

		[ItemNotNull]
		public Task<GetDocflowEventsResponse> GetDocflowEventsAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetDocflowEventsRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetDocflowEvents", boxId);
			return PerformHttpRequestAsync<GetDocflowEventsRequest, GetDocflowEventsResponse>(authToken, queryString, request);
		}

		[ItemNotNull]
		public Task<SearchDocflowsResponse> SearchDocflowsAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] SearchDocflowsRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/SearchDocflows", boxId);
			return PerformHttpRequestAsync<SearchDocflowsRequest, SearchDocflowsResponse>(authToken, queryString, request);
		}

		[ItemNotNull]
		public Task<GetDocflowsByPacketIdResponse> GetDocflowsByPacketIdAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetDocflowsByPacketIdRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetDocflowsByPacketId", boxId);
			return PerformHttpRequestAsync<GetDocflowsByPacketIdRequest, GetDocflowsByPacketIdResponse>(authToken, queryString, request);
		}

		public partial class DocflowHttpApi
		{
			[ItemNotNull]
			public Task<GetDocflowBatchResponseV3> GetDocflowsAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetDocflowBatchRequest request)
			{
				var queryString = BuildQueryStringWithBoxId("/V3/GetDocflows", boxId);
				return diadocHttpApi.PerformHttpRequestAsync<GetDocflowBatchRequest, GetDocflowBatchResponseV3>(authToken, queryString, request);
			}

			[ItemNotNull]
			public Task<GetDocflowEventsResponseV3> GetDocflowEventsAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetDocflowEventsRequest request)
			{
				var queryString = BuildQueryStringWithBoxId("/V3/GetDocflowEvents", boxId);
				return diadocHttpApi.PerformHttpRequestAsync<GetDocflowEventsRequest, GetDocflowEventsResponseV3>(authToken, queryString, request);
			}

			[ItemNotNull]
			public Task<SearchDocflowsResponseV3> SearchDocflowsAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] SearchDocflowsRequest request)
			{
				var queryString = BuildQueryStringWithBoxId("/V3/SearchDocflows", boxId);
				return diadocHttpApi.PerformHttpRequestAsync<SearchDocflowsRequest, SearchDocflowsResponseV3>(authToken, queryString, request);
			}

			[ItemNotNull]
			public Task<GetDocflowsByPacketIdResponseV3> GetDocflowsByPacketIdAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetDocflowsByPacketIdRequest request)
			{
				var queryString = BuildQueryStringWithBoxId("/V3/GetDocflowsByPacketId", boxId);
				return diadocHttpApi.PerformHttpRequestAsync<GetDocflowsByPacketIdRequest, GetDocflowsByPacketIdResponseV3>(authToken, queryString, request);
			}
		}
	}
}
