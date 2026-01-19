using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Docflow
{
	[ComVisible(true)]
	[Guid("33DE52CE-F251-408C-A5DA-3F462E74B98A")]
	public interface IOutOfWorkflowUniversalMessageDocflow
	{
		ReadonlyList OutOfWorkflowUniversalMessagesList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.OutOfWorkflowUniversalMessageDocflow")]
	[Guid("41F973AB-1DAC-487F-9701-7BF947778A84")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IOutOfWorkflowUniversalMessageDocflow))]
	public partial class OutOfWorkflowUniversalMessageDocflow : SafeComObject, IOutOfWorkflowUniversalMessageDocflow
	{
		public ReadonlyList OutOfWorkflowUniversalMessagesList => new ReadonlyList(Messages);
	}
}
