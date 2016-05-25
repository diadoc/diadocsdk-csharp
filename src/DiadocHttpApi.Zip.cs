using Diadoc.Api.Annotations;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Documents;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[NotNull]
		public IDocumentZipGenerationResult GenerateDocumentZip(string authToken, string boxId, string messageId, string documentId, bool fullDocflow)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateDocumentZip");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("messageId", messageId);
			queryBuilder.AddParameter("documentId", documentId);
			queryBuilder.AddParameter("fullDocflow", fullDocflow.ToString());
			var request = BuildHttpRequest(authToken, "GET", queryBuilder.BuildPathAndQuery(), null);
			var response = HttpClient.PerformHttpRequest(request);
			var result = Deserialize<DocumentZipGenerationResult>(response.Content);
			if (response.RetryAfter.HasValue)
				result.RetryAfter = response.RetryAfter.Value;
			return result;
		}
	}
}
