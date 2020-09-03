using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("AF03C0EF-FC3D-43E5-9E94-C6A874C71237")]
	[XmlType(TypeName = "OuterStatusType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum OuterStatusType
	{
		UnknownStatus = Proto.OuterStatusType.UnknownStatus,
		Normal = Proto.OuterStatusType.Normal,
		Success = Proto.OuterStatusType.Success,
		Warning = Proto.OuterStatusType.Warning,
		Error = Proto.OuterStatusType.Error
	}
}
