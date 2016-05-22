using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Documents.UnilateralDocument
{
	[ComVisible(true)]
	[Guid("7EB31A92-103A-435D-9053-C35B622F50CB")]
	public interface IProformaInvoiceMetadata
	{
		Com.UnilateralDocumentStatus Status { get; }
		string Total { get; }
		string Vat { get; }
		string Grounds { get; }
		Com.ReceiptStatus DocumentReceiptStatus { get; }
	}

	[ComVisible(true)]
	[Guid("94649650-687D-4151-A200-9465DF5730C7")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IProformaInvoiceMetadata))]
	public partial class ProformaInvoiceMetadata : SafeComObject, IProformaInvoiceMetadata
	{
		public Com.UnilateralDocumentStatus Status { get { return (Com.UnilateralDocumentStatus)((int)DocumentStatus); } }
		public Com.ReceiptStatus DocumentReceiptStatus { get { return (Com.ReceiptStatus)((int)ReceiptStatus); } }
	}

	[ComVisible(true)]
	[Guid("8C85850A-CBDC-4465-8535-A576E352C0AC")]
	public interface IServiceDetailsMetadata
	{
		Com.UnilateralDocumentStatus Status { get; }
		Com.ReceiptStatus DocumentReceiptStatus { get; }
	}

	[ComVisible(true)]
	[Guid("0E9127DC-9442-4503-A682-C68C5E85461E")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IServiceDetailsMetadata))]
	public partial class ServiceDetailsMetadata : SafeComObject, IServiceDetailsMetadata
	{
		public Com.UnilateralDocumentStatus Status { get { return (Com.UnilateralDocumentStatus)((int)DocumentStatus); } }
		public Com.ReceiptStatus DocumentReceiptStatus { get { return (Com.ReceiptStatus)((int)ReceiptStatus); } }
	}
}
