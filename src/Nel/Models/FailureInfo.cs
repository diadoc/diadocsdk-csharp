namespace Diadoc.Api.Nel.Models
{
	public class FailureInfo
	{
		public readonly string Phase;
		public readonly string Detail;

		public FailureInfo(string phase, string detail)
		{
			Phase = phase;
			Detail = detail;
		}
	}
}
