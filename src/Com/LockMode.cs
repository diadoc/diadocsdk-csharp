using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
    [ComVisible(true)]
    [Guid("6E6F5B96-4044-4703-8FC7-57033C6CC2D8")]
    [XmlType(TypeName = "LockMode", Namespace = "https://diadoc-api.kontur.ru")]
    public enum LockMode
    {
        Unknown = Proto.LockMode.Unknown,
        None = Proto.LockMode.None,
        Send = Proto.LockMode.Send,
        Full = Proto.LockMode.Full
    }
}