using System;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<AsyncMethodResult> CloudSignAsync(string authToken, CloudSignRequest request, string certificateThumbprint = null)
		{
			var queryString = new PathAndQueryBuilder("/CloudSign");
			if (!string.IsNullOrEmpty(certificateThumbprint))
				queryString.AddParameter("certificateThumbprint", certificateThumbprint);
			return PerformHttpRequestAsync<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery(), Serialize(request));
		}

		public Task<CloudSignResult> WaitCloudSignResultAsync(string authToken, string taskId, TimeSpan? timeout = null)
		{
			return WaitTaskResultAsync<CloudSignResult>(authToken, "/CloudSignResult", taskId, timeout);
		}

		public Task<AsyncMethodResult> CloudSignConfirmAsync(string authToken, string cloudSignToken, string confirmationCode, ContentLocationPreference? locationPreference = null)
		{
			var queryString = new PathAndQueryBuilder("/CloudSignConfirm");
			queryString.AddParameter("token", cloudSignToken);
			queryString.AddParameter("confirmationCode", confirmationCode);
			if (locationPreference.HasValue)
				queryString.AddParameter("return", locationPreference.Value.ToString());
			return PerformHttpRequestAsync<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery());
		}

		public Task<CloudSignConfirmResult> WaitCloudSignConfirmResultAsync(string authToken, string taskId, TimeSpan? timeout = null)
		{
			return WaitTaskResultAsync<CloudSignConfirmResult>(authToken, "/CloudSignConfirmResult", taskId, timeout);
		}

		public Task<AsyncMethodResult> AutoSignReceiptsAsync(string authToken, string boxId, string certificateThumbprint, string batchKey)
		{
			if (boxId == null) throw new ArgumentNullException(nameof(boxId));
			var queryString = new PathAndQueryBuilder("/AutoSignReceipts");
			queryString.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(certificateThumbprint))
				queryString.AddParameter("certificateThumbprint", certificateThumbprint);
			if (!string.IsNullOrEmpty(batchKey))
				queryString.AddParameter("batchKey", batchKey);
			return PerformHttpRequestAsync<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery());
		}

		public Task<AutosignReceiptsResult> WaitAutosignReceiptsResultAsync(string authToken, string taskId, TimeSpan? timeout = null)
		{
			return WaitTaskResultAsync<AutosignReceiptsResult>(authToken, "/AutosignReceiptsResult", taskId, timeout);
		}
	}
}
