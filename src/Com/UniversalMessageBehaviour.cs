using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("527D69FA-60D9-4D8E-8F40-7274371979C5")]
	[XmlType(TypeName = "UniversalMessageBehaviour", Namespace = "https://diadoc-api.kontur.ru")]
	public enum UniversalMessageBehaviour
	{
		Undefined = Proto.UniversalMessageBehaviour.Undefined,
		AffectsWorkflow = Proto.UniversalMessageBehaviour.AffectsWorkflow,
		DoesNotAffectWorkflow = Proto.UniversalMessageBehaviour.DoesNotAffectWorkflow,
	}
}
