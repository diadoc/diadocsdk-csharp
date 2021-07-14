using System;
using System.Collections.Specialized;
using System.Globalization;
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
		private readonly NameValueCollection headers;

		public HttpResponse(HttpStatusCode statusCode, byte[] content, NameValueCollection headers)
		{
			StatusCode = statusCode;
			Content = content;
			this.headers = headers;
		}

		public HttpStatusCode StatusCode { get; private set; }

		[CanBeNull]
		public string ContentType => TryGetContentType(headers);

		[CanBeNull]
		public string ContentDispositionFileName => TryGetContentDispositionFileName(headers);

		[CanBeNull]
		public int? RetryAfter => TryGetRetryAfter(headers);

		[CanBeNull]
		public ContentRange ContentRange => TryGetContentRange(headers);

		[NotNull]
		public byte[] Content { get; private set; }

		[CanBeNull]
		public string DiadocErrorCode => TryGetDiadocErrorCode(headers);

		public override string ToString()
		{
			var responseHeaders = headers.AllKeys.Any()
				? headers.AllKeys.Aggregate("\r\nResponseHeaders:", (s, key) => s + "\r\n  " + key + ": " + headers[key])
				: string.Empty;
			var sb = new StringBuilder();
			sb.AppendFormat("{0} ({1})", (int)StatusCode, StatusCode);
			sb.AppendFormat(responseHeaders);
			sb.AppendFormat("\r\nContent: {0}", FormatContent());

			return sb.ToString();
		}

		[NotNull]
		private string FormatContent()
		{
			if (Content.Length == 0)
			{
				return "<NONE>";
			}

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

		[CanBeNull]
		private static string TryGetContentDispositionFileName([NotNull] NameValueCollection webResponseHeaders)
		{
			var dispositions = webResponseHeaders.GetValues("Content-Disposition");
			if (dispositions == null || dispositions.Length == 0)
			{
				return null;
			}

			return new ContentDisposition(dispositions[0]).FileName;
		}

		[CanBeNull]
		private static int? TryGetRetryAfter([NotNull] NameValueCollection webResponseHeaders)
		{
			var values = webResponseHeaders.GetValues("Retry-After");
			if (values == null || values.Length == 0)
			{
				return null;
			}

			return Convert.ToInt32(values[0], CultureInfo.InvariantCulture);
		}

		[CanBeNull]
		private static string TryGetContentType([NotNull] NameValueCollection webResponseHeaders)
		{
			var values = webResponseHeaders.GetValues("Content-Type");
			if (values == null || values.Length == 0)
			{
				return null;
			}

			return values[0];
		}

		[CanBeNull]
		private static string TryGetDiadocErrorCode([NotNull] NameValueCollection webResponseHeaders)
		{
			var errorCodes = webResponseHeaders.GetValues("X-Diadoc-ErrorCode");
			if (errorCodes == null || errorCodes.Length == 0)
			{
				return null;
			}

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
			{
				return null;
			}

			var parts = values[0].Split(' ', '-', '/');
			if (parts.Length < 3)
			{
				return null;
			}

			if (parts[1] == "*")
				// Content-Range: bytes */1234
			{
				return new ContentRange(Convert.ToInt64(parts[2], CultureInfo.InvariantCulture));
			}

			var range = new Range(
				Convert.ToInt32(parts[1], CultureInfo.InvariantCulture),
				Convert.ToInt32(parts[2], CultureInfo.InvariantCulture));

			if (parts[3] == "*")
				// Content-Range: bytes 42-1233/*
			{
				return new ContentRange(range);
			}

			// Content-Range: bytes 42-1233/1234
			return new ContentRange(range, Convert.ToInt64(parts[3], CultureInfo.InvariantCulture));
		}
	}
}
