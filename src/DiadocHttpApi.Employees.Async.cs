using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Employees;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<Employee> GetEmployeeAsync(string authToken, string boxId, string userId)
		{
			var queryString = new PathAndQueryBuilder("/GetEmployee");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("userId", userId);
			return PerformHttpRequestAsync<Employee>(authToken, "GET", queryString.BuildPathAndQuery());
		}
	}
}