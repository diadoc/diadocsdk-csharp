using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("C8F6A2CE-CC30-416F-8AE0-26395DC65792")]
	public enum ItemMark
	{
		NotSpecified = 0,
		Property = 1,
		Job = 2,
		Service = 3,
		PropertyRights = 4,
		Other = 5
	}
}