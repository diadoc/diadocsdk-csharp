using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("D31ECD49-37D7-4163-8685-85D7331438E5")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "ProxySignatureStatus", Namespace = "https://diadoc-api.kontur.ru")]
	public enum ProxySignatureStatus
	{
		UnknownProxySignatureStatus = Diadoc.Api.Proto.Documents.ProxySignatureStatus.UnknownProxySignatureStatus,
		ProxySignatureStatusNone = Diadoc.Api.Proto.Documents.ProxySignatureStatus.ProxySignatureStatusNone,
		WaitingForProxySignature = Diadoc.Api.Proto.Documents.ProxySignatureStatus.WaitingForProxySignature,
		WithProxySignature = Diadoc.Api.Proto.Documents.ProxySignatureStatus.WithProxySignature,
		ProxySignatureRejected = Diadoc.Api.Proto.Documents.ProxySignatureStatus.ProxySignatureRejected,
		InvalidProxySignature = Diadoc.Api.Proto.Documents.ProxySignatureStatus.InvalidProxySignature,
	}
}
