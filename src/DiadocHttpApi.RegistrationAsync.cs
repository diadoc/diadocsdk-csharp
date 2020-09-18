using System.Threading;
using System.Threading.Tasks;
using Diadoc.Api.Proto.Registration;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<RegistrationResponse> RegisterAsync(string authToken, RegistrationRequest registrationRequest, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<RegistrationRequest, RegistrationResponse>(
				authToken,
				"/Register",
				registrationRequest,
				ct: ct);
		}

		public Task RegisterConfirmAsync(string authToken, RegistrationConfirmRequest registrationConfirmRequest, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync(
				authToken,
				"POST",
				"/RegisterConfirm",
				Serialize(registrationConfirmRequest),
				ct: ct);
		}
	}
}