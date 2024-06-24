using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<ExternalServiceAuthInfo> GetExternalServiceAuthInfoAsync(string key)
		{
			return GetExternalServiceAuthInfoAsync(null, key);
		}

		public Task<ExternalServiceAuthInfo> GetExternalServiceAuthInfoAsync(string authToken, string key)
		{
			var qsb = new PathAndQueryBuilder("/GetExternalServiceAuthInfo");
			qsb.AddParameter("key", key);
			return PerformHttpRequestAsync<ExternalServiceAuthInfo>(authToken, "GET", qsb.BuildPathAndQuery());
		}
	}
}
