using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public GeneratedFile GenerateSystemUniversalMessage(string authToken, string boxId, string messageId, string attachmentId, byte[] userContractData)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateSystemUniversalMessage");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("messageId", messageId);
			queryBuilder.AddParameter("attachmentId", attachmentId);

			var request = BuildHttpRequest(authToken, "POST", queryBuilder.BuildPathAndQuery(), userContractData);
			var response = HttpClient.PerformHttpRequest(request);
			return new GeneratedFile(response.ContentDispositionFileName, response.Content);
		}
	}
}
