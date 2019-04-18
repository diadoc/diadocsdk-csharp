using System.Threading.Tasks;
using Diadoc.Api.Proto.Registration;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<RegistrationResponse> RegisterAsync(string authToken, RegistrationRequest registrationRequest)
		{
			return PerformHttpRequestAsync<RegistrationRequest, RegistrationResponse>(
				authToken,
				"/Register",
				registrationRequest);
		}

		public Task RegisterConfirmAsync(string authToken, RegistrationConfirmRequest registrationConfirmRequest)
		{
			return PerformHttpRequestAsync(
				authToken,
				"POST",
				"/RegisterConfirm",
				Serialize(registrationConfirmRequest));
		}
	}
}