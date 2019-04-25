using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Organizations
{
	[ComVisible(true)]
	[Guid("247C99B3-D949-4BC5-AC97-92B8D2EBFB30")]
	public interface IAutoBlockStatus
	{
		bool IsBlocked { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.AutoBlockStatus")]
	[Guid("4FCBA205-52ED-48D7-8771-0FDB3569630F")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IAutoBlockStatus))]
	public partial class AutoBlockStatus : SafeComObject, IAutoBlockStatus
	{
	}

	[ComVisible(true)]
	[Guid("2F4ABB59-D80A-491E-A226-DBA00E2CA7AF")]
	public interface IManualBlockStatus
	{
		bool IsBlocked { get; }
		long RequestedTicks { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ManualBlockStatus")]
	[Guid("4CEDA75D-5651-4509-97AC-AEFE514D7818")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IManualBlockStatus))]
	public partial class ManualBlockStatus : SafeComObject, IManualBlockStatus
	{
	}

	[ComVisible(true)]
	[Guid("90C384DB-5853-4F37-9C43-C67739F085B9")]
	public interface IBlockStatus
	{
		ManualBlockStatus ManualBlockStatus { get; }
		AutoBlockStatus AutoBlockStatus { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.BlockStatus")]
	[Guid("4C1FAD1E-FDC8-4080-B01F-18AC696E1B87")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IBlockStatus))]
	public partial class BlockStatus : SafeComObject, IBlockStatus
	{
	}

	[ComVisible(true)]
	[Guid("82A94D26-DB85-4466-971E-F0CF07B09EE6")]
	public interface IOrganizationFeatures
	{
		BlockStatus BlockStatus { get; }
		ReadonlyList FeaturesList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.OrganizationFeatures")]
	[Guid("7A37EDCC-8B23-4352-873D-539D89A483C0")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IOrganizationFeatures))]
	public partial class OrganizationFeatures : SafeComObject, IOrganizationFeatures
	{
		public ReadonlyList FeaturesList => new ReadonlyList(Features);
	}
}