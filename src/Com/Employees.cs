using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Employees
{
	[ComVisible(true)]
	[Guid("609A7FB0-401D-44B0-86AB-7AAC547E0AC5")]
	public interface IEmployee
	{
		UserV2 User { get; }
		EmployeePermissions Permissions { get; }
		bool CanBeInvitedForChat { get; }
		Timestamp CreationTimestamp { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.Employee")]
	[Guid("CA8D3613-5441-4269-BF99-77B7D2BAA206")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployee))]
	public partial class Employee : SafeComObject, IEmployee
	{
	}

	[ComVisible(true)]
	[Guid("C6F9CC84-5CE1-4B32-A1C2-1E40C52B2925")]
	public interface IEmployeePermissions
	{
		string UserDepartmentId { get; }
		bool IsAdministrator { get; }
		Diadoc.Api.Proto.DocumentAccessLevel DocumentAccessLevel { get; }
		ReadonlyList SelectedDepartmentIdsList { get; }
		ReadonlyList ActionsList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeePermissions")]
	[Guid("C03B7727-D19D-4038-A5F0-FB42D346837D")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeePermissions))]
	public partial class EmployeePermissions : SafeComObject, IEmployeePermissions
	{
		public ReadonlyList SelectedDepartmentIdsList
		{
			get { return new ReadonlyList(SelectedDepartmentIds); }
		}

		public ReadonlyList ActionsList
		{
			get { return new ReadonlyList(Actions); }
		}
	}

	[ComVisible(true)]
	[Guid("267DE3D2-8E5D-423D-AF2C-6252293570D5")]
	public interface IEmployeeAction
	{
		string Name { get; }
		bool IsAllowed { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeeAction")]
	[Guid("991DC3B3-0488-499F-893A-AC14417077EF")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeeAction))]
	public partial class EmployeeAction : SafeComObject, IEmployeeAction
	{
	}
}