using System.ComponentModel;

namespace Diadoc.Api.Nel.Models
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ReportingEndpoints
	{
		public string Name { get; set; }

		public string Endpoint { get; set; }
	}
}
