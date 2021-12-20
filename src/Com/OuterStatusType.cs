using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("AF03C0EF-FC3D-43E5-9E94-C6A874C71237")]
	[XmlType(TypeName = "OuterStatusType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum OuterStatusType
	{
		UnknownStatus = Proto.OuterDocflows.OuterStatusType.UnknownStatus,
		Normal = Proto.OuterDocflows.OuterStatusType.Normal,
		Success = Proto.OuterDocflows.OuterStatusType.Success,
		Warning = Proto.OuterDocflows.OuterStatusType.Warning,
		Error = Proto.OuterDocflows.OuterStatusType.Error
	}
}
