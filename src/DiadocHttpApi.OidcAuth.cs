using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Diadoc.Api.Http;
using Newtonsoft.Json;
#if !NET35
using System.Threading.Tasks;
#endif

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		private const string DefaultOidcBaseUrl = "https://identity.kontur.ru";
		private const string OidcTokenEndpointPath = "/connect/token";

		private readonly string oidcBaseUrl;
		private HttpClient oidcHttpClient;

		private HttpClient OidcHttpClient
		{
			get
			{
				if (oidcHttpClient != null)
					return oidcHttpClient;

				var newClient = new HttpClient(string.IsNullOrEmpty(oidcBaseUrl) ? DefaultOidcBaseUrl : oidcBaseUrl);
				return Interlocked.CompareExchange(ref oidcHttpClient, newClient, null) ?? newClient;
			}
		}

		/// <summary>
		///     Gets an access token via OIDC grant_type=refresh_token.
		/// </summary>
		/// <param name="clientId">Integrator's Client ID from the integrator cabinet</param>
		/// <param name="clientSecret">Integrator's Client Secret</param>
		/// <param name="refreshToken">Integrator's Refresh Token</param>
		/// <returns>Access token as a string</returns>
		public string AuthenticateWithOidc(string clientId, string clientSecret, string refreshToken)
		{
			var response = PerformOidcTokenRequest(clientId, clientSecret, refreshToken);
			return response.AccessToken;
		}

#if !NET35
		public async Task<string> AuthenticateWithOidcAsync(string clientId, string clientSecret, string refreshToken)
		{
			var response = await PerformOidcTokenRequestAsync(clientId, clientSecret, refreshToken).ConfigureAwait(false);
			return response.AccessToken;
		}
#endif

		private OidcTokenResponse PerformOidcTokenRequest(
			string clientId, string clientSecret, string refreshToken)
		{
			var request = BuildOidcHttpRequest(clientId, clientSecret, refreshToken);
			var httpResponse = OidcHttpClient.PerformHttpRequest(request);
			return ParseOidcTokenResponse(httpResponse);
		}

#if !NET35
		private async Task<OidcTokenResponse> PerformOidcTokenRequestAsync(
			string clientId, string clientSecret, string refreshToken)
		{
			var request = BuildOidcHttpRequest(clientId, clientSecret, refreshToken);
			var httpResponse = await OidcHttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
			return ParseOidcTokenResponse(httpResponse);
		}
#endif

		private HttpRequest BuildOidcHttpRequest(string clientId, string clientSecret, string refreshToken)
		{
			var parameters = new Dictionary<string, string>
			{
				{"grant_type", "refresh_token"},
				{"refresh_token", refreshToken},
				{"client_id", clientId},
				{"client_secret", clientSecret}
			};

			var body = EncodeFormUrlEncoded(parameters);
			var bodyBytes = Encoding.UTF8.GetBytes(body);

			var request = new HttpRequest(
				"POST",
				OidcTokenEndpointPath,
				new HttpRequestBody(bodyBytes, "application/x-www-form-urlencoded"));

			return request;
		}

		private static OidcTokenResponse ParseOidcTokenResponse(HttpResponse httpResponse)
		{
			var json = Encoding.UTF8.GetString(httpResponse.Content);
			var result = JsonConvert.DeserializeObject<OidcTokenResponse>(json);

			if (result == null || string.IsNullOrEmpty(result.AccessToken))
				throw new InvalidOperationException("OIDC token endpoint returned response without access_token. Response body: " + json);

			return result;
		}

		private static string EncodeFormUrlEncoded(Dictionary<string, string> parameters)
		{
			return string.Join("&", parameters.Select(keyValuePair => Uri.EscapeDataString(keyValuePair.Key) + "=" + Uri.EscapeDataString(keyValuePair.Value)).ToArray());
		}
	}

	public class OidcTokenResponse
	{
		[JsonProperty("access_token")] public string AccessToken { get; set; }
		[JsonProperty("token_type")] public string TokenType { get; set; }
		[JsonProperty("expires_in")] public int ExpiresIn { get; set; }
		[JsonProperty("refresh_token")] public string RefreshToken { get; set; }
	}
}
