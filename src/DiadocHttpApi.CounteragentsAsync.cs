using System;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[Obsolete("Use GetCounteragentV3Async()")]
		public Task<Counteragent> GetCounteragentAsync(string authToken, string myOrgId, string counteragentOrgId)
		{
			var qsb = new PathAndQueryBuilder("V2/GetCounteragent");
			qsb.AddParameter("myOrgId", myOrgId);
			qsb.AddParameter("counteragentOrgId", counteragentOrgId);
			return PerformHttpRequestAsync<Counteragent>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public Task<Counteragent> GetCounteragentV3Async(string authToken, string myBoxId, string counteragentBoxId)
		{
			var qsb = new PathAndQueryBuilder("V3/GetCounteragent");
			qsb.AddParameter("myBoxId", myBoxId);
			qsb.AddParameter("counteragentBoxId", counteragentBoxId);
			return PerformHttpRequestAsync<Counteragent>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[Obsolete("Use GetCounteragentsV3Async()")]
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

		public Task<CounteragentList> GetCounteragentsV3Async(string authToken, string myBoxId, string counteragentStatus, string afterIndexKey, string query = null, int? pageSize = null)
		{
			var qsb = new PathAndQueryBuilder("V3/GetCounteragents");
			qsb.AddParameter("myBoxId", myBoxId);
			if (!string.IsNullOrEmpty(counteragentStatus)) qsb.AddParameter("counteragentStatus", counteragentStatus);
			if (!string.IsNullOrEmpty(afterIndexKey)) qsb.AddParameter("afterIndexKey", afterIndexKey);
			if (!string.IsNullOrEmpty(query)) qsb.AddParameter("query", query);
			if (pageSize != null) qsb.AddParameter("pageSize", pageSize.ToString());
			return PerformHttpRequestAsync<CounteragentList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[Obsolete("Use GetCounteragentCertificatesV2Async()")]
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

		public Task<CounteragentCertificateList> GetCounteragentCertificatesV2Async(
			string authToken, 
			string myBoxId,
			string counteragentBoxId)
		{
			var qsb = new PathAndQueryBuilder("/V2/GetCounteragentCertificates");
			qsb.AddParameter("myBoxId", myBoxId);
			qsb.AddParameter("counteragentBoxId", counteragentBoxId);
			return PerformHttpRequestAsync<CounteragentCertificateList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[Obsolete("Use BreakWithCounteragentV2Async()")]
		public Task BreakWithCounteragentAsync(string authToken, string myOrgId, string counteragentOrgId, string comment)
		{
			var qsb = CounteragentOperationPathAndQuery("/BreakWithCounteragent", myOrgId, counteragentOrgId, comment);
			return PerformHttpRequestAsync(authToken, "POST", qsb.BuildPathAndQuery());
		}

		public Task BreakWithCounteragentV2Async(string authToken, string myBoxId, string counteragentBoxId, string comment)
		{
			var qsb = new PathAndQueryBuilder("/V2/BreakWithCounteragent");
			qsb.AddParameter("myBoxId", myBoxId);
			qsb.AddParameter("counteragentBoxId", counteragentBoxId);
			if (!string.IsNullOrEmpty(comment)) qsb.AddParameter("comment", comment);
			return PerformHttpRequestAsync(authToken, "POST", qsb.BuildPathAndQuery());
		}

		[Obsolete("Use AcquireCounteragentV3Async()")]
		public Task<AsyncMethodResult> AcquireCounteragentAsync(string authToken, string myOrgId, AcquireCounteragentRequest request, string myDepartmentId = null)
		{
			var queryString = new PathAndQueryBuilder("/V2/AcquireCounteragent");
			queryString.AddParameter("myOrgId", myOrgId);
			if (!string.IsNullOrEmpty(myDepartmentId))
				queryString.AddParameter("myDepartmentId", myDepartmentId);

			return PerformHttpRequestAsync<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery(), Serialize(request));
		}

		public Task<AsyncMethodResult> AcquireCounteragentV3Async(string authToken, string myBoxId, AcquireCounteragentRequest request, string myDepartmentId = null)
		{
			var queryString = new PathAndQueryBuilder("/V3/AcquireCounteragent");
			queryString.AddParameter("myBoxId", myBoxId);
			if (!string.IsNullOrEmpty(myDepartmentId))
				queryString.AddParameter("myDepartmentId", myDepartmentId);

			return PerformHttpRequestAsync<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery(), Serialize(request));
		}

		[Obsolete("Use WaitAcquireCounteragentResultV2Async()")]
		public Task<AcquireCounteragentResult> WaitAcquireCounteragentResultAsync(string authToken, string taskId, TimeSpan? timeout = null, TimeSpan? delay = null)
		{
			return WaitTaskResultAsync<AcquireCounteragentResult>(authToken, "/AcquireCounteragentResult", taskId, timeout, delay);
		}

		public Task<AcquireCounteragentResultV2> WaitAcquireCounteragentResultV2Async(string authToken, string taskId, TimeSpan? timeout = null, TimeSpan? delay = null)
		{
			return WaitTaskResultAsync<AcquireCounteragentResultV2>(authToken, "/V2/AcquireCounteragentResult", taskId, timeout, delay);
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
