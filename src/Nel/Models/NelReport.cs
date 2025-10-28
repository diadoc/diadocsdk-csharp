using System.ComponentModel;
using Newtonsoft.Json;

namespace Diadoc.Api.Nel.Models
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class NelReport
	{
		[JsonProperty("type")]
		public string Type { get; set; } = "network-error";

		[JsonProperty("age")]
		public int Age { get; set; } = 0;

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("user_agent")]
		public string UserAgent { get; set; }

		[JsonProperty("body")]
		public NelReportBody Body { get; set; }
	}
}
