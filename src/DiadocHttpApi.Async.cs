using System;
using System.Threading.Tasks;
using Diadoc.Api.Proto.Events;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[ItemNotNull]
		protected Task<byte[]> PerformHttpRequestAsync([CanBeNull] string token, [NotNull] string method, [NotNull] string queryString, [CanBeNull] byte[] requestBody = null)
		{
			return PerformHttpRequestAsync(token, method, queryString, requestBody, responseContent => responseContent);
		}

		[ItemNotNull]
		protected Task<TResponse> PerformHttpRequestAsync<TRequest, TResponse>([CanBeNull] string token, [NotNull] string queryString, [NotNull] TRequest request)
			where TRequest: class
			where TResponse: class
		{
			return PerformHttpRequestAsync(token, "POST", queryString, Serialize(request), Deserialize<TResponse>);
		}

		[ItemNotNull]
		protected Task<TResponse> PerformHttpRequestAsync<TRequest, TResponse>([CanBeNull] string token, [NotNull] string queryString, [NotNull] TRequest request, [NotNull] Func<byte[], TResponse> convertResponse)
			where TRequest: class
			where TResponse: class
		{
			return PerformHttpRequestAsync(token, "POST", queryString, Serialize(request), convertResponse);
		}

		[ItemNotNull]
		protected Task<TResponse> PerformHttpRequestAsync<TResponse>([CanBeNull] string token, [NotNull] string method, [NotNull] string queryString, [CanBeNull] byte[] requestBody = null)
			where TResponse: class
		{
			return PerformHttpRequestAsync(token, method, queryString, requestBody, Deserialize<TResponse>);
		}

		[ItemNotNull]
		protected async Task<TResponse> PerformHttpRequestAsync<TResponse>([CanBeNull] string token, [NotNull] string method, [NotNull] string queryString, [CanBeNull] byte[] requestBody, [NotNull] Func<byte[], TResponse> convertResponse)
		{
			var request = BuildHttpRequest(token, method, queryString, requestBody);
			var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
			return DeserializeResponse(request, response, convertResponse);
		}

		[ItemNotNull]
		protected async Task<GeneratedFile> PerformGenerateXmlHttpRequestAsync<TRequest>([CanBeNull] string token, [NotNull] string queryString, [NotNull] TRequest requestObject)
			where TRequest: class
		{
			var request = BuildHttpRequest(token, "POST", queryString, Serialize(requestObject));
			var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
			return new GeneratedFile(response.ContentDispositionFileName, response.Content);
		}
	}
}
