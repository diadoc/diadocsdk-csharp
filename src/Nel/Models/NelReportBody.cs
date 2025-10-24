using System.ComponentModel;
using Newtonsoft.Json;

namespace Diadoc.Api.Nel.Models
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class NelReportBody
	{
		[JsonProperty("protocol")]
		public string Protocol { get; set; }

		[JsonProperty("method")]
		public string Method { get; set; }

		[JsonProperty("status_code")]
		public int StatusCode { get; set; }

		[JsonProperty("phase")]
		public string Phase { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }
		
		[JsonProperty("sampling_fraction")]
		public double? SamplingFraction { get; set; }
	}
}
