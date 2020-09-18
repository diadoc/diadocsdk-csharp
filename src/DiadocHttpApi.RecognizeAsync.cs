using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Recognition;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public async Task<string> RecognizeAsync(string fileName, byte[] content, CancellationToken ct = default)
		{
			var queryString = $"/Recognize?filename={fileName}";
			var responseBytes = await PerformHttpRequestAsync(null, "POST", queryString, content, ct: ct).ConfigureAwait(false);
			return Encoding.UTF8.GetString(responseBytes);
		}

		public Task<Recognized> GetRecognizedAsync(string recognitionId, CancellationToken ct = default)
		{
			var queryString = $"/GetRecognized?recognitionId={recognitionId}";
			return PerformHttpRequestAsync<Recognized>(null, "GET", queryString, ct: ct);
		}

		public Task<RussianAddress> ParseRussianAddressAsync(string address, CancellationToken ct = default)
		{
			var queryString = $"/ParseRussianAddress?address={address}";
			return PerformHttpRequestAsync<RussianAddress>(null, "GET", queryString, ct: ct);
		}
	}
}
