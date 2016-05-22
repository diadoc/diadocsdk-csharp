using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Diadoc.Api
{
	[ComVisible(true)]
	[Guid("8BEF2043-A8E6-47A2-8DE2-659578019726")]
	public interface IPrintFormResult
	{
		bool HasContent { get; }
		PrintFormContent Content { get; }
		int RetryAfter { get; }
	}

	[ComVisible(true)]
	[Guid("8E600F20-912B-4567-BCDB-EC6AEE8964E9")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPrintFormResult))]
	public class PrintFormResult : SafeComObject, IPrintFormResult
	{
		public PrintFormResult(PrintFormContent content)
		{
			Content = content;
			RetryAfter = 0;
		}

		public PrintFormResult(int retryAfter)
		{
			RetryAfter = retryAfter;
		}

		public bool HasContent { get { return Content != null; } }
		public PrintFormContent Content { get; private set; }
		public int RetryAfter { get; private set; }
	}

	[ComVisible(true)]
	[Guid("F2CF16EF-9100-411B-8ACF-7D9FE6BE09B4")]
	public interface IPrintFormContent
	{
		string ContentType { get; }
		string FileName { get; }
		byte[] Bytes { get; }
		void SaveToFile(string path);
	}

	[ComVisible(true)]
	[Guid("0736656F-4490-42AD-97A4-02D94603F378")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPrintFormContent))]
	public class PrintFormContent : SafeComObject, IPrintFormContent
	{
		public PrintFormContent(string contentType, string fileName, byte[] bytes)
		{
			ContentType = contentType;
			FileName = fileName;
			Bytes = bytes;
		}

		public string ContentType { get; private set; }
		public string FileName { get; private set; }
		public byte[] Bytes { get; private set; }

		public void SaveToFile(string path)
		{
			if (Bytes == null) throw new Exception("There is no content to save");
			File.WriteAllBytes(path, Bytes);
		}
	}
}
