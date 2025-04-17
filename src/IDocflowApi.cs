using System;
using Diadoc.Api.Proto.Docflow;

#if !NET35
using System.Threading.Tasks;
#endif

namespace Diadoc.Api
{
	public interface IDocflowApi
	{
		[Obsolete("Use GetDocflowsV4()")]
		GetDocflowBatchResponseV3 GetDocflows(string authToken, string boxId, GetDocflowBatchRequest request);
		[Obsolete("Use GetDocflowEventsV4()")]
		GetDocflowEventsResponseV3 GetDocflowEvents(string authToken, string boxId, GetDocflowEventsRequest request);
		[Obsolete("Use SearchDocflowsV4()")]
		SearchDocflowsResponseV3 SearchDocflows(string authToken, string boxId, SearchDocflowsRequest request);
		[Obsolete("Use GetDocflowsByPacketIdV4()")]
		GetDocflowsByPacketIdResponseV3 GetDocflowsByPacketId(string authToken, string boxId, GetDocflowsByPacketIdRequest request);
		GetDocflowBatchResponseV4 GetDocflowsV4(string authToken, string boxId, GetDocflowBatchRequest request);
		GetDocflowEventsResponseV4 GetDocflowEventsV4(string authToken, string boxId, GetDocflowEventsRequest request);
		SearchDocflowsResponseV4 SearchDocflowsV4(string authToken, string boxId, SearchDocflowsRequest request);
		GetDocflowsByPacketIdResponseV4 GetDocflowsByPacketIdV4(string authToken, string boxId, GetDocflowsByPacketIdRequest request);

#if !NET35
		[Obsolete("Use GetDocflowsV4Async()")]
		Task<GetDocflowBatchResponseV3> GetDocflowsAsync(string authToken, string boxId, GetDocflowBatchRequest request);
		[Obsolete("Use GetDocflowEventsV4Async()")]
		Task<GetDocflowEventsResponseV3> GetDocflowEventsAsync(string authToken, string boxId, GetDocflowEventsRequest request);
		[Obsolete("Use SearchDocflowsV4Async()")]
		Task<SearchDocflowsResponseV3> SearchDocflowsAsync(string authToken, string boxId, SearchDocflowsRequest request);
		[Obsolete("Use GetDocflowsByPacketIdV4Async()")]
		Task<GetDocflowsByPacketIdResponseV3> GetDocflowsByPacketIdAsync(string authToken, string boxId, GetDocflowsByPacketIdRequest request);
		Task<GetDocflowBatchResponseV4> GetDocflowsV4Async(string authToken, string boxId, GetDocflowBatchRequest request);
		Task<GetDocflowEventsResponseV4> GetDocflowEventsV4Async(string authToken, string boxId, GetDocflowEventsRequest request);
		Task<SearchDocflowsResponseV4> SearchDocflowsV4Async(string authToken, string boxId, SearchDocflowsRequest request);
		Task<GetDocflowsByPacketIdResponseV4> GetDocflowsByPacketIdV4Async(string authToken, string boxId, GetDocflowsByPacketIdRequest request);
#endif
	}
}
