using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("5DBFFEBD-2DA6-4FA0-92FC-4D25082E3034")]
	public enum SortDirection
	{

		UnknownSortDirection = Diadoc.Api.Proto.SortDirection.UnknownSortDirection,
		Ascending = Diadoc.Api.Proto.SortDirection.Ascending,
		Descending = Diadoc.Api.Proto.SortDirection.Descending
	}
}