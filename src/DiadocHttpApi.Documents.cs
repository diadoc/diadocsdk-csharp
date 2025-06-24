using System;
using System.Globalization;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Documents.Types;
using Diadoc.Api.Proto.Workflows;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[NotNull]
		public DocumentList GetDocuments([NotNull] string authToken, [NotNull] DocumentsFilter filter)
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
			return PerformHttpRequest<DocumentList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[NotNull]
		public DocumentList GetDocuments(
			[NotNull] string authToken,
			[NotNull] string boxId,
			[NotNull] string filterCategory,
			[CanBeNull] string counteragentBoxId,
			DateTime? timestampFrom,
			DateTime? timestampTo,
			[CanBeNull] string fromDocumentDate,
			[CanBeNull] string toDocumentDate,
			[CanBeNull] string departmentId,
			bool excludeSubdepartments,
			[CanBeNull] string afterIndexKey,
			int? count = null
		)
		{
			return GetDocuments(authToken, new DocumentsFilter
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
			});
		}

		[NotNull]
		public Document GetDocument([NotNull] string authToken, [NotNull] string boxId, [NotNull] string messageId, [NotNull] string entityId)
		{
			var qsb = new PathAndQueryBuilder("/V3/GetDocument");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			qsb.AddParameter("entityId", entityId);
			return PerformHttpRequest<Document>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public void Delete([NotNull] string authToken, [NotNull] string boxId, [NotNull] string messageId, [CanBeNull] string documentId)
		{
			MessageOrDocumentCommand("/Delete", authToken, boxId, messageId, documentId);
		}

		public void Restore([NotNull] string authToken, [NotNull] string boxId, [NotNull] string messageId, [CanBeNull] string documentId)
		{
			MessageOrDocumentCommand("/Restore", authToken, boxId, messageId, documentId);
		}

		private void MessageOrDocumentCommand(string action, [NotNull] string authToken, [NotNull] string boxId, [NotNull] string messageId, [CanBeNull] string documentId)
		{
			var qsb = new PathAndQueryBuilder(action);
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			if (!string.IsNullOrEmpty(documentId)) qsb.AddParameter("documentId", documentId);
			PerformHttpRequest(authToken, "POST", qsb.BuildPathAndQuery());
		}

		public void MoveDocuments([NotNull] string authToken, [NotNull] DocumentsMoveOperation query)
		{
			var qsb = new PathAndQueryBuilder("/MoveDocuments");
			PerformHttpRequest(authToken, "POST", qsb.BuildPathAndQuery(), Serialize(query));
		}

		[NotNull]
		public DocumentList GetDocumentsByCustomId([NotNull] string authToken, [NotNull] string boxId, [NotNull] string customDocumentId)
		{
			var qsb = new PathAndQueryBuilder("/GetDocumentsByCustomId");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("customDocumentId", customDocumentId);
			return PerformHttpRequest<DocumentList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public DocumentList GetDocumentsByMessageId(string authToken, string boxId, string messageId)
		{
			var qsb = new PathAndQueryBuilder("/GetDocumentsByMessageId");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			return PerformHttpRequest<DocumentList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public DocumentWorkflowSettingsListV2 GetWorkflowsSettings([NotNull] string authToken, string boxId)
		{
			var qsb = new PathAndQueryBuilder("/V2/GetWorkflowsSettings");
			qsb.AddParameter("boxId", boxId);
			return PerformHttpRequest<DocumentWorkflowSettingsListV2>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public SignatureInfo GetSignatureInfo(string authToken, string boxId, string messageId, string entityId)
		{
			var qsb = new PathAndQueryBuilder("/GetSignatureInfo");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			qsb.AddParameter("entityId", entityId);
			return PerformHttpRequest<SignatureInfo>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[Obsolete("Use GetResolutionRoutes")]
		public ResolutionRouteList GetResolutionRoutesForOrganization([NotNull] string authToken, [NotNull] string orgId)
		{
			var queryString = string.Format("/GetResolutionRoutesForOrganization?orgId={0}", orgId);
			return PerformHttpRequest<ResolutionRouteList>(authToken, "GET", queryString);
		}

		[NotNull]
		public ResolutionRouteList GetResolutionRoutes([NotNull] string authToken, [NotNull] string boxId)
		{
			var queryString = $"/GetResolutionRoutes?boxId={boxId}";
			return PerformHttpRequest<ResolutionRouteList>(authToken, "GET", queryString);
		}

		public GetDocumentTypesResponseV2 GetDocumentTypesV2(string authToken, string boxId)
		{
			var qsb = new PathAndQueryBuilder("/V2/GetDocumentTypes");
			qsb.AddParameter("boxId", boxId);
			return PerformHttpRequest<GetDocumentTypesResponseV2>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[Obsolete("Use DetectDocumentTitles")]
		public DetectDocumentTypesResponse DetectDocumentTypes(string authToken, string boxId, string nameOnShelf)
		{
			var qsb = new PathAndQueryBuilder("/DetectDocumentTypes");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("nameOnShelf", nameOnShelf);
			return PerformHttpRequest<DetectDocumentTypesResponse>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[Obsolete("Use DetectDocumentTitles")]
		public DetectDocumentTypesResponse DetectDocumentTypes(string authToken, string boxId, byte[] content)
		{
			var qsb = new PathAndQueryBuilder("/DetectDocumentTypes");
			qsb.AddParameter("boxId", boxId);
			return PerformHttpRequest<DetectDocumentTypesResponse>(authToken, "POST", qsb.BuildPathAndQuery(), content);
		}

		public DetectTitleResponse DetectDocumentTitles(string authToken, string boxId, string nameOnShelf)
		{
			var qsb = new PathAndQueryBuilder("/DetectDocumentTitles");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("nameOnShelf", nameOnShelf);
			return PerformHttpRequest<DetectTitleResponse>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public DetectTitleResponse DetectDocumentTitles(string authToken, string boxId, byte[] content)
		{
			var qsb = new PathAndQueryBuilder("/DetectDocumentTitles");
			qsb.AddParameter("boxId", boxId);
			return PerformHttpRequest<DetectTitleResponse>(authToken, "POST", qsb.BuildPathAndQuery(), content);
		}

		public FileContent GetContent(string authToken, string typeNamedId, string function, string version, int titleIndex, XsdContentType contentType = default(XsdContentType))
		{
			var qsb = new PathAndQueryBuilder("/GetContent");
			qsb.AddParameter("typeNamedId", typeNamedId);
			qsb.AddParameter("function", function);
			qsb.AddParameter("version", version);
			qsb.AddParameter("titleIndex", titleIndex.ToString());
			qsb.AddParameter("contentType", contentType.ToString());

			var request = BuildHttpRequest(authToken, "GET", qsb.BuildPathAndQuery(), null);
			var response = HttpClient.PerformHttpRequest(request);

			return new FileContent(response.Content, response.ContentDispositionFileName);
		}
	}
}
