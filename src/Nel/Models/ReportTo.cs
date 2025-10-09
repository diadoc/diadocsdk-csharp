using Diadoc.Api.Proto;
using Newtonsoft.Json;

namespace Diadoc.Api.Nel.Models
{
	public class ReportTo
	{
		[JsonProperty("group")]
		public string Group { get; set; }

		[JsonProperty("max_age")]
		public int MaxAge { get; set; }

		[JsonProperty("endpoints")]
		public NelUrl[] Endpoints { get; set; }

		[JsonIgnore]
		public Timestamp ExpirationDate { get; set; }

		public class NelUrl
		{
			[JsonProperty("url")]
			public string Url { get; set; }
		}
	}
}
