using System.Runtime.InteropServices;
using Diadoc.Api.Com;
using Diadoc.Api.Proto.PowersOfAttorney;

namespace Diadoc.Api.Proto.Employees.PowersOfAttorney
{
	[ComVisible(true)]
	[Guid("72AFDCF8-ED09-4774-8421-8310C45FDE7F")]
	public interface IEmployeePowerOfAttorneyList
	{
		ReadonlyList PowersOfAttorneyList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeePowerOfAttorneyList")]
	[Guid("AC99E3FA-81B5-41D0-8877-FA98C76DACDB")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeePowerOfAttorneyList))]
	public partial class EmployeePowerOfAttorneyList : SafeComObject, IEmployeePowerOfAttorneyList
	{
		public ReadonlyList PowersOfAttorneyList
		{
			get { return new ReadonlyList(PowersOfAttorney); }
		}
	}

	[ComVisible(true)]
	[Guid("78D65632-EC02-4FE1-83AD-A88A409E0125")]
	public interface IEmployeePowerOfAttorney
	{
		PowerOfAttorney PowerOfAttorney { get; }
		bool IsDefault { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeePowerOfAttorney")]
	[Guid("8329C467-BA0E-4949-9B80-8247B5442609")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeePowerOfAttorney))]
	public partial class EmployeePowerOfAttorney : SafeComObject, IEmployeePowerOfAttorney
	{
	}

	[ComVisible(true)]
	[Guid("0AF370C1-83F2-4A8E-8C6F-7348678932E1")]
	public interface IEmployeePowerOfAttorneyToUpdate
	{
		EmployeePowerOfAttorneyIsDefaultPatch IsDefaultPatch { get; set; }
		void SetIsDefaultPatch([MarshalAs(UnmanagedType.IDispatch)] object isDefaultPatch);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeePowerOfAttorneyToUpdate")]
	[Guid("1EF3A30D-D0EC-4069-AE97-B9BB641C7D68")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeePowerOfAttorneyToUpdate))]
	public partial class EmployeePowerOfAttorneyToUpdate : SafeComObject, IEmployeePowerOfAttorneyToUpdate
	{
		public void SetIsDefaultPatch(object isDefaultPatch)
		{
			IsDefaultPatch = (EmployeePowerOfAttorneyIsDefaultPatch) isDefaultPatch;
		}
	}
	
	[ComVisible(true)]
	[Guid("5aaa8da0-42b3-4c63-a0e4-097f64b83422")]
	public interface IEmployeePowerOfAttorneyToUpdateV2
	{
		PowerOfAttorneyFullId PowerOfAttorneyFullId { get; set; }
		EmployeePowerOfAttorneyIsDefaultPatch IsDefaultPatch { get; set; }
		void SetFullId([MarshalAs(UnmanagedType.IDispatch)] object fullId);
		void SetIsDefaultPatch([MarshalAs(UnmanagedType.IDispatch)] object isDefaultPatch);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeePowerOfAttorneyToUpdateV2")]
	[Guid("b3f84a7b-4a47-41c7-a106-fce70cea6f83")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeePowerOfAttorneyToUpdateV2))]
	public partial class EmployeePowerOfAttorneyToUpdateV2 : SafeComObject, IEmployeePowerOfAttorneyToUpdateV2
	{
		public void SetFullId(object fullId)
		{
			PowerOfAttorneyFullId = (PowerOfAttorneyFullId) fullId;
		}
		
		public void SetIsDefaultPatch(object isDefaultPatch)
		{
			IsDefaultPatch = (EmployeePowerOfAttorneyIsDefaultPatch) isDefaultPatch;
		}
	}

	[ComVisible(true)]
	[Guid("A63713EF-D9F1-4568-A98B-7B137A778717")]
	public interface IEmployeePowerOfAttorneyIsDefaultPatch
	{
		bool IsDefault { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EmployeePowerOfAttorneyIsDefaultPatch")]
	[Guid("10977096-67DD-4153-A876-45F4E3DEE396")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployeePowerOfAttorneyIsDefaultPatch))]
	public partial class EmployeePowerOfAttorneyIsDefaultPatch : SafeComObject, IEmployeePowerOfAttorneyIsDefaultPatch
	{
	}
}
