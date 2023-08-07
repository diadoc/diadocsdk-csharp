using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("7B7F18EB-E490-40C7-88F0-B653E8460590")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "DssConfirmType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum DssConfirmType
	{
		ConfirmTypeUnknown = -1,
		None = 0,
		Sms = 1,
		MyDss = 2,
		Applet = 3,
		MobileSdk = 4
	}
}
