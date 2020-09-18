using System.Threading;
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
		public Task<ForwardDocumentResponse> ForwardDocumentAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] ForwardDocumentRequest request, CancellationToken ct = default)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/ForwardDocument", boxId);
			return PerformHttpRequestAsync<ForwardDocumentRequest, ForwardDocumentResponse>(authToken, queryString, request, ct: ct);
		}

		[ItemNotNull]
		public Task<GetForwardedDocumentsResponse> GetForwardedDocumentsAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetForwardedDocumentsRequest request, CancellationToken ct = default)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetForwardedDocuments", boxId);
			return PerformHttpRequestAsync<GetForwardedDocumentsRequest, GetForwardedDocumentsResponse>(authToken, queryString, request, ct: ct);
		}

		[ItemNotNull]
		public Task<GetForwardedDocumentEventsResponse> GetForwardedDocumentEventsAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetForwardedDocumentEventsRequest request, CancellationToken ct = default)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetForwardedDocumentEvents", boxId);
			return PerformHttpRequestAsync<GetForwardedDocumentEventsRequest, GetForwardedDocumentEventsResponse>(authToken, queryString, request, ct: ct);
		}

		[ItemNotNull]
		public Task<byte[]> GetForwardedEntityContentAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] ForwardedDocumentId forwardedDocumentId, [NotNull] string entityId, CancellationToken ct = default)
		{
			var queryString = new PathAndQueryBuilder("/V2/GetForwardedEntityContent")
				.WithBoxId(boxId)
				.WithForwardedDocumentId(forwardedDocumentId)
				.With("entityId", entityId)
				.BuildPathAndQuery();
			return PerformHttpRequestAsync(authToken, "GET", queryString, ct: ct);
		}

		[ItemNotNull]
		public Task<DocumentProtocolResult> GenerateForwardedDocumentProtocolAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] ForwardedDocumentId forwardedDocumentId, CancellationToken ct = default)
		{
			var queryString = new PathAndQueryBuilder("/V2/GenerateForwardedDocumentProtocol")
				.WithBoxId(boxId)
				.WithForwardedDocumentId(forwardedDocumentId)
				.BuildPathAndQuery();
			var request = BuildHttpRequest(authToken, "GET", queryString, null);
			return GenerateDocumentProtocolAsync(request, ct: ct);
		}

		[ItemNotNull]
		public Task<PrintFormResult> GenerateForwardedDocumentPrintFormAsync([NotNull] string authToken, [NotNull] string boxId, [NotNull] ForwardedDocumentId forwardedDocumentId, CancellationToken ct = default)
		{
			var queryString = new PathAndQueryBuilder("/GenerateForwardedDocumentPrintForm")
				.WithBoxId(boxId)
				.WithForwardedDocumentId(forwardedDocumentId)
				.BuildPathAndQuery();
			return GetPrintFormResultAsync(authToken, queryString, ct: ct);
		}
	}
}
