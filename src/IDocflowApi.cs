using Diadoc.Api.Proto.Docflow;

#if !NET35
using System.Threading.Tasks;
#endif

namespace Diadoc.Api
{
	public interface IDocflowApi
	{
		GetDocflowBatchResponseV3 GetDocflows(string authToken, string boxId, GetDocflowBatchRequest request);
		GetDocflowEventsResponseV3 GetDocflowEvents(string authToken, string boxId, GetDocflowEventsRequest request);
		SearchDocflowsResponseV3 SearchDocflows(string authToken, string boxId, SearchDocflowsRequest request);
		GetDocflowsByPacketIdResponseV3 GetDocflowsByPacketId(string authToken, string boxId, GetDocflowsByPacketIdRequest request);
		GetDocflowBatchResponseV4 GetDocflowsV4(string authToken, string boxId, GetDocflowBatchRequest request);
		GetDocflowEventsResponseV4 GetDocflowEventsV4(string authToken, string boxId, GetDocflowEventsRequest request);
		SearchDocflowsResponseV4 SearchDocflowsV4(string authToken, string boxId, SearchDocflowsRequest request);
		GetDocflowsByPacketIdResponseV4 GetDocflowsByPacketIdV4(string authToken, string boxId, GetDocflowsByPacketIdRequest request);

#if !NET35
		Task<GetDocflowBatchResponseV3> GetDocflowsAsync(string authToken, string boxId, GetDocflowBatchRequest request);
		Task<GetDocflowEventsResponseV3> GetDocflowEventsAsync(string authToken, string boxId, GetDocflowEventsRequest request);
		Task<SearchDocflowsResponseV3> SearchDocflowsAsync(string authToken, string boxId, SearchDocflowsRequest request);
		Task<GetDocflowsByPacketIdResponseV3> GetDocflowsByPacketIdAsync(string authToken, string boxId, GetDocflowsByPacketIdRequest request);
		Task<GetDocflowBatchResponseV4> GetDocflowsV4Async(string authToken, string boxId, GetDocflowBatchRequest request);
		Task<GetDocflowEventsResponseV4> GetDocflowEventsV4Async(string authToken, string boxId, GetDocflowEventsRequest request);
		Task<SearchDocflowsResponseV4> SearchDocflowsV4Async(string authToken, string boxId, SearchDocflowsRequest request);
		Task<GetDocflowsByPacketIdResponseV4> GetDocflowsByPacketIdV4Async(string authToken, string boxId, GetDocflowsByPacketIdRequest request);
#endif
	}
}
