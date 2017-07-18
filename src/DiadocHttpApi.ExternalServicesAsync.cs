using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<ExternalServiceAuthInfo> GetExternalServiceAuthInfoAsync(string key)
		{
			var qsb = new PathAndQueryBuilder("/GetExternalServiceAuthInfo");
			qsb.AddParameter("key", key);
			return PerformHttpRequestAsync<ExternalServiceAuthInfo>(null, "GET", qsb.BuildPathAndQuery());
		}
	}
}
