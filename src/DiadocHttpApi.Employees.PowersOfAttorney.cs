using Diadoc.Api.Http;
using Diadoc.Api.Proto.Employees.PowersOfAttorney;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public EmployeePowerOfAttorneyList GetEmployeePowersOfAttorney(string authToken, string boxId, string userId, bool onlyActual)
		{
			var queryString = new PathAndQueryBuilder("/GetEmployeePowersOfAttorney");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("userId", userId);
			queryString.AddParameter("onlyActual", onlyActual.ToString());
			return PerformHttpRequest<EmployeePowerOfAttorneyList>(authToken, "GET", queryString.BuildPathAndQuery());
		}

		public EmployeePowerOfAttorney UpdateEmployeePowerOfAttorney(
			string authToken,
			string boxId,
			string userId,
			string registrationNumber,
			string issuerInn,
			EmployeePowerOfAttorneyToUpdate powerOfAttorneyToUpdate)
		{
			var queryString = new PathAndQueryBuilder("/UpdateEmployeePowerOfAttorney");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("userId", userId);
			queryString.AddParameter("registrationNumber", registrationNumber);
			queryString.AddParameter("issuerInn", issuerInn);
			return PerformHttpRequest<EmployeePowerOfAttorneyToUpdate, EmployeePowerOfAttorney>(authToken, queryString.BuildPathAndQuery(), powerOfAttorneyToUpdate);
		}

		public EmployeePowerOfAttorney AddEmployeePowerOfAttorney(
			string authToken,
			string boxId,
			string userId,
			string registrationNumber,
			string issuerInn)
		{
			var queryString = new PathAndQueryBuilder("/AddEmployeePowerOfAttorney");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("userId", userId);
			queryString.AddParameter("registrationNumber", registrationNumber);
			queryString.AddParameter("issuerInn", issuerInn);
			return PerformHttpRequest<EmployeePowerOfAttorney>(authToken, "POST", queryString.BuildPathAndQuery());
		}
	}
}
