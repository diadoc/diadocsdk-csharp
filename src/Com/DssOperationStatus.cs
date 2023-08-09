using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("B67AD250-09EF-4A87-953B-288CCB437810")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "DssOperationStatus", Namespace = "https://diadoc-api.kontur.ru")]
	public enum DssOperationStatus
	{
		Unknown = 0,
		InProgress = 1,
		Completed = 2,
		CanceledByUser = 3,
		Timeout = 4,
		Crashed = 5,
		UserHasUnconfirmedOperation = 6,
		OperationRetryRequired = 7
	}
}
