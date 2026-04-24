using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{

		public Task<SecurityEventList> GetSecurityEventsAsync(string authToken, string boxId, string afterIndexKey = null, int? count = null)
		{
			var queryString = new PathAndQueryBuilder("/V1/GetSecurityEvents");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("afterIndexKey", afterIndexKey);
			queryString.AddParameter("count", count?.ToString());
			return PerformHttpRequestAsync<SecurityEventList>(authToken, "GET", queryString.BuildPathAndQuery());
		}
	}
}
