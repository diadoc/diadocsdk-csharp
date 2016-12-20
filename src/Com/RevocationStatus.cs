using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("8D421F65-3D2E-4994-AB89-028222D3E9A5")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "RevocationStatus", Namespace = "https://diadoc-api.kontur.ru")]
	public enum RevocationStatus
	{
		UnknownRevocationStatus = 0,
		RevocationStatusNone = 1,
		RevocationIsRequestedByMe = 2,
		RequestsMyRevocation = 3,
		RevocationAccepted = 4,
		RevocationRejected = 5
	}
}