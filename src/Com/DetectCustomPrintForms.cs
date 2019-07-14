using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("C6D9DD7A-5A2B-418F-92D3-3416C4EEF776")]
	public interface ICustomPrintFormDetectionResult
	{
		ReadonlyList ResultItems { get; }
	}

	[ComVisible(true)]
	[Guid("1791854C-F968-424A-8923-DCEAE56D43EA")]
	public interface ICustomPrintFormDetectionItemResult
	{
	}

	[ComVisible(true)]
	[Guid("11C3ED82-DD61-4511-AE79-F0143FAD7035")]
	public interface ICustomPrintFormDetectionRequest
	{
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.CustomPrintFormDetectionResult")]
	[Guid("DA08CDE2-6609-499D-B0B5-0A25339C0003")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICustomPrintFormDetectionResult))]
	public partial class CustomPrintFormDetectionResult : SafeComObject, ICustomPrintFormDetectionResult
	{
		public ReadonlyList ResultItems => new ReadonlyList(Items);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.CustomPrintFormDetectionItemResult")]
	[Guid("37BE67F6-3700-4F3B-B811-BC924BCA720C")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICustomPrintFormDetectionItemResult))]
	public partial class CustomPrintFormDetectionItemResult : SafeComObject, ICustomPrintFormDetectionItemResult
	{
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.CustomPrintFormDetectionRequest")]
	[Guid("1993990B-017E-42ED-8C1F-CFA3D888205F")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICustomPrintFormDetectionRequest))]
	public partial class CustomPrintFormDetectionRequest : SafeComObject, ICustomPrintFormDetectionRequest
	{
	}
}