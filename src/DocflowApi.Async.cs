using System;
using System.Threading.Tasks;
using Diadoc.Api.Proto.Docflow;
using Diadoc.Api.Proto.PartnerEvents;

namespace Diadoc.Api
{
	public partial class DocflowApi : IDocflowApi
	{
		[Obsolete("Use GetDocflowsV4Async() instead")]
		public Task<GetDocflowBatchResponseV3> GetDocflowsAsync(string authToken, string boxId, GetDocflowBatchRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return docflowHttpApi.GetDocflowsAsync(authToken, boxId, request);
		}

		[Obsolete("Use GetDocflowEventsV4Async() instead")]
		public Task<GetDocflowEventsResponseV3> GetDocflowEventsAsync(string authToken, string boxId, GetDocflowEventsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return docflowHttpApi.GetDocflowEventsAsync(authToken, boxId, request);
		}

		[Obsolete("Use SearchDocflowsV4Async() instead")]
		public Task<SearchDocflowsResponseV3> SearchDocflowsAsync(string authToken, string boxId, SearchDocflowsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return docflowHttpApi.SearchDocflowsAsync(authToken, boxId, request);
		}

		[Obsolete("Use GetDocflowsByPacketIdV4Async() instead")]
		public Task<GetDocflowsByPacketIdResponseV3> GetDocflowsByPacketIdAsync(string authToken, string boxId, GetDocflowsByPacketIdRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return docflowHttpApi.GetDocflowsByPacketIdAsync(authToken, boxId, request);
		}
		
		public Task<GetDocflowBatchResponseV4> GetDocflowsV4Async(string authToken, string boxId, GetDocflowBatchRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return docflowHttpApi.GetDocflowsV4Async(authToken, boxId, request);
		}

		public Task<GetDocflowEventsResponseV4> GetDocflowEventsV4Async(string authToken, string boxId, GetDocflowEventsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return docflowHttpApi.GetDocflowEventsV4Async(authToken, boxId, request);
		}

		public Task<SearchDocflowsResponseV4> SearchDocflowsV4Async(string authToken, string boxId, SearchDocflowsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return docflowHttpApi.SearchDocflowsV4Async(authToken, boxId, request);
		}

		public Task<GetDocflowsByPacketIdResponseV4> GetDocflowsByPacketIdV4Async(string authToken, string boxId, GetDocflowsByPacketIdRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return docflowHttpApi.GetDocflowsByPacketIdV4Async(authToken, boxId, request);
		}

		public Task<GetPartnerEventsResponse> GetPartnerEventsV4Async(string authToken, GetPartnerEventsRequest request)
		{
			return docflowHttpApi.GetPartnerEventsV4Async(authToken, request);
		}
	}
}
