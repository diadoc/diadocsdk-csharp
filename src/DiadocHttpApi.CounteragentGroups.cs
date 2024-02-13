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
	}
}
