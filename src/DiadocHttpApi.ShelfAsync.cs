using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public async Task<string> UploadFileToShelfAsync(string authToken, byte[] data)
		{
			var nameOnShelf = $"api-{Guid.NewGuid()}";
			var parts = SplitDataIntoParts(data).ToList();

			var httpErrors = new List<HttpClientException>();
			var attempts = 0;
			var missingParts = Enumerable.Range(0, parts.Count).ToArray();
			while(missingParts.Length > 0)
			{
				if (++attempts > maxAttempts)
					throw new AggregateException("Reached the limit of attempts to send a file", httpErrors.ToArray());
				missingParts = await PutMissingPartsAsync(authToken, nameOnShelf, parts, missingParts, httpErrors);
			}

			return nameOnShelf;
		}

		[NotNull]
		private async Task<int[]> PutMissingPartsAsync(string authToken, string nameOnShelf, [NotNull] IList<ArraySegment<byte>> allParts, [NotNull] IList<int> missingParts, [NotNull] ICollection<HttpClientException> httpErrors)
		{
			int[] currentMissingParts = null;
			for (var i = 0; i < missingParts.Count; ++i)
			{
				var partIndex = missingParts[i];
				currentMissingParts = await PutPartAsync(authToken, nameOnShelf, allParts[partIndex], partIndex, i == missingParts.Count - 1, httpErrors);
			}
			if (currentMissingParts == null)
				throw new Exception("ShelfUpload did not return missing parts");
			return currentMissingParts;
		}

		public Task<byte[]> GetFileFromShelfAsync(string authToken, string nameOnShelf)
		{
			if (!nameOnShelf.Contains("__userId__"))
				nameOnShelf = $"__userId__/{nameOnShelf}";
			var queryString = $"ShelfDownload?nameOnShelf={nameOnShelf}";
			return PerformHttpRequestAsync(authToken, "GET", queryString);
		}

		[CanBeNull]
		private async Task<int[]> PutPartAsync(string authToken, string nameOnShelf, ArraySegment<byte> part, int partIndex, bool isLast, ICollection<HttpClientException> httpErrors)
		{
			var queryString = $"ShelfUpload?nameOnShelf=__userId__/{nameOnShelf}&partIndex={partIndex}";
			if (isLast)
				queryString = queryString + "&isLastPart=1";
			try
			{
				var request = BuildRequest(authToken, "POST", queryString, new HttpRequestBody(part));
				var response = await HttpClient.PerformHttpRequestAsync(request);
				if (isLast)
				{
					var responseString = Encoding.UTF8.GetString(response.Content);
					var result = responseString.Split(new[] {',', '[', ']'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
					return result;
				}
				return null;
			}
			catch (HttpClientException e)
			{
				if (e.ResponseStatusCode.HasValue && nonRetriableStatusCodes.Contains(e.ResponseStatusCode.Value)) throw;
				httpErrors.Add(e);
				return isLast ? new[] { partIndex } : null;
			}
		}
	}
}
