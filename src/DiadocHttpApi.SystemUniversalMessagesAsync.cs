using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public async Task<GeneratedFile> GenerateSystemUniversalMessageAsync(string authToken, string boxId, string messageId, string attachmentId, byte[] userContractData)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateSystemUniversalMessage");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("messageId", messageId);
			queryBuilder.AddParameter("attachmentId", attachmentId);

			var request = BuildHttpRequest(authToken, "POST", queryBuilder.BuildPathAndQuery(), userContractData);
			var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
			return new GeneratedFile(response.ContentDispositionFileName, response.Content);
		}
	}
}
