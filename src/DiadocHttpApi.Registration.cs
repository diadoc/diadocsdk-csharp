using Diadoc.Api.Proto.Registration;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public RegistrationResponse Register(string authToken, RegistrationRequest registrationRequest)
		{
			return PerformHttpRequest<RegistrationRequest, RegistrationResponse>(
				authToken,
				"/Register",
				registrationRequest);
		}

		public void RegisterConfirm(string authToken, RegistrationConfirmRequest registrationConfirmRequest)
		{
			PerformHttpRequest(
				authToken,
				"POST",
				"/RegisterConfirm",
				Serialize(registrationConfirmRequest));
		}
	}
}