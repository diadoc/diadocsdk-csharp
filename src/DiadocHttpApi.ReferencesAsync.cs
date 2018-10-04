using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Users;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<OrganizationUserPermissions> GetMyPermissionsAsync(string authToken, string orgId)
		{
			var qsb = new PathAndQueryBuilder("/GetMyPermissions");
			qsb.AddParameter("orgId", orgId);
			return PerformHttpRequestAsync<OrganizationUserPermissions>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public Task<OrganizationList> GetMyOrganizationsAsync(string authToken, bool autoRegister = true)
		{
			var queryBuilder = new PathAndQueryBuilder("/GetMyOrganizations");
			if (!autoRegister) queryBuilder.AddParameter("autoRegister", "false");
			return PerformHttpRequestAsync<OrganizationList>(authToken, "GET", queryBuilder.BuildPathAndQuery());
		}

		public Task<User> GetMyUserAsync(string authToken)
		{
			return PerformHttpRequestAsync<User>(authToken, "GET", "/GetMyUser");
		}

		public Task<UserV2> GetMyUserV2Async(string authToken)
		{
			return PerformHttpRequestAsync<UserV2>(authToken, "GET", "/V2/GetMyUser");
		}

		public Task<UserV2> UpdateMyUserAsync(string authToken, UserToUpdate userToUpdate)
		{
			return PerformHttpRequestAsync<UserToUpdate, UserV2>(authToken, "/UpdateMyUser", userToUpdate);
		}

		public Task<OrganizationList> GetOrganizationsByInnKppAsync(string inn, string kpp, bool includeRelations = false)
		{
			var qsb = new PathAndQueryBuilder("/GetOrganizationsByInnKpp");
			qsb.AddParameter("inn", inn);
			if (!string.IsNullOrEmpty(kpp)) qsb.AddParameter("kpp", kpp);
			if (includeRelations)
				qsb.AddParameter("includeRelations", "true");
			return PerformHttpRequestAsync<OrganizationList>(null, "GET", qsb.BuildPathAndQuery());
		}

		public Task<Organization> GetOrganizationByIdAsync(string orgId)
		{
			return GetOrganizationAsync($"/GetOrganization?orgId={orgId}");
		}

		public Task<Organization> GetOrganizationByBoxIdAsync(string boxId)
		{
			return GetOrganizationAsync($"/GetOrganization?boxId={boxId}");
		}

		public Task<Organization> GetOrganizationByFnsParticipantIdAsync(string fnsParticipantId)
		{
			return GetOrganizationAsync($"/GetOrganization?fnsParticipantId={fnsParticipantId}");
		}

		public Task<Organization> GetOrganizationByInnKppAsync(string inn, string kpp)
		{
			var qsb = new PathAndQueryBuilder("/GetOrganization");
			qsb.AddParameter("inn", inn);
			if (!string.IsNullOrEmpty(kpp))
				qsb.AddParameter("kpp", kpp);
			var queryString = qsb.BuildPathAndQuery();
			return GetOrganizationAsync(queryString);
		}

		private Task<Organization> GetOrganizationAsync(string queryString)
		{
			return PerformHttpRequestAsync<Organization>(null, "GET", queryString);
		}

		public Task<Box> GetBoxAsync(string boxId)
		{
			var queryString = $"/GetBox?boxId={boxId}";
			return PerformHttpRequestAsync<Box>(null, "GET", queryString);
		}

		public Task<Department> GetDepartmentAsync(string authToken, string orgId, string departmentId)
		{
			var queryBuilder = new PathAndQueryBuilder("/GetDepartment");
			queryBuilder.AddParameter("orgId", orgId);
			queryBuilder.AddParameter("departmentId", departmentId);
			return PerformHttpRequestAsync<Department>(authToken, "GET", queryBuilder.BuildPathAndQuery());
		}

		public Task UpdateOrganizationPropertiesAsync(string authToken, OrganizationPropertiesToUpdate orgProps)
		{
			return PerformHttpRequestAsync(authToken, "POST", "/UpdateOrganizationProperties", Serialize(orgProps));
		}

		public Task<OrganizationUsersList> GetOrganizationUsersAsync(string authToken, string orgId)
		{
			var queryString = $"/GetOrganizationUsers?orgId={orgId}";
			return PerformHttpRequestAsync<OrganizationUsersList>(authToken, "GET", queryString);
		}

		public async Task<List<Organization>> GetOrganizationsByInnListAsync(GetOrganizationsByInnListRequest innList)
		{
			const string queryString = "/GetOrganizationsByInnList";
			var response = await PerformHttpRequestAsync<GetOrganizationsByInnListRequest, GetOrganizationsByInnListResponse>(null, queryString, innList).ConfigureAwait(false);
			return response.Organizations.Select(o => o.Organization).ToList();
		}

		public async Task<List<OrganizationWithCounteragentStatus>> GetOrganizationsByInnListAsync(string authToken, string myOrgId, GetOrganizationsByInnListRequest innList)
		{
			var queryString = $"/GetOrganizationsByInnList?myOrgId={myOrgId}";
			var response = await PerformHttpRequestAsync<GetOrganizationsByInnListRequest, GetOrganizationsByInnListResponse>(authToken, queryString, innList).ConfigureAwait(false);
			return response.Organizations;
		}
	}
}
