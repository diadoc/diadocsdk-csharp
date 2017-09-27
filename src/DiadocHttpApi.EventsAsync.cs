using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<BoxEventList> GetNewEventsAsync(string authToken, string boxId, string afterEventId = null)
		{
			var qsb = new PathAndQueryBuilder("/V5/GetNewEvents");
			qsb.AddParameter("includeDrafts");
			qsb.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(afterEventId)) qsb.AddParameter("afterEventId", afterEventId);
			return PerformHttpRequestAsync<BoxEventList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public Task<BoxEvent> GetEventAsync(string authToken, string boxId, string eventId)
		{
			var qsb = new PathAndQueryBuilder("/V2/GetEvent");
			qsb.AddParameter("eventId", eventId);
			qsb.AddParameter("boxId", boxId);
			return PerformHttpRequestAsync<BoxEvent>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public Task<Message> GetMessageAsync(string authToken, string boxId, string messageId, bool withOriginalSignature = false, bool injectEntityContent = false)
		{
			var qsb = new PathAndQueryBuilder("/V4/GetMessage");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			if (withOriginalSignature)
				qsb.AddParameter("originalSignature");
			qsb.AddParameter("injectEntityContent", injectEntityContent.ToString());
			return PerformHttpRequestAsync<Message>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public Task<Message> GetMessageAsync(string authToken, string boxId, string messageId, string entityId, bool withOriginalSignature = false, bool injectEntityContent = false)
		{
			var qsb = new PathAndQueryBuilder("/V4/GetMessage");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			qsb.AddParameter("entityId", entityId);
			if (withOriginalSignature)
				qsb.AddParameter("originalSignature");
			qsb.AddParameter("injectEntityContent", injectEntityContent.ToString());
			return PerformHttpRequestAsync<Message>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public Task<byte[]> GetEntityContentAsync(string authToken, string boxId, string messageId, string entityId)
		{
			var qsb = new PathAndQueryBuilder("/V4/GetEntityContent");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			qsb.AddParameter("entityId", entityId);
			return PerformHttpRequestAsync(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public Task<Message> PostMessageAsync(string authToken, MessageToPost msg, string operationId = null)
		{
			var qsb = new PathAndQueryBuilder("/V3/PostMessage");
			qsb.AddParameter("operationId", operationId);
			return PerformHttpRequestAsync<MessageToPost, Message>(authToken, qsb.BuildPathAndQuery(), msg);
		}

		public Task<MessagePatch> PostMessagePatchAsync(string authToken, MessagePatchToPost patch, string operationId = null)
		{
			var qsb = new PathAndQueryBuilder("/V3/PostMessagePatch");
			qsb.AddParameter("operationId", operationId);
			return PerformHttpRequestAsync<MessagePatchToPost, MessagePatch>(authToken, qsb.BuildPathAndQuery(), patch);
		}

		public Task PostRoamingNotificationAsync(string authToken, RoamingNotificationToPost notification)
		{
			return PerformHttpRequestAsync(authToken, "POST", "/PostRoamingNotification", Serialize(notification));
		}

		public Task<PrepareDocumentsToSignResponse> PrepareDocumentsToSignAsync(string authToken, PrepareDocumentsToSignRequest request, bool excludeContent = false)
		{
			var queryString = "/PrepareDocumentsToSign" + (excludeContent ? "?excludeContent" : "");
			return PerformHttpRequestAsync<PrepareDocumentsToSignRequest, PrepareDocumentsToSignResponse>(authToken, queryString, request);
		}
	}
}
