using System.Threading.Tasks;
using Diadoc.Api.Http;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public async Task<byte[]> GenerateTtGisFixationCancellationRequestAsync(string authToken, string boxId, string messageId, string documentId)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateTtGisFixationCancellationRequest");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("messageId", messageId);
			queryBuilder.AddParameter("documentId", documentId);
			
			var request = BuildHttpRequest(authToken, "POST", queryBuilder.BuildPathAndQuery(), null);
			var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
			return response.Content;
		}
	}
}
