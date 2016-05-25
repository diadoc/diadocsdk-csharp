using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Documents
{
	[ComVisible(true)]
	[Guid("C04B249A-7FEA-4C4D-A273-AEEDA147AF4F")]
	public interface IDocumentList
	{
		int TotalCount { get; }
		ReadonlyList DocumentsList { get; }
	}

	[ComVisible(true)]
	[Guid("4E7F2FF8-ECD0-43B6-A89D-041715BA5AD6")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentList))]
	public partial class DocumentList : SafeComObject, IDocumentList
	{
		public ReadonlyList DocumentsList { get { return new ReadonlyList(Documents); } }
	}
}
