using System.Collections.Generic;
using System.Linq;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Users;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public OrganizationUserPermissions GetMyPermissions(string authToken, string orgId)
		{
			var qsb = new PathAndQueryBuilder("/GetMyPermissions");
			qsb.AddParameter("orgId", orgId);
			return PerformHttpRequest<OrganizationUserPermissions>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public OrganizationList GetMyOrganizations(string authToken, bool autoRegister = true)
		{
			var queryBuilder = new PathAndQueryBuilder("/GetMyOrganizations");
			if (!autoRegister) queryBuilder.AddParameter("autoRegister", "false");
			return PerformHttpRequest<OrganizationList>(authToken, "GET", queryBuilder.BuildPathAndQuery());
		}

		public User GetMyUser(string authToken)
		{
			return PerformHttpRequest<User>(authToken, "GET", "/GetMyUser");
		}

		public UserV2 GetMyUserV2(string authToken)
		{
			return PerformHttpRequest<UserV2>(authToken, "GET", "/V2/GetMyUser");
		}

		public UserV2 UpdateMyUser(string authToken, UserToUpdate userToUpdate)
		{
			return PerformHttpRequest<UserToUpdate, UserV2>(authToken, "/UpdateMyUser", userToUpdate);
		}

		public OrganizationList GetOrganizationsByInnKpp(string inn, string kpp, bool includeRelations = false)
		{
			var qsb = new PathAndQueryBuilder("/GetOrganizationsByInnKpp");
			qsb.AddParameter("inn", inn);
			if (!string.IsNullOrEmpty(kpp)) qsb.AddParameter("kpp", kpp);
			if (includeRelations)
				qsb.AddParameter("includeRelations", "true");
			return PerformHttpRequest<OrganizationList>(null, "GET", qsb.BuildPathAndQuery());
		}

		public Organization GetOrganizationById(string orgId)
		{
			return GetOrganization(string.Format("/GetOrganization?orgId={0}", orgId));
		}

		public Organization GetOrganizationByBoxId(string boxId)
		{
			return GetOrganization(string.Format("/GetOrganization?boxId={0}", boxId));
		}

		public Organization GetOrganizationByFnsParticipantId(string fnsParticipantId)
		{
			return GetOrganization(string.Format("/GetOrganization?fnsParticipantId={0}", fnsParticipantId));
		}

		public Organization GetOrganizationByInnKpp(string inn, string kpp)
		{
			var qsb = new PathAndQueryBuilder("/GetOrganization");
			qsb.AddParameter("inn", inn);
			if (!string.IsNullOrEmpty(kpp))
				qsb.AddParameter("kpp", kpp);
			var queryString = qsb.BuildPathAndQuery();
			return GetOrganization(queryString);
		}

		private Organization GetOrganization(string queryString)
		{
			return PerformHttpRequest<Organization>(null, "GET", queryString);
		}

		public Box GetBox(string boxId)
		{
			var queryString = string.Format("/GetBox?boxId={0}", boxId);
			return PerformHttpRequest<Box>(null, "GET", queryString);
		}

		public Department GetDepartment(string authToken, string orgId, string departmentId)
		{
			var queryBuilder = new PathAndQueryBuilder("/GetDepartment");
			queryBuilder.AddParameter("orgId", orgId);
			queryBuilder.AddParameter("departmentId", departmentId);
			return PerformHttpRequest<Department>(authToken, "GET", queryBuilder.BuildPathAndQuery());
		}

		public void UpdateOrganizationProperties(string authToken, OrganizationPropertiesToUpdate orgProps)
		{
			PerformHttpRequest(authToken, "POST", "/UpdateOrganizationProperties", Serialize(orgProps));
		}

		public OrganizationUsersList GetOrganizationUsers(string authToken, string orgId)
		{
			var queryString = string.Format("/GetOrganizationUsers?orgId={0}", orgId);
			return PerformHttpRequest<OrganizationUsersList>(authToken, "GET", queryString);
		}

		public List<Organization> GetOrganizationsByInnList(GetOrganizationsByInnListRequest innList)
		{
			const string queryString = "/GetOrganizationsByInnList";
			var response = PerformHttpRequest<GetOrganizationsByInnListRequest, GetOrganizationsByInnListResponse>(null, queryString, innList);
			return response.Organizations.Select(o => o.Organization).ToList();
		}

		public List<OrganizationWithCounteragentStatus> GetOrganizationsByInnList(string authToken, string myOrgId, GetOrganizationsByInnListRequest innList)
		{
			var queryString = string.Format("/GetOrganizationsByInnList?myOrgId={0}", myOrgId);
			var response = PerformHttpRequest<GetOrganizationsByInnListRequest, GetOrganizationsByInnListResponse>(authToken, queryString, innList);
			return response.Organizations;
		}
	}
}
