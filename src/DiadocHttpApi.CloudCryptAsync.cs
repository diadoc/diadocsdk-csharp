using System;
using System.Threading;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Dss;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<AsyncMethodResult> CloudSignAsync(string authToken, CloudSignRequest request, string certificateThumbprint = null, CancellationToken ct = default)
		{
			var queryString = new PathAndQueryBuilder("/CloudSign");
			if (!string.IsNullOrEmpty(certificateThumbprint))
				queryString.AddParameter("certificateThumbprint", certificateThumbprint);
			return PerformHttpRequestAsync<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery(), Serialize(request), ct: ct);
		}

		public Task<CloudSignResult> WaitCloudSignResultAsync(string authToken, string taskId, TimeSpan? timeout = null, CancellationToken ct = default)
		{
			return WaitTaskResultAsync<CloudSignResult>(authToken, "/CloudSignResult", taskId, timeout, ct: ct);
		}

		public Task<AsyncMethodResult> CloudSignConfirmAsync(string authToken, string cloudSignToken, string confirmationCode, ContentLocationPreference? locationPreference = null, CancellationToken ct = default)
		{
			var queryString = new PathAndQueryBuilder("/CloudSignConfirm");
			queryString.AddParameter("token", cloudSignToken);
			queryString.AddParameter("confirmationCode", confirmationCode);
			if (locationPreference.HasValue)
				queryString.AddParameter("return", locationPreference.Value.ToString());
			return PerformHttpRequestAsync<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery(), ct: ct);
		}

		public Task<CloudSignConfirmResult> WaitCloudSignConfirmResultAsync(string authToken, string taskId, TimeSpan? timeout = null, CancellationToken ct = default)
		{
			return WaitTaskResultAsync<CloudSignConfirmResult>(authToken, "/CloudSignConfirmResult", taskId, timeout, ct: ct);
		}

		public Task<AsyncMethodResult> AutoSignReceiptsAsync(string authToken, string boxId, string certificateThumbprint, string batchKey, CancellationToken ct = default)
		{
			if (boxId == null) throw new ArgumentNullException(nameof(boxId));
			var queryString = new PathAndQueryBuilder("/AutoSignReceipts");
			queryString.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(certificateThumbprint))
				queryString.AddParameter("certificateThumbprint", certificateThumbprint);
			if (!string.IsNullOrEmpty(batchKey))
				queryString.AddParameter("batchKey", batchKey);
			return PerformHttpRequestAsync<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery(), ct: ct);
		}

		public Task<AutosignReceiptsResult> WaitAutosignReceiptsResultAsync(string authToken, string taskId, TimeSpan? timeout = null, CancellationToken ct = default)
		{
			return WaitTaskResultAsync<AutosignReceiptsResult>(authToken, "/AutosignReceiptsResult", taskId, timeout, ct: ct);
		}

		public Task<AsyncMethodResult> DssSignAsync(string authToken, string boxId, DssSignRequest request, string certificateThumbprint = null, CancellationToken ct = default)
		{
			var queryString = new PathAndQueryBuilder("/DssSign");
			queryString.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(certificateThumbprint))
			{
				queryString.AddParameter("certificateThumbprint", certificateThumbprint);
			}

			return PerformHttpRequestAsync<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery(), Serialize(request), ct: ct);
		}

		public Task<DssSignResult> DssSignResultAsync(string authToken, string boxId, string taskId, CancellationToken ct = default)
		{
			var queryString = new PathAndQueryBuilder("/DssSignResult");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("taskId", taskId);
			return PerformHttpRequestAsync<DssSignResult>(authToken, "GET", queryString.BuildPathAndQuery(), ct: ct);
		}
	}
}
