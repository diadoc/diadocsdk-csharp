using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("493B6FF2-0488-4D1B-AAE5-FBDCA0E8F509")]
	public interface IRoamingSendingStatus
	{
		Com.Severity Severity { get; }
		Com.RoamingSendingStatusNamedId StatusNamedId { get; }
		string StatusText { get; }
		ReadonlyList ErrorsList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RoamingSendingStatus")]
	[Guid("95AF61C2-CD66-493C-B00C-3B6E7C3EAB70")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRoamingSendingStatus))]
	public partial class RoamingSendingStatus : SafeComObject, IRoamingSendingStatus
	{
		Com.Severity IRoamingSendingStatus.Severity
		{
			get { return (Com.Severity) Severity; }
		}

		Com.RoamingSendingStatusNamedId IRoamingSendingStatus.StatusNamedId
		{
			get { return (Com.RoamingSendingStatusNamedId) StatusNamedId; }
		}

		public ReadonlyList ErrorsList
		{
			get { return new ReadonlyList(Errors); }
		}
	}

	[ComVisible(true)]
	[Guid("9CE2C9D5-5A1F-46E8-9570-7A2DC44022AF")]
	public interface IRoamingSendingError
	{
		string Code { get; set; }
		string Text { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RoamingSendingError")]
	[Guid("1026ECDE-03ED-41D4-B526-D0ECE80FFB6C")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRoamingSendingError))]
	public partial class RoamingSendingError : SafeComObject, IRoamingSendingError
	{
	}
}

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("511EABD0-D3F6-41B5-A34C-EFED601C6E24")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "RoamingSendingStatusNamedId", Namespace = "https://diadoc-api.kontur.ru")]
	public enum RoamingSendingStatusNamedId
	{
		UnknownStatus = 0,
		IsSent = 1,
		SendingError = 2,
	}
}
