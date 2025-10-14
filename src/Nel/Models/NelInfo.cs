using JetBrains.Annotations;

namespace Diadoc.Api.Nel.Models
{
	public static class NelInfo
	{
		[CanBeNull]
		public static NelConfiguration NelConfiguration { get; set; }

		[CanBeNull]
		public static ReportTo ReportToConfigurations { get; set; }
		
		[CanBeNull]
		public static ReportingEndpoints[] ReportingEndpoints { get; set; }
	}
}
