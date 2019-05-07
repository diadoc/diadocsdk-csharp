using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Proto.Registration
{
	[ComVisible(true)]
	[Guid("7990F335-05D6-4F33-A63F-26A3F0F70BE6")]
	public interface IRegistrationRequest
	{
		byte[] CertificateContent { get; set; }
		string Thumbprint { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RegistrationRequest")]
	[Guid("2AB66402-97B3-4859-9548-670CDC819C0E")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRegistrationRequest))]
	public partial class RegistrationRequest : SafeComObject, IRegistrationRequest
	{
	}

	[ComVisible(true)]
	[Guid("B3CDACFC-9EB0-4564-BA8F-D5F68155FCAC")]
	public interface IRegistrationConfirmRequest
	{
		byte[] CertificateContent { get; set; }
		string Thumbprint { get; set; }
		byte[] DataToSign { get; set; }
		byte[] Signature { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RegistrationConfirmRequest")]
	[Guid("26BE5F53-E620-4394-B013-753247F9CBF3")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRegistrationConfirmRequest))]
	public partial class RegistrationConfirmRequest : SafeComObject, IRegistrationConfirmRequest
	{
	}

	[ComVisible(true)]
	[Guid("6834099F-6DEC-4AE8-9CD4-006D4FEB77CE")]
	public interface IRegistrationResponse
	{
		Com.RegistrationStatus RegistrationStatusValue { get; }
		string BoxId { get; }
		byte[] DataToSign { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.RegistrationResponse")]
	[Guid("F6E7226A-2EBF-4839-A247-6900ED457BFF")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRegistrationResponse))]
	public partial class RegistrationResponse : SafeComObject, IRegistrationResponse
	{
		public Com.RegistrationStatus RegistrationStatusValue
		{
			get { return (Com.RegistrationStatus) RegistrationStatus; }
			set { RegistrationStatus = (RegistrationStatus) value; }
		}
	}
}

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("D3D81DDE-D171-4392-98FA-D2A75802F689")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "RegistrationStatus", Namespace = "https://diadoc-api.kontur.ru")]
	public enum RegistrationStatus
	{
		Unknown = Proto.Registration.RegistrationStatus.Unknown,
		AccessIsDenied = Proto.Registration.RegistrationStatus.AccessIsDenied,
		AccessRequestIsRejected = Proto.Registration.RegistrationStatus.AccessRequestIsRejected,
		CertificateOwnershipProofIsRequired = Proto.Registration.RegistrationStatus.CertificateOwnershipProofIsRequired,
		CertificateIsNotQualified = Proto.Registration.RegistrationStatus.CertificateIsNotQualified,
		RegistrationIsCompleted = Proto.Registration.RegistrationStatus.RegistrationIsCompleted,
		RegistrationIsInProgress = Proto.Registration.RegistrationStatus.RegistrationIsInProgress,
		RegistrationInBranchIsForbidden = Proto.Registration.RegistrationStatus.RegistrationInBranchIsForbidden,
		AccessRequestIsPending = Proto.Registration.RegistrationStatus.AccessRequestIsPending,
		OrganizationNotFound = Proto.Registration.RegistrationStatus.OrganizationNotFound
	}
}