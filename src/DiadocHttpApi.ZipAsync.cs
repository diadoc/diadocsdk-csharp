using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Documents;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[NotNull]
		public async Task<DocumentZipGenerationResult> GenerateDocumentZipAsync(string authToken, string boxId, string messageId, string documentId, bool fullDocflow)
		{
			var qsb = new PathAndQueryBuilder("/GenerateDocumentZip");
			qsb.AddParameter("boxId", boxId);
			qsb.AddParameter("messageId", messageId);
			qsb.AddParameter("documentId", documentId);
			qsb.AddParameter("fullDocflow", fullDocflow.ToString());
			var request = BuildHttpRequest(authToken, "GET", qsb.BuildPathAndQuery(), null);
			var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
			var result = Deserialize<DocumentZipGenerationResult>(response.Content);
			if (response.RetryAfter.HasValue)
				result.RetryAfter = response.RetryAfter.Value;
			return result;
		}
	}
}
