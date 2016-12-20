using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("5E689AEA-C664-43ED-A934-134EDB8ADC2F")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов https://yt.skbkontur.ru/issue/ddsupport-373
	[XmlType(TypeName = "DocflowStatusSeverity", Namespace = "https://diadoc-api.kontur.ru")]
	public enum DocflowStatusSeverity
	{

		UnknownDocflowStatusSeverity = Diadoc.Api.Proto.Docflow.DocflowStatusSeverity.UnknownDocflowStatusSeverity,
		Info = Diadoc.Api.Proto.Docflow.DocflowStatusSeverity.Info,
		Success = Diadoc.Api.Proto.Docflow.DocflowStatusSeverity.Success,
		Warning = Diadoc.Api.Proto.Docflow.DocflowStatusSeverity.Warning,
		Error = Diadoc.Api.Proto.Docflow.DocflowStatusSeverity.Error
	}
}