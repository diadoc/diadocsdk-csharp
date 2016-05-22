using System;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;

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
	}
}
