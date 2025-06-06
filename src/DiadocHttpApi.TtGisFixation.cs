using Diadoc.Api.Http;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public byte[] GenerateTtGisFixationCancellationRequest(string authToken, string boxId, string messageId, string documentId)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateTtGisFixationCancellationRequest");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("messageId", messageId);
			queryBuilder.AddParameter("documentId", documentId);
			
			var request = BuildHttpRequest(authToken, "POST", queryBuilder.BuildPathAndQuery(), null);
			var response = HttpClient.PerformHttpRequest(request);
			return response.Content;
		}
	}
}
