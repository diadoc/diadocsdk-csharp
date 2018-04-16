using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("94C79296-8301-4886-A865-C45363FD71F7")]
	[XmlType(TypeName = "CustomDataPatchOperation", Namespace = "https://diadoc-api.kontur.ru")]
	public enum CustomDataPatchOperation
	{
		Set = 0,
		Remove = 1
	}
}