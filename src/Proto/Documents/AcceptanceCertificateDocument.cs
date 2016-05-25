using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Documents.AcceptanceCertificateDocument
{
	[ComVisible(true)]
	[Guid("6481A7F3-EFAC-488F-A1C5-EAA921A2A16D")]
	public interface IAcceptanceCertificateMetadata
	{
		Com.AcceptanceCertificateDocumentStatus Status { get; }
		string Total { get; }
		string Vat { get; }
		string Grounds { get; }
		Com.ReceiptStatus DocumentReceiptStatus { get; }
	}

	[ComVisible(true)]
	[Guid("0C187F9C-1AC5-4B11-AA85-BA1E78FC04BE")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IAcceptanceCertificateMetadata))]
	public partial class AcceptanceCertificateMetadata : SafeComObject, IAcceptanceCertificateMetadata
	{
		public Com.AcceptanceCertificateDocumentStatus Status
		{
			get
			{
				return (Com.AcceptanceCertificateDocumentStatus)((int)DocumentStatus);
			}
		}

		public Com.ReceiptStatus DocumentReceiptStatus { get { return (Com.ReceiptStatus)((int)ReceiptStatus); } }
	}
}