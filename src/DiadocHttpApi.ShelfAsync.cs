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
		[Obsolete("Use UploadFileToShelfV2Async or UploadLargeFileToShelfAsync")]
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
				missingParts = await PutMissingPartsAsync(authToken, nameOnShelf, parts, missingParts, httpErrors).ConfigureAwait(false);
			}

			return nameOnShelf;
		}

		public async Task<string> UploadFileToShelfV2Async(string authToken, byte[] content, [CanBeNull] string fileExtension)
		{
			var httpErrors = new List<HttpClientException>();
			var queryString = string.Format("V2/ShelfUpload?fileExtension={0}", fileExtension);

			for (var i = 0; i < maxAttempts; i++)
			{
				try
				{
					var request = BuildRequest(authToken, "POST", queryString, new HttpRequestBody(content));
					var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
					return Encoding.UTF8.GetString(response.Content);
				}
				catch (HttpClientException e)
				{
					if (e.ResponseStatusCode.HasValue && nonRetriableStatusCodes.Contains(e.ResponseStatusCode.Value)) throw;
					httpErrors.Add(e);
				}	
			}

			throw new AggregateException("Reached the limit of attempts to send a file", httpErrors.ToArray());
		}

		public async Task<string> UploadLargeFileToShelfAsync(string authToken, byte[] content, [CanBeNull] string fileExtension)
		{
			var parts = SplitDataIntoParts(content).ToList();

			var httpErrors = new List<HttpClientException>();
			var attempts = 0;
			var missingParts = Enumerable.Range(0, parts.Count).ToList();
			string fileName = null;

			while (missingParts.Count > 0)
			{
				if (++attempts > maxAttempts)
				{
					throw new AggregateException("Reached the limit of attempts to send a file", httpErrors.ToArray());
				}

				if (fileName == null)
				{
					fileName = await ShelfUploadPartInitAsync(authToken, parts[0], missingParts, httpErrors, fileExtension).ConfigureAwait(false);
				}

				if (fileName != null)
				{
					missingParts = await ShelfUploadPartsAsync(authToken, fileName, parts, missingParts, httpErrors)
						.ConfigureAwait(false);
				}
			}

			return fileName;
		}

		[ItemCanBeNull]
		private async Task<string> ShelfUploadPartInitAsync(string authToken, ArraySegment<byte> firstPart, [NotNull] IList<int> missingParts, [NotNull] ICollection<HttpClientException> httpErrors, [CanBeNull] string fileExtension)
		{
			var queryString = string.Format("ShelfUploadPartInit?fileExtension={0}", fileExtension);
			if (missingParts.Count == 1)
            {
            	queryString += "&isLastPart=true";
            }

			try
			{
				var request = BuildRequest(authToken, "POST", queryString, new HttpRequestBody(firstPart));
				var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
				missingParts.Remove(0);

				return Encoding.UTF8.GetString(response.Content);
			}
			catch (HttpClientException e)
			{
				if (e.ResponseStatusCode.HasValue && nonRetriableStatusCodes.Contains(e.ResponseStatusCode.Value)) throw;
				httpErrors.Add(e);
			}

			return null;
		}

		[ItemNotNull]
		private async Task<List<int>> ShelfUploadPartsAsync(string authToken, string fileName, [NotNull] IList<ArraySegment<byte>> parts, [NotNull] IList<int> missingParts, [NotNull] ICollection<HttpClientException> httpErrors)
		{
			var currentMissingParts = missingParts.Count == 0 ? new List<int>() : null;
			for (var i = 0; i < missingParts.Count; ++i)
			{
				var partIndex = missingParts[i];
				currentMissingParts = await UploadPartAsync(authToken, fileName, parts[partIndex], partIndex, i == missingParts.Count - 1, httpErrors)
					.ConfigureAwait(false);
			}

			if (currentMissingParts == null)
			{
				throw new Exception("UploadPartAsync did not return missing parts");
			}

			return currentMissingParts;
		}

		[ItemCanBeNull]
		private async Task<List<int>> UploadPartAsync(string authToken, string fileName, ArraySegment<byte> part, int partIndex, bool isLast, ICollection<HttpClientException> httpErrors)
		{
			var queryString = string.Format("ShelfUploadPart?fileName={0}&partIndex={1}&isLastPart={2}", fileName, partIndex, isLast);
			try
			{
				var request = BuildRequest(authToken, "POST", queryString, new HttpRequestBody(part));
				var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
				if (isLast)
				{
					var responseString = Encoding.UTF8.GetString(response.Content);
					return responseString
						.Split(new[] { ',', '[', ']' }, StringSplitOptions.RemoveEmptyEntries)
						.Select(int.Parse)
						.ToList();
				}

				return null;
			}
			catch (HttpClientException e)
			{
				if (e.ResponseStatusCode.HasValue && nonRetriableStatusCodes.Contains(e.ResponseStatusCode.Value)) throw;
				httpErrors.Add(e);
				return isLast ? new List<int> { partIndex } : null;
			}
		}

		[ItemNotNull]
		private async Task<int[]> PutMissingPartsAsync(string authToken, string nameOnShelf, [NotNull] IList<ArraySegment<byte>> allParts, [NotNull] IList<int> missingParts, [NotNull] ICollection<HttpClientException> httpErrors)
		{
			int[] currentMissingParts = null;
			for (var i = 0; i < missingParts.Count; ++i)
			{
				var partIndex = missingParts[i];
				currentMissingParts = await PutPartAsync(authToken, nameOnShelf, allParts[partIndex], partIndex, i == missingParts.Count - 1, httpErrors).ConfigureAwait(false);
			}
			if (currentMissingParts == null)
				throw new Exception("ShelfUpload did not return missing parts");
			return currentMissingParts;
		}

		[Obsolete("Use GetFileFromShelfV2Async")]
		public Task<byte[]> GetFileFromShelfAsync(string authToken, string nameOnShelf)
		{
			if (!nameOnShelf.Contains("__userId__"))
				nameOnShelf = $"__userId__/{nameOnShelf}";
			var queryString = $"ShelfDownload?nameOnShelf={nameOnShelf}";
			return PerformHttpRequestAsync(authToken, "GET", queryString);
		}

		public Task<byte[]> GetFileFromShelfV2Async(string authToken, string fileName)
		{
			var queryString = $"V2/ShelfDownload?fileName={fileName}";
			return PerformHttpRequestAsync(authToken, "GET", queryString);
		}

		[ItemCanBeNull]
		private async Task<int[]> PutPartAsync(string authToken, string nameOnShelf, ArraySegment<byte> part, int partIndex, bool isLast, ICollection<HttpClientException> httpErrors)
		{
			var queryString = $"ShelfUpload?nameOnShelf=__userId__/{nameOnShelf}&partIndex={partIndex}";
			if (isLast)
				queryString = queryString + "&isLastPart=1";
			try
			{
				var request = BuildRequest(authToken, "POST", queryString, new HttpRequestBody(part));
				var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
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
