using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Documents.NonformalizedDocument
{
	[ComVisible(true)]
	[Guid("07F1F498-CED1-4816-8C6F-459A438DDB64")]
	public interface INonformalizedDocumentMetadata
	{
		Com.NonformalizedDocumentStatus Status { get; }
		Com.ReceiptStatus DocumentReceiptStatus { get; }
	}

	[ComVisible(true)]
	[Guid("88C6B11F-4A04-4DF3-8D87-37DD9E150489")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(INonformalizedDocumentMetadata))]
	public partial class NonformalizedDocumentMetadata : SafeComObject, INonformalizedDocumentMetadata
	{
		public Com.NonformalizedDocumentStatus Status { get { return (Com.NonformalizedDocumentStatus)((int)DocumentStatus); } }

		public Com.ReceiptStatus DocumentReceiptStatus { get { return (Com.ReceiptStatus)((int)ReceiptStatus); } }
	}
}
