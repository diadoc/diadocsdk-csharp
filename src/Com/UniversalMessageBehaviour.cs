using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("527D69FA-60D9-4D8E-8F40-7274371979C5")]
	public enum UniversalMessageBehaviour
	{
		Undefined = Proto.UniversalMessageBehaviour.Undefined,
		AffectsWorkflow = Proto.UniversalMessageBehaviour.AffectsWorkflow,
		DoesNotAffectWorkflow = Proto.UniversalMessageBehaviour.DoesNotAffectWorkflow,
	}
}
