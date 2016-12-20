using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("47A05513-9DBE-4C91-BEBC-876BF70F8BC5")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "CounteragentStatus", Namespace = "https://diadoc-api.kontur.ru")]
	public enum CounteragentStatus
	{
		UnknownCounteragentStatus = 0,
		IsMyCounteragent = 1,
		InvitesMe = 2,
		IsInvitedByMe = 3,
		RejectsMe = 5,
		IsRejectedByMe = 6,
		NotInCounteragentList = 7
	}
}