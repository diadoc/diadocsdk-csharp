using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public async Task<GeneratedFile> GenerateUniversalMessageAsync(string authToken, string boxId, string messageId, string attachmentId, byte[] userContractData)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateUniversalMessage");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("messageId", messageId);
			queryBuilder.AddParameter("attachmentId", attachmentId);

			var request = BuildHttpRequest(authToken, "POST", queryBuilder.BuildPathAndQuery(), userContractData);
			var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
			return new GeneratedFile(response.ContentDispositionFileName, response.Content);
		}
		
		public async Task<byte[]> ParseUniversalMessageAsync(string authToken, string boxId, string messageId, string attachmentId)
		{
			var queryBuilder = new PathAndQueryBuilder("/ParseUniversalMessage");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("messageId", messageId);
			queryBuilder.AddParameter("attachmentId", attachmentId);

			var request = BuildHttpRequest(authToken, "POST", queryBuilder.BuildPathAndQuery(), null);
			var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
			return response.Content;
		}

		public async Task<byte[]> ParseUniversalMessageXmlAsync(string authToken, byte[] content)
		{
			var queryBuilder = new PathAndQueryBuilder("/ParseUniversalMessageXml");

			var request = BuildHttpRequest(authToken, "POST", queryBuilder.BuildPathAndQuery(), content);
			var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);

			return response.Content;
		}
	}
}
