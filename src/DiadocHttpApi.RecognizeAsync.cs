using System;
using System.Threading.Tasks;
using Diadoc.Api.Constants;
using Diadoc.Api.Proto;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<RussianAddress> ParseRussianAddressAsync(string address)
		{
			return ParseRussianAddressAsync(null, address);
		}

		public Task<RussianAddress> ParseRussianAddressAsync(string authToken, string address)
		{
			var queryString = $"/ParseRussianAddress?address={address}";
			return PerformHttpRequestAsync<RussianAddress>(authToken, "GET", queryString);
		}

		public Task<GarAddress> ParseGarAddressAsync(string authToken, string address)
		{
			var queryString = $"/V1/ParseGarAddress?address={address}";
			return PerformHttpRequestAsync<GarAddress>(authToken, "GET", queryString);
		}
	}
}
