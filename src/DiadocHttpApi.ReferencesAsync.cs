using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diadoc.Api.Constants;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Certificates;
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
		
		public Task<CertificateList> GetMyCertificatesAsync(string authToken, string boxId)
		{
			var queryBuilder = new PathAndQueryBuilder("/GetMyCertificates");
			queryBuilder.AddParameter("boxId", boxId);

			return PerformHttpRequestAsync<CertificateList>(authToken, "GET", queryBuilder.BuildPathAndQuery());
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<OrganizationList> GetOrganizationsByInnKppAsync(string inn, string kpp, bool includeRelations = false)
		{
			return GetOrganizationsByInnKppAsync(null, inn, kpp, includeRelations);
		}

		public Task<OrganizationList> GetOrganizationsByInnKppAsync(string authToken, string inn, string kpp, bool includeRelations = false)
		{
			var qsb = new PathAndQueryBuilder("/GetOrganizationsByInnKpp");
			qsb.AddParameter("inn", inn);
			if (!string.IsNullOrEmpty(kpp)) qsb.AddParameter("kpp", kpp);
			if (includeRelations)
				qsb.AddParameter("includeRelations", "true");
			return PerformHttpRequestAsync<OrganizationList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<Organization> GetOrganizationByIdAsync(string orgId)
		{
			return GetOrganizationByIdAsync(null, orgId);
		}

		public Task<Organization> GetOrganizationByIdAsync(string authToken, string orgId)
		{
			return GetOrganizationAsync(authToken, $"/GetOrganization?orgId={orgId}");
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<Organization> GetOrganizationByBoxIdAsync(string boxId)
		{
			return GetOrganizationByBoxIdAsync(null, boxId);
		}

		public Task<Organization> GetOrganizationByBoxIdAsync(string authToken, string boxId)
		{
			return GetOrganizationAsync(authToken, $"/GetOrganization?boxId={boxId}");
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<Organization> GetOrganizationByFnsParticipantIdAsync(string fnsParticipantId)
		{
			return GetOrganizationByFnsParticipantIdAsync(null, fnsParticipantId);
		}

		public Task<Organization> GetOrganizationByFnsParticipantIdAsync(string authToken, string fnsParticipantId)
		{
			return GetOrganizationAsync(authToken, $"/GetOrganization?fnsParticipantId={fnsParticipantId}");
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<Organization> GetOrganizationByInnKppAsync(string inn, string kpp)
		{
			return GetOrganizationByInnKppAsync(null, inn, kpp);
		}

		public Task<Organization> GetOrganizationByInnKppAsync(string authToken, string inn, string kpp)
		{
			var qsb = new PathAndQueryBuilder("/GetOrganization");
			qsb.AddParameter("inn", inn);
			if (!string.IsNullOrEmpty(kpp))
				qsb.AddParameter("kpp", kpp);
			var queryString = qsb.BuildPathAndQuery();
			return GetOrganizationAsync(authToken, queryString);
		}

		private Task<Organization> GetOrganizationAsync(string authToken, string queryString)
		{
			return PerformHttpRequestAsync<Organization>(authToken, "GET", queryString);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<Box> GetBoxAsync(string boxId)
		{
			return GetBoxAsync(null, boxId);
		}

		public Task<Box> GetBoxAsync(string authToken, string boxId)
		{
			var queryString = $"/GetBox?boxId={boxId}";
			return PerformHttpRequestAsync<Box>(authToken, "GET", queryString);
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

		[Obsolete("Use GetOrganizationUsersV2Async()")]
		public Task<OrganizationUsersList> GetOrganizationUsersAsync(string authToken, string orgId)
		{
			var queryString = $"/GetOrganizationUsers?orgId={orgId}";
			return PerformHttpRequestAsync<OrganizationUsersList>(authToken, "GET", queryString);
		}

		public Task<OrganizationUsersList> GetOrganizationUsersV2Async(string authToken, string boxId)
		{
			var queryString = $@"V2/GetOrganizationUsers?boxId={boxId}";
			return PerformHttpRequestAsync<OrganizationUsersList>(authToken, "GET", queryString);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<List<Organization>> GetOrganizationsByInnListAsync(GetOrganizationsByInnListRequest innList)
		{
			return GetOrganizationsByInnListAsync(null, innList);
		}

		public async Task<List<Organization>> GetOrganizationsByInnListAsync(string authToken, GetOrganizationsByInnListRequest innList)
		{
			const string queryString = "/GetOrganizationsByInnList";
			var response = await PerformHttpRequestAsync<GetOrganizationsByInnListRequest, GetOrganizationsByInnListResponse>(authToken, queryString, innList).ConfigureAwait(false);
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
