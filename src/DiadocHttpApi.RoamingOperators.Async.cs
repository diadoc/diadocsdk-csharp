using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<RoamingOperatorList> GetRoamingOperatorsAsync(string authToken, string boxId)
		{
			var queryString = new PathAndQueryBuilder("/GetRoamingOperators");
			queryString.AddParameter("boxId", boxId);
			return PerformHttpRequestAsync<RoamingOperatorList>(authToken, "GET", queryString.BuildPathAndQuery());
		}
	}
}
