using Diadoc.Api.Proto.Docflow;
using JetBrains.Annotations;

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

		public partial class DocflowHttpApi
		{
			private readonly DiadocHttpApi diadocHttpApi;

			public DocflowHttpApi(DiadocHttpApi diadocHttpApi)
			{
				this.diadocHttpApi = diadocHttpApi;
			}

			[NotNull]
			public GetDocflowBatchResponseV3 GetDocflows([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetDocflowBatchRequest request)
			{
				var queryString = BuildQueryStringWithBoxId("/V3/GetDocflows", boxId);
				return diadocHttpApi.PerformHttpRequest<GetDocflowBatchRequest, GetDocflowBatchResponseV3>(authToken, queryString, request);
			}

			[NotNull]
			public GetDocflowEventsResponseV3 GetDocflowEvents([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetDocflowEventsRequest request)
			{
				var queryString = BuildQueryStringWithBoxId("/V3/GetDocflowEvents", boxId);
				return diadocHttpApi.PerformHttpRequest<GetDocflowEventsRequest, GetDocflowEventsResponseV3>(authToken, queryString, request);
			}

			[NotNull]
			public SearchDocflowsResponseV3 SearchDocflows([NotNull] string authToken, [NotNull] string boxId, [NotNull] SearchDocflowsRequest request)
			{
				var queryString = BuildQueryStringWithBoxId("/V3/SearchDocflows", boxId);
				return diadocHttpApi.PerformHttpRequest<SearchDocflowsRequest, SearchDocflowsResponseV3>(authToken, queryString, request);
			}

			[NotNull]
			public GetDocflowsByPacketIdResponseV3 GetDocflowsByPacketId([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetDocflowsByPacketIdRequest request)
			{
				var queryString = BuildQueryStringWithBoxId("/V3/GetDocflowsByPacketId", boxId);
				return diadocHttpApi.PerformHttpRequest<GetDocflowsByPacketIdRequest, GetDocflowsByPacketIdResponseV3>(authToken, queryString, request);
			}
		}
	}
}
