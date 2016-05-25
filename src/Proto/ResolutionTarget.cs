using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("A536CEA0-3009-4344-AA8C-A69240D210FE")]
	public interface IResolutionTarget
	{
		string Department { get; }
		string DepartmentId { get; }
		string User { get; }
		string UserId { get; }
	}

	[ComVisible(true)]
	[Guid("E12C20A0-AC54-4A00-A28C-63C15B17A82D")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IResolutionTarget))]
	public partial class ResolutionTarget : SafeComObject, IResolutionTarget
	{
		
	}
}
