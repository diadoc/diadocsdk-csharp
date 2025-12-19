using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("E7B3C8F4-5E9F-4D71-8B6A-2B6E1A9C7F30")]
	public interface IUniversalMessageEvent
	{
		int StatusCode { get; }
		string PlainText { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.UniversalMessageEvent")]
	[Guid("2B5D7C1A-3E42-4F7C-9B21-8C9F2A1D4E5B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUniversalMessageEvent))]
	public partial class UniversalMessageEvent : SafeComObject, IUniversalMessageEvent
	{
	}

	[ComVisible(true)]
	[Guid("A1C2E9F7-6B3A-4E4C-9F1D-2B6C7D8E9F10")]
	public interface IUniversalMessageCreator
	{
		string BoxId { get; }
		string DepartmentId { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.UniversalMessageCreator")]
	[Guid("5D2B3C4A-6F7E-4A8B-9C0D-1E2F3A4B5C6D")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUniversalMessageCreator))]
	public partial class UniversalMessageCreator : SafeComObject, IUniversalMessageCreator
	{
	}

	[ComVisible(true)]
	[Guid("B0F8D2C5-6A1A-4C2A-9D7E-0D9F8B2C3A11")]
	public interface IUniversalMessageInfo
	{
		Com.UniversalMessageCodeGroup CodeGroupValue { get; }
		ReadonlyList EventsList { get; }
		UniversalMessageCreator UniversalMessageCreator { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.UniversalMessageInfo")]
	[Guid("3A8D9C12-7F3B-4B44-8D6F-1E2C3B4A5D6E")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUniversalMessageInfo))]
	public partial class UniversalMessageInfo : SafeComObject, IUniversalMessageInfo
	{
		public Com.UniversalMessageCodeGroup CodeGroupValue => (Com.UniversalMessageCodeGroup) CodeGroup;

		public ReadonlyList EventsList => new ReadonlyList(Events);
	}
}
