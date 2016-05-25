using System.Net;
using Diadoc.Api.Annotations;

namespace Diadoc.Api.Http
{
	public class HttpClientException : WebException
	{
		public HttpClientException([NotNull] string message, [NotNull] string additionalMessage, [NotNull] string requestPathAndQuery, [NotNull] WebException innerException, [CanBeNull] HttpResponse httpResponse)
			: base(message, innerException, innerException.Status, innerException.Response)
		{
			AdditionalMessage = additionalMessage;
			RequestPathAndQuery = requestPathAndQuery;
			HttpResponse = httpResponse;
			if (httpResponse != null)
			{
				ResponseStatusCode = httpResponse.StatusCode;
				RetryAfter = httpResponse.RetryAfter;
				DiadocErrorCode = httpResponse.DiadocErrorCode;
			}
		}

		[NotNull]
		public string RequestPathAndQuery { get; private set; }

		[NotNull]
		public string AdditionalMessage { get; private set; }

		[CanBeNull]
		public HttpResponse HttpResponse { get; private set; }

		[CanBeNull]
		public HttpStatusCode? ResponseStatusCode { get; private set; }

		[CanBeNull]
		public int? RetryAfter { get; private set; }

		[CanBeNull]
		public string DiadocErrorCode { get; private set; }
	}
}