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

		public Task<CounteragentGroup> GetCounteragentGroupAsync(string authToken, string boxId, string counteragentGroupId)
		{
			var queryString = new PathAndQueryBuilder("/GetCounteragentGroup");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("counteragentGroupId", counteragentGroupId);
			return PerformHttpRequestAsync<CounteragentGroup>(authToken, "GET", queryString.BuildPathAndQuery());
		}

		public Task<CounteragentGroupsList> GetCounteragentGroupsAsync(string authToken, string boxId, int? page = null, int? count = null)
		{
			var queryString = new PathAndQueryBuilder("/GetCounteragentGroups");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("page", page?.ToString());
			queryString.AddParameter("count", count?.ToString());
			return PerformHttpRequestAsync<CounteragentGroupsList>(authToken, "GET", queryString.BuildPathAndQuery());
		}

		public Task AddCounteragentToGroupAsync(string authToken, string boxId, string counteragentBoxId, string counteragentGroupId)
		{
			var queryString = new PathAndQueryBuilder("/AddCounteragentToGroup");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("counteragentBoxId", counteragentBoxId);
			queryString.AddParameter("counteragentGroupId", counteragentGroupId);
			return PerformHttpRequestAsync(authToken, "POST", queryString.BuildPathAndQuery());
		}

		public Task<CounteragentFromGroupResponse> GetCounteragentsFromGroupAsync(string authToken, string boxId, string counteragentGroupId, int? count = null, string afterIndexKey = null)
		{
			var queryString = new PathAndQueryBuilder("/GetCounteragentsFromGroup");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("counteragentGroupId", counteragentGroupId);
			queryString.AddParameter("count", count?.ToString());
			queryString.AddParameter("afterIndexKey", afterIndexKey);
			return PerformHttpRequestAsync<CounteragentFromGroupResponse>(authToken, "GET", queryString.BuildPathAndQuery());
		}
	}
}
