using System;
using Diadoc.Api.Proto.Docflow;

namespace Diadoc.Api
{
	public partial class DocflowApi : IDocflowApi
	{
		private readonly DiadocHttpApi.DocflowHttpApi docflowHttpApi;

		public DocflowApi(DiadocHttpApi.DocflowHttpApi docflowHttpApi)
		{
			if (docflowHttpApi == null) throw new ArgumentNullException("diadocHttpApi");
			this.docflowHttpApi = docflowHttpApi;
		}

		public GetDocflowBatchResponseV3 GetDocflows(string authToken, string boxId, GetDocflowBatchRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return docflowHttpApi.GetDocflows(authToken, boxId, request);
		}

		public GetDocflowEventsResponseV3 GetDocflowEvents(string authToken, string boxId, GetDocflowEventsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return docflowHttpApi.GetDocflowEvents(authToken, boxId, request);
		}

		public SearchDocflowsResponseV3 SearchDocflows(string authToken, string boxId, SearchDocflowsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return docflowHttpApi.SearchDocflows(authToken, boxId, request);
		}

		public GetDocflowsByPacketIdResponseV3 GetDocflowsByPacketId(string authToken, string boxId,
			GetDocflowsByPacketIdRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return docflowHttpApi.GetDocflowsByPacketId(authToken, boxId, request);
		}
	}
}