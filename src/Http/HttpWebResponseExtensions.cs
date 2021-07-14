using System;
using System.IO;
using System.Net;
using JetBrains.Annotations;

namespace Diadoc.Api.Http
{
	public static class HttpWebResponseExtensions
	{
		public static HttpResponse ToHttpResponse(this HttpWebResponse webResponse)
		{
			return new HttpResponse(webResponse.StatusCode, GetResponseContent(webResponse), webResponse.Headers);
		}

		[NotNull]
		private static byte[] GetResponseContent([NotNull] HttpWebResponse webResponse)
		{
			var isChunked = webResponse.GetResponseHeader("Transfer-Encoding").ToLowerInvariant() == "chunked";
			var contentLength = webResponse.ContentLength;
			if (contentLength <= 0 && !isChunked)
			{
				return new byte[0];
			}

			using (var responseStream = webResponse.GetResponseStream())
			{
				if (responseStream == null)
				{
					return new byte[0];
				}

				var buffer = new byte[!isChunked ? contentLength : 8192];
				if (!isChunked)
				{
					var index = 0;
					while (index < buffer.Length)
					{
						var count = responseStream.Read(buffer, index, buffer.Length - index);
						if (count == 0)
						{
							throw new InvalidOperationException("HttpResponse content is incomplete.");
						}

						index += count;
					}

					return buffer;
				}

				using (var memoryStream = new MemoryStream())
				{
					int count;
					do
					{
						count = responseStream.Read(buffer, 0, buffer.Length);
						if (count > 0)
						{
							memoryStream.Write(buffer, 0, count);
						}
					} while (count > 0);

					return memoryStream.ToArray();
				}
			}
		}
	}
}
