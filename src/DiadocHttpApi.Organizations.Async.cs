using System.Threading;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Organizations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<OrganizationFeatures> GetOrganizationFeaturesAsync(string authToken, string boxId, CancellationToken ct = default)
		{
			var queryString = new PathAndQueryBuilder("/GetOrganizationFeatures");
			queryString.AddParameter("boxId", boxId);
			return PerformHttpRequestAsync<OrganizationFeatures>(authToken, "GET", queryString.BuildPathAndQuery(), ct: ct);
		}
	}
}