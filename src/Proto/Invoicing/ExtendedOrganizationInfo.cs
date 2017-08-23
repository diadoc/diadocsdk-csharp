using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Invoicing.Organizations
{
	[ComVisible(true)]
	[Guid("10B0D9C8-6B4E-4D9A-A28F-80F11B438937")]
	public interface IExtendedOrganizationInfo
	{
		string BoxId { get; set; }
		string OrgName { get; set; }
		string Inn { get; set; }
		string Kpp { get; set; }
		Address Address { get; set; }
		string FnsParticipantId { get; set; }
		Com.OrgType OrgTypeValue { get; set; }
		string Okopf { get; set; }
		string Okpo { get; set; }
		string Okdp { get; set; }
		string Phone { get; set; }
		string Email { get; set; }
		string CorrespondentAccount { get; set; }
		string BankAccountNumber { get; set; }
		string BankName { get; set; }
		string BankId { get; set; }
		string Department { get; set; }
		string OrganizationAdditionalInfo { get; set; }
		string OrganizationOrPersonInfo { get; set; }
		string IndividualEntityRegistrationCertificate { get; set; }
		string Country { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ExtendedOrganizationInfo")]
	[Guid("25E6380F-2615-4961-B747-46537F2C3EC1")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IExtendedOrganizationInfo))]
	public partial class ExtendedOrganizationInfo
		: SafeComObject
		, IExtendedOrganizationInfo
	{
		public Com.OrgType OrgTypeValue
		{
			get { return (Com.OrgType)(int)OrgType; }
			set { OrgType = (OrgType)(int)value; }
		}
	}
}