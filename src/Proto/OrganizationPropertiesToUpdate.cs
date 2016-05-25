using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("2C9C8072-C5F7-48D9-B68C-4ED865A2CB9B")]
	public interface IStringValue
	{
		string Value { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.StringValue")]
	[Guid("7202E87C-1CF9-4165-9BBC-C42EBF365566")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IStringValue))]
	public partial class StringValue : SafeComObject, IStringValue
	{
		public StringValue(string value)
		{
			Value = value;
		}
	}

	[ComVisible(true)]
	[Guid("09E0E22E-DE42-4602-8CF4-29C937857E84")]
	public interface IOrganizationPropertiesToUpdate
	{
		string OrgId { get; set; }
		StringValue Ogrn { get; set; }
		StringValue IfnsCode { get; set; }
		Address Address { get; set; }
		HeadOrganizationPropertiesToUpdate HeadOrganizationProperties { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.OrganizationPropertiesToUpdate")]
	[Guid("4F0D20CE-2A07-45FE-B089-EA39FD27A8E4")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IOrganizationPropertiesToUpdate))]
	public partial class OrganizationPropertiesToUpdate : SafeComObject, IOrganizationPropertiesToUpdate
	{
		public OrganizationPropertiesToUpdate(string orgId)
		{
			OrgId = orgId;
		}
	}

	[ComVisible(true)]
	[Guid("E6D9FB90-2ED8-4409-B674-CE9F3A256D0C")]
	public interface IHeadOrganizationPropertiesToUpdate
	{
		StringValue Kpp { get; set; }
		StringValue FullName { get; set; }
		Address Address { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.HeadOrganizationPropertiesToUpdate")]
	[Guid("D13AC3E7-47F8-4EBE-BC4C-4AE448ABD0E9")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IHeadOrganizationPropertiesToUpdate))]
	public partial class HeadOrganizationPropertiesToUpdate : SafeComObject, IHeadOrganizationPropertiesToUpdate
	{
	}
}
