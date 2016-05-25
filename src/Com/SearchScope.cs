using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("F67D8D54-FE62-4A7C-9BEE-40122DD083FD")]
	public enum SearchScope
	{

		SearchScopeAny = Diadoc.Api.Proto.Docflow.SearchScope.SearchScopeAny,
		SearchScopeIncoming = Diadoc.Api.Proto.Docflow.SearchScope.SearchScopeIncoming,
		SearchScopeOutgoing = Diadoc.Api.Proto.Docflow.SearchScope.SearchScopeOutgoing,
		SearchScopeDeleted = Diadoc.Api.Proto.Docflow.SearchScope.SearchScopeDeleted,
		SearchScopeInternal = Diadoc.Api.Proto.Docflow.SearchScope.SearchScopeInternal
	}
}