using System;
using System.IO;
using Diadoc.Api.Annotations;

namespace Diadoc.Api.Http
{
	public class HttpRequestBody
	{
		private readonly ArraySegment<byte> body;

		public HttpRequestBody([NotNull] byte[] body, [CanBeNull] string contentType = null)
			: this(new ArraySegment<byte>(body), contentType)
		{
		}

		public HttpRequestBody(ArraySegment<byte> body, [CanBeNull] string contentType = null)
		{
			this.body = body;
			ContentType = contentType;
		}

		[CanBeNull]
		public string ContentType { get; private set; }

		public long ContentLength { get { return body.Count; } }

		public void WriteToStream([NotNull] Stream stream)
		{
			stream.Write(body.Array, body.Offset, body.Count);
		}

		public byte[] Content
		{
			get
			{
				var result = new byte[body.Count];
				Array.Copy(body.Array, body.Offset, result, 0, body.Count);
				return result;
			}
		}
	}
}