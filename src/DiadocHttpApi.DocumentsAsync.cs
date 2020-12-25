using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Documents.Types;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[ItemNotNull]
		public Task<DocumentList> GetDocumentsAsync([NotNull] string authToken, [NotNull] DocumentsFilter filter, CancellationToken ct = default)
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
			if (!string.IsNullOrEmpty(filter.FromDepartmentId)) qsb.AddParameter("fromDepartmentId", filter.FromDepartmentId);
			if (!string.IsNullOrEmpty(filter.ToDepartmentId)) qsb.AddParameter("toDepartmentId", filter.ToDepartmentId);
			if (filter.ExcludeSubdepartments) qsb.AddParameter("excludeSubdepartments");
			if (!string.IsNullOrEmpty(filter.AfterIndexKey)) qsb.AddParameter("afterIndexKey", filter.AfterIndexKey);
			if (!string.IsNullOrEmpty(filter.SortDirection)) qsb.AddParameter("sortDirection", filter.SortDirection);
			if (filter.Count.HasValue) qsb.AddParameter("count", filter.Count.ToString());
			return PerformHttpRequestAsync<DocumentList>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		[ItemNotNull]
		public Task<DocumentList> GetDocumentsAsync(
			[NotNull] string authToken,
			[NotNull] string boxId,
			[NotNull] string filterCategory,
			[CanBeNull] string counteragentBoxId,
			DateTime? timestampFrom, DateTime? timestampTo,
			[CanBeNull] string fromDocumentDate, [CanBeNull] string toDocumentDate,
			[CanBeNull] string departmentId, bool excludeSubdepartments,
			[CanBeNull] string afterIndexKey,
			int? count = null,
			CancellationToken ct = default
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
				Count = count
			}, ct: ct);
		}

		[ItemNotNull]
		public Task<Document> GetDocumentAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] string messageId, [NotNull] string entityId, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/V3/GetDocument");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			qsb.AddParameter("entityId", entityId);
			return PerformHttpRequestAsync<Document>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task DeleteAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] string messageId, [CanBeNull] string documentId, CancellationToken ct = default)
		{
			return MessageOrDocumentCommandAsync("/Delete", authToken, boxId, messageId, documentId, ct: ct);
		}

		public Task RestoreAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] string messageId, [CanBeNull] string documentId, CancellationToken ct = default)
		{
			return MessageOrDocumentCommandAsync("/Restore", authToken, boxId, messageId, documentId, ct: ct);
		}

		private Task MessageOrDocumentCommandAsync(string action, [NotNull] string authToken, [NotNull] string boxId, [NotNull] string messageId, [CanBeNull] string documentId, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder(action);
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			if (!string.IsNullOrEmpty(documentId)) qsb.AddParameter("documentId", documentId);
			return PerformHttpRequestAsync(authToken, "POST", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task MoveDocumentsAsync([NotNull] string authToken, [NotNull] DocumentsMoveOperation query, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/MoveDocuments");
			return PerformHttpRequestAsync(authToken, "POST", qsb.BuildPathAndQuery(), Serialize(query), ct: ct);
		}

		[ItemNotNull]
		public Task<DocumentList> GetDocumentsByCustomIdAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] string customDocumentId, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/GetDocumentsByCustomId");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("customDocumentId", customDocumentId);
			return PerformHttpRequestAsync<DocumentList>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<DocumentList> GetDocumentsByMessageIdAsync(string authToken, string boxId, string messageId, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/GetDocumentsByMessageId");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			return PerformHttpRequestAsync<DocumentList>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<SignatureInfo> GetSignatureInfoAsync(string authToken, string boxId, string messageId, string entityId, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/GetSignatureInfo");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			qsb.AddParameter("entityId", entityId);
			return PerformHttpRequestAsync<SignatureInfo>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		[ItemNotNull]
		public Task<ResolutionRouteList> GetResolutionRoutesForOrganizationAsync([NotNull] string authToken, [NotNull] string orgId, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/GetResolutionRoutesForOrganization");
			qsb.AddParameter("orgId", orgId);
			return PerformHttpRequestAsync<ResolutionRouteList>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<GetDocumentTypesResponse> GetDocumentTypesAsync(string authToken, string boxId, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/GetDocumentTypes");
			qsb.AddParameter("boxId", boxId);
			return PerformHttpRequestAsync<GetDocumentTypesResponse>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<DetectDocumentTypesResponse> DetectDocumentTypesAsync(string authToken, string boxId, string nameOnShelf, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/DetectDocumentTypes");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("nameOnShelf", nameOnShelf);
			return PerformHttpRequestAsync<DetectDocumentTypesResponse>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<DetectDocumentTypesResponse> DetectDocumentTypesAsync(string authToken, string boxId, byte[] content, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/DetectDocumentTypes");
			qsb.AddParameter("boxId", boxId);
			return PerformHttpRequestAsync<DetectDocumentTypesResponse>(authToken, "POST", qsb.BuildPathAndQuery(), content, ct: ct);
		}
		
		public Task<DetectTitleResponse> DetectDocumentTitlesAsync(string authToken, string boxId, string nameOnShelf, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/DetectDocumentTitles");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("nameOnShelf", nameOnShelf);
			return PerformHttpRequestAsync<DetectTitleResponse>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<DetectTitleResponse> DetectDocumentTitlesAsync(string authToken, string boxId, byte[] content, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/DetectDocumentTitles");
			qsb.AddParameter("boxId", boxId);
			return PerformHttpRequestAsync<DetectTitleResponse>(authToken, "POST", qsb.BuildPathAndQuery(), content, ct: ct);
		}

		public async Task<FileContent> GetContentAsync(string authToken, string typeNamedId, string function, string version, int titleIndex, XsdContentType contentType = default(XsdContentType), CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/GetContent");
			qsb.AddParameter("typeNamedId", typeNamedId);
			qsb.AddParameter("function", function);
			qsb.AddParameter("version", version);
			qsb.AddParameter("titleIndex", titleIndex.ToString());
			qsb.AddParameter("contentType", contentType.ToString());

			var request = BuildHttpRequest(authToken, "GET", qsb.BuildPathAndQuery(), null);
			var response = await HttpClient.PerformHttpRequestAsync(request, ct: ct).ConfigureAwait(false);

			return new FileContent(response.Content, response.ContentDispositionFileName);
		}
	}
}
