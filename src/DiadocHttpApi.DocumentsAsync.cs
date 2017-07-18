using System;
using System.Globalization;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Documents;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[NotNull]
		public Task<DocumentList> GetDocumentsAsync([NotNull] string authToken, [NotNull] DocumentsFilter filter)
		{
			var qsb = new PathAndQueryBuilder("/V3/GetDocuments");
			qsb.AddParameter("boxId", filter.BoxId);
			qsb.AddParameter("filterCategory", filter.FilterCategory);
			if (!string.IsNullOrEmpty(filter.CounteragentBoxId)) qsb.AddParameter("counteragentBoxId", filter.CounteragentBoxId);
			if (filter.TimestampFrom.HasValue) qsb.AddParameter("timestampFromTicks", filter.TimestampFrom.Value.ToUniversalTime().Ticks.ToString(CultureInfo.InvariantCulture));
			if (filter.TimestampTo.HasValue) qsb.AddParameter("timestampToTicks", filter.TimestampTo.Value.ToUniversalTime().Ticks.ToString(CultureInfo.InvariantCulture));
			if (!string.IsNullOrEmpty(filter.FromDocumentDate)) qsb.AddParameter("fromDocumentDate", filter.FromDocumentDate);
			if (!string.IsNullOrEmpty(filter.ToDocumentDate)) qsb.AddParameter("toDocumentDate", filter.ToDocumentDate);
			if (!string.IsNullOrEmpty(filter.DocumentNumber)) qsb.AddParameter("documentNumber", filter.DocumentNumber);
			if (!string.IsNullOrEmpty(filter.DepartmentId)) qsb.AddParameter("departmentId", filter.DepartmentId);
			if (!string.IsNullOrEmpty(filter.ToDepartmentId)) qsb.AddParameter("toDepartmentId", filter.ToDepartmentId);
			if (filter.ExcludeSubdepartments) qsb.AddParameter("excludeSubdepartments");
			if (!string.IsNullOrEmpty(filter.AfterIndexKey)) qsb.AddParameter("afterIndexKey", filter.AfterIndexKey);
			if (!string.IsNullOrEmpty(filter.SortDirection)) qsb.AddParameter("sortDirection", filter.SortDirection);
			return PerformHttpRequestAsync<DocumentList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[NotNull]
		public Task<DocumentList> GetDocumentsAsync(
			[NotNull] string authToken,
			[NotNull] string boxId,
			[NotNull] string filterCategory,
			[CanBeNull] string counteragentBoxId,
			DateTime? timestampFrom, DateTime? timestampTo,
			[CanBeNull] string fromDocumentDate, [CanBeNull] string toDocumentDate,
			[CanBeNull] string departmentId, bool excludeSubdepartments,
			[CanBeNull] string afterIndexKey
		)
		{
			return GetDocumentsAsync(authToken, new DocumentsFilter
			{
				BoxId = boxId,
				FilterCategory = filterCategory,
				CounteragentBoxId = counteragentBoxId,
				TimestampFrom = timestampFrom,
				TimestampTo = timestampTo,
				FromDocumentDate = fromDocumentDate,
				ToDocumentDate = toDocumentDate,
				DepartmentId = departmentId,
				ExcludeSubdepartments = excludeSubdepartments,
				AfterIndexKey = afterIndexKey,
			});
		}

		[NotNull]
		public Task<Document> GetDocumentAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] string messageId, [NotNull] string entityId)
		{
			var qsb = new PathAndQueryBuilder("/V3/GetDocument");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			qsb.AddParameter("entityId", entityId);
			return PerformHttpRequestAsync<Document>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public Task DeleteAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] string messageId, [CanBeNull] string documentId)
		{
			return MessageOrDocumentCommandAsync("/Delete", authToken, boxId, messageId, documentId);
		}

		public Task RestoreAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] string messageId, [CanBeNull] string documentId)
		{
			return MessageOrDocumentCommandAsync("/Restore", authToken, boxId, messageId, documentId);
		}

		private Task MessageOrDocumentCommandAsync(string action, [NotNull] string authToken, [NotNull] string boxId, [NotNull] string messageId, [CanBeNull] string documentId)
		{
			var qsb = new PathAndQueryBuilder(action);
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			if (!string.IsNullOrEmpty(documentId)) qsb.AddParameter("documentId", documentId);
			return PerformHttpRequestAsync(authToken, "POST", qsb.BuildPathAndQuery());
		}

		public Task MoveDocumentsAsync([NotNull] string authToken, [NotNull] DocumentsMoveOperation query)
		{
			var qsb = new PathAndQueryBuilder("/MoveDocuments");
			return PerformHttpRequestAsync(authToken, "POST", qsb.BuildPathAndQuery(), Serialize(query));
		}

		[NotNull]
		public Task<DocumentList> GetDocumentsByCustomIdAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] string customDocumentId)
		{
			var qsb = new PathAndQueryBuilder("/GetDocumentsByCustomId");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("customDocumentId", customDocumentId);
			return PerformHttpRequestAsync<DocumentList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public Task<DocumentList> GetDocumentsByMessageIdAsync(string authToken, string boxId, string messageId)
		{
			var qsb = new PathAndQueryBuilder("/GetDocumentsByMessageId");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			return PerformHttpRequestAsync<DocumentList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[NotNull]
		public Task<ResolutionRouteList> GetResolutionRoutesForOrganizationAsync([NotNull] string authToken, [NotNull] string orgId)
		{
			var qsb = new PathAndQueryBuilder("/GetResolutionRoutesForOrganization");
			qsb.AddParameter("orgId", orgId);
			return PerformHttpRequestAsync<ResolutionRouteList>(authToken, "GET", qsb.BuildPathAndQuery());
		}
	}
}
