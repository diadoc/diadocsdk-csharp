using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Diadoc.Api.Proto.Events;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[ItemNotNull]
		protected Task<byte[]> PerformHttpRequestAsync([CanBeNull] string token, [NotNull] string method, [NotNull] string queryString, [CanBeNull] byte[] requestBody = null, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync(token, method, queryString, requestBody, responseContent => responseContent, ct: ct);
		}

		[ItemNotNull]
		protected Task<TResponse> PerformHttpRequestAsync<TRequest, TResponse>([CanBeNull] string token, [NotNull] string queryString, [NotNull] TRequest request, CancellationToken ct = default)
			where TRequest: class
			where TResponse: class
		{
			return PerformHttpRequestAsync(token, "POST", queryString, Serialize(request), Deserialize<TResponse>, ct: ct);
		}

		[ItemNotNull]
		protected Task<TResponse> PerformHttpRequestAsync<TResponse>(
			[CanBeNull] string token, 
			[NotNull] string method, 
			[NotNull] string queryString, 
			[CanBeNull] byte[] requestBody = null, 
			CancellationToken ct = default,
			params HttpStatusCode[] allowStatusCodes)
			where TResponse: class
		{
			return PerformHttpRequestAsync(token, method, queryString, requestBody, Deserialize<TResponse>, ct: ct, allowStatusCodes);
		}

		[ItemNotNull]
		protected async Task<TResponse> PerformHttpRequestAsync<TResponse>(
			[CanBeNull] string token, 
			[NotNull] string method, 
			[NotNull] string queryString, 
			[CanBeNull] byte[] requestBody, 
			[NotNull] Func<byte[], TResponse> convertResponse,
			CancellationToken ct = default,
			params HttpStatusCode[] allowStatusCodes)
		{
			var request = BuildHttpRequest(token, method, queryString, requestBody);
			var response = await HttpClient.PerformHttpRequestAsync(request, ct: ct, allowStatusCodes).ConfigureAwait(false);
			return DeserializeResponse(request, response, convertResponse);
		}

		[ItemNotNull]
		protected async Task<GeneratedFile> PerformGenerateXmlHttpRequestAsync<TRequest>([CanBeNull] string token, [NotNull] string queryString, [NotNull] TRequest requestObject, CancellationToken ct = default)
			where TRequest: class
		{
			var request = BuildHttpRequest(token, "POST", queryString, Serialize(requestObject));
			var response = await HttpClient.PerformHttpRequestAsync(request, ct: ct).ConfigureAwait(false);
			return new GeneratedFile(response.ContentDispositionFileName, response.Content);
		}
	}
}
