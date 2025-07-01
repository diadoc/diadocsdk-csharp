using System;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Employees.PowersOfAttorney;
using Diadoc.Api.Proto.PowersOfAttorney;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public EmployeePowerOfAttorneyList GetEmployeePowersOfAttorney(string authToken, string boxId, [CanBeNull] string userId, bool onlyActual)
		{
			var queryString = new PathAndQueryBuilder("/GetEmployeePowersOfAttorney");
			queryString.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(userId)) queryString.AddParameter("userId", userId);
			queryString.AddParameter("onlyActual", onlyActual.ToString());
			return PerformHttpRequest<EmployeePowerOfAttorneyList>(authToken, "GET", queryString.BuildPathAndQuery());
		}

		[Obsolete("Use UpdateEmployeePowerOfAttorneyV2()")]
		public EmployeePowerOfAttorney UpdateEmployeePowerOfAttorney(
			string authToken,
			string boxId,
			[CanBeNull] string userId,
			string registrationNumber,
			string issuerInn,
			EmployeePowerOfAttorneyToUpdate powerOfAttorneyToUpdate)
		{
			var queryString = new PathAndQueryBuilder("/UpdateEmployeePowerOfAttorney");
			queryString.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(userId)) queryString.AddParameter("userId", userId);
			queryString.AddParameter("registrationNumber", registrationNumber);
			queryString.AddParameter("issuerInn", issuerInn);
			return PerformHttpRequest<EmployeePowerOfAttorneyToUpdate, EmployeePowerOfAttorney>(authToken, queryString.BuildPathAndQuery(), powerOfAttorneyToUpdate);
		}
		
		public EmployeePowerOfAttorney UpdateEmployeePowerOfAttorneyV2(
			string authToken,
			string boxId,
			[CanBeNull] string userId,
			EmployeePowerOfAttorneyToUpdateV2 powerOfAttorneyToUpdate)
		{
			var queryString = new PathAndQueryBuilder("/V2/UpdateEmployeePowerOfAttorney");
			queryString.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(userId)) queryString.AddParameter("userId", userId);
			return PerformHttpRequest<EmployeePowerOfAttorneyToUpdateV2, EmployeePowerOfAttorney>(authToken, queryString.BuildPathAndQuery(), powerOfAttorneyToUpdate);
		}

		[Obsolete("Use AddEmployeePowerOfAttorneyV2()")]
		public EmployeePowerOfAttorney AddEmployeePowerOfAttorney(
			string authToken,
			string boxId,
			[CanBeNull] string userId,
			string registrationNumber,
			string issuerInn)
		{
			var queryString = new PathAndQueryBuilder("/AddEmployeePowerOfAttorney");
			queryString.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(userId)) queryString.AddParameter("userId", userId);
			queryString.AddParameter("registrationNumber", registrationNumber);
			queryString.AddParameter("issuerInn", issuerInn);
			return PerformHttpRequest<EmployeePowerOfAttorney>(authToken, "POST", queryString.BuildPathAndQuery());
		}
		
		public EmployeePowerOfAttorney AddEmployeePowerOfAttorneyV2(
			string authToken,
			string boxId,
			[CanBeNull] string userId,
			PowerOfAttorneyFullId fullId)
		{
			var queryString = new PathAndQueryBuilder("/V2/AddEmployeePowerOfAttorney");
			queryString.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(userId)) queryString.AddParameter("userId", userId);
			return PerformHttpRequest<PowerOfAttorneyFullId, EmployeePowerOfAttorney>(authToken, queryString.BuildPathAndQuery(), fullId);
		}

		[Obsolete("Use DeleteEmployeePowerOfAttorneyV2()")]
		public void DeleteEmployeePowerOfAttorney(
			string authToken,
			string boxId,
			[CanBeNull] string userId,
			string registrationNumber,
			string issuerInn)
		{
			var queryString = new PathAndQueryBuilder("/DeleteEmployeePowerOfAttorney");
			queryString.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(userId)) queryString.AddParameter("userId", userId);
			queryString.AddParameter("registrationNumber", registrationNumber);
			queryString.AddParameter("issuerInn", issuerInn);
			PerformHttpRequest(authToken, "POST", queryString.BuildPathAndQuery());
		}
		
		public void DeleteEmployeePowerOfAttorneyV2(
			string authToken,
			string boxId,
			[CanBeNull] string userId,
			PowerOfAttorneyFullId fullId)
		{
			var queryString = new PathAndQueryBuilder("/V2/DeleteEmployeePowerOfAttorney");
			queryString.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(userId)) queryString.AddParameter("userId", userId);
			PerformHttpRequest(authToken, "POST", queryString.BuildPathAndQuery(), Serialize(fullId));
		}
	}
}
