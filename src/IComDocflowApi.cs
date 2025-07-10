using System.Runtime.InteropServices;
using Diadoc.Api.Proto.Docflow;
using Diadoc.Api.Proto.PartnerEvents;

namespace Diadoc.Api
{
	[ComVisible(true)]
	[Guid("1431A43D-64EF-4D14-95C9-8C75DDE3EDE9")]
	public interface IComDocflowApi
	{
		GetDocflowBatchResponseV3 GetDocflows(string authToken, string boxId, [MarshalAs(UnmanagedType.IDispatch)] object request);
		GetDocflowEventsResponseV3 GetDocflowEvents(string authToken, string boxId, [MarshalAs(UnmanagedType.IDispatch)] object request);
		SearchDocflowsResponseV3 SearchDocflows(string authToken, string boxId, [MarshalAs(UnmanagedType.IDispatch)] object request);
		GetDocflowsByPacketIdResponseV3 GetDocflowsByPacketId(string authToken, string boxId, [MarshalAs(UnmanagedType.IDispatch)] object request);
		GetDocflowBatchResponseV4 GetDocflowsV4(string authToken, string boxId, [MarshalAs(UnmanagedType.IDispatch)] object request);
		GetDocflowEventsResponseV4 GetDocflowEventsV4(string authToken, string boxId, [MarshalAs(UnmanagedType.IDispatch)] object request);
		SearchDocflowsResponseV4 SearchDocflowsV4(string authToken, string boxId, [MarshalAs(UnmanagedType.IDispatch)] object request);
		GetDocflowsByPacketIdResponseV4 GetDocflowsByPacketIdV4(string authToken, string boxId, [MarshalAs(UnmanagedType.IDispatch)] object request);
		GetPartnerEventsResponse GetPartnerEvents(string authToken, [MarshalAs(UnmanagedType.IDispatch)] object request);
	}
}
