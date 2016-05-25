using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Documents
{
	[ComVisible(true)]
	[Guid("E4435583-6966-4563-BF11-A381E6D3F890")]
	public interface IDocumentProtocol
	{
		byte[] PrintForm { get; }
		byte[] Signature { get; }
	}

	[ComVisible(true)]
	[Guid("ABF9ADFD-26E5-4C45-A1C0-9D1615DF256D")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentProtocol))]
	public partial class DocumentProtocol: SafeComObject, IDocumentProtocol
	{
	}

	[ComVisible(true)]
	[Guid("6DD4CFDC-748E-435D-A74E-169080DCC2C2")]
	public interface IDocumentProtocolResult
	{
		bool HasContent { get; }
		int RetryAfter { get; }
		IDocumentProtocol Protocol { get; }
	}

	[ComVisible(true)]
	[Guid("A4FB96DF-B8BE-485A-9A0A-BCA8DFE5CF58")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentProtocolResult))]
	public class DocumentProtocolResult : SafeComObject, IDocumentProtocolResult
	{
		public DocumentProtocolResult(IDocumentProtocol protocol)
		{
			Protocol = protocol;
			RetryAfter = 0;
		}

		public DocumentProtocolResult(int retryAfter)
		{
			RetryAfter = retryAfter;
		}

		public bool HasContent { get { return Protocol != null; } }
		public int RetryAfter { get; private set; }
		public IDocumentProtocol Protocol { get; private set; }
	}
}