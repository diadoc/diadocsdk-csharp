using System;
using System.Net;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[Obsolete("Use GetNewEventsV8")]
		public BoxEventList GetNewEvents(
			string authToken,
			string boxId,
			string afterEventId = null,
			string afterIndexKey = null,
			string departmentId = null,
			string[] messageTypes = null,
			string[] typeNamedIds = null,
			string[] documentDirections = null,
			long? timestampFromTicks = null,
			long? timestampToTicks = null,
			string counteragentBoxId = null,
			string orderBy = null,
			int? limit = null)
		{
			var qsb = new PathAndQueryBuilder("/V7/GetNewEvents");
			qsb.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(afterEventId))
			{
				qsb.AddParameter("afterEventId", afterEventId);
			}
			if (afterIndexKey != null)
			{
				qsb.AddParameter("afterIndexKey", afterIndexKey);
			}
			if (!string.IsNullOrEmpty(departmentId))
			{
				qsb.AddParameter("departmentId", departmentId);
			}
			qsb.AddCommaSeparatedParameter("messageType", messageTypes);
			qsb.AddCommaSeparatedParameter("typeNamedId", typeNamedIds);
			qsb.AddCommaSeparatedParameter("documentDirection", documentDirections);
			if (timestampFromTicks != null)
			{
				qsb.AddParameter("timestampFromTicks", timestampFromTicks.ToString());
			}
			if (timestampToTicks != null)
			{
				qsb.AddParameter("timestampToTicks", timestampToTicks.ToString());
			}
			if (!string.IsNullOrEmpty(counteragentBoxId))
			{
				qsb.AddParameter("counteragentBoxId", counteragentBoxId);
			}
			if (!string.IsNullOrEmpty(orderBy))
			{
				qsb.AddParameter("orderBy", orderBy);
			}
			if (limit != null)
			{
				qsb.AddParameter("limit", limit.ToString());
			}
			return PerformHttpRequest<BoxEventList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public BoxEventList GetNewEventsV8(
			string authToken,
			string boxId,
			string afterEventId = null,
			string afterIndexKey = null,
			string departmentId = null,
			string[] messageTypes = null,
			string[] typeNamedIds = null,
			string[] documentDirections = null,
			long? timestampFromTicks = null,
			long? timestampToTicks = null,
			string counteragentBoxId = null,
			string orderBy = null,
			int? limit = null)
		{
			var qsb = new PathAndQueryBuilder("/V8/GetNewEvents");
			qsb.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(afterEventId))
			{
				qsb.AddParameter("afterEventId", afterEventId);
			}
			if (afterIndexKey != null)
			{
				qsb.AddParameter("afterIndexKey", afterIndexKey);
			}
			if (!string.IsNullOrEmpty(departmentId))
			{
				qsb.AddParameter("departmentId", departmentId);
			}
			qsb.AddCommaSeparatedParameter("messageType", messageTypes);
			qsb.AddCommaSeparatedParameter("typeNamedId", typeNamedIds);
			qsb.AddCommaSeparatedParameter("documentDirection", documentDirections);
			if (timestampFromTicks != null)
			{
				qsb.AddParameter("timestampFromTicks", timestampFromTicks.ToString());
			}
			if (timestampToTicks != null)
			{
				qsb.AddParameter("timestampToTicks", timestampToTicks.ToString());
			}
			if (!string.IsNullOrEmpty(counteragentBoxId))
			{
				qsb.AddParameter("counteragentBoxId", counteragentBoxId);
			}
			if (!string.IsNullOrEmpty(orderBy))
			{
				qsb.AddParameter("orderBy", orderBy);
			}
			if (limit != null)
			{
				qsb.AddParameter("limit", limit.ToString());
			}
			return PerformHttpRequest<BoxEventList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[Obsolete("Use GetEventV3")]
		public BoxEvent GetEvent(string authToken, string boxId, string eventId)
		{
			var queryString = string.Format("/V2/GetEvent?eventId={0}&boxId={1}", eventId, boxId);
			return PerformHttpRequest<BoxEvent>(authToken, "GET", queryString);
		}

		public BoxEvent GetEventV3(string authToken, string boxId, string eventId)
		{
			var queryString = string.Format("/V3/GetEvent?eventId={0}&boxId={1}", eventId, boxId);
			return PerformHttpRequest<BoxEvent>(authToken, "GET", queryString);
		}

		[Obsolete("Use GetMessageV6")]
		public Message GetMessage(string authToken, string boxId, string messageId, bool withOriginalSignature = false, bool injectEntityContent = false)
		{
			var qsb = new PathAndQueryBuilder("/V5/GetMessage");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			if (withOriginalSignature)
				qsb.AddParameter("originalSignature");
			qsb.AddParameter("injectEntityContent", injectEntityContent.ToString());
			return PerformHttpRequest<Message>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public Message GetMessageV6(string authToken, string boxId, string messageId, bool withOriginalSignature = false, bool injectEntityContent = false)
		{
			var qsb = new PathAndQueryBuilder("/V6/GetMessage");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			if (withOriginalSignature)
				qsb.AddParameter("originalSignature");
			qsb.AddParameter("injectEntityContent", injectEntityContent.ToString());
			return PerformHttpRequest<Message>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[Obsolete("Use GetMessageV6()")]
		public Message GetMessage(
			string authToken,
			string boxId,
			string messageId,
			string entityId,
			bool withOriginalSignature = false,
			bool injectEntityContent = false)
		{
			var qsb = new PathAndQueryBuilder("/V5/GetMessage");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			qsb.AddParameter("entityId", entityId);
			if (withOriginalSignature)
				qsb.AddParameter("originalSignature");
			qsb.AddParameter("injectEntityContent", injectEntityContent.ToString());
			return PerformHttpRequest<Message>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public Message GetMessageV6(
			string authToken,
			string boxId,
			string messageId,
			string entityId,
			bool withOriginalSignature = false,
			bool injectEntityContent = false)
		{
			var qsb = new PathAndQueryBuilder("/V6/GetMessage");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			qsb.AddParameter("entityId", entityId);
			if (withOriginalSignature)
				qsb.AddParameter("originalSignature");
			qsb.AddParameter("injectEntityContent", injectEntityContent.ToString());
			return PerformHttpRequest<Message>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public Template GetTemplate(string authToken, string boxId, string templateId, string entityId = null)
		{
			var qsb = new PathAndQueryBuilder("/GetTemplate");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("templateId", templateId);
			qsb.AddParameter("entityId", entityId);

			return PerformHttpRequest<Template>(authToken, "GET", qsb.BuildPathAndQuery());
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

		public Template PostTemplate(string authToken, TemplateToPost template, string operationId = null)
		{
			var queryString = string.Format("/PostTemplate?operationId={0}", operationId);
			return PerformHttpRequest<TemplateToPost, Template>(authToken, queryString, template);
		}

		public Message TransformTemplateToMessage(string authToken, TemplateTransformationToPost templateTransformation, string operationId = null)
		{
			var qsb = new PathAndQueryBuilder("/TransformTemplateToMessage");
			qsb.AddParameter("operationId", operationId);

			return PerformHttpRequest<TemplateTransformationToPost, Message>(authToken, qsb.BuildPathAndQuery(), templateTransformation);
		}

		public MessagePatch PostMessagePatch(string authToken, MessagePatchToPost patch, string operationId = null)
		{
			var qsb = new PathAndQueryBuilder("/V3/PostMessagePatch");
			qsb.AddParameter("operationId", operationId);
			return PerformHttpRequest<MessagePatchToPost, MessagePatch>(authToken, qsb.BuildPathAndQuery(), patch);
		}

		public MessagePatch PostMessagePatchV4(string authToken, MessagePatchToPostV2 patch, string operationId = null)
		{
			var qsb = new PathAndQueryBuilder("/V4/PostMessagePatch");
			qsb.AddParameter("operationId", operationId);
			return PerformHttpRequest<MessagePatchToPostV2, MessagePatch>(authToken, qsb.BuildPathAndQuery(), patch);
		}

		public MessagePatch PostTemplatePatch(
			string authToken,
			string boxId,
			string templateId,
			TemplatePatchToPost patch,
			string operationId = null)
		{
			var qsb = new PathAndQueryBuilder("/PostTemplatePatch");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("templateId", templateId);
			qsb.AddParameter("operationId", operationId);
			return PerformHttpRequest<TemplatePatchToPost, MessagePatch>(authToken, qsb.BuildPathAndQuery(), patch);
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

		[Obsolete("Use GetLastEventV2()")]
		public BoxEvent GetLastEvent(string authToken, string boxId)
		{
			var queryString = BuildQueryStringWithBoxId("GetLastEvent", boxId);
			return PerformHttpRequest<BoxEvent>(authToken, "GET", queryString, allowedStatusCodes: HttpStatusCode.NoContent);
		}

		public BoxEvent GetLastEventV2(string authToken, string boxId)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetLastEvent", boxId);
			return PerformHttpRequest<BoxEvent>(authToken, "GET", queryString, allowedStatusCodes: HttpStatusCode.NoContent);
		}
	}
}
