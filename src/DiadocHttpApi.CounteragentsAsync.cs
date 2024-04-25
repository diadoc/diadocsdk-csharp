using System;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<Counteragent> GetCounteragentAsync(string authToken, string myOrgId, string counteragentOrgId)
		{
			var qsb = new PathAndQueryBuilder("V2/GetCounteragent");
			qsb.AddParameter("myOrgId", myOrgId);
			qsb.AddParameter("counteragentOrgId", counteragentOrgId);
			return PerformHttpRequestAsync<Counteragent>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public Task<CounteragentList> GetCounteragentsAsync(string authToken, string myOrgId, string counteragentStatus, string afterIndexKey, string query = null, int? pageSize = null)
		{
			var qsb = new PathAndQueryBuilder("V2/GetCounteragents");
			qsb.AddParameter("myOrgId", myOrgId);
			if (!string.IsNullOrEmpty(counteragentStatus)) qsb.AddParameter("counteragentStatus", counteragentStatus);
			if (!string.IsNullOrEmpty(afterIndexKey)) qsb.AddParameter("afterIndexKey", afterIndexKey);
			if (!string.IsNullOrEmpty(query)) qsb.AddParameter("query", query);
			if (pageSize != null) qsb.AddParameter("pageSize", pageSize.ToString());
			return PerformHttpRequestAsync<CounteragentList>(authToken, "GET", qsb.BuildPathAndQuery());
		}


		public Task<CounteragentCertificateList> GetCounteragentCertificatesAsync(
			string authToken,
			string myOrgId,
			string counteragentOrgId)
		{
			var qsb = new PathAndQueryBuilder("/GetCounteragentCertificates");
			qsb.AddParameter("myOrgId", myOrgId);
			qsb.AddParameter("counteragentOrgId", counteragentOrgId);
			return PerformHttpRequestAsync<CounteragentCertificateList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public Task BreakWithCounteragentAsync(string authToken, string myOrgId, string counteragentOrgId, string comment)
		{
			var qsb = CounteragentOperationPathAndQuery("/BreakWithCounteragent", myOrgId, counteragentOrgId, comment);
			return PerformHttpRequestAsync(authToken, "POST", qsb.BuildPathAndQuery());
		}

		public Task<AsyncMethodResult> AcquireCounteragentAsync(string authToken, string myOrgId, AcquireCounteragentRequest request, string myDepartmentId = null)
		{
			var queryString = new PathAndQueryBuilder("/V2/AcquireCounteragent");
			queryString.AddParameter("myOrgId", myOrgId);
			if (!string.IsNullOrEmpty(myDepartmentId))
				queryString.AddParameter("myDepartmentId", myDepartmentId);

			return PerformHttpRequestAsync<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery(), Serialize(request));
		}

		public Task<AcquireCounteragentResult> WaitAcquireCounteragentResultAsync(string authToken, string taskId, TimeSpan? timeout = null, TimeSpan? delay = null)
		{
			return WaitTaskResultAsync<AcquireCounteragentResult>(authToken, "/AcquireCounteragentResult", taskId, timeout, delay);
		}

		public Task<BoxCounteragentEventList> GetCounteragentEventsAsync(
			string authToken,
			string boxId,
			string afterIndexKey = null,
			long? timestampFromTicks = null,
			long? timestampToTicks = null,
			int? limit = null)
		{
			var qsb = new PathAndQueryBuilder("/V1/GetCounteragentEvents");
			qsb.AddParameter("boxId", boxId);
			if (afterIndexKey != null)
			{
				qsb.AddParameter("afterIndexKey", afterIndexKey);
			}
			if (timestampFromTicks != null)
			{
				qsb.AddParameter("timestampFromTicks", timestampFromTicks.ToString());
			}
			if (timestampToTicks != null)
			{
				qsb.AddParameter("timestampToTicks", timestampToTicks.ToString());
			}
			if (limit != null)
			{
				qsb.AddParameter("limit", limit.ToString());
			}
			return PerformHttpRequestAsync<BoxCounteragentEventList>(authToken, "GET", qsb.BuildPathAndQuery());
		}
	}
}
