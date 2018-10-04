using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Employees;
using Diadoc.Api.Proto.Employees.Subscriptions;

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

		public Task<Employee> CreateEmployeeAsync(string authToken, string boxId, EmployeeToCreate employeeToCreate)
		{
			var queryString = new PathAndQueryBuilder("/CreateEmployee");
			queryString.AddParameter("boxId", boxId);
			return PerformHttpRequestAsync<EmployeeToCreate, Employee>(authToken, queryString.BuildPathAndQuery(), employeeToCreate);
		}

		public Task<Employee> UpdateEmployeeAsync(string authToken, string boxId, string userId, EmployeeToUpdate employeeToUpdate)
		{
			var queryString = new PathAndQueryBuilder("/UpdateEmployee");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("userId", userId);
			return PerformHttpRequestAsync<EmployeeToUpdate, Employee>(authToken, queryString.BuildPathAndQuery(), employeeToUpdate);
		}

		public Task DeleteEmployeeAsync(string authToken, string boxId, string userId)
		{
			var queryString = new PathAndQueryBuilder("/DeleteEmployee");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("userId", userId);
			return PerformHttpRequestAsync(authToken, "POST", queryString.BuildPathAndQuery());
		}

		public Task<EmployeeSubscriptions> GetSubscriptionsAsync(string authToken, string boxId, string userId)
		{
			var queryString = new PathAndQueryBuilder("/GetSubscriptions");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("userId", userId);
			return PerformHttpRequestAsync<EmployeeSubscriptions>(authToken, "GET", queryString.BuildPathAndQuery());
		}

		public Task<EmployeeSubscriptions> UpdateSubscriptionsAsync(string authToken, string boxId, string userId, SubscriptionsToUpdate subscriptionsToUpdate)
		{
			var queryString = new PathAndQueryBuilder("/UpdateSubscriptions");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("userId", userId);
			return PerformHttpRequestAsync<SubscriptionsToUpdate, EmployeeSubscriptions>(authToken, queryString.BuildPathAndQuery(), subscriptionsToUpdate);
		}
	}
}