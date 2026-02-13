using System;
using System.Runtime.InteropServices;
using Diadoc.Api.Proto.Docflow;
using Diadoc.Api.Proto.PartnerEvents;

namespace Diadoc.Api
{
	[ComVisible(true)]
	[ProgId("Diadoc.Api.ComDocflowApi")]
	[Guid("793A18BE-51F2-43AF-A5A3-C78EE03E68D6")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IComDocflowApi))]
	public class ComDocflowApi : SafeComObject, IComDocflowApi
	{
		private readonly IDocflowApi docflowApi;

		public ComDocflowApi(IDocflowApi docflowApi)
		{
			this.docflowApi = docflowApi;
		}

		[Obsolete("Use GetDocflowsV4()")]
		public GetDocflowBatchResponseV3 GetDocflows(string authToken, string boxId, object request)
		{
			return docflowApi.GetDocflows(authToken, boxId, (GetDocflowBatchRequest) request);
		}

		public GetDocflowBatchResponseV4 GetDocflowsV4(string authToken, string boxId, object request)
		{
			return docflowApi.GetDocflowsV4(authToken, boxId, (GetDocflowBatchRequest) request);
		}

		[Obsolete("Use GetDocflowEventsV4()")]
		public GetDocflowEventsResponseV3 GetDocflowEvents(string authToken, string boxId, object request)
		{
			return docflowApi.GetDocflowEvents(authToken, boxId, (GetDocflowEventsRequest) request);
		}

		public GetDocflowEventsResponseV4 GetDocflowEventsV4(string authToken, string boxId, object request)
		{
			return docflowApi.GetDocflowEventsV4(authToken, boxId, (GetDocflowEventsRequest) request);
		}

		[Obsolete("Use SearchDocflowsV4()")]
		public SearchDocflowsResponseV3 SearchDocflows(string authToken, string boxId, object request)
		{
			return docflowApi.SearchDocflows(authToken, boxId, (SearchDocflowsRequest) request);
		}

		public SearchDocflowsResponseV4 SearchDocflowsV4(string authToken, string boxId, object request)
		{
			return docflowApi.SearchDocflowsV4(authToken, boxId, (SearchDocflowsRequest) request);
		}

		[Obsolete("Use GetDocflowsByPacketIdV4()")]
		public GetDocflowsByPacketIdResponseV3 GetDocflowsByPacketId(string authToken, string boxId, object request)
		{
			return docflowApi.GetDocflowsByPacketId(authToken, boxId, (GetDocflowsByPacketIdRequest) request);
		}

		public GetDocflowsByPacketIdResponseV4 GetDocflowsByPacketIdV4(string authToken, string boxId, object request)
		{
			return docflowApi.GetDocflowsByPacketIdV4(authToken, boxId, (GetDocflowsByPacketIdRequest) request);
		}

		public GetPartnerEventsResponse GetPartnerEvents(string authToken, object request)
		{
			return docflowApi.GetPartnerEventsV4(authToken, (GetPartnerEventsRequest) request);
		}
	}
}
