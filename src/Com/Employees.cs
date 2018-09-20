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
		string UserDepartmentId { get; set; }
		bool IsAdministrator { get; set; }
		Com.DocumentAccessLevel DocumentAccessLevel { get; set; }
		ReadonlyList SelectedDepartmentIdsList { get; }
		ReadonlyList ActionsList { get; }
		void AddSelectedDepartmentIdItem(string departmentId);
		void AddActionItem([MarshalAs(UnmanagedType.IDispatch)] object item);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeePermissions")]
	[Guid("C03B7727-D19D-4038-A5F0-FB42D346837D")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeePermissions))]
	public partial class EmployeePermissions : SafeComObject, IEmployeePermissions
	{
		Com.DocumentAccessLevel IEmployeePermissions.DocumentAccessLevel
		{
			get { return (Com.DocumentAccessLevel) DocumentAccessLevel; }
			set { DocumentAccessLevel = (DocumentAccessLevel) value; }
		}

		public ReadonlyList SelectedDepartmentIdsList
		{
			get { return new ReadonlyList(SelectedDepartmentIds); }
		}

		public ReadonlyList ActionsList
		{
			get { return new ReadonlyList(Actions); }
		}

		public void AddSelectedDepartmentIdItem(string departmentId)
		{
			SelectedDepartmentIds.Add(departmentId);
		}

		public void AddActionItem(object item)
		{
			Actions.Add((EmployeeAction) item);
		}
	}

	[ComVisible(true)]
	[Guid("267DE3D2-8E5D-423D-AF2C-6252293570D5")]
	public interface IEmployeeAction
	{
		string Name { get; set; }
		bool IsAllowed { get; set; }
	}

	[ComVisible(true)]
	[Guid("89C89C89-FDD7-4035-AAD8-F36A518AFC28")]
	public interface IEmployeeToCreate
	{
		EmployeeToCreateCredentials Credentials { get; set; }
		string Position { get; set; }
		bool CanBeInvitedForChat { get; set; }
		EmployeePermissions Permissions { get; set; }
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
		EmployeeToCreateByLogin Login { get; set; }
		EmployeeToCreateByCertificate Certificate { get; set; }
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
		string Login { get; set; }
		FullName FullName { get; set; }
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
		byte[] Content { get; set; }
		string AccessBasis { get; set; }
		string Email { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeeToCreateByCertificate")]
	[Guid("58901395-AFF3-47FC-B800-01C138C0B982")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeeToCreateByCertificate))]
	public partial class EmployeeToCreateByCertificate : SafeComObject, IEmployeeToCreateByCertificate
	{
	}

	[ComVisible(true)]
	[Guid("86729B4A-37DD-4CEF-841F-F099304B4AEF")]
	public interface IEmployeeToUpdate
	{
		EmployeePermissionsPatch Permissions { get; set; }
		EmployeePositionPatch Position { get; set; }
		EmployeeCanBeInvitedForChatPatch CanBeInvitedForChat { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeeToUpdate")]
	[Guid("43916C31-477C-428C-B87A-60D0D5C9E5DA")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeeToUpdate))]
	public partial class EmployeeToUpdate : SafeComObject, IEmployeeToUpdate
	{
	}

	[ComVisible(true)]
	[Guid("2212A693-A3F2-410F-B5DB-728F26EC3DC3")]
	public interface IEmployeePermissionsPatch
	{
		EmployeeDepartmentPatch Department { get; set; }
		EmployeeIsAdministratorPatch IsAdministrator { get; set; }
		EmployeeDocumentAccessLevelPatch DocumentAccessLevel { get; set; }
		EmployeeSelectedDepartmentsPatch SelectedDepartments { get; set; }

		ReadonlyList ActionsList { get; }
		void AddActionItem([MarshalAs(UnmanagedType.IDispatch)] object item);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeePermissionsPatch")]
	[Guid("00C8C4CA-160D-41FF-8C6A-5D9B04080935")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeePermissionsPatch))]
	public partial class EmployeePermissionsPatch : SafeComObject, IEmployeePermissionsPatch
	{
		public ReadonlyList ActionsList
		{
			get { return new ReadonlyList(Actions); }
		}

		public void AddActionItem(object item)
		{
			Actions.Add((EmployeeAction) item);
		}
	}

	[ComVisible(true)]
	[Guid("6BDB877B-4819-4C21-9096-2AF09ADC9D84")]
	public interface IEmployeeDepartmentPatch
	{
		string DepartmentId { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeeDepartmentPatch")]
	[Guid("0B07EE01-E785-498E-95BA-A9B302EA72FD")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeeDepartmentPatch))]
	public partial class EmployeeDepartmentPatch : SafeComObject, IEmployeeDepartmentPatch
	{
	}

	[ComVisible(true)]
	[Guid("D497C78D-E53B-4283-AEFE-A8E30BA9A41C")]
	public interface IEmployeeIsAdministratorPatch
	{
		bool IsAdministrator { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeeIsAdministratorPatch")]
	[Guid("24CA4053-4E61-43CE-932E-0F4F68884A74")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeeIsAdministratorPatch))]
	public partial class EmployeeIsAdministratorPatch : SafeComObject, IEmployeeIsAdministratorPatch
	{
	}

	[ComVisible(true)]
	[Guid("3A407382-DEF7-4EED-8591-6F726FAAB6D0")]
	public interface IEmployeeDocumentAccessLevelPatch
	{
		Com.DocumentAccessLevel DocumentAccessLevel { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeeDocumentAccessLevelPatch")]
	[Guid("5C9B6064-813A-409E-9F30-A41F607B96D7")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeeDocumentAccessLevelPatch))]
	public partial class EmployeeDocumentAccessLevelPatch : SafeComObject, IEmployeeDocumentAccessLevelPatch
	{
		Com.DocumentAccessLevel IEmployeeDocumentAccessLevelPatch.DocumentAccessLevel
		{
			get { return (Com.DocumentAccessLevel) DocumentAccessLevel; }
			set { DocumentAccessLevel = (DocumentAccessLevel) value; }
		}
	}

	[ComVisible(true)]
	[Guid("4F36EF7E-D14B-4548-BEB2-2A5869A695BE")]
	public interface IEmployeeSelectedDepartmentsPatch
	{
		ReadonlyList SelectedDepartmentIdsList { get; }
		void AddSelectedDepartmentIdItem(string departmentId);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeeSelectedDepartmentsPatch")]
	[Guid("6DFBF545-C56A-4A25-88AF-A52BF32B4861")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeeSelectedDepartmentsPatch))]
	public partial class EmployeeSelectedDepartmentsPatch : SafeComObject, IEmployeeSelectedDepartmentsPatch
	{
		public ReadonlyList SelectedDepartmentIdsList
		{
			get { return new ReadonlyList(SelectedDepartmentIds); }
		}

		public void AddSelectedDepartmentIdItem(string departmentId)
		{
			SelectedDepartmentIds.Add(departmentId);
		}
	}

	[ComVisible(true)]
	[Guid("85844EC2-100B-46C5-94A7-3B75EE0DC862")]
	public interface IEmployeePositionPatch
	{
		string Position { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeePositionPatch")]
	[Guid("D9DC18F8-BD00-4255-B1A9-FC70C6F619B5")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeePositionPatch))]
	public partial class EmployeePositionPatch : SafeComObject, IEmployeePositionPatch
	{
	}

	[ComVisible(true)]
	[Guid("446D3936-7251-4C9F-9E87-91F4B9CC82F9")]
	public interface IEmployeeCanBeInvitedForChatPatch
	{
		bool CanBeInvitedForChat { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeeCanBeInvitedForChatPatch")]
	[Guid("528CD906-0122-4343-AC8E-4FB7E0722F7A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeeCanBeInvitedForChatPatch))]
	public partial class EmployeeCanBeInvitedForChatPatch : SafeComObject, IEmployeeCanBeInvitedForChatPatch
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
		void AddSubscriptionItem([MarshalAs(UnmanagedType.IDispatch)] object item);
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

		public void AddSubscriptionItem(object item)
		{
			Subscriptions.Add((Subscription) item);
		}
	}
}