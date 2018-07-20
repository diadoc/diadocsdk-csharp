using Diadoc.Api.Http;
using Diadoc.Api.Proto.Employees;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Employee GetEmployee(string authToken, string boxId, string userId)
		{
			var queryString = new PathAndQueryBuilder("/GetEmployee");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("userId", userId);
			return PerformHttpRequest<Employee>(authToken, "GET", queryString.BuildPathAndQuery());
		}
	}
}