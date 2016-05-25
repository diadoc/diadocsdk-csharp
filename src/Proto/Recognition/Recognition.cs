using System.IO;
using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Recognition
{
	[ComVisible(true)]
	[Guid("AB8CB0F0-C53C-4B10-B0BB-02C0564C3EFF")]
	public interface IRecognized
	{
		bool RecognizedSuccessfully { get; }
		string RecognitionId { get; }
		string FileName { get; }
		string ErrorMessage { get; }
		byte[] Content { get; }
		RecognizedInvoice Invoice { get; }
		void SaveContentToFile(string path);
	}

	[ComVisible(true)]
	[Guid("B97446E7-A637-431D-8EB8-F1E76BE01AB6")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRecognized))]
	public partial class Recognized : SafeComObject, IRecognized
	{
		public bool RecognizedSuccessfully
		{
			get { return string.IsNullOrEmpty(ErrorMessage); }
		}

		public void SaveContentToFile(string path)
		{
			File.WriteAllBytes(path, Content);
		}
	}
}
