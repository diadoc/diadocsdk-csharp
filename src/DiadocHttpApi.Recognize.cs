using System;
using Diadoc.Api.Constants;
using Diadoc.Api.Proto;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public RussianAddress ParseRussianAddress(string address)
		{
			return ParseRussianAddress(null, address);
		}

		public RussianAddress ParseRussianAddress(string authToken, string address)
		{
			var queryString = string.Format("/ParseRussianAddress?address={0}", address);
			return PerformHttpRequest<RussianAddress>(authToken, "GET", queryString);
		}
	}
}
