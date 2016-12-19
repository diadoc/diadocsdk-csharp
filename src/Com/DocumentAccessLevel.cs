using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("55D1F8E3-C075-46EA-94BE-47561C1BA4C7")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "DocumentAccessLevel", Namespace = "https://diadoc-api.kontur.ru")]
	public enum DocumentAccessLevel
	{
		UnknownDocumentAccessLevel = -1,
		DepartmentOnly = 0,
		DepartmentAndSubdepartments = 1,
		AllDocuments = 2,
		SelectedDepartments = 3,
	}
}