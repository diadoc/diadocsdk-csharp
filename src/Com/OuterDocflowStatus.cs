using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.OuterDocflows
{
	[ComVisible(true)]
	[Guid("13E72D45-0EA2-44E4-844A-5EE08DEEFC0E")]
	public interface IStatus
	{
		string NamedId { get; }
		string FriendlyName { get; }
		Com.OuterStatusType OuterStatusType { get; }
		string Description { get; }
		ReadonlyList DetailsList { get; }
	}

	[ComVisible(true)]
	[Guid("0D125D16-CC36-4214-BDCB-C308FE3DD9E1")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IStatus))]
	public partial class Status : SafeComObject, IStatus
	{
		public Com.OuterStatusType OuterStatusType
		{
			get { return (Com.OuterStatusType)((int)Type); }
		}

		public ReadonlyList DetailsList
		{
			get { return new ReadonlyList(Details); }
		}
	}

	[ComVisible(true)]
	[Guid("6F894E9C-8DCC-4D39-A251-EF37A26778C3")]
	public interface IStatusDetail
	{
		string Code { get; }
		string Text { get; }
	}

	[ComVisible(true)]
	[Guid("BBA0B5FD-6924-4323-9C02-9348668D71A6")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IStatusDetail))]
	public partial class StatusDetail : SafeComObject, IStatusDetail
	{
	}
}
