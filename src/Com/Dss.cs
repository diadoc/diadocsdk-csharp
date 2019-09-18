using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Dss
{
	[ComVisible(true)]
	[Guid("9FD6F79C-26AB-4F1A-893C-D6CD02549033")]
	public interface IDssSignRequest
	{
		ReadonlyList FilesList { get; }
		void AddFileItem([MarshalAs(UnmanagedType.IDispatch)] object file);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DssSignRequest")]
	[Guid("72B4E6A4-B4F1-4DA4-A3B4-E5F7C8E624A2")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDssSignRequest))]
	public partial class DssSignRequest : SafeComObject, IDssSignRequest
	{
		public ReadonlyList FilesList
		{
			get { return new ReadonlyList(Files); }
		}

		public void AddFileItem(object file)
		{
			Files.Add((DssSignFile) file);
		}
	}

	[ComVisible(true)]
	[Guid("FBFB0B49-92B1-4A6E-9404-5285298B715B")]
	public interface IDssSignFile
	{
		Content_v3 Content { get; set; }
		string FileName { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DssSignFile")]
	[Guid("8F0C1F7C-FF35-44B8-B642-D13CB9A9B770")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDssSignFile))]
	public partial class DssSignFile : SafeComObject, IDssSignFile
	{
	}

	[ComVisible(true)]
	[Guid("6112D2B3-05B4-43E1-BDC8-0E68E0962976")]
	public interface IDssSignResult
	{
		Com.DssOperationStatus OperationStatus { get; }
		ReadonlyList FileSigningResultsList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DssSignResult")]
	[Guid("57C7C2BD-8677-4E4C-9467-54B50B5C6128")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDssSignResult))]
	public partial class DssSignResult : SafeComObject, IDssSignResult
	{
		public ReadonlyList FileSigningResultsList
		{
			get { return new ReadonlyList(FileSigningResults); }
		}

		Com.DssOperationStatus IDssSignResult.OperationStatus
		{
			get { return (Com.DssOperationStatus) OperationStatus; }
		}
	}

	[ComVisible(true)]
	[Guid("116A8398-BC41-43E8-B90A-6E0F58B08D56")]
	public interface IDssFileSigningResult
	{
		Com.DssFileSigningStatus FileSigningStatus { get; }
		byte[] Signature { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DssFileSigningResult")]
	[Guid("60AD361F-40D4-4E04-8D9D-CF02C3FA5E6E")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDssFileSigningResult))]
	public partial class DssFileSigningResult : SafeComObject, IDssFileSigningResult
	{
		Com.DssFileSigningStatus IDssFileSigningResult.FileSigningStatus
		{
			get { return (Com.DssFileSigningStatus) FileSigningStatus; }
		}
	}
}