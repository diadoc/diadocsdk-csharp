using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<BoxEventList> GetNewEventsAsync(string authToken, string boxId, string afterEventId = null, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/V5/GetNewEvents");
			qsb.AddParameter("includeDrafts");
			qsb.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(afterEventId)) qsb.AddParameter("afterEventId", afterEventId);
			return PerformHttpRequestAsync<BoxEventList>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<BoxEvent> GetEventAsync(string authToken, string boxId, string eventId, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/V2/GetEvent");
			qsb.AddParameter("eventId", eventId);
			qsb.AddParameter("boxId", boxId);
			return PerformHttpRequestAsync<BoxEvent>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<Message> GetMessageAsync(string authToken, string boxId, string messageId, bool withOriginalSignature = false, bool injectEntityContent = false, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/V4/GetMessage");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			if (withOriginalSignature)
				qsb.AddParameter("originalSignature");
			qsb.AddParameter("injectEntityContent", injectEntityContent.ToString());
			return PerformHttpRequestAsync<Message>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<Message> GetMessageAsync(string authToken, string boxId, string messageId, string entityId, bool withOriginalSignature = false, bool injectEntityContent = false, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/V4/GetMessage");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			qsb.AddParameter("entityId", entityId);
			if (withOriginalSignature)
				qsb.AddParameter("originalSignature");
			qsb.AddParameter("injectEntityContent", injectEntityContent.ToString());
			return PerformHttpRequestAsync<Message>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<Template> GetTemplateAsync(string authToken, string boxId, string templateId, string entityId = null, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/GetTemplate");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("templateId", templateId);

			if (!string.IsNullOrEmpty(entityId))
			{
				qsb.AddParameter("entityId", entityId);
			}

			return PerformHttpRequestAsync<Template>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<byte[]> GetEntityContentAsync(string authToken, string boxId, string messageId, string entityId, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/V4/GetEntityContent");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			qsb.AddParameter("entityId", entityId);
			return PerformHttpRequestAsync(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<Message> PostMessageAsync(string authToken, MessageToPost msg, string operationId = null, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/V3/PostMessage");
			qsb.AddParameter("operationId", operationId);
			return PerformHttpRequestAsync<MessageToPost, Message>(authToken, qsb.BuildPathAndQuery(), msg, ct: ct);
		}

		public Task<Template> PostTemplateAsync(string authToken, TemplateToPost template, string operationId = null, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/PostTemplate");
			qsb.AddParameter("operationId", operationId);
			return PerformHttpRequestAsync<TemplateToPost, Template>(authToken, qsb.BuildPathAndQuery(), template, ct: ct);
		}

		public Task<Message> TransformTemplateToMessageAsync(string authToken, TemplateTransformationToPost templateTransformation, string operationId = null, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/TransformTemplateToMessage");
			qsb.AddParameter("operationId", operationId);

			return PerformHttpRequestAsync<TemplateTransformationToPost, Message>(authToken, qsb.BuildPathAndQuery(), templateTransformation, ct: ct);
		}

		public Task<MessagePatch> PostMessagePatchAsync(string authToken, MessagePatchToPost patch, string operationId = null, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/V3/PostMessagePatch");
			qsb.AddParameter("operationId", operationId);
			return PerformHttpRequestAsync<MessagePatchToPost, MessagePatch>(authToken, qsb.BuildPathAndQuery(), patch, ct: ct);
		}

		public Task<MessagePatch> PostTemplatePatchAsync(
			string authToken,
			string boxId,
			string templateId,
			TemplatePatchToPost patch,
			string operationId = null, 
			CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/PostTemplatePatch");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("templateId", templateId);
			qsb.AddParameter("operationId", operationId);
			return PerformHttpRequestAsync<TemplatePatchToPost, MessagePatch>(authToken, qsb.BuildPathAndQuery(), patch, ct: ct);
		}

		public Task PostRoamingNotificationAsync(string authToken, RoamingNotificationToPost notification, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync(authToken, "POST", "/PostRoamingNotification", Serialize(notification), ct: ct);
		}

		public Task<PrepareDocumentsToSignResponse> PrepareDocumentsToSignAsync(string authToken, PrepareDocumentsToSignRequest request, bool excludeContent = false, CancellationToken ct = default)
		{
			var queryString = "/PrepareDocumentsToSign" + (excludeContent ? "?excludeContent" : "");
			return PerformHttpRequestAsync<PrepareDocumentsToSignRequest, PrepareDocumentsToSignResponse>(authToken, queryString, request, ct: ct);
		}
		
		[NotNull]
		public Task<BoxEvent> GetLastEventAsync([NotNull] string authToken, [NotNull] string boxId, CancellationToken ct = default)
		{
			var queryString = BuildQueryStringWithBoxId("GetLastEvent", boxId);
			return PerformHttpRequestAsync<BoxEvent>(authToken,"GET", queryString, allowStatusCodes: HttpStatusCode.NoContent, ct: ct);
		}
	}
}
