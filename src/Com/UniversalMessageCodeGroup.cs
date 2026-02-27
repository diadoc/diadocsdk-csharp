using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("D9B6A0B8-6A9B-4E31-9E9B-0A6D4B6F3C21")]
	[XmlType(TypeName = "UniversalMessageCodeGroup", Namespace = "https://diadoc-api.kontur.ru")]
	public enum UniversalMessageCodeGroup
	{
		UnknownUniversalMessageCodeGroup = Proto.UniversalMessageCodeGroup.UnknownUniversalMessageCodeGroup,
		Receipt = Proto.UniversalMessageCodeGroup.Receipt,
		AmendmentRequest = Proto.UniversalMessageCodeGroup.AmendmentRequest,
		Rejection = Proto.UniversalMessageCodeGroup.Rejection,
		InformationMessage = Proto.UniversalMessageCodeGroup.InformationMessage
	}
}
