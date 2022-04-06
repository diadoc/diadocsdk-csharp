using Diadoc.Api.Http;
using Diadoc.Api.Proto;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public RoamingOperatorList GetRoamingOperators(string authToken, string boxId)
		{
			var queryString = new PathAndQueryBuilder("/GetRoamingOperators");
			queryString.AddParameter("boxId", boxId);
			return PerformHttpRequest<RoamingOperatorList>(authToken, "GET", queryString.BuildPathAndQuery());
		}
	}
}
