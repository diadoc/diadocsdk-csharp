using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Invoicing.Signers
{
	[ComVisible(true)]
	[Guid("CF559AA6-C353-473F-B595-AC634EC5B28D")]
	public interface IExtendedSigner
	{
		string BoxId { get; set; }
		byte[] SignerCertificate { get; set; }
		string SignerCertificateThumbprint { get; set; }
		ExtendedSignerDetails SignerDetails { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ExtendedSigner")]
	[Guid("DEDACEE7-9280-4FBA-9CFF-DC84E7C5718B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IExtendedSigner))]
	public partial class ExtendedSigner
		: SafeComObject
		, IExtendedSigner
	{
	}

	[ComVisible(true)]
	[Guid("43B13368-E3EE-4D3A-BAA2-C53822CE97EB")]
	public interface IExtendedSignerDetails
	{
		string Surname { get; set; }
		string FirstName { get; set; }
		string Patronymic { get; set; }
		string JobTitle { get; set; }
		string Inn { get; set; }
		string RegistrationCertificate { get; set; }
		Com.SignerType SignerTypeValue { get; set; }
		Com.SignerPowers SignerPowersValue { get; set; }
		Com.SignerStatus SignerStatusValue { get; set; }
		string SignerOrganizationName { get; set; }
		string SignerInfo { get; set; }
		string SignerPowersBase { get; set; }
		string SignerOrgPowersBase { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ExtendedSignerDetails")]
	[Guid("9179E323-2421-4AFD-AE9E-2425727C5CD6")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IExtendedSignerDetails))]
	public partial class ExtendedSignerDetails
		: SafeComObject, IExtendedSignerDetails
	{
		Com.SignerType IExtendedSignerDetails.SignerTypeValue
		{
			get { return (Com.SignerType)(int)SignerType; }
			set { SignerType = (SignerType)(int)value; }
		}

		Com.SignerPowers IExtendedSignerDetails.SignerPowersValue
		{
			get { return (Com.SignerPowers)(int)SignerPowers; }
			set { SignerPowers = (SignerPowers)(int)value; }
		}

		Com.SignerStatus IExtendedSignerDetails.SignerStatusValue
		{
			get { return (Com.SignerStatus)(int)SignerStatus; }
			set { SignerStatus = (SignerStatus)(int)value; }
		}
	}
}