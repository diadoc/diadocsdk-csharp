using System;
using System.Threading.Tasks;
using Diadoc.Api.Proto.Docflow;

namespace Diadoc.Api
{
	public partial class DocflowApi : IDocflowApi
	{
		public Task<GetDocflowBatchResponseV3> GetDocflowsAsync(string authToken, string boxId, GetDocflowBatchRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return docflowHttpApi.GetDocflowsAsync(authToken, boxId, request);
		}

		public Task<GetDocflowEventsResponseV3> GetDocflowEventsAsync(string authToken, string boxId, GetDocflowEventsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return docflowHttpApi.GetDocflowEventsAsync(authToken, boxId, request);
		}

		public Task<SearchDocflowsResponseV3> SearchDocflowsAsync(string authToken, string boxId, SearchDocflowsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return docflowHttpApi.SearchDocflowsAsync(authToken, boxId, request);
		}

		public Task<GetDocflowsByPacketIdResponseV3> GetDocflowsByPacketIdAsync(string authToken, string boxId,
			GetDocflowsByPacketIdRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return docflowHttpApi.GetDocflowsByPacketIdAsync(authToken, boxId, request);
		}
	}
}