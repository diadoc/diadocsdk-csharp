using Diadoc.Api.Http;
using Diadoc.Api.Proto.Departments;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Department GetDepartmentByFullId(string authToken, string boxId, string departmentId)
		{
			var queryString = new PathAndQueryBuilder("/admin/GetDepartment");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("departmentId", departmentId);
			return PerformHttpRequest<Department>(authToken, "GET", queryString.BuildPathAndQuery());
		}

		public DepartmentList GetDepartments(string authToken, string boxId, int? page = null, int? count = null)
		{
			var queryString = new PathAndQueryBuilder("/admin/GetDepartments");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("page", page.ToString());
			queryString.AddParameter("count", count.ToString());
			return PerformHttpRequest<DepartmentList>(authToken, "GET", queryString.BuildPathAndQuery());
		}

		public Department CreateDepartment(string authToken, string boxId, DepartmentToCreate departmentToCreate)
		{
			var queryString = new PathAndQueryBuilder("/admin/CreateDepartment");
			queryString.AddParameter("boxId", boxId);
			return PerformHttpRequest<DepartmentToCreate, Department>(authToken, queryString.BuildPathAndQuery(), departmentToCreate);
		}

		public Department UpdateDepartment(string authToken, string boxId, string departmentId, DepartmentToUpdate departmentToUpdate)
		{
			var queryString = new PathAndQueryBuilder("/admin/UpdateDepartment");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("departmentId", departmentId);
			return PerformHttpRequest<DepartmentToUpdate, Department>(authToken, queryString.BuildPathAndQuery(), departmentToUpdate);
		}

		public void DeleteDepartment(string authToken, string boxId, string departmentId)
		{
			var queryString = new PathAndQueryBuilder("/admin/DeleteDepartment");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("departmentId", departmentId);
			PerformHttpRequest(authToken, "Post", queryString.BuildPathAndQuery());
		}
	}
}