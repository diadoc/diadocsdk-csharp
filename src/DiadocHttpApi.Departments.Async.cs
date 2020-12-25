using System.Threading;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Departments;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<Department> GetDepartmentByFullIdAsync(string authToken, string boxId, string departmentId, CancellationToken ct = default)
		{
			var queryString = new PathAndQueryBuilder("/admin/GetDepartment");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("departmentId", departmentId);
			return PerformHttpRequestAsync<Department>(authToken, "GET", queryString.BuildPathAndQuery(), ct: ct);
		}

		public Task<DepartmentList> GetDepartmentsAsync(string authToken, string boxId, int? page = null, int? count = null, CancellationToken ct = default)
		{
			var queryString = new PathAndQueryBuilder("/admin/GetDepartments");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("page", page?.ToString());
			queryString.AddParameter("count", count?.ToString());
			return PerformHttpRequestAsync<DepartmentList>(authToken, "GET", queryString.BuildPathAndQuery(), ct: ct);
		}

		public Task<Department> CreateDepartmentAsync(string authToken, string boxId, DepartmentToCreate departmentToCreate, CancellationToken ct = default)
		{
			var queryString = new PathAndQueryBuilder("/admin/CreateDepartment");
			queryString.AddParameter("boxId", boxId);
			return PerformHttpRequestAsync<DepartmentToCreate, Department>(authToken, queryString.BuildPathAndQuery(), departmentToCreate, ct: ct);
		}

		public Task<Department> UpdateDepartmentAsync(string authToken, string boxId, string departmentId, DepartmentToUpdate departmentToUpdate, CancellationToken ct = default)
		{
			var queryString = new PathAndQueryBuilder("/admin/UpdateDepartment");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("departmentId", departmentId);
			return PerformHttpRequestAsync<DepartmentToUpdate, Department>(authToken, queryString.BuildPathAndQuery(), departmentToUpdate, ct: ct);
		}

		public Task DeleteDepartmentAsync(string authToken, string boxId, string departmentId, CancellationToken ct = default)
		{
			var queryString = new PathAndQueryBuilder("/admin/DeleteDepartment");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("departmentId", departmentId);
			return PerformHttpRequestAsync(authToken, "Post", queryString.BuildPathAndQuery(), ct: ct);
		}
	}
}