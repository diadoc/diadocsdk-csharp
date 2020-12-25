using System;
using System.Threading;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<Counteragent> GetCounteragentAsync(string authToken, string myOrgId, string counteragentOrgId, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("V2/GetCounteragent");
			qsb.AddParameter("myOrgId", myOrgId);
			qsb.AddParameter("counteragentOrgId", counteragentOrgId);
			return PerformHttpRequestAsync<Counteragent>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<CounteragentList> GetCounteragentsAsync(string authToken, string myOrgId, string counteragentStatus, string afterIndexKey, string query = null, int? pageSize = null, CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("V2/GetCounteragents");
			qsb.AddParameter("myOrgId", myOrgId);
			if (!string.IsNullOrEmpty(counteragentStatus)) qsb.AddParameter("counteragentStatus", counteragentStatus);
			if (!string.IsNullOrEmpty(afterIndexKey)) qsb.AddParameter("afterIndexKey", afterIndexKey);
			if (!string.IsNullOrEmpty(query)) qsb.AddParameter("query", query);
			if (pageSize != null) qsb.AddParameter("pageSize", pageSize.ToString());
			return PerformHttpRequestAsync<CounteragentList>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}


		public Task<CounteragentCertificateList> GetCounteragentCertificatesAsync(
			string authToken,
			string myOrgId,
			string counteragentOrgId, 
			CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/GetCounteragentCertificates");
			qsb.AddParameter("myOrgId", myOrgId);
			qsb.AddParameter("counteragentOrgId", counteragentOrgId);
			return PerformHttpRequestAsync<CounteragentCertificateList>(authToken, "GET", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task BreakWithCounteragentAsync(string authToken, string myOrgId, string counteragentOrgId, string comment, CancellationToken ct = default)
		{
			var qsb = CounteragentOperationPathAndQuery("/BreakWithCounteragent", myOrgId, counteragentOrgId, comment);
			return PerformHttpRequestAsync(authToken, "POST", qsb.BuildPathAndQuery(), ct: ct);
		}

		public Task<AsyncMethodResult> AcquireCounteragentAsync(string authToken, string myOrgId, AcquireCounteragentRequest request, string myDepartmentId = null, CancellationToken ct = default)
		{
			var queryString = new PathAndQueryBuilder("/V2/AcquireCounteragent");
			queryString.AddParameter("myOrgId", myOrgId);
			if (!string.IsNullOrEmpty(myDepartmentId))
				queryString.AddParameter("myDepartmentId", myDepartmentId);

			return PerformHttpRequestAsync<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery(), Serialize(request), ct: ct);
		}

		public Task<AcquireCounteragentResult> WaitAcquireCounteragentResultAsync(string authToken, string taskId, TimeSpan? timeout = null, TimeSpan? delay = null, CancellationToken ct = default)
		{
			return WaitTaskResultAsync<AcquireCounteragentResult>(authToken, "/AcquireCounteragentResult", taskId, timeout, delay, ct: ct);
		}
	}
}
