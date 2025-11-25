using System;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Forwarding;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[NotNull]
		[Obsolete("Method ForwardDocument will be removed soon")]
		public ForwardDocumentResponse ForwardDocument([NotNull] string authToken, [NotNull] string boxId, [NotNull] ForwardDocumentRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/ForwardDocument", boxId);
			return PerformHttpRequest<ForwardDocumentRequest, ForwardDocumentResponse>(authToken, queryString, request);
		}

		[NotNull]
		[Obsolete("Method GetForwardedDocuments will be removed soon")]
		public GetForwardedDocumentsResponse GetForwardedDocuments([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetForwardedDocumentsRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetForwardedDocuments", boxId);
			return PerformHttpRequest<GetForwardedDocumentsRequest, GetForwardedDocumentsResponse>(authToken, queryString, request);
		}

		[NotNull]
		[Obsolete("Method GetForwardedDocumentEvents will be removed soon")]
		public GetForwardedDocumentEventsResponse GetForwardedDocumentEvents([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetForwardedDocumentEventsRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetForwardedDocumentEvents", boxId);
			return PerformHttpRequest<GetForwardedDocumentEventsRequest, GetForwardedDocumentEventsResponse>(authToken, queryString, request);
		}

		[NotNull]
		[Obsolete("Method GetForwardedEntityContent will be removed soon")]
		public byte[] GetForwardedEntityContent([NotNull] string authToken, [NotNull] string boxId, [NotNull] ForwardedDocumentId forwardedDocumentId, [NotNull] string entityId)
		{
			var queryString = new PathAndQueryBuilder("/V2/GetForwardedEntityContent")
				.WithBoxId(boxId)
				.WithForwardedDocumentId(forwardedDocumentId)
				.With("entityId", entityId)
				.BuildPathAndQuery();
			return PerformHttpRequest(authToken, "GET", queryString);
		}
	}
}
