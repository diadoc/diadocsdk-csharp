using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("FB300E42-1ABF-4553-8882-3692ACA21F48")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "RoamingNotificationStatus", Namespace = "https://diadoc-api.kontur.ru")]
	public enum RoamingNotificationStatus
	{
		UnknownRoamingNotificationStatus = Diadoc.Api.Proto.Documents.RoamingNotificationStatus.UnknownRoamingNotificationStatus,
		RoamingNotificationStatusNone = Diadoc.Api.Proto.Documents.RoamingNotificationStatus.RoamingNotificationStatusNone,
		RoamingNotificationStatusSuccess = Diadoc.Api.Proto.Documents.RoamingNotificationStatus.RoamingNotificationStatusSuccess,
		RoamingNotificationStatusError = Diadoc.Api.Proto.Documents.RoamingNotificationStatus.RoamingNotificationStatusError
	}
}
