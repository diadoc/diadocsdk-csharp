using Diadoc.Api.Annotations;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Forwarding;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[NotNull]
		public ForwardDocumentResponse ForwardDocument([NotNull] string authToken, [NotNull] string boxId, [NotNull] ForwardDocumentRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/ForwardDocument", boxId);
			return PerformHttpRequest<ForwardDocumentRequest, ForwardDocumentResponse>(authToken, queryString, request);
		}

		[NotNull]
		public GetForwardedDocumentsResponse GetForwardedDocuments([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetForwardedDocumentsRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetForwardedDocuments", boxId);
			return PerformHttpRequest<GetForwardedDocumentsRequest, GetForwardedDocumentsResponse>(authToken, queryString, request);
		}

		[NotNull]
		public GetForwardedDocumentEventsResponse GetForwardedDocumentEvents([NotNull] string authToken, [NotNull] string boxId, [NotNull] GetForwardedDocumentEventsRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("/V2/GetForwardedDocumentEvents", boxId);
			return PerformHttpRequest<GetForwardedDocumentEventsRequest, GetForwardedDocumentEventsResponse>(authToken, queryString, request);
		}

		[NotNull]
		public byte[] GetForwardedEntityContent([NotNull] string authToken, [NotNull] string boxId, [NotNull] ForwardedDocumentId forwardedDocumentId, [NotNull] string entityId)
		{
			var queryString = new PathAndQueryBuilder("/V2/GetForwardedEntityContent")
				.WithBoxId(boxId)
				.WithForwardedDocumentId(forwardedDocumentId)
				.With("entityId", entityId)
				.BuildPathAndQuery();
			return PerformHttpRequest(authToken, "GET", queryString);
		}

		[NotNull]
		public IDocumentProtocolResult GenerateForwardedDocumentProtocol([NotNull] string authToken, [NotNull] string boxId, [NotNull] ForwardedDocumentId forwardedDocumentId)
		{
			var queryString = new PathAndQueryBuilder("/V2/GenerateForwardedDocumentProtocol")
				.WithBoxId(boxId)
				.WithForwardedDocumentId(forwardedDocumentId)
				.BuildPathAndQuery();
			var request = BuildHttpRequest(authToken, "GET", queryString, null);
			return GenerateDocumentProtocol(request);
		}
		
		[NotNull]
		public PrintFormResult GenerateForwardedDocumentPrintForm([NotNull] string authToken, [NotNull] string boxId, [NotNull] ForwardedDocumentId forwardedDocumentId)
		{
			var queryString = new PathAndQueryBuilder("/GenerateForwardedDocumentPrintForm")
				.WithBoxId(boxId)
				.WithForwardedDocumentId(forwardedDocumentId)
				.BuildPathAndQuery();
			return GetPrintFormResult(authToken, queryString);
		}
	}
}
