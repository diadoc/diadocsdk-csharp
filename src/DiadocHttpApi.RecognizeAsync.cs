using System;
using System.Text;
using System.Threading.Tasks;
using Diadoc.Api.Constants;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Recognition;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public async Task<string> RecognizeAsync(string fileName, byte[] content)
		{
			var queryString = $"/Recognize?filename={fileName}";
			var responseBytes = await PerformHttpRequestAsync(null, "POST", queryString, content).ConfigureAwait(false);
			return Encoding.UTF8.GetString(responseBytes);
		}

		public Task<Recognized> GetRecognizedAsync(string recognitionId)
		{
			var queryString = $"/GetRecognized?recognitionId={recognitionId}";
			return PerformHttpRequestAsync<Recognized>(null, "GET", queryString);
		}

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
	}
}
