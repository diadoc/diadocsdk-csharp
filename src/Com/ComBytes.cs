using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("AC96A3DD-E099-44CC-ABD5-11C303307159")]
	public interface IComBytes
	{
		byte[] Bytes { get; set; }

		void ReadFromFile(string path);
		void SaveToFile(string path);
		void ReadFromText(string text, string codePage);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ComBytes")]
	[Guid("97D998E3-E603-4450-A027-EA774091BE72")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IComBytes))]
	public class ComBytes : SafeComObject, IComBytes
	{
		public byte[] Bytes { get; set; }

		public void ReadFromFile(string path)
		{
			Bytes = File.ReadAllBytes(path);
		}
		
		public void ReadFromText(string text, string codePage)
		{
			if (string.IsNullOrEmpty(codePage)) codePage = "utf-8";
			var encoding = Encoding.GetEncoding(codePage);
			Bytes = encoding.GetBytes(text);
		}

		public void SaveToFile(string path)
		{
			if (Bytes != null)
			{
				File.WriteAllBytes(path, Bytes);
			}
		}
	}
}
