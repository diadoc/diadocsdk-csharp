using System;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Dss;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public AsyncMethodResult CloudSign(string authToken, CloudSignRequest request, string certificateThumbprint = null)
		{
			var queryString = new PathAndQueryBuilder("/CloudSign");
			if (!string.IsNullOrEmpty(certificateThumbprint))
				queryString.AddParameter("certificateThumbprint", certificateThumbprint);
			var response = PerformHttpRequest<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery(), Serialize(request));
			return response;
		}

		public CloudSignResult WaitCloudSignResult(string authToken, string taskId, TimeSpan? timeout = null)
		{
			return WaitTaskResult<CloudSignResult>(authToken, "/CloudSignResult", taskId, timeout);
		}

		public AsyncMethodResult CloudSignConfirm(string authToken, string cloudSignToken, string confirmationCode, ContentLocationPreference? locationPreference = null)
		{
			var queryString = new PathAndQueryBuilder("/CloudSignConfirm");
			queryString.AddParameter("token", cloudSignToken);
			queryString.AddParameter("confirmationCode", confirmationCode);
			if (locationPreference.HasValue)
				queryString.AddParameter("return", locationPreference.Value.ToString());
			return PerformHttpRequest<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery());
		}

		public CloudSignConfirmResult WaitCloudSignConfirmResult(string authToken, string taskId, TimeSpan? timeout = null)
		{
			return WaitTaskResult<CloudSignConfirmResult>(authToken, "/CloudSignConfirmResult", taskId, timeout);
		}

		public AsyncMethodResult AutoSignReceipts(string authToken, string boxId, string certificateThumbprint, string batchKey)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			var queryString = new PathAndQueryBuilder("/AutoSignReceipts");
			queryString.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(certificateThumbprint))
				queryString.AddParameter("certificateThumbprint", certificateThumbprint);
			if (!string.IsNullOrEmpty(batchKey))
				queryString.AddParameter("batchKey", batchKey);
			return PerformHttpRequest<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery());
		}

		public AutosignReceiptsResult WaitAutosignReceiptsResult(string authToken, string taskId, TimeSpan? timeout = null)
		{
			return WaitTaskResult<AutosignReceiptsResult>(authToken, "/AutosignReceiptsResult", taskId, timeout);
		}

		public AsyncMethodResult DssSign(string authToken, string boxId, DssSignRequest request, string certificateThumbprint = null)
		{
			var queryString = new PathAndQueryBuilder("/DssSign");
			queryString.AddParameter("boxId", boxId);
			if (!string.IsNullOrEmpty(certificateThumbprint))
			{
				queryString.AddParameter("certificateThumbprint", certificateThumbprint);
			}

			return PerformHttpRequest<AsyncMethodResult>(authToken, "POST", queryString.BuildPathAndQuery(), Serialize(request));
		}

		public DssSignResult DssSignResult(string authToken, string boxId, string taskId)
		{
			var queryString = new PathAndQueryBuilder("/DssSignResult");
			queryString.AddParameter("boxId", boxId);
			queryString.AddParameter("taskId", taskId);
			return PerformHttpRequest<DssSignResult>(authToken, "GET", queryString.BuildPathAndQuery());
		}
	}
}
