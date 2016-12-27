using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("F67D8D54-FE62-4A7C-9BEE-40122DD083FD")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "SearchScope", Namespace = "https://diadoc-api.kontur.ru")]
	public enum SearchScope
	{

		SearchScopeAny = Diadoc.Api.Proto.Docflow.SearchScope.SearchScopeAny,
		SearchScopeIncoming = Diadoc.Api.Proto.Docflow.SearchScope.SearchScopeIncoming,
		SearchScopeOutgoing = Diadoc.Api.Proto.Docflow.SearchScope.SearchScopeOutgoing,
		SearchScopeDeleted = Diadoc.Api.Proto.Docflow.SearchScope.SearchScopeDeleted,
		SearchScopeInternal = Diadoc.Api.Proto.Docflow.SearchScope.SearchScopeInternal
	}
}