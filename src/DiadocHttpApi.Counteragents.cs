using System;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Events;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[Obsolete("Use GetCounteragentV3()")]
		public Counteragent GetCounteragent(string authToken, string myOrgId, string counteragentOrgId)
		{
			var qsb = new PathAndQueryBuilder("V2/GetCounteragent");
			qsb.AddParameter("myOrgId", myOrgId);
			qsb.AddParameter("counteragentOrgId", counteragentOrgId);
			return PerformHttpRequest<Counteragent>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		public Counteragent GetCounteragentV3(string authToken, string myBoxId, string counteragentBoxId)
		{
			var qsb = new PathAndQueryBuilder("V3/GetCounteragent");
			qsb.AddParameter("myBoxId", myBoxId);
			qsb.AddParameter("counteragentBoxId", counteragentBoxId);
			return PerformHttpRequest<Counteragent>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[Obsolete("Use GetCounteragentsV3()")]
		public CounteragentList GetCounteragents(string authToken, string myOrgId, string counteragentStatus, string afterIndexKey, string query = null, int? pageSize = null)
		{
			var qsb = new PathAndQueryBuilder("V2/GetCounteragents");
			qsb.AddParameter("myOrgId", myOrgId);
			if (!string.IsNullOrEmpty(counteragentStatus)) qsb.AddParameter("counteragentStatus", counteragentStatus);
			if (!string.IsNullOrEmpty(afterIndexKey)) qsb.AddParameter("afterIndexKey", afterIndexKey);
			if (!string.IsNullOrEmpty(query)) qsb.AddParameter("query", query);
			if (pageSize != null) qsb.AddParameter("pageSize", pageSize.ToString());
			return PerformHttpRequest<CounteragentList>(authToken, "GET", qsb.BuildPathAndQuery());
		}
		
		public CounteragentList GetCounteragentsV3(string authToken, string myBoxId, string counteragentStatus, string afterIndexKey, string query = null, int? pageSize = null)
		{
			var qsb = new PathAndQueryBuilder("V3/GetCounteragents");
			qsb.AddParameter("myBoxId", myBoxId);
			if (!string.IsNullOrEmpty(counteragentStatus)) qsb.AddParameter("counteragentStatus", counteragentStatus);
			if (!string.IsNullOrEmpty(afterIndexKey)) qsb.AddParameter("afterIndexKey", afterIndexKey);
			if (!string.IsNullOrEmpty(query)) qsb.AddParameter("query", query);
			if (pageSize != null) qsb.AddParameter("pageSize", pageSize.ToString());
			return PerformHttpRequest<CounteragentList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[Obsolete("Use GetCounteragentCertificatesV2()")]
		public CounteragentCertificateList GetCounteragentCertificates(string authToken, string myOrgId,
			string counteragentOrgId)
		{
			var qsb = new PathAndQueryBuilder("/GetCounteragentCertificates");
			qsb.AddParameter("myOrgId", myOrgId);
			qsb.AddParameter("counteragentOrgId", counteragentOrgId);
			return PerformHttpRequest<CounteragentCertificateList>(authToken, "GET", qsb.BuildPathAndQuery());
		}
		
		public CounteragentCertificateList GetCounteragentCertificatesV2(
			string authToken, 
			string myBoxId,
			string counteragentBoxId)
		{
			var qsb = new PathAndQueryBuilder("/V2/GetCounteragentCertificates");
			qsb.AddParameter("myBoxId", myBoxId);
			qsb.AddParameter("counteragentBoxId", counteragentBoxId);
			return PerformHttpRequest<CounteragentCertificateList>(authToken, "GET", qsb.BuildPathAndQuery());
		}

		[Obsolete("Use BreakWithCounteragentV2()")]
		public void BreakWithCounteragent(string authToken, string myOrgId, string counteragentOrgId, string comment)
		{
			var qsb = CounteragentOperationPathAndQuery("/BreakWithCounteragent", myOrgId, counteragentOrgId, comment);
			PerformHttpRequest(authToken, "POST", qsb.BuildPathAndQuery());
		}
		
		public void BreakWithCounteragentV2(string authToken, string myBoxId, string counteragentBoxId, string comment)
		{
			var qsb = new PathAndQueryBuilder("/V2/BreakWithCounteragent");
			qsb.AddParameter("myBoxId", myBoxId);
			qsb.AddParameter("counteragentBoxId", counteragentBoxId);
			if (!string.IsNullOrEmpty(comment)) qsb.AddParameter("comment", comment);
			PerformHttpRequest(authToken, "POST", qsb.BuildPathAndQuery());
		}

		[NotNull]
		private static PathAndQueryBuilder CounteragentOperationPathAndQuery([NotNull] string operation, [NotNull] string myOrgId, [NotNull] string counteragentOrgId, [CanBeNull] string comment)
		{
			var qsb = new PathAndQueryBuilder(operation);
			qsb.AddParameter("myOrgId", myOrgId);
			qsb.AddParameter("counteragentOrgId", counteragentOrgId);
			if (!string.IsNullOrEmpty(comment)) qsb.AddParameter("comment", comment);
			return qsb;
		}

		[Obsolete("Use AcquireCounteragentV3()")]
		public AsyncMethodResult AcquireCounteragent(string authToken, string myOrgId, AcquireCounteragentRequest request, string myDepartmentId = null)
		{
			var queryString = new PathAndQueryBuilder("/V2/AcquireCounteragent");
			queryString.AddParameter("myOrgId", myOrgId);
			if (!string.IsNullOrEmpty(myDepartmentId))
				queryString.AddParameter("myDepartmentId", myDepartmentId);

			var response = PerformHttpRequest<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery(), Serialize(request));
			return response;
		}
		
		public AsyncMethodResult AcquireCounteragentV3(string authToken, string myBoxId, AcquireCounteragentRequest request, string myDepartmentId = null)
		{
			var queryString = new PathAndQueryBuilder("/V3/AcquireCounteragent");
			queryString.AddParameter("myBoxId", myBoxId);
			if (!string.IsNullOrEmpty(myDepartmentId))
				queryString.AddParameter("myDepartmentId", myDepartmentId);

			return PerformHttpRequest<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery(), Serialize(request));
		}

		[Obsolete("Use WaitAcquireCounteragentResultV2()")]
		public AcquireCounteragentResult WaitAcquireCounteragentResult(string authToken, string taskId, TimeSpan? timeout = null, TimeSpan? delay = null)
		{
			return WaitTaskResult<AcquireCounteragentResult>(authToken, "/AcquireCounteragentResult", taskId, timeout, delay);
		}
		
		public AcquireCounteragentResultV2 WaitAcquireCounteragentResultV2(string authToken, string taskId, TimeSpan? timeout = null, TimeSpan? delay = null)
		{
			return WaitTaskResult<AcquireCounteragentResultV2>(authToken, "/V2/AcquireCounteragentResult", taskId, timeout, delay);
		}

		public BoxCounteragentEventList GetCounteragentEvents(
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
			return PerformHttpRequest<BoxCounteragentEventList>(authToken, "GET", qsb.BuildPathAndQuery());
		}
	}
}
