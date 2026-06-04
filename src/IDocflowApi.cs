using System;
using Diadoc.Api.Proto.Docflow;
using Diadoc.Api.Proto.PartnerEvents;

#if !NET35
using System.Threading.Tasks;
#endif

namespace Diadoc.Api
{
	public interface IDocflowApi
	{
		[Obsolete("Use GetDocflowsV5()")]
		GetDocflowBatchResponseV3 GetDocflows(string authToken, string boxId, GetDocflowBatchRequest request);
		[Obsolete("Use GetDocflowEventsV5()")]
		GetDocflowEventsResponseV3 GetDocflowEvents(string authToken, string boxId, GetDocflowEventsRequest request);
		[Obsolete("Use SearchDocflowsV5()")]
		SearchDocflowsResponseV3 SearchDocflows(string authToken, string boxId, SearchDocflowsRequest request);
		[Obsolete("Use GetDocflowsByPacketIdV5()")]
		GetDocflowsByPacketIdResponseV3 GetDocflowsByPacketId(string authToken, string boxId, GetDocflowsByPacketIdRequest request);

		[Obsolete("Use GetDocflowsV5()")]
		GetDocflowBatchResponseV4 GetDocflowsV4(string authToken, string boxId, GetDocflowBatchRequest request);
		[Obsolete("Use GetDocflowEventsV5()")]
		GetDocflowEventsResponseV4 GetDocflowEventsV4(string authToken, string boxId, GetDocflowEventsRequest request);
		[Obsolete("Use SearchDocflowsV5()")]
		SearchDocflowsResponseV4 SearchDocflowsV4(string authToken, string boxId, SearchDocflowsRequest request);
		[Obsolete("Use GetDocflowsByPacketIdV5()")]
		GetDocflowsByPacketIdResponseV4 GetDocflowsByPacketIdV4(string authToken, string boxId, GetDocflowsByPacketIdRequest request);

		GetPartnerEventsResponse GetPartnerEventsV4(string authToken, GetPartnerEventsRequest request);

		GetDocflowBatchResponseV5 GetDocflowsV5(string authToken, string boxId, GetDocflowBatchRequest request);
		GetDocflowEventsResponseV5 GetDocflowEventsV5(string authToken, string boxId, GetDocflowEventsRequest request);
		SearchDocflowsResponseV5 SearchDocflowsV5(string authToken, string boxId, SearchDocflowsRequest request);
		GetDocflowsByPacketIdResponseV5 GetDocflowsByPacketIdV5(string authToken, string boxId, GetDocflowsByPacketIdRequest request);

#if !NET35
		[Obsolete("Use GetDocflowsV5Async()")]
		Task<GetDocflowBatchResponseV3> GetDocflowsAsync(string authToken, string boxId, GetDocflowBatchRequest request);
		[Obsolete("Use GetDocflowEventsV5Async()")]
		Task<GetDocflowEventsResponseV3> GetDocflowEventsAsync(string authToken, string boxId, GetDocflowEventsRequest request);
		[Obsolete("Use SearchDocflowsV5Async()")]
		Task<SearchDocflowsResponseV3> SearchDocflowsAsync(string authToken, string boxId, SearchDocflowsRequest request);
		[Obsolete("Use GetDocflowsByPacketIdV5Async()")]
		Task<GetDocflowsByPacketIdResponseV3> GetDocflowsByPacketIdAsync(string authToken, string boxId, GetDocflowsByPacketIdRequest request);

		[Obsolete("Use GetDocflowsV5Async()")]
		Task<GetDocflowBatchResponseV4> GetDocflowsV4Async(string authToken, string boxId, GetDocflowBatchRequest request);
		[Obsolete("Use GetDocflowEventsV5Async()")]
		Task<GetDocflowEventsResponseV4> GetDocflowEventsV4Async(string authToken, string boxId, GetDocflowEventsRequest request);
		[Obsolete("Use SearchDocflowsV5Async()")]
		Task<SearchDocflowsResponseV4> SearchDocflowsV4Async(string authToken, string boxId, SearchDocflowsRequest request);
		[Obsolete("Use GetDocflowsByPacketIdV5Async()")]
		Task<GetDocflowsByPacketIdResponseV4> GetDocflowsByPacketIdV4Async(string authToken, string boxId, GetDocflowsByPacketIdRequest request);

		Task<GetPartnerEventsResponse> GetPartnerEventsV4Async(string authToken, GetPartnerEventsRequest request);

		Task<GetDocflowBatchResponseV5> GetDocflowsV5Async(string authToken, string boxId, GetDocflowBatchRequest request);
		Task<GetDocflowEventsResponseV5> GetDocflowEventsV5Async(string authToken, string boxId, GetDocflowEventsRequest request);
		Task<SearchDocflowsResponseV5> SearchDocflowsV5Async(string authToken, string boxId, SearchDocflowsRequest request);
		Task<GetDocflowsByPacketIdResponseV5> GetDocflowsByPacketIdV5Async(string authToken, string boxId, GetDocflowsByPacketIdRequest request);
#endif
	}
}
