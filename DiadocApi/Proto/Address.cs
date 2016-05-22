using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("A9E036B5-C6C4-4C3C-BDA8-BBBCC50AE04F")]
	public interface IAddress
	{
		RussianAddress RussianAddress { get; set; }
		ForeignAddress ForeignAddress { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.Address")]
	[Guid("0A53FE4F-0592-4195-A39F-66E700F7B573")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IAddress))]
	public partial class Address : SafeComObject, IAddress
	{
	}

	[ComVisible(true)]
	[Guid("5EC97880-0408-453F-AD5F-E6058F81FB53")]
	public interface IRussianAddress
	{
		string ZipCode { get; set; }
		string Region { get; set; }
		string Territory { get; set; }
		string City { get; set; }
		string Locality { get; set; }
		string Street { get; set; }
		string Building { get; set; }
		string Block { get; set; }
		string Apartment { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RussianAddress")]
	[Guid("A67573FA-9C4B-45D9-B174-966C131FD4C2")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRussianAddress))]
	public partial class RussianAddress : SafeComObject, IRussianAddress
	{
	}

	[ComVisible(true)]
	[Guid("D2020CED-06BD-4A73-AAD1-18D7CEEFF494")]
	public interface IForeignAddress
	{
		string Country { get; set; }
		string Address { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ForeignAddress")]
	[Guid("EBF9267A-7055-4C5E-890A-293AD61C5DCA")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IForeignAddress))]
	public partial class ForeignAddress : SafeComObject, IForeignAddress
	{
	}
}
