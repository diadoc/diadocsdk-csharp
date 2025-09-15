using System.ComponentModel;
using Newtonsoft.Json;

namespace Diadoc.Api.Nel.Models
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class NelConfiguration
	{
		[JsonProperty("report_to")]
		public string ReportTo { get; set; }

		[JsonProperty("max_age")]
		public int MaxAge { get; set; }

		[JsonProperty("include_subdomains")]
		public bool IncludeSubdomains { get; set; }
		
		[JsonProperty("failure_fraction")]
		public double? FailureFraction { get; set; }
	}
}
