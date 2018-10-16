using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("63D6AE87-F026-4DAC-AD9B-A415C57B0B7B")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "SignerType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum DocumentTitleSignerType
	{
		None = 0,
		Signer = 1,
		ExtendedSigner = 2
	}
}