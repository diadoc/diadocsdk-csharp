using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Events
{
	[ComVisible(true)]
	[Guid("519FF0FA-75FF-419B-9DBE-B015B6F7CC45")]
	public interface IBoxCounteragentEventList
	{
		int TotalCount { get; }
		ReadonlyList EventsList { get; }
		Com.TotalCountType TotalCountTypeValue { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.BoxCounteragentEventList")]
	[Guid("43D67091-BEC8-4DAE-94B1-EEF1A5C729DB")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IBoxCounteragentEventList))]
	public partial class BoxCounteragentEventList : SafeComObject, IBoxCounteragentEventList
	{
		public ReadonlyList EventsList
		{
			get { return new ReadonlyList(Events); }
		}

		public Com.TotalCountType TotalCountTypeValue
		{
			get { return (Com.TotalCountType)TotalCountType; }
		}
	}
}
