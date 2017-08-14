using System;
using System.Globalization;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Documents;
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
			if (!string.IsNullOrEmpty(filter.ToDepartmentId)) qsb.AddParameter("toDepartmentId", filter.ToDepartmentId);
			if (filter.ExcludeSubdepartments) qsb.AddParameter("excludeSubdepartments");
			if (!string.IsNullOrEmpty(filter.AfterIndexKey)) qsb.AddParameter("afterIndexKey", filter.AfterIndexKey);
			if (!string.IsNullOrEmpty(filter.SortDirection)) qsb.AddParameter("sortDirection", filter.SortDirection);
			return PerformHttpRequest<DocumentList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[NotNull]
		public DocumentList GetDocuments(
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

		public SignatureInfo GetSignatureInfo(string authToken, string boxId, string messageId, string entityId)
		{
			var qsb = new PathAndQueryBuilder("/GetSignatureInfo");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			qsb.AddParameter("entityId", entityId);
			return PerformHttpRequest<SignatureInfo>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[NotNull]
		public ResolutionRouteList GetResolutionRoutesForOrganization([NotNull] string authToken, [NotNull] string orgId)
		{
			var queryString = string.Format("/GetResolutionRoutesForOrganization?orgId={0}", orgId);
			return PerformHttpRequest<ResolutionRouteList>(authToken, "GET", queryString);
		}
	}
}
