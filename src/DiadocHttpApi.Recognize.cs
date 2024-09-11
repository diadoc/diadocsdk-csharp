using System;
using System.Text;
using Diadoc.Api.Constants;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Recognition;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public string Recognize(string fileName, byte[] content)
		{
			var queryString = string.Format("/Recognize?filename={0}", fileName);
			var responseBytes = PerformHttpRequest(null, "POST", queryString, content);
			return Encoding.UTF8.GetString(responseBytes);
		}

		public Recognized GetRecognized(string recognitionId)
		{
			var queryString = string.Format("/GetRecognized?recognitionId={0}", recognitionId);
			return PerformHttpRequest<Recognized>(null, "GET", queryString);
		}

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
