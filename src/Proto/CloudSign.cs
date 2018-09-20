using System.Net;
using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("67C13483-041C-4269-9A53-BD9C5F71237D")]
	public interface ICloudSignRequest
	{
		ReadonlyList FilesList { get; }
		void AddFile([MarshalAs(UnmanagedType.IDispatch)] object file);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.CloudSignRequest")]
	[Guid("3B7D49D7-22CE-4E7E-B138-7F12575F8118")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICloudSignRequest))]
	public partial class CloudSignRequest : SafeComObject, ICloudSignRequest
	{
		public ReadonlyList FilesList
		{
			get { return new ReadonlyList(Files); }
		}

		public void AddFile([MarshalAs(UnmanagedType.IDispatch)] object file)
		{
			Files.Add((CloudSignFile)file);
		}
	}

	[ComVisible(true)]
	[Guid("2D555AF5-D05B-4CD0-858D-7B07A9D359E5")]
	public interface ICloudSignFile
	{
		Content_v2 Content { get; set; }
		string FileName { get; set; }
	}

	[ComVisible(true)]
	[Guid("5740E68E-ED44-4FC3-90FE-CD3B4E9888F1")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICloudSignFile))]
	public partial class CloudSignFile : SafeComObject, ICloudSignFile
	{
	}

	[ComVisible(true)]
	[Guid("6B62071D-3BE0-46C3-BA4F-62F35FB85719")]
	public interface ICloudSignResult
	{
		string Token { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.CloudSignResult")]
	[Guid("6509E8BC-385F-4036-9D8B-FCAE73D28727")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICloudSignResult))]
	public partial class CloudSignResult : SafeComObject, ICloudSignResult
	{
	}

	[ComVisible(true)]
	[Guid("C846A910-0A92-41A8-82C9-8C4A7A39C80D")]
	public interface ICloudSignConfirmResult
	{
		ReadonlyList SignaturesList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.CloudSignConfirmResult")]
	[Guid("64C2C140-895B-4C21-8D08-6154998F6890")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICloudSignConfirmResult))]
	public partial class CloudSignConfirmResult : SafeComObject, ICloudSignConfirmResult
	{
		public ReadonlyList SignaturesList
		{
			get { return new ReadonlyList(Signatures); }
		}
	}

	[ComVisible(true)]
	[Guid("B28C21AF-FE53-4327-A193-05F42A0CC614")]
	public interface IAutosignReceiptsResult
	{
		long SignedReceiptsCount { get; set; }
		string NextBatchKey { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.CloudSignConfirmResult")]
	[Guid("2467CFAD-BA7F-44D0-A6CB-0D38664BBB0F")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IAutosignReceiptsResult))]
	public partial class AutosignReceiptsResult : SafeComObject, IAutosignReceiptsResult
	{
	}
}