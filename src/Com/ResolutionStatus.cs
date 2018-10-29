using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("66FBA265-01ED-4B1A-8756-73ED6114EA92")]
	public enum ResolutionStatus
	{
		UnknownStatus = Proto.Docflow.ResolutionStatus.UnknownStatus,
		None = Proto.Docflow.ResolutionStatus.None,
		Approved = Proto.Docflow.ResolutionStatus.Approved,
		Disapproved = Proto.Docflow.ResolutionStatus.Disapproved,
		ApprovementRequested = Proto.Docflow.ResolutionStatus.ApprovementRequested,
		ApprovementSignatureRequested = Proto.Docflow.ResolutionStatus.ApprovementSignatureRequested,
		PrimarySignatureRequested = Proto.Docflow.ResolutionStatus.PrimarySignatureRequested,
		SignatureRequestRejected = Proto.Docflow.ResolutionStatus.SignatureRequestRejected,
		SignedWithApprovingSignature = Proto.Docflow.ResolutionStatus.SignedWithApprovingSignature,
		SignedWithPrimarySignature = Proto.Docflow.ResolutionStatus.SignedWithPrimarySignature,
		PrimarySignatureRejected = Proto.Docflow.ResolutionStatus.PrimarySignatureRejected
	}
}