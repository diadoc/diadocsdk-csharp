using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using JetBrains.Annotations;

namespace Diadoc.Api.Http
{
	[Serializable]
	public class HttpResponse
	{
		private readonly WebHeaderCollection webResponseHeaders;

		public HttpResponse([NotNull] HttpWebResponse webResponse)
		{
			webResponseHeaders = webResponse.Headers;
			StatusCode = webResponse.StatusCode;
			ContentType = webResponse.ContentType;
			ContentDispositionFileName = TryGetContentDispositionFileName(webResponseHeaders);
			RetryAfter = TryGetRetryAfter(webResponseHeaders);
			Content = GetResponseContent(webResponse);
			DiadocErrorCode = TryGetDiadocErrorCode(webResponseHeaders);
			ContentRange = TryGetContentRange(webResponseHeaders);
		}

		public HttpStatusCode StatusCode { get; private set; }

		[NotNull]
		public string ContentType { get; private set; }

		[CanBeNull]
		public string ContentDispositionFileName { get; private set; }

		[CanBeNull]
		public int? RetryAfter { get; private set; }

		[CanBeNull]
		public ContentRange ContentRange { get; private set; }

		[NotNull]
		public byte[] Content { get; private set; }

		[CanBeNull]
		public string DiadocErrorCode { get; private set; }

		public override string ToString()
		{
			var responseHeaders = webResponseHeaders.AllKeys.Any()
				                      ? webResponseHeaders.AllKeys.Aggregate("\r\nResponseHeaders:", (s, key) => s + "\r\n  " + key + ": " + webResponseHeaders[key])
				                      : string.Empty;
			var sb = new StringBuilder();
			sb.AppendFormat("{0} ({1})", (int) StatusCode, StatusCode);
			sb.AppendFormat(responseHeaders);
			sb.AppendFormat("\r\nContent: {0}", FormatContent());
			return sb.ToString();
		}

		[NotNull]
		private string FormatContent()
		{
			if (Content.Length == 0) return "<NONE>";
			string content;
			try
			{
				content = Encoding.UTF8.GetString(Content);
			}
			catch
			{
				content = Convert.ToBase64String(Content.Take(4096).ToArray());
			}
			return string.Format("[{0}]\r\n{1}", Content.Length, content);
		}

		[NotNull]
		public static byte[] GetResponseContent([NotNull] HttpWebResponse webResponse)
		{
			var isChunked = webResponse.GetResponseHeader("Transfer-Encoding").ToLowerInvariant() == "chunked";
			var contentLength = webResponse.ContentLength;
			if (contentLength <= 0 && !isChunked)
				return new byte[0];
			var responseStream = webResponse.GetResponseStream();
			if (responseStream == null)
				return new byte[0];
			using (responseStream)
			{
				var buffer = new byte[!isChunked ? contentLength : 8192];
				if (!isChunked)
				{
					var index = 0;
					while (index < buffer.Length)
					{
						var count = responseStream.Read(buffer, index, buffer.Length - index);
						if (count == 0)
							throw new InvalidOperationException("HttpResponse content is incomplete.");
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
							memoryStream.Write(buffer, 0, count);
					} while (count > 0);
					return memoryStream.ToArray();
				}
			}
		}

		[CanBeNull]
		private static string TryGetContentDispositionFileName([NotNull] NameValueCollection webResponseHeaders)
		{
			var dispositions = webResponseHeaders.GetValues("Content-Disposition");
			if (dispositions == null || dispositions.Length == 0) return null;
			return new ContentDisposition(dispositions[0]).FileName;
		}

		[CanBeNull]
		private static int? TryGetRetryAfter([NotNull] NameValueCollection webResponseHeaders)
		{
			var values = webResponseHeaders.GetValues("Retry-After");
			if (values == null || values.Length == 0) return null;
			return Convert.ToInt32(values[0], CultureInfo.InvariantCulture);
		}

		[CanBeNull]
		private static string TryGetDiadocErrorCode([NotNull] NameValueCollection webResponseHeaders)
		{
			var errorCodes = webResponseHeaders.GetValues("X-Diadoc-ErrorCode");
			if (errorCodes == null || errorCodes.Length == 0)
				return null;
			return errorCodes[0];
		}

		[CanBeNull]
		private static ContentRange TryGetContentRange([NotNull] NameValueCollection webResponseHeaders)
		{
			// https://tools.ietf.org/html/rfc7233#section-4.2
			// Content-Range       = byte-content-range / other-content-range
			//
			// byte-content-range  = bytes-unit SP ( byte-range-resp / unsatisfied-range )
			//
			// byte-range-resp     = byte-range "/" ( complete-length / "*" )
			// byte-range          = first-byte-pos "-" last-byte-pos
			// unsatisfied-range   = "*/" complete-length
			//
			// complete-length     = 1*DIGIT
			//
			// other-content-range = other-range-unit SP other-range-resp
			// other-range-resp    = *CHAR

			var values = webResponseHeaders.GetValues("Content-Range");
			if (values == null || values.Length == 0)
				return null;

			var parts = values[0].Split(' ', '-', '/');
			if (parts.Length < 3)
				return null;

			if (parts[1] == "*")
			{
				// Content-Range: bytes */1234
				return new ContentRange(Convert.ToInt64(parts[2], CultureInfo.InvariantCulture));
			}

			var range = new Range(
				Convert.ToInt32(parts[1], CultureInfo.InvariantCulture),
				Convert.ToInt32(parts[2], CultureInfo.InvariantCulture));

			if (parts[3] == "*")
			{
				// Content-Range: bytes 42-1233/*
				return new ContentRange(range);
			}

			// Content-Range: bytes 42-1233/1234
			return new ContentRange(range, Convert.ToInt64(parts[3], CultureInfo.InvariantCulture));
		}
	}
}