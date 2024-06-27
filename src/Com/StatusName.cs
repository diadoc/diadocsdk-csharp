using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("31005758-E76A-4E64-9BAA-8E78C380243B")]
	[XmlType(TypeName = "StatusName", Namespace = "https://diadoc-api.kontur.ru")]
	public enum StatusName
	{
		Unknown = Proto.Docflow.StatusName.Unknown,
		PowerOfAttorneyAttached = Proto.Docflow.StatusName.PowerOfAttorneyAttached,
		PowerOfAttorneyNotRequired = Proto.Docflow.StatusName.PowerOfAttorneyNotRequired,
		PowerOfAttorneyRequired = Proto.Docflow.StatusName.PowerOfAttorneyRequired
	}
}
