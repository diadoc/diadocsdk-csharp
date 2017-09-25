using System.Runtime.InteropServices;

namespace Diadoc.Api
{
	[ComVisible(true)]
	[Guid("DAB67C3A-6226-4460-ADC2-43953F2EECB2")]
	public interface IFileContent
	{
		byte[] Bytes { get; }
		string FileName { get; }
	}

	[ComVisible(true)]
	[Guid("E8C81684-6C5B-467D-87B9-D0DD7F5AEE90")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IFileContent))]
	public class FileContent : SafeComObject, IFileContent
	{
		public FileContent(byte[] bytes, string fileName)
		{
			Bytes = bytes;
			FileName = fileName;
		}

		public byte[] Bytes { get; private set; }
		public string FileName { get; private set; }
	}
}