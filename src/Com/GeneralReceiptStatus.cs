using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("48DB6119-EAE4-4B4F-9170-F31E36AA3775")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "GeneralReceiptStatus", Namespace = "https://diadoc-api.kontur.ru")]
	public enum GeneralReceiptStatus
	{
		GeneralReceiptStatusUnknown = Diadoc.Api.Proto.Documents.GeneralReceiptStatus.GeneralReceiptStatusUnknown,
		GeneralReceiptStatusNotAcceptable = Diadoc.Api.Proto.Documents.GeneralReceiptStatus.GeneralReceiptStatusNotAcceptable,
		HaveToCreateReceipt = Diadoc.Api.Proto.Documents.GeneralReceiptStatus.HaveToCreateReceipt,
		WaitingForReceipt = Diadoc.Api.Proto.Documents.GeneralReceiptStatus.WaitingForReceipt,
		Finished = Diadoc.Api.Proto.Documents.GeneralReceiptStatus.Finished,
	}
}
