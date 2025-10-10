using JetBrains.Annotations;

namespace Diadoc.Api.Nel
{
	public static class NelInfo
	{
		[CanBeNull]
		public static NelConfiguration NelConfiguration { get; set; }

		[CanBeNull]
		public static ReportTo ReportToConfigurations { get; set; }
	}
}
