using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("BAFACF30-4B77-4200-A539-504DEDA2F7A3")]
	public interface IDocflowStatusV3
	{
		DocflowStatusModelV3 PrimaryStatus { get; }
		DocflowStatusModelV3 SecondaryStatus { get; }
		PowersOfAttorney.PowerOfAttorneyValidationStatus PowerOfAttorneyGeneralStatus { get; }
	}

	[ComVisible(true)]
	[Guid("AB36BCA0-47DD-4D91-AF6A-D213A900E558")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocflowStatusV3))]
	public partial class DocflowStatusV3 : SafeComObject, IDocflowStatusV3
	{
	}

	[ComVisible(true)]
	[Guid("7E61D9E9-A2FC-4A04-B10D-30E891286D6A")]
	public interface IDocflowStatusModelV3
	{
		string Severity { get; }
		string StatusText { get; }
	}

	[ComVisible(true)]
	[Guid("F22FD85F-F149-4F53-9206-8E05A8784626")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocflowStatusModelV3))]
	public partial class DocflowStatusModelV3 : SafeComObject, IDocflowStatusModelV3
	{
	}
}
