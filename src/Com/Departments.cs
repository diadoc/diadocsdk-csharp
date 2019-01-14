using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Departments
{
	[ComVisible(true)]
	[Guid("0D16B20A-9017-4516-BC4A-0CD00FDD3EF2")]
	public interface IDepartment
	{
		string Id { get; }
		string ParentDepartmentId { get; }
		string Name { get; }
		string Abbreviation { get; }
		string Kpp { get; }
		Address Address { get; }
		Routing Routing { get; }
		Timestamp CreationTimestamp { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.Department")]
	[Guid("7285E8DF-691F-4749-BF9B-19FBB22186E1")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDepartment))]
	public partial class Department : SafeComObject, IDepartment
	{
	}

	[ComVisible(true)]
	[Guid("B60516F1-CE77-4D16-AD0E-40E5A5394032")]
	public interface IDepartmentList
	{
		ReadonlyList DepartmentsList { get; }
		int TotalCount { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DepartmentList")]
	[Guid("E7FC50DC-7395-4B24-986F-49B7E344CC6C")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDepartmentList))]
	public partial class DepartmentList : SafeComObject, IDepartmentList
	{
		public ReadonlyList DepartmentsList => new ReadonlyList(Departments);
	}

	[ComVisible(true)]
	[Guid("374DB167-38C9-4E09-9194-9BB099A4BBCB")]
	public interface IDepartmentToCreate
	{
		string ParentDepartmentId { get; set; }
		string Name { get; set; }
		string Abbreviation { get; set; }
		string Kpp { get; set; }
		Address Address { get; set; }
		Routing Routing { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DepartmentToCreate")]
	[Guid("832C7880-7F25-4763-AB95-6B0BCFC3CCE2")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDepartmentToCreate))]
	public partial class DepartmentToCreate : SafeComObject, IDepartmentToCreate
	{
	}

	[ComVisible(true)]
	[Guid("6C9C3488-73EC-4780-BE42-4ABEB9C06D1A")]
	public interface IDepartmentToUpdate
	{
		ParentDepartmentPatch ParentDepartment { get; set; }
		DepartmentNamingPatch DepartmentNaming { get; set; }
		DepartmentKppPatch Kpp { get; set; }
		DepartmentAddressPatch Address { get; set; }
		DepartmentRoutingPatch Routing { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DepartmentToUpdate")]
	[Guid("85FE470E-B472-4AD7-88D4-A98D18189AF3")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDepartmentToUpdate))]
	public partial class DepartmentToUpdate : SafeComObject, IDepartmentToUpdate
	{
	}

	[ComVisible(true)]
	[Guid("96AC8B07-6BF2-467B-86D9-F3E8EF72B791")]
	public interface IParentDepartmentPatch
	{
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ParentDepartmentPatch")]
	[Guid("14883F25-EB8B-4182-B676-2A3863EFA124")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IParentDepartmentPatch))]
	public partial class ParentDepartmentPatch : SafeComObject, IParentDepartmentPatch
	{
	}

	[ComVisible(true)]
	[Guid("F47F2099-5A27-47A2-880B-CE8480A033FA")]
	public interface IDepartmentNamingPatch
	{
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DepartmentNamingPatch")]
	[Guid("4CD6F15E-CB57-4210-B5DA-50415FB41FA9")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDepartmentNamingPatch))]
	public partial class DepartmentNamingPatch : SafeComObject, IDepartmentNamingPatch
	{
	}

	[ComVisible(true)]
	[Guid("DAC8D95F-BA9D-4C68-86D2-80528F87C71A")]
	public interface IDepartmentKppPatch
	{
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DepartmentKppPatch")]
	[Guid("1BDF8752-8D43-4CBF-B68C-523AEC568BFF")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDepartmentKppPatch))]
	public partial class DepartmentKppPatch : SafeComObject, IDepartmentKppPatch
	{
	}

	[ComVisible(true)]
	[Guid("1F1994B6-3718-4ABD-B1EE-FEA03E7D8A6A")]
	public interface IDepartmentAddressPatch
	{
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DepartmentAddressPatch")]
	[Guid("F247AFA3-72B1-4FFF-9BB3-293BFF91B0D6")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDepartmentAddressPatch))]
	public partial class DepartmentAddressPatch : SafeComObject, IDepartmentAddressPatch
	{
	}

	[ComVisible(true)]
	[Guid("3610B29B-4931-4C14-83AA-395F054D7E76")]
	public interface IDepartmentRoutingPatch
	{
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DepartmentRoutingPatch")]
	[Guid("CBEA04DD-CB57-4676-AA27-47EB639D1633")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDepartmentRoutingPatch))]
	public partial class DepartmentRoutingPatch : SafeComObject, IDepartmentRoutingPatch
	{
	}

	[ComVisible(true)]
	[Guid("D53E336F-C6B6-4CCB-B945-1C7FEB4C7659")]
	public interface IRouting
	{
		bool Kpp { get; set; }
		bool Address { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.Routing")]
	[Guid("DE16C6AA-D450-43E1-820F-7A76963473E5")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRouting))]
	public partial class Routing : SafeComObject, IRouting
	{
	}
}