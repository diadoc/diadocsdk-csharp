using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[ProgId("Diadoc.Api.GetOrganizationsByInnListRequest")]
	[Guid("E4F795AB-2C76-4FA8-97F9-902CEEF83481")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IGetOrganizationsByInnListRequest))]
	public partial class GetOrganizationsByInnListRequest : SafeComObject, IGetOrganizationsByInnListRequest
	{
		public void AddInn(string inn)
		{
			InnList.Add(inn);
		}
	}

	[ComVisible(true)]
	[Guid("B7078C38-0821-4171-ABE2-167AEF4B0970")]
	public interface IGetOrganizationsByInnListRequest
	{
		void AddInn(string inn);
	}

	[ComVisible(true)]
	[Guid("FA34221F-ABC3-4AE8-B986-BBCDB533B9AE")]
	public interface IOrganizationWithCounteragentStatus
	{
		IOrganization CounteragentOrganization { get; }
		Com.CounteragentStatus CounteragentStatusValue { get; }
	}

	[ComVisible(true)]
	[Guid("60B53C24-0E52-4790-AF40-BBAD0AB8D89E")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IOrganizationWithCounteragentStatus))]
	public partial class OrganizationWithCounteragentStatus : SafeComObject, IOrganizationWithCounteragentStatus
	{
		public IOrganization CounteragentOrganization
		{
			get
			{
				return Organization;
			}
		}

		public Com.CounteragentStatus CounteragentStatusValue
		{
			get { return (Com.CounteragentStatus)CounteragentStatus; }
		}
	}
}