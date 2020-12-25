using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;

namespace Diadoc.Api.Http
{
	public class HttpRequest
	{
		public HttpRequest(
			string httpMethod,
			string pathAndQuery,
			[CanBeNull] HttpRequestBody requestBody = null,
			int? timeoutInSeconds = null,
			string accept = null,
			Range range = null)
		{
			Method = httpMethod.ToUpper();
			PathAndQuery = pathAndQuery;
			Body = requestBody;
			Accept = accept;
			Range = range;
			TimeoutInSeconds = timeoutInSeconds ?? (Debugger.IsAttached ? 100500 : 100);
		}

		public string Method { get; private set; }

		public string PathAndQuery { get; private set; }

		public int TimeoutInSeconds { get; private set; }

		public string Accept { get; private set; }

		[CanBeNull]
		public Range Range { get; private set; }

		[CanBeNull]
		public HttpRequestBody Body { get; private set; }

		[CanBeNull]
		public Dictionary<string, string> AdditionalHeaders { get; private set; }

		public void AddHeader(string name, string value)
		{
			if (AdditionalHeaders == null)
			{
				AdditionalHeaders = new Dictionary<string, string>();
			}

			AdditionalHeaders.Add(name, value);
		}

		public override string ToString()
		{
			var additionalHeaders = AdditionalHeaders == null
				? string.Empty
				: AdditionalHeaders.Aggregate("\r\nAdditionalHeaders:", (s, kvp) => s + "\r\n  " + kvp.Key + ": " + kvp.Value);
			return string.Format("{0} {1}{2}", Method, PathAndQuery, additionalHeaders);
		}
	}
}
