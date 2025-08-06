using System;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Employees.PowersOfAttorney;
using Diadoc.Api.Proto.PowersOfAttorney;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<EmployeePowerOfAttorneyList> GetEmployeePowersOfAttorneyAsync(string authToken, string boxId, [CanBeNull] string userId, bool onlyActual)
		{
			var queryString = new PathAndQueryBuilder("/GetEmployeePowersOfAttorney");
			queryString.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(userId)) queryString.AddParameter("userId", userId);
			queryString.AddParameter("onlyActual", onlyActual.ToString());
			return PerformHttpRequestAsync<EmployeePowerOfAttorneyList>(authToken, "GET", queryString.BuildPathAndQuery());
		}

		[Obsolete("Use UpdateEmployeePowerOfAttorneyV2Async()")]
		public Task<EmployeePowerOfAttorney> UpdateEmployeePowerOfAttorneyAsync(
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
			return PerformHttpRequestAsync<EmployeePowerOfAttorneyToUpdate, EmployeePowerOfAttorney>(authToken, queryString.BuildPathAndQuery(), powerOfAttorneyToUpdate);
		}
		
		public Task<EmployeePowerOfAttorney> UpdateEmployeePowerOfAttorneyV2Async(
			string authToken,
			string boxId,
			[CanBeNull] string userId,
			EmployeePowerOfAttorneyToUpdateV2 powerOfAttorneyToUpdate)
		{
			var queryString = new PathAndQueryBuilder("/V2/UpdateEmployeePowerOfAttorney");
			queryString.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(userId)) queryString.AddParameter("userId", userId);
			return PerformHttpRequestAsync<EmployeePowerOfAttorneyToUpdateV2, EmployeePowerOfAttorney>(authToken, queryString.BuildPathAndQuery(), powerOfAttorneyToUpdate);
		}

		[Obsolete("Use AddEmployeePowerOfAttorneyV2Async()")]
		public Task<EmployeePowerOfAttorney> AddEmployeePowerOfAttorneyAsync(
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
			return PerformHttpRequestAsync<EmployeePowerOfAttorney>(authToken, "POST", queryString.BuildPathAndQuery());
		}
		
		public Task<EmployeePowerOfAttorney> AddEmployeePowerOfAttorneyV2Async(
			string authToken,
			string boxId,
			[CanBeNull] string userId,
			PowerOfAttorneyFullId fullId)
		{
			var queryString = new PathAndQueryBuilder("/V2/AddEmployeePowerOfAttorney");
			queryString.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(userId)) queryString.AddParameter("userId", userId);
			return PerformHttpRequestAsync<PowerOfAttorneyFullId, EmployeePowerOfAttorney>(authToken, queryString.BuildPathAndQuery(), fullId);
		}

		[Obsolete("Use DeleteEmployeePowerOfAttorneyV2Async")]
		public Task DeleteEmployeePowerOfAttorneyAsync(
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
			return PerformHttpRequestAsync(authToken, "POST", queryString.BuildPathAndQuery());
		}
		
		public Task DeleteEmployeePowerOfAttorneyV2Async(
			string authToken,
			string boxId,
			[CanBeNull] string userId,
			PowerOfAttorneyFullId fullId)
		{
			var queryString = new PathAndQueryBuilder("/V2/DeleteEmployeePowerOfAttorney");
			queryString.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(userId)) queryString.AddParameter("userId", userId);
			return PerformHttpRequestAsync(authToken, "POST", queryString.BuildPathAndQuery(), Serialize(fullId));
		}
	}
}
