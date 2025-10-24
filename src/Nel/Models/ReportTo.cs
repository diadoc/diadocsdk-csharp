using System.ComponentModel;
using Diadoc.Api.Proto;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Diadoc.Api.Nel.Models
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ReportTo
	{
		[JsonProperty("group")]
		public string Group { get; set; }

		[JsonProperty("max_age")]
		public int MaxAge { get; set; }

		[JsonProperty("endpoints")]
		public NelUrl[] Endpoints { get; set; }

		[JsonIgnore]
		[CanBeNull]
		public Timestamp ExpirationDate { get; set; }

		public class NelUrl
		{
			[JsonProperty("url")]
			public string Url { get; set; }
		}
	}
}
