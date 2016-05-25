using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public BoxEventList GetNewEvents(string authToken, string boxId, string afterEventId = null)
		{
			var qsb = new PathAndQueryBuilder("/V4/GetNewEvents");
			qsb.AddParameter("includeDrafts");
			qsb.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(afterEventId)) qsb.AddParameter("afterEventId", afterEventId);
			return PerformHttpRequest<BoxEventList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public BoxEvent GetEvent(string authToken, string boxId, string eventId)
		{
			var queryString = string.Format("/V2/GetEvent?eventId={0}&boxId={1}", eventId, boxId);
			return PerformHttpRequest<BoxEvent>(authToken, "GET", queryString);
		}

		public Message GetMessage(string authToken, string boxId, string messageId, bool withOriginalSignature = false)
		{
			var qsb = new PathAndQueryBuilder("/V3/GetMessage");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			if (withOriginalSignature)
				qsb.AddParameter("originalSignature");
			return PerformHttpRequest<Message>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public Message GetMessage(string authToken, string boxId, string messageId, string entityId, bool withOriginalSignature = false)
		{
			var qsb = new PathAndQueryBuilder("/V3/GetMessage");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			qsb.AddParameter("entityId", entityId);
			if (withOriginalSignature)
				qsb.AddParameter("originalSignature");
			return PerformHttpRequest<Message>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public byte[] GetEntityContent(string authToken, string boxId, string messageId, string entityId)
		{
			var queryString = string.Format("/V4/GetEntityContent?boxId={0}&messageId={1}&entityId={2}", boxId, messageId, entityId);
			return PerformHttpRequest(authToken, "GET", queryString);
		}

		public Message PostMessage(string authToken, MessageToPost msg, string operationId = null)
		{
			var queryString = string.Format("/V3/PostMessage?operationId={0}", operationId);
			return PerformHttpRequest<MessageToPost, Message>(authToken, queryString, msg);
		}

		public MessagePatch PostMessagePatch(string authToken, MessagePatchToPost patch, string operationId = null)
		{
			var queryString = string.Format("/V3/PostMessagePatch?operationId={0}", operationId);
			return PerformHttpRequest<MessagePatchToPost, MessagePatch>(authToken, queryString, patch);
		}

		public void PostRoamingNotification(string authToken, RoamingNotificationToPost notification)
		{
			PerformHttpRequest(authToken, "POST", "/PostRoamingNotification", Serialize(notification));
		}

		public PrepareDocumentsToSignResponse PrepareDocumentsToSign(string authToken, PrepareDocumentsToSignRequest request, bool excludeContent = false)
		{
			var queryString = "/PrepareDocumentsToSign" + (excludeContent ? "?excludeContent" : "");
			return PerformHttpRequest<PrepareDocumentsToSignRequest, PrepareDocumentsToSignResponse>(authToken, queryString, request);
		}
	}
}
