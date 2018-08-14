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
		DocumentAccessLevel DocumentAccessLevel { get; }
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
	[Guid("89C89C89-FDD7-4035-AAD8-F36A518AFC28")]
	public interface IEmployeeToCreate
	{
		EmployeeToCreateCredentials Credentials { get; }
		string Position { get; }
		bool CanBeInvitedForChat { get; }
		EmployeePermissions Permissions { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeeAction")]
	[Guid("991DC3B3-0488-499F-893A-AC14417077EF")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeeAction))]
	public partial class EmployeeAction : SafeComObject, IEmployeeAction
	{
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeeToCreate")]
	[Guid("41867515-8F9E-4367-A6D3-D8C345F5D36C")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeeToCreate))]
	public partial class EmployeeToCreate : SafeComObject, IEmployeeToCreate
	{
	}

	[ComVisible(true)]
	[Guid("7A3AB4C0-8005-4A28-9186-73CFC9387CE3")]
	public interface IEmployeeToCreateCredentials
	{
		EmployeeToCreateByLogin Login { get; }
		EmployeeToCreateByCertificate Certificate { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeeToCreateCredentials")]
	[Guid("5650F7FB-4581-4EF1-91F4-7D5BB7698DD7")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeeToCreateCredentials))]
	public partial class EmployeeToCreateCredentials : SafeComObject, IEmployeeToCreateCredentials
	{
	}

	[ComVisible(true)]
	[Guid("36988BEA-F0EA-492D-9BAF-E418513EF77E")]
	public interface IEmployeeToCreateByLogin
	{
		string Login { get; }
		FullName FullName { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeeToCreateByLogin")]
	[Guid("46ED009A-A232-4133-B99A-2B6454221385")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeeToCreateByLogin))]
	public partial class EmployeeToCreateByLogin : SafeComObject, IEmployeeToCreateByLogin
	{
	}

	[ComVisible(true)]
	[Guid("CCC77101-B480-4E8A-8E23-2A5262DB1186")]
	public interface IEmployeeToCreateByCertificate
	{
		byte[] Content { get; }
		string AccessBasis { get; }
		string Email { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeeToCreateByCertificate")]
	[Guid("58901395-AFF3-47FC-B800-01C138C0B982")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeeToCreateByCertificate))]
	public partial class EmployeeToCreateByCertificate : SafeComObject, IEmployeeToCreateByCertificate
	{
	}
}

namespace Diadoc.Api.Proto.Employees.Subscriptions
{
	[ComVisible(true)]
	[Guid("1DEF9D00-851C-4187-90BA-FCD6BC398551")]
	public interface ISubscription
	{
		string Id { get; set; }
		bool IsSubscribed { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.Subscription")]
	[Guid("EFC2B45C-801A-418B-ABE3-01455368B992")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISubscription))]
	public partial class Subscription : SafeComObject, ISubscription
	{
	}

	[ComVisible(true)]
	[Guid("7A858026-C1AC-4E72-8E40-9F5ECF6019CA")]
	public interface IEmployeeSubscriptions
	{
		ReadonlyList SubscriptionsList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeeSubscriptions")]
	[Guid("DFB50BF0-A22D-4960-941A-D8325D727B8D")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeeSubscriptions))]
	public partial class EmployeeSubscriptions : SafeComObject, IEmployeeSubscriptions
	{
		public ReadonlyList SubscriptionsList
		{
			get { return new ReadonlyList(Subscriptions); }
		}
	}

	[ComVisible(true)]
	[Guid("8DE9C414-FE4E-484F-B676-62FA655E8672")]
	public interface ISubscriptionsToUpdate
	{
		ReadonlyList SubscriptionsList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SubscriptionsToUpdate")]
	[Guid("D4190F23-C51C-4981-A607-E5571776B8E9")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISubscriptionsToUpdate))]
	public partial class SubscriptionsToUpdate : SafeComObject, ISubscriptionsToUpdate
	{
		public ReadonlyList SubscriptionsList
		{
			get { return new ReadonlyList(Subscriptions); }
		}
	}
}