using Diadoc.Api.Http;
using Diadoc.Api.Proto.Organizations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public OrganizationFeatures GetOrganizationFeatures(string authToken, string boxId)
		{
			var queryString = new PathAndQueryBuilder("/GetOrganizationFeatures");
			queryString.AddParameter("boxId", boxId);
			return PerformHttpRequest<OrganizationFeatures>(authToken, "GET", queryString.BuildPathAndQuery());
		}
	}
}