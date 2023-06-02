using JetBrains.Annotations;

namespace Diadoc.Api.DataXml.CommonSignerUserContract
{
	public partial class Signers
	{
		private Signers()
		{
		}

		public Signers(Signer[] signers)
		{
			Signer = signers;
		}
	}

	public partial class Signer
	{
		private Signer()
		{
		}

		public Signer(SignerDetails signerDetails)
		{
			Item = signerDetails;
		}

		public Signer(SignerReference signerReference)
		{
			Item = signerReference;
		}
	}

	public partial class SignerDetails
	{
		private SignerDetails()
		{
		}

		public SignerDetails(
			string lastName,
			string firstName,
			[CanBeNull] string middleName,
			string position,
			[CanBeNull] string registrationCertificate,
			[CanBeNull] string signerOrganizationName,
			SignerDetailsSignerPowers? signerPowers,
			[CanBeNull] string signerPowersBase,
			[CanBeNull] string signerOrgPowersBase,
			SignerDetailsSignerStatus? signerStatus,
			SignerDetailsSignerType? signerType,
			[CanBeNull] string signerInfo,
			[CanBeNull] string inn)
		{
			LastName = lastName;
			FirstName = firstName;
			MiddleName = middleName;
			Position = position;
			RegistrationCertificate = registrationCertificate;
			SignerOrganizationName = signerOrganizationName;
			SignerPowers = signerPowers ?? default;
			SignerPowersSpecified = signerPowers != null;
			SignerPowersBase = signerPowersBase;
			SignerOrgPowersBase = signerOrgPowersBase;
			SignerStatus = signerStatus ?? default;
			SignerStatusSpecified = signerStatus != null;
			SignerType = signerType ?? default;
			SignerTypeSpecified = signerType != null;
			SignerInfo = signerInfo;
			Inn = inn;
		}
	}

	public partial class PowerOfAttorneyFullId
	{
		private PowerOfAttorneyFullId()
		{
		}

		public PowerOfAttorneyFullId(string registrationNumber, string issuerInn)
		{
			RegistrationNumber = registrationNumber;
			IssuerInn = issuerInn;
		}
	}

	public partial class PowerOfAttorney
	{
		private PowerOfAttorney()
		{
		}

		public PowerOfAttorney(PowerOfAttorneyFullId fullId, PowerOfAttorneyUseDefault useDefault)
		{
			FullId = fullId;
			UseDefault = useDefault;
		}
	}

	public partial class SignerReference
	{
		private SignerReference()
		{
		}

		public SignerReference(PowerOfAttorney powerOfAttorney, string boxId, string certificateThumbprint, byte[] certificateBytes)
		{
			PowerOfAttorney = powerOfAttorney;
			BoxId = boxId;
			CertificateThumbprint = certificateThumbprint;
			CertificateBytes = certificateBytes;
		}
	}
}
