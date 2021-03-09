using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{

	[ComVisible(true)]
	[Guid("0173E1F5-03B2-4BD5-B95A-747933EB58B6")]
	[XmlType(TypeName = "LockMode", Namespace = "https://diadoc-api.kontur.ru")]
    public enum LockMode
    {
		Unknown = Diadoc.Api.Proto.LockMode.Unknown,
		None = Diadoc.Api.Proto.LockMode.None,
		Send = Diadoc.Api.Proto.LockMode.Send,
		Full = Diadoc.Api.Proto.LockMode.Full
	}
  
}