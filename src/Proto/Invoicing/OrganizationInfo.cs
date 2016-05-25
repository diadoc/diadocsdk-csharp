using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Invoicing
{
	[ComVisible(true)]
	[Guid("21EC1E9C-7022-465C-A047-47BFAC89FEC6")]
	public interface IDiadocOrganizationInfo
	{
		string BoxId { get; set; }
		OrganizationInfo OrgInfo { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DiadocOrganizationInfo")]
	[Guid("8319DA7A-6024-415F-A3B8-EC281A8FE46B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDiadocOrganizationInfo))]
	public partial class DiadocOrganizationInfo : SafeComObject, IDiadocOrganizationInfo
	{
	}

	[ComVisible(true)]
	[Guid("ACA8614F-81B4-4CEA-BBC0-6FBE0BAC7A49")]
	public interface IOrganizationInfo
	{
		string Name { get; set; }
		string Inn { get; set; }
		string Kpp { get; set; }
		Address Address { get; set; }
		bool IsSoleProprietor { get; set; }
		string Okopf { get; set; }
		string Okpo { get; set; }
		string Okdp { get; set; }
		string Phone { get; set; }
		string Fax { get; set; }
		string BankAccountNumber { get; set; }
		string BankName { get; set; }
		string BankId { get; set; }
		string Department { get; set; }
		string FnsParticipantId { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.OrganizationInfo")]
	[Guid("43BD2094-A062-4BFD-922F-FB7C80CDB7E2")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IOrganizationInfo))]
	public partial class OrganizationInfo : SafeComObject, IOrganizationInfo
	{
	}
}
