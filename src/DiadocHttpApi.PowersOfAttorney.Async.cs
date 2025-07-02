﻿using System;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.PowersOfAttorney;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<AsyncMethodResult> RegisterPowerOfAttorneyAsync(
			string authToken,
			string boxId,
			PowerOfAttorneyToRegister powerOfAttorneyToRegister)
		{
			var queryString = new PathAndQueryBuilder("/RegisterPowerOfAttorney");
			queryString.AddParameter("boxId", boxId);
			return PerformHttpRequestAsync<PowerOfAttorneyToRegister, AsyncMethodResult>(authToken, queryString.BuildPathAndQuery(), powerOfAttorneyToRegister);
		}

		public Task<PowerOfAttorneyRegisterResult> RegisterPowerOfAttorneyResultAsync(
			string authToken,
			string boxId,
			string taskId)
		{
			var queryString = new PathAndQueryBuilder("/RegisterPowerOfAttorneyResult");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("taskId", taskId);
			return PerformHttpRequestAsync<PowerOfAttorneyRegisterResult>(authToken, "GET", queryString.BuildPathAndQuery());
		}

		[Obsolete("Use PrevalidatePowerOfAttorneyV2Async()")]
		public Task<PowerOfAttorneyPrevalidateResult> PrevalidatePowerOfAttorneyAsync(
			string authToken,
			string boxId,
			string registrationNumber,
			string issuerInn,
			PowerOfAttorneyPrevalidateRequest request)
		{
			var queryString = new PathAndQueryBuilder("/PrevalidatePowerOfAttorney");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("registrationNumber", registrationNumber);
			queryString.AddParameter("issuerInn", issuerInn);
			return PerformHttpRequestAsync<PowerOfAttorneyPrevalidateRequest, PowerOfAttorneyPrevalidateResult>(authToken, queryString.BuildPathAndQuery(), request);
		}
		
		public Task<PowerOfAttorneyPrevalidateResult> PrevalidatePowerOfAttorneyV2Async(
			string authToken,
			string boxId,
			PowerOfAttorneyPrevalidateRequestV2 request)
		{
			var queryString = new PathAndQueryBuilder("/V2/PrevalidatePowerOfAttorney");
			queryString.AddParameter("boxId", boxId);
			return PerformHttpRequestAsync<PowerOfAttorneyPrevalidateRequestV2, PowerOfAttorneyPrevalidateResult>(authToken, queryString.BuildPathAndQuery(), request);
		}

		public Task<PowerOfAttorney> GetPowerOfAttorneyInfoAsync(
			string authToken,
			string boxId,
			string messageId,
			string entityId)
		{
			var queryString = new PathAndQueryBuilder("/GetPowerOfAttorneyInfo");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("messageId", messageId);
			queryString.AddParameter("entityId", entityId);
			return PerformHttpRequestAsync<PowerOfAttorney>(authToken, "GET", queryString.BuildPathAndQuery());
		}

		public Task<PowerOfAttorneyContent> GetPowerOfAttorneyContentAsync(
			string authToken,
			string boxId,
			string messageId,
			string entityId)
		{
			var queryString = new PathAndQueryBuilder("/GetPowerOfAttorneyContent");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("messageId", messageId);
			queryString.AddParameter("entityId", entityId);
			return PerformHttpRequestAsync<PowerOfAttorneyContent>(authToken, "GET", queryString.BuildPathAndQuery());
		}
		
		public Task<PowerOfAttorneyContentResponse> GetPowerOfAttorneyContentV2Async(
			string authToken,
			string boxId,
			string messageId,
			string entityId)
		{
			var queryString = new PathAndQueryBuilder("V2/GetPowerOfAttorneyContent");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("messageId", messageId);
			queryString.AddParameter("entityId", entityId);
			return PerformHttpRequestAsync<PowerOfAttorneyContentResponse>(authToken, "GET", queryString.BuildPathAndQuery());
		}
	}
}
