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

		public int ShelfUploadChunkSize  { get { return partLength; } }
		public int ShelfUploadMaxAttemptsCount { get { return maxAttempts; } }

		[Obsolete("Use UploadFileToShelfV2 or UploadLargeFileToShelf")]
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

		public string UploadFileToShelfV2(string authToken, byte[] content, [CanBeNull] string fileExtension)
		{
			var httpErrors = new List<HttpClientException>();
			var queryString = string.Format("V2/ShelfUpload?fileExtension={0}", fileExtension);

			for (var i = 0; i < maxAttempts; i++)
			{
				try
				{
					var request = BuildRequest(authToken, "POST", queryString, new HttpRequestBody(content));
					var response = HttpClient.PerformHttpRequest(request);
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

		public string UploadLargeFileToShelf(string authToken, byte[] content, [CanBeNull] string fileExtension)
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
					fileName = ShelfUploadPartInit(authToken, parts[0], missingParts, httpErrors, fileExtension);
				}

				if (fileName != null)
				{
					missingParts = ShelfUploadParts(authToken, fileName, parts, missingParts, httpErrors);
				}
			}

			return fileName;
		}

		[CanBeNull]
		private string ShelfUploadPartInit(string authToken, ArraySegment<byte> firstPart, [NotNull] IList<int> missingParts, [NotNull] ICollection<HttpClientException> httpErrors, [CanBeNull] string fileExtension)
		{
			var queryString = string.Format("ShelfUploadPartInit?fileExtension={0}", fileExtension);
			if (missingParts.Count == 1)
			{
				queryString += "&isLastPart=true";
			}
			
			try
			{
				var request = BuildRequest(authToken, "POST", queryString, new HttpRequestBody(firstPart));
				var response = HttpClient.PerformHttpRequest(request);
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

		[NotNull]
		private List<int> ShelfUploadParts(string authToken, string fileName, [NotNull] IList<ArraySegment<byte>> parts, [NotNull] IList<int> missingParts, [NotNull] ICollection<HttpClientException> httpErrors)
		{
			var currentMissingParts = missingParts.Count == 0 ? new List<int>() : null;
			for (var i = 0; i < missingParts.Count; ++i)
			{
				var partIndex = missingParts[i];
				currentMissingParts = UploadPart(authToken, fileName, parts[partIndex], partIndex, i == missingParts.Count - 1, httpErrors);
			}

			if (currentMissingParts == null)
			{
				throw new Exception("UploadPart did not return missing parts");
			}

			return currentMissingParts;
		}

		[CanBeNull]
		private List<int> UploadPart(string authToken, string fileName, ArraySegment<byte> part, int partIndex, bool isLast, ICollection<HttpClientException> httpErrors)
		{
			var queryString = string.Format("ShelfUploadPart?fileName={0}&partIndex={1}&isLastPart={2}", fileName, partIndex, isLast);
			try
			{
				var request = BuildRequest(authToken, "POST", queryString, new HttpRequestBody(part));
				var response = HttpClient.PerformHttpRequest(request);
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

		[Obsolete("Use GetFileFromShelfV2")]
		public byte[] GetFileFromShelf(string authToken, string nameOnShelf)
		{
			if (!nameOnShelf.Contains("__userId__"))
				nameOnShelf = string.Format("__userId__/{0}", nameOnShelf);
			var queryString = string.Format("ShelfDownload?nameOnShelf={0}", nameOnShelf);
			return PerformHttpRequest(authToken, "GET", queryString);
		}

		public byte[] GetFileFromShelfV2(string authToken, string fileName)
		{
			var queryString = $"V2/ShelfDownload?fileName={fileName}";
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
