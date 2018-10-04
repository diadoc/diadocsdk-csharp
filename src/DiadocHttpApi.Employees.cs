using Diadoc.Api.Http;
using Diadoc.Api.Proto.Employees;
using Diadoc.Api.Proto.Employees.Subscriptions;

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

		public Employee CreateEmployee(string authToken, string boxId, EmployeeToCreate employeeToCreate)
		{
			var queryString = new PathAndQueryBuilder("/CreateEmployee");
			queryString.AddParameter("boxId", boxId);
			return PerformHttpRequest<EmployeeToCreate, Employee>(authToken, queryString.BuildPathAndQuery(), employeeToCreate);
		}

		public Employee UpdateEmployee(string authToken, string boxId, string userId, EmployeeToUpdate employeeToUpdate)
		{
			var queryString = new PathAndQueryBuilder("/UpdateEmployee");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("userId", userId);
			return PerformHttpRequest<EmployeeToUpdate, Employee>(authToken, queryString.BuildPathAndQuery(), employeeToUpdate);
		}

		public void DeleteEmployee(string authToken, string boxId, string userId)
		{
			var queryString = new PathAndQueryBuilder("/DeleteEmployee");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("userId", userId);
			PerformHttpRequest(authToken, "POST", queryString.BuildPathAndQuery());
		}

		public EmployeeSubscriptions GetSubscriptions(string authToken, string boxId, string userId)
		{
			var queryString = new PathAndQueryBuilder("/GetSubscriptions");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("userId", userId);
			return PerformHttpRequest<EmployeeSubscriptions>(authToken, "GET", queryString.BuildPathAndQuery());
		}

		public EmployeeSubscriptions UpdateSubscriptions(string authToken, string boxId, string userId, SubscriptionsToUpdate subscriptionsToUpdate)
		{
			var queryString = new PathAndQueryBuilder("/UpdateSubscriptions");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("userId", userId);
			return PerformHttpRequest<SubscriptionsToUpdate, EmployeeSubscriptions>(authToken, queryString.BuildPathAndQuery(), subscriptionsToUpdate);
		}
	}
}