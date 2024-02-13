using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.CounteragentGroups;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<CounteragentGroup> CreateCounteragentGroupAsync(string authToken, string boxId, CounteragentGroupToCreate counteragentGroupToCreate)
		{
			var queryString = new PathAndQueryBuilder("/CreateCounteragentGroup");
			queryString.AddParameter("boxId", boxId);
			return PerformHttpRequestAsync<CounteragentGroupToCreate, CounteragentGroup>(authToken, queryString.BuildPathAndQuery(), counteragentGroupToCreate);
		}

		public Task<CounteragentGroup> UpdateCounteragentGroupAsync(string authToken, string boxId, string counteragentGroupId, CounteragentGroupToUpdate counteragentGroupToUpdate)
		{
			var queryString = new PathAndQueryBuilder("/UpdateCounteragentGroup");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("counteragentGroupId", counteragentGroupId);
			return PerformHttpRequestAsync<CounteragentGroupToUpdate, CounteragentGroup>(authToken, queryString.BuildPathAndQuery(), counteragentGroupToUpdate);
		}

		public Task DeleteCounteragentGroupAsync(string authToken, string boxId, string counteragentGroupId)
		{
			var queryString = new PathAndQueryBuilder("/DeleteCounteragentGroup");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("counteragentGroupId", counteragentGroupId);
			return PerformHttpRequestAsync(authToken, "POST", queryString.BuildPathAndQuery());
		}
	}
}
