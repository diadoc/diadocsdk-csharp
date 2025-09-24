using System;
using System.Collections.Generic;
using System.Linq;
using Diadoc.Api.Constants;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Certificates;

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

		public CertificateList GetMyCertificates(string authToken, string boxId)
		{
			var queryBuilder = new PathAndQueryBuilder("/GetMyCertificates");
			queryBuilder.AddParameter("boxId", boxId);

			return PerformHttpRequest<CertificateList>(authToken, "GET", queryBuilder.BuildPathAndQuery());
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public OrganizationList GetOrganizationsByInnKpp(string inn, string kpp, bool includeRelations = false)
		{
			return GetOrganizationsByInnKpp(null, inn, kpp, includeRelations);
		}

		public OrganizationList GetOrganizationsByInnKpp(string authToken, string inn, string kpp, bool includeRelations = false)
		{
			var qsb = new PathAndQueryBuilder("/GetOrganizationsByInnKpp");
			qsb.AddParameter("inn", inn);
			if (!string.IsNullOrEmpty(kpp)) qsb.AddParameter("kpp", kpp);
			if (includeRelations)
				qsb.AddParameter("includeRelations", "true");
			return PerformHttpRequest<OrganizationList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Organization GetOrganizationById(string orgId)
		{
			return GetOrganizationById(null, orgId);
		}

		public Organization GetOrganizationById(string authToken, string orgId)
		{
			return GetOrganization(authToken, string.Format("/GetOrganization?orgId={0}", orgId));
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Organization GetOrganizationByBoxId(string boxId)
		{
			return GetOrganizationByBoxId(null, boxId);
		}

		public Organization GetOrganizationByBoxId(string authToken, string boxId)
		{
			return GetOrganization(authToken, string.Format("/GetOrganization?boxId={0}", boxId));
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Organization GetOrganizationByFnsParticipantId(string fnsParticipantId)
		{
			return GetOrganizationByFnsParticipantId(null, fnsParticipantId);
		}

		public Organization GetOrganizationByFnsParticipantId(string authToken, string fnsParticipantId)
		{
			return GetOrganization(authToken, string.Format("/GetOrganization?fnsParticipantId={0}", fnsParticipantId));
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Organization GetOrganizationByInnKpp(string inn, string kpp)
		{
			return GetOrganizationByInnKpp(null, inn, kpp);
		}

		public Organization GetOrganizationByInnKpp(string authToken, string inn, string kpp)
		{
			var qsb = new PathAndQueryBuilder("/GetOrganization");
			qsb.AddParameter("inn", inn);
			if (!string.IsNullOrEmpty(kpp))
				qsb.AddParameter("kpp", kpp);
			var queryString = qsb.BuildPathAndQuery();
			return GetOrganization(authToken, queryString);
		}

		private Organization GetOrganization(string authToken, string queryString)
		{
			return PerformHttpRequest<Organization>(authToken, "GET", queryString);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Box GetBox(string boxId)
		{
			return GetBox(null, boxId);
		}

		public Box GetBox(string authToken, string boxId)
		{
			var queryString = string.Format("/GetBox?boxId={0}", boxId);
			return PerformHttpRequest<Box>(authToken, "GET", queryString);
		}

		[Obsolete("Use a similar method with boxId: GetDepartmentV2()")]
		public Department GetDepartment(string authToken, string orgId, string departmentId)
		{
			var queryBuilder = new PathAndQueryBuilder("/GetDepartment");
			queryBuilder.AddParameter("orgId", orgId);
			queryBuilder.AddParameter("departmentId", departmentId);
			return PerformHttpRequest<Department>(authToken, "GET", queryBuilder.BuildPathAndQuery());
		}

		public Department GetDepartmentV2(string authToken, string boxId, string departmentId)
		{
			var queryBuilder = new PathAndQueryBuilder("V2/GetDepartment");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("departmentId", departmentId);
			return PerformHttpRequest<Department>(authToken, "GET", queryBuilder.BuildPathAndQuery());
		}

		[Obsolete("Method UpdateOrganizationProperties will be removed soon")]
		public void UpdateOrganizationProperties(string authToken, OrganizationPropertiesToUpdate orgProps)
		{
			PerformHttpRequest(authToken, "POST", "/UpdateOrganizationProperties", Serialize(orgProps));
		}

		[Obsolete("Use GetOrganizationUsersV2()")]
		public OrganizationUsersList GetOrganizationUsers(string authToken, string orgId)
		{
			var queryString = string.Format("/GetOrganizationUsers?orgId={0}", orgId);
			return PerformHttpRequest<OrganizationUsersList>(authToken, "GET", queryString);
		}

		public OrganizationUsersList GetOrganizationUsersV2(string authToken, string boxId)
		{
			var queryString = string.Format("V2/GetOrganizationUsers?boxId={0}", boxId);
			return PerformHttpRequest<OrganizationUsersList>(authToken, "GET", queryString);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public List<Organization> GetOrganizationsByInnList(GetOrganizationsByInnListRequest innList)
		{
			return GetOrganizationsByInnList(null, innList);
		}

		[Obsolete("Use a similar method: GetOrganizationsByInnListV2()")]
		public List<Organization> GetOrganizationsByInnList(string authToken, GetOrganizationsByInnListRequest innList)
		{
			const string queryString = "/GetOrganizationsByInnList";
			var response = PerformHttpRequest<GetOrganizationsByInnListRequest, GetOrganizationsByInnListResponse>(authToken, queryString, innList);
			return response.Organizations.Select(o => o.Organization).ToList();
		}

		[Obsolete("Use a similar method with boxId: GetOrganizationsByInnListV2()")]
		public List<OrganizationWithCounteragentStatus> GetOrganizationsByInnList(string authToken, string myOrgId, GetOrganizationsByInnListRequest innList)
		{
			var queryString = string.Format("/GetOrganizationsByInnList?myOrgId={0}", myOrgId);
			var response = PerformHttpRequest<GetOrganizationsByInnListRequest, GetOrganizationsByInnListResponse>(authToken, queryString, innList);
			return response.Organizations;
		}

		public List<Organization> GetOrganizationsByInnListV2(string authToken, GetOrganizationsByInnListRequest innList)
		{
			const string queryString = "/V2/GetOrganizationsByInnList";
			var response = PerformHttpRequest<GetOrganizationsByInnListRequest, GetOrganizationsByInnListResponse>(authToken, queryString, innList);
			return response.Organizations.Select(o => o.Organization).ToList();
		}

		public List<OrganizationWithCounteragentStatus> GetOrganizationsByInnListV2(string authToken, string myBoxId, GetOrganizationsByInnListRequest innList)
		{
			var queryString = string.Format("/V2/GetOrganizationsByInnList?myBoxId={0}", myBoxId);
			var response = PerformHttpRequest<GetOrganizationsByInnListRequest, GetOrganizationsByInnListResponse>(authToken, queryString, innList);
			return response.Organizations;
		}
	}
}
