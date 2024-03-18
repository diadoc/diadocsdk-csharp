using Diadoc.Api.Http;
using Diadoc.Api.Proto.CounteragentGroups;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public CounteragentGroup CreateCounteragentGroup(string authToken, string boxId, CounteragentGroupToCreate counteragentGroupToCreate)
		{
			var queryString = new PathAndQueryBuilder("/CreateCounteragentGroup");
			queryString.AddParameter("boxId", boxId);
			return PerformHttpRequest<CounteragentGroupToCreate, CounteragentGroup>(authToken, queryString.BuildPathAndQuery(), counteragentGroupToCreate);
		}

		public CounteragentGroup UpdateCounteragentGroup(string authToken, string boxId, string counteragentGroupId, CounteragentGroupToUpdate counteragentGroupToUpdate)
		{
			var queryString = new PathAndQueryBuilder("/UpdateCounteragentGroup");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("counteragentGroupId", counteragentGroupId);
			return PerformHttpRequest<CounteragentGroupToUpdate, CounteragentGroup>(authToken, queryString.BuildPathAndQuery(), counteragentGroupToUpdate);
		}

		public void DeleteCounteragentGroup(string authToken, string boxId, string counteragentGroupId)
		{
			var queryString = new PathAndQueryBuilder("/DeleteCounteragentGroup");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("counteragentGroupId", counteragentGroupId);
			PerformHttpRequest(authToken, "POST", queryString.BuildPathAndQuery());
		}

		public CounteragentGroup GetCounteragentGroup(string authToken, string boxId, string counteragentGroupId)
		{
			var queryString = new PathAndQueryBuilder("/GetCounteragentGroup");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("counteragentGroupId", counteragentGroupId);
			return PerformHttpRequest<CounteragentGroup>(authToken, "GET", queryString.BuildPathAndQuery());
		}

		public CounteragentGroupsList GetCounteragentGroups(string authToken, string boxId, int? page = null, int? count = null)
		{
			var queryString = new PathAndQueryBuilder("/GetCounteragentGroups");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("page", page?.ToString());
			queryString.AddParameter("count", count?.ToString());
			return PerformHttpRequest<CounteragentGroupsList>(authToken, "GET", queryString.BuildPathAndQuery());
		}

		public void AddCounteragentToGroup(string authToken, string boxId, string counteragentBoxId, string counteragentGroupId)
		{
			var queryString = new PathAndQueryBuilder("/AddCounteragentToGroup");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("counteragentBoxId", counteragentBoxId);
			queryString.AddParameter("counteragentGroupId", counteragentGroupId);
			PerformHttpRequest(authToken, "POST", queryString.BuildPathAndQuery());
		}

		public CounteragentFromGroupResponse GetCounteragentsFromGroup(string authToken, string boxId, string counteragentGroupId, int? count = null, string afterIndexKey = null)
		{
			var queryString = new PathAndQueryBuilder("/GetCounteragentsFromGroup");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("counteragentGroupId", counteragentGroupId);
			queryString.AddParameter("count", count?.ToString());
			queryString.AddParameter("afterIndexKey", afterIndexKey);
			return PerformHttpRequest<CounteragentFromGroupResponse>(authToken, "GET", queryString.BuildPathAndQuery());
		}
	}
}
