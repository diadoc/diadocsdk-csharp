using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Documents
{
	[ComVisible(true)]
	[Guid("7A0E4E36-4ECD-4CBB-A326-66CE219E4601")]
	public interface IDetectTitleResponse
	{
		ReadonlyList DocumentTitlesList { get; }
	}

	[ComVisible(true)]
	[Guid("38C6484E-78A5-4DD5-9C54-F761B26E2801")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDetectTitleResponse))]
	public partial class DetectTitleResponse : SafeComObject, IDetectTitleResponse
	{
		public ReadonlyList DocumentTitlesList
		{
			get { return new ReadonlyList(DocumentTitles); }
		}
	}

	[ComVisible(true)]
	[Guid("48594CB9-DB64-4252-A8E3-6631BFABE94F")]
	public interface IDetectedDocumentTitle
	{
		string TypeNamedId { get; }
		string Function { get; }
		string Version { get; }
		int TitleIndex { get; }
		ReadonlyList MetadataList { get; }
	}

	[ComVisible(true)]
	[Guid("7AB7A204-FC84-4C62-9D40-EF1CDE29F9E6")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDetectedDocumentTitle))]
	public partial class DetectedDocumentTitle : SafeComObject, IDetectedDocumentTitle
	{
		public ReadonlyList MetadataList
		{
			get { return new ReadonlyList(Metadata); }
		}
	}
}