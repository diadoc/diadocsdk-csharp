using Diadoc.Api.Proto;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public ExternalServiceAuthInfo GetExternalServiceAuthInfo(string authToken, string key)
		{
			var queryString = string.Format("/GetExternalServiceAuthInfo?key={0}", key);
			return PerformHttpRequest<ExternalServiceAuthInfo>(authToken, "GET", queryString);
		}
	}
}
