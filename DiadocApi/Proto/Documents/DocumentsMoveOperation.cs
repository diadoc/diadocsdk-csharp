using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Documents
{
	[ComVisible(true)]
	[Guid("7EB82BEA-985D-4104-93B1-E2029A46AFE4")]
	public interface IDocumentsMoveOperation
	{
		string BoxId { get; set; }
		string ToDepartmentId { get; set; }
		void AddDocumentId([MarshalAs(UnmanagedType.IDispatch)] object documentId);
	}

	[ComVisible(true)]
	[ProgId("DiaDoc.Api.DocumentsMoveOperation")]
	[Guid("C8F9DED9-F9E4-4153-AB66-C9080F93A6D6")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentsMoveOperation))]
	public partial class DocumentsMoveOperation : SafeComObject, IDocumentsMoveOperation
	{
		public void AddDocumentId(object documentId)
		{
			DocumentIds.Add((DocumentId)documentId);
		}
	}
}
