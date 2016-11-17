using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Diadoc.Api.Http;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		private const int partLength = 512 * 1024;
		private const int maxAttempts = 3;
		private readonly HttpStatusCode[] nonRetriableStatusCodes =
		{
			HttpStatusCode.OK,
			HttpStatusCode.Unauthorized,
			HttpStatusCode.Forbidden,
			HttpStatusCode.PaymentRequired
		};

		public int ShelfUploadChunkSize { get { return partLength; } }
		public int ShelfUploadMaxAttemptsCount { get { return maxAttempts; } }

		public string UploadFileToShelf(string authToken, byte[] data)
		{
			var nameOnShelf = string.Format("api-{0}", Guid.NewGuid());
			var parts = SplitDataIntoParts(data).ToList();

			var httpErrors = new List<HttpClientException>();
			var attempts = 0;
			var missingParts = Enumerable.Range(0, parts.Count).ToArray();
			while(missingParts.Length > 0)
			{
				if (++attempts > maxAttempts)
					throw new AggregateException("Reached the limit of attempts to send a file", httpErrors.ToArray());
				missingParts = PutMissingParts(authToken, nameOnShelf, parts, missingParts, httpErrors);
			}

			return nameOnShelf;
		}

		[NotNull]
		private int[] PutMissingParts(string authToken, string nameOnShelf, [NotNull] IList<ArraySegment<byte>> allParts, [NotNull] IList<int> missingParts, [NotNull] ICollection<HttpClientException> httpErrors)
		{
			int[] currentMissingParts = null;
			for (var i = 0; i < missingParts.Count; ++i)
			{
				var partIndex = missingParts[i];
				currentMissingParts = PutPart(authToken, nameOnShelf, allParts[partIndex], partIndex, i == missingParts.Count - 1, httpErrors);
			}
			if (currentMissingParts == null)
				throw new Exception("ShelfUpload did not return missing parts");
			return currentMissingParts;
		}

		public byte[] GetFileFromShelf(string authToken, string nameOnShelf)
		{
			if (!nameOnShelf.Contains("__userId__"))
				nameOnShelf = string.Format("__userId__/{0}", nameOnShelf);
			var queryString = string.Format("ShelfDownload?nameOnShelf={0}", nameOnShelf);
			return PerformHttpRequest(authToken, "GET", queryString);
		}

		[CanBeNull]
		private int[] PutPart(string authToken, string nameOnShelf, ArraySegment<byte> part, int partIndex, bool isLast, ICollection<HttpClientException> httpErrors)
		{
			var queryString = string.Format("ShelfUpload?nameOnShelf=__userId__/{0}&partIndex={1}", nameOnShelf, partIndex);
			if (isLast)
				queryString = queryString + "&isLastPart=1";
			try
			{
				var request = BuildRequest(authToken, "POST", queryString, new HttpRequestBody(part));
				var response = HttpClient.PerformHttpRequest(request);
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

		private static IEnumerable<ArraySegment<byte>> SplitDataIntoParts(byte[] data)
		{
			var currentPartOffset = 0;
			while (currentPartOffset < data.Length)
			{
				var length = Math.Min(data.Length - currentPartOffset, partLength);
				yield return new ArraySegment<byte>(data, currentPartOffset, length);
				currentPartOffset += length;
			}
		}
	}
}
