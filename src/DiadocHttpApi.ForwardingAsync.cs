using System;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Forwarding;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[ItemNotNull]
		[Obsolete("Method ForwardDocumentAsync will be removed soon")]
		public Task<ForwardDocumentResponse> ForwardDocumentAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] ForwardDocumentRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/ForwardDocument", boxId);
			return PerformHttpRequestAsync<ForwardDocumentRequest, ForwardDocumentResponse>(authToken, queryString, request);
		}

		[ItemNotNull]
		[Obsolete("Method GetForwardedDocumentsAsync will be removed soon")]
		public Task<GetForwardedDocumentsResponse> GetForwardedDocumentsAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetForwardedDocumentsRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetForwardedDocuments", boxId);
			return PerformHttpRequestAsync<GetForwardedDocumentsRequest, GetForwardedDocumentsResponse>(authToken, queryString, request);
		}

		[ItemNotNull]
		[Obsolete("Method GetForwardedDocumentEventsAsync will be removed soon")]
		public Task<GetForwardedDocumentEventsResponse> GetForwardedDocumentEventsAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetForwardedDocumentEventsRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetForwardedDocumentEvents", boxId);
			return PerformHttpRequestAsync<GetForwardedDocumentEventsRequest, GetForwardedDocumentEventsResponse>(authToken, queryString, request);
		}

		[ItemNotNull]
		[Obsolete("Method GetForwardedEntityContentAsync will be removed soon")]
		public Task<byte[]> GetForwardedEntityContentAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] ForwardedDocumentId forwardedDocumentId, [NotNull] string entityId)
		{
			var queryString = new PathAndQueryBuilder("/V2/GetForwardedEntityContent")
				.WithBoxId(boxId)
				.WithForwardedDocumentId(forwardedDocumentId)
				.With("entityId", entityId)
				.BuildPathAndQuery();
			return PerformHttpRequestAsync(authToken, "GET", queryString);
		}
	}
}
