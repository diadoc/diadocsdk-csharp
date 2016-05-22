using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("55D1F8E3-C075-46EA-94BE-47561C1BA4C7")]
	public enum DocumentAccessLevel
	{
		UnknownDocumentAccessLevel = -1,
		DepartmentOnly = 0,
		DepartmentAndSubdepartments = 1,
		AllDocuments = 2,
		SelectedDepartments = 3,
	}
}