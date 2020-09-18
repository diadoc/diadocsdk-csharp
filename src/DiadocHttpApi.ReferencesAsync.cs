using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Users;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<OrganizationUserPermissions> GetMyPermissionsAsync(string authToken, string orgId, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/GetMyPermissions");
			qsb.AddParameter("orgId", orgId);
			return PerformHttpRequestAsync<OrganizationUserPermissions>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<OrganizationList> GetMyOrganizationsAsync(string authToken, bool autoRegister = true, CancellationToken ct = default)
		{
			var queryBuilder = new PathAndQueryBuilder("/GetMyOrganizations");
			if (!autoRegister) queryBuilder.AddParameter("autoRegister", "false");
			return PerformHttpRequestAsync<OrganizationList>(authToken, "GET", queryBuilder.BuildPathAndQuery(), ct: ct);
		}

		public Task<User> GetMyUserAsync(string authToken, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<User>(authToken, "GET", "/GetMyUser", ct: ct);
		}

		public Task<UserV2> GetMyUserV2Async(string authToken, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<UserV2>(authToken, "GET", "/V2/GetMyUser", ct: ct);
		}

		public Task<UserV2> UpdateMyUserAsync(string authToken, UserToUpdate userToUpdate, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<UserToUpdate, UserV2>(authToken, "/UpdateMyUser", userToUpdate, ct: ct);
		}
		
		public Task<CertificateList> GetMyCertificatesAsync(string authToken, string boxId, CancellationToken ct = default)
		{
			var queryBuilder = new PathAndQueryBuilder("/GetMyCertificates");
			queryBuilder.AddParameter("boxId", boxId);

			return PerformHttpRequestAsync<CertificateList>(authToken, "GET", queryBuilder.BuildPathAndQuery(), ct: ct);
		}

		public Task<OrganizationList> GetOrganizationsByInnKppAsync(string inn, string kpp, bool includeRelations = false, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/GetOrganizationsByInnKpp");
			qsb.AddParameter("inn", inn);
			if (!string.IsNullOrEmpty(kpp)) qsb.AddParameter("kpp", kpp);
			if (includeRelations)
				qsb.AddParameter("includeRelations", "true");
			return PerformHttpRequestAsync<OrganizationList>(null, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<Organization> GetOrganizationByIdAsync(string orgId, CancellationToken ct = default)
		{
			return GetOrganizationAsync($"/GetOrganization?orgId={orgId}", ct: ct);
		}

		public Task<Organization> GetOrganizationByBoxIdAsync(string boxId, CancellationToken ct = default)
		{
			return GetOrganizationAsync($"/GetOrganization?boxId={boxId}", ct: ct);
		}

		public Task<Organization> GetOrganizationByFnsParticipantIdAsync(string fnsParticipantId, CancellationToken ct = default)
		{
			return GetOrganizationAsync($"/GetOrganization?fnsParticipantId={fnsParticipantId}", ct: ct);
		}

		public Task<Organization> GetOrganizationByInnKppAsync(string inn, string kpp, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/GetOrganization");
			qsb.AddParameter("inn", inn);
			if (!string.IsNullOrEmpty(kpp))
				qsb.AddParameter("kpp", kpp);
			var queryString = qsb.BuildPathAndQuery();
			return GetOrganizationAsync(queryString, ct: ct);
		}

		private Task<Organization> GetOrganizationAsync(string queryString, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<Organization>(null, "GET", queryString, ct: ct);
		}

		public Task<Box> GetBoxAsync(string boxId, CancellationToken ct = default)
		{
			var queryString = $"/GetBox?boxId={boxId}";
			return PerformHttpRequestAsync<Box>(null, "GET", queryString, ct: ct);
		}

		public Task<Department> GetDepartmentAsync(string authToken, string orgId, string departmentId, CancellationToken ct = default)
		{
			var queryBuilder = new PathAndQueryBuilder("/GetDepartment");
			queryBuilder.AddParameter("orgId", orgId);
			queryBuilder.AddParameter("departmentId", departmentId);
			return PerformHttpRequestAsync<Department>(authToken, "GET", queryBuilder.BuildPathAndQuery(), ct: ct);
		}

		public Task UpdateOrganizationPropertiesAsync(string authToken, OrganizationPropertiesToUpdate orgProps, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync(authToken, "POST", "/UpdateOrganizationProperties", Serialize(orgProps), ct: ct);
		}

		public Task<OrganizationUsersList> GetOrganizationUsersAsync(string authToken, string orgId, CancellationToken ct = default)
		{
			var queryString = $"/GetOrganizationUsers?orgId={orgId}";
			return PerformHttpRequestAsync<OrganizationUsersList>(authToken, "GET", queryString, ct: ct);
		}

		public async Task<List<Organization>> GetOrganizationsByInnListAsync(GetOrganizationsByInnListRequest innList, CancellationToken ct = default)
		{
			const string queryString = "/GetOrganizationsByInnList";
			var response = await PerformHttpRequestAsync<GetOrganizationsByInnListRequest, GetOrganizationsByInnListResponse>(null, queryString, innList, ct: ct).ConfigureAwait(false);
			return response.Organizations.Select(o => o.Organization).ToList();
		}

		public async Task<List<OrganizationWithCounteragentStatus>> GetOrganizationsByInnListAsync(string authToken, string myOrgId, GetOrganizationsByInnListRequest innList, CancellationToken ct = default)
		{
			var queryString = $"/GetOrganizationsByInnList?myOrgId={myOrgId}";
			var response = await PerformHttpRequestAsync<GetOrganizationsByInnListRequest, GetOrganizationsByInnListResponse>(authToken, queryString, innList, ct: ct).ConfigureAwait(false);
			return response.Organizations;
		}
	}
}
