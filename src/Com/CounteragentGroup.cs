using System.Collections.Generic;
using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.CounteragentGroups
{
	[ComVisible(true)]
	[Guid("A9C27759-FAAF-4AA0-8853-86E851ABC54D")]
	public interface ICounteragentGroup
	{
		string CounteragentGroupId { get; }
		string Name { get; }
		DepartmentsInGroup Departments { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.CounteragentGroup")]
	[Guid("9A0ACC3E-D778-44A0-BCFC-74C0C5C06310")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICounteragentGroup))]
	public partial class CounteragentGroup : SafeComObject, ICounteragentGroup
	{
	}

	[ComVisible(true)]
	[Guid("31F17592-C2B5-4EB5-BC57-560FF19FD3CC")]
	public interface IDepartmentsInGroup
	{
		ReadonlyList DepartmentIdList { get; }
		void AddDepartmentId(string departmentId);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DepartmentsInGroup")]
	[Guid("7399A897-F1F9-4BD0-B915-3ECF583D9F74")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDepartmentsInGroup))]
	public partial class DepartmentsInGroup : SafeComObject, IDepartmentsInGroup
	{
		public ReadonlyList DepartmentIdList => new ReadonlyList(DepartmentId);

		public void AddDepartmentId(string departmentId)
		{
			DepartmentId.Add(departmentId);
		}
	}

	[ComVisible(true)]
	[Guid("BD519680-01C8-4F37-B4B6-3B07F3CF9293")]
	public interface ICounteragentGroupToCreate
	{
		string Name { get; set; }
		DepartmentsInGroup Departments { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.CounteragentGroupToCreate")]
	[Guid("12A7E7F8-8047-446D-B03A-4D49744254B1")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICounteragentGroupToCreate))]
	public partial class CounteragentGroupToCreate : SafeComObject, ICounteragentGroupToCreate
	{
	}

	[ComVisible(true)]
	[Guid("8FE339E5-094D-47C2-8867-E6B68FA142CE")]
	public interface ICounteragentGroupToUpdate
	{
		string Name { get; set; }
		CounteragentGroupDepartmentPatch Departments { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.CounteragentGroupToUpdate")]
	[Guid("E204A54A-0DF4-4306-AD40-AE455A9BC45E")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICounteragentGroupToUpdate))]
	public partial class CounteragentGroupToUpdate : SafeComObject, ICounteragentGroupToUpdate
	{
	}

	[ComVisible(true)]
	[Guid("8535025D-1A18-4C58-A618-E613C8C908BB")]
	public interface ICounteragentGroupDepartmentPatch
	{
		bool AnyDepartment { get; set; }
		DepartmentsInGroup DepartmentId { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.CounteragentGroupDepartmentPatch")]
	[Guid("086FA1D7-520F-4999-904B-AB1F2F82B045")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICounteragentGroupDepartmentPatch))]
	public partial class CounteragentGroupDepartmentPatch : SafeComObject, ICounteragentGroupDepartmentPatch
	{
	}
}
