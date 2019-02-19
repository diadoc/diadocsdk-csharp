using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("05D18CB8-D0DC-41B6-B2C9-FBEE460EFF5B")]
	public enum ResolutionAction
	{

		UnknownAction = Proto.ResolutionAction.UnknownAction,
		ApproveAction = Proto.ResolutionAction.ApproveAction,
		DisapproveAction = Proto.ResolutionAction.DisapproveAction,
		SignWithApprovementSignature = Proto.ResolutionAction.SignWithApprovementSignature,
		SignWithPrimarySignature = Proto.ResolutionAction.SignWithPrimarySignature,
		DenySignatureRequest = Proto.ResolutionAction.DenySignatureRequest,
		RejectSigning = Proto.ResolutionAction.RejectSigning
	}
}