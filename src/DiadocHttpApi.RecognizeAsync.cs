using System.Text;
using System.Threading.Tasks;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Recognition;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public async Task<string> RecognizeAsync(string authToken, string fileName, byte[] content)
		{
			var queryString = $"/Recognize?filename={fileName}";
			var responseBytes = await PerformHttpRequestAsync(authToken, "POST", queryString, content).ConfigureAwait(false);
			return Encoding.UTF8.GetString(responseBytes);
		}

		public Task<Recognized> GetRecognizedAsync(string authToken, string recognitionId)
		{
			var queryString = $"/GetRecognized?recognitionId={recognitionId}";
			return PerformHttpRequestAsync<Recognized>(authToken, "GET", queryString);
		}

		public Task<RussianAddress> ParseRussianAddressAsync(string authToken, string address)
		{
			var queryString = $"/ParseRussianAddress?address={address}";
			return PerformHttpRequestAsync<RussianAddress>(authToken, "GET", queryString);
		}
	}
}
