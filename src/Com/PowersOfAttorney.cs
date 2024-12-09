using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.PowersOfAttorney
{
	[ComVisible(true)]
	[Guid("8CAE7E41-5306-431E-A9F3-37211755FCBE")]
	public interface IPowerOfAttorneyFullId
	{
		string RegistrationNumber { get; set; }
		string IssuerInn { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyFullId")]
	[Guid("5B40AE3C-0054-4791-9BB9-E0056DFD325A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyFullId))]
	public partial class PowerOfAttorneyFullId : SafeComObject, IPowerOfAttorneyFullId
	{
	}

	[ComVisible(true)]
	[Guid("8693BD64-97CF-41F1-A478-9A1CA417574C")]
	public interface IPowerOfAttorney
	{
		PowerOfAttorneyFullId FullId { get; set; }
		PowerOfAttorneyIssuer Issuer { get; set; }
		PowerOfAttorneyConfidant Confidant { get; set; }
		Timestamp StartAt { get; set; }
		Timestamp ExpireAt { get; set; }
		string System { get; set; }
		string IdFile { get; set; }
		ReadonlyList DelegationChainList { get; }
		PowerOfAttorneyPermissionsInfo PermissionsInfo { get; set; }
		PowerOfAttorneyDelegationInfo DelegationInfo { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorney")]
	[Guid("5415CFFF-140C-4AFB-A9A5-E7D8E9A1A420")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorney))]
	public partial class PowerOfAttorney : SafeComObject, IPowerOfAttorney
	{
		public ReadonlyList DelegationChainList
		{
			get { return new ReadonlyList(DelegationChain); }
		}
	}

	[ComVisible(true)]
	[Guid("55DFF897-C2FE-4A71-ADC2-7907C2190750")]
	public interface IPowerOfAttorneyIssuer
	{
		Com.PowerOfAttorneyIssuerType Type { get; set; }
		PowerOfAttorneyIssuerLegalEntity LegalEntity { get; set; }
		PowerOfAttorneyIssuerForeignEntity ForeignEntity { get; set; }
		PowerOfAttorneyIssuerIndividualEntity IndividualEntity { get; set; }
		PowerOfAttorneyIssuerPhysicalEntity PhysicalEntity { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyIssuer")]
	[Guid("BAD272F1-BC98-4CFE-9350-24ACBB1A0BBC")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyIssuer))]
	public partial class PowerOfAttorneyIssuer : SafeComObject, IPowerOfAttorneyIssuer
	{
		Com.PowerOfAttorneyIssuerType IPowerOfAttorneyIssuer.Type
		{
			get { return (Com.PowerOfAttorneyIssuerType) Type; }
			set { Type = (PowerOfAttorneyIssuerType) value; }
		}
	}

	[ComVisible(true)]
	[Guid("0707D3F6-EAEC-40D0-BC82-38E538D6DD2A")]
	public interface IPowerOfAttorneyIssuerLegalEntity
	{
		string Inn { get; set; }
		string Kpp { get; set; }
		string OrganizationName { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyIssuerLegalEntity")]
	[Guid("4096059F-021E-4D85-8D2D-E0EA00F82C78")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyIssuerLegalEntity))]
	public partial class PowerOfAttorneyIssuerLegalEntity : SafeComObject, IPowerOfAttorneyIssuerLegalEntity
	{
	}

	[ComVisible(true)]
	[Guid("8E935CF8-8276-4120-A188-86ACFB90584F")]
	public interface IPowerOfAttorneyIssuerForeignEntity
	{
		string Inn { get; set; }
		string Kpp { get; set; }
		string OrganizationName { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyIssuerForeignEntity")]
	[Guid("14950451-63B6-4632-BCB2-6ED7FF5131F3")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyIssuerForeignEntity))]
	public partial class PowerOfAttorneyIssuerForeignEntity : SafeComObject, IPowerOfAttorneyIssuerForeignEntity
	{
	}

	[ComVisible(true)]
	[Guid("F7D5AF94-80FB-4543-978D-E91A313CDDA1")]
	public interface IPowerOfAttorneyIssuerIndividualEntity
	{
		string Inn { get; set; }
		string OrganizationName { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyIssuerIndividualEntity")]
	[Guid("4A6A0D66-E1B3-40F1-976E-20AD7B0584A8")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyIssuerIndividualEntity))]
	public partial class PowerOfAttorneyIssuerIndividualEntity : SafeComObject, IPowerOfAttorneyIssuerIndividualEntity
	{
	}

	[ComVisible(true)]
	[Guid("26D685B7-E031-40FD-A1D3-69BB9C5E5F17")]
	public interface IPowerOfAttorneyIssuerPhysicalEntity
	{
		string Inn { get; set; }
		FullName PersonName { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyIssuerPhysicalEntity")]
	[Guid("59E1AEFB-A762-4231-A1E5-F7FF0B6ABB81")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyIssuerPhysicalEntity))]
	public partial class PowerOfAttorneyIssuerPhysicalEntity : SafeComObject, IPowerOfAttorneyIssuerPhysicalEntity
	{
	}

	[ComVisible(true)]
	[Guid("6C0B0D18-E6DA-4D3A-A0B3-68DFC222E8C8")]
	public interface IPowerOfAttorneyConfidant
	{
		FullName PersonName { get; set; }
		string Inn { get; set; }
		PowerOfAttorneyConfidantOrganization Organization { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyConfidant")]
	[Guid("BC1CE6C1-C213-48EE-839C-F6D1A6C1DCB5")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyConfidant))]
	public partial class PowerOfAttorneyConfidant : SafeComObject, IPowerOfAttorneyConfidant
	{
	}

	[ComVisible(true)]
	[Guid("E6EAA9F9-2EB1-4093-9952-954885430C43")]
	public interface IPowerOfAttorneyConfidantOrganization
	{
		string Inn { get; set; }
		string Kpp { get; set; }
		string Name { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyConfidantOrganization")]
	[Guid("29DFCDC1-4847-4245-93E0-4AEE2CFDF05A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyConfidantOrganization))]
	public partial class PowerOfAttorneyConfidantOrganization : SafeComObject, IPowerOfAttorneyConfidantOrganization
	{
	}

	[ComVisible(true)]
	[Guid("C1779391-DABC-4724-8FBC-48ECA36B0993")]
	public interface IPowerOfAttorneyPrevalidateRequest
	{
		ConfidantCertificateToPrevalidate ConfidantCertificate { get; set; }
		void SetConfidantCertificate([MarshalAs(UnmanagedType.IDispatch)] object confidantCertificate);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyPrevalidateRequest")]
	[Guid("835617CD-48C4-49C2-8B39-8D7F64B3BF49")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyPrevalidateRequest))]
	public partial class PowerOfAttorneyPrevalidateRequest : SafeComObject, IPowerOfAttorneyPrevalidateRequest
	{
		public void SetConfidantCertificate(object confidantCertificate)
		{
			ConfidantCertificate = (ConfidantCertificateToPrevalidate) confidantCertificate;
		}
	}

	[ComVisible(true)]
	[Guid("4BB1DB4A-957D-4473-87AE-394A020E1153")]
	public interface IConfidantCertificateToPrevalidate
	{
		string Thumbprint { get; set; }
		Content_v3 Content { get; set; }

		void SetContent([MarshalAs(UnmanagedType.IDispatch)] object content);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ConfidantCertificateToPrevalidate")]
	[Guid("85ED5BA5-F88C-487F-A9BC-0F50B1702D29")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IConfidantCertificateToPrevalidate))]
	public partial class ConfidantCertificateToPrevalidate : SafeComObject, IConfidantCertificateToPrevalidate
	{
		public void SetContent(object content)
		{
			Content = (Content_v3) content;
		}
	}

	[ComVisible(true)]
	[Guid("C32C2057-2EE8-4813-91C1-31787AD54C07")]
	public interface IPowerOfAttorneyPrevalidateResult
	{
		PowerOfAttorneyValidationStatus PrevalidateStatus { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyPrevalidateResult")]
	[Guid("38CB9559-81F3-4AA8-8A72-941DBA5836A4")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyPrevalidateResult))]
	public partial class PowerOfAttorneyPrevalidateResult : SafeComObject, IPowerOfAttorneyPrevalidateResult
	{
	}

	[ComVisible(true)]
	[Guid("BAA0604F-7111-47E5-880C-81B6FD6683CD")]
	public interface IValidationProtocol
	{
		ReadonlyList CheckResultsList { get; }
	}
	
	[ComVisible(true)]
	[ProgId("Diadoc.Api.ValidationProtocol")]
	[Guid("ECE41E66-4BFC-4C96-BB3A-AF7D9734927B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IValidationProtocol))]
	public partial class ValidationProtocol : SafeComObject, IValidationProtocol
	{
		public ReadonlyList CheckResultsList
		{
			get { return new ReadonlyList(CheckResults); }
		}
	}

	[ComVisible(true)]
	[Guid("26B870BC-D36B-4CA2-8C5B-903DE398CD28")]
	public interface IPowerOfAttorneyValidationStatus
	{
		Com.Severity Severity { get; }
		Com.PowerOfAttorneyValidationStatusNamedId StatusNamedId { get; }
		string StatusText { get; }
		[Obsolete]
		ReadonlyList ErrorsList { get; }
		ValidationProtocol ValidationProtocol { get; }
		PowerOfAttorneyValidationError OperationError { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyValidationStatus")]
	[Guid("FC779ADD-A511-4AA0-B911-F4803554562B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyValidationStatus))]
	public partial class PowerOfAttorneyValidationStatus : SafeComObject, IPowerOfAttorneyValidationStatus
	{
		Com.Severity IPowerOfAttorneyValidationStatus.Severity
		{
			get { return (Com.Severity) Severity; }
		}

		Com.PowerOfAttorneyValidationStatusNamedId IPowerOfAttorneyValidationStatus.StatusNamedId
		{
			get { return (Com.PowerOfAttorneyValidationStatusNamedId) StatusNamedId; }
		}

		public ReadonlyList ErrorsList
		{
			get { return new ReadonlyList(Errors); }
		}
	}

	[ComVisible(true)]
	[Guid("DEF90B88-40A7-4EA1-81CD-30D60F3F99FD")]
	public interface IPowerOfAttorneyValidationError
	{
		string Code { get; set; }
		string Text { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyValidationError")]
	[Guid("91FAF81E-AC57-4D32-B91B-CE45F328DF6B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyValidationError))]
	public partial class PowerOfAttorneyValidationError : SafeComObject, IPowerOfAttorneyValidationError
	{
	}
	
	[ComVisible(true)]
	[Guid("AC2185A0-5D6D-4371-A487-A75EC0627897")]
	public interface IValidationCheckResult
	{
		Com.PowerOfAttorneyValidationCheckStatus Status { get; }
		string Name { get; }
		
		PowerOfAttorneyValidationError Error { get; }
	}
	
	[ComVisible(true)]
	[ProgId("Diadoc.Api.ValidationCheckResult")]
	[Guid("04838F25-EFC4-4ACD-9789-731F07CAA1C9")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IValidationCheckResult))]
	public partial class ValidationCheckResult : SafeComObject, IValidationCheckResult
	{
		Com.PowerOfAttorneyValidationCheckStatus IValidationCheckResult.Status
		{
			get { return (Com.PowerOfAttorneyValidationCheckStatus) Status; }
		}
	}

	[ComVisible(true)]
	[Guid("8A664FAC-D82E-4E4A-8DC4-AA8C16D0763A")]
	public interface IPowerOfAttorneyToRegister
	{
		PowerOfAttorneyFullId FullId { get; set; }
		PowerOfAttorneySignedContent Content { get; set; }

		void SetFullId([MarshalAs(UnmanagedType.IDispatch)] object fullId);
		void SetContent([MarshalAs(UnmanagedType.IDispatch)] object content);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyToRegister")]
	[Guid("76417A13-5B84-4EDC-933A-330B042B84B3")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyToRegister))]
	public partial class PowerOfAttorneyToRegister : SafeComObject, IPowerOfAttorneyToRegister
	{
		public void SetFullId(object fullId)
		{
			FullId = (PowerOfAttorneyFullId) fullId;
		}

		public void SetContent(object content)
		{
			Content = (PowerOfAttorneySignedContent) content;
		}
	}

	[ComVisible(true)]
	[Guid("E3C5A91B-2552-433F-B095-5D97D784B404")]
	public interface IPowerOfAttorneySignedContent
	{
		Content_v3 Content { get; set; }
		Content_v3 Signature { get; set; }

		void SetContent([MarshalAs(UnmanagedType.IDispatch)] object content);
		void SetSignature([MarshalAs(UnmanagedType.IDispatch)] object signature);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneySignedContent")]
	[Guid("4DE57BCF-6375-41B0-9DD7-BFD86565779C")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneySignedContent))]
	public partial class PowerOfAttorneySignedContent : SafeComObject, IPowerOfAttorneySignedContent
	{
		public void SetContent(object content)
		{
			Content = (Content_v3) content;
		}

		public void SetSignature(object signature)
		{
			Signature = (Content_v3) signature;
		}
	}

	[ComVisible(true)]
	[Guid("6209DE7D-5B43-4F92-8F78-E24AD9E0E256")]
	public interface IPowerOfAttorneyRegisterResult
	{
		string OperationStatus { get; set; }
		PowerOfAttorney PowerOfAttorney { get; set; }
		PowerOfAttorneyStatus Status { get; set; }
		ReadonlyList ErrorsList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyRegisterResult")]
	[Guid("9FCCF5C3-4A6B-4112-9B40-1FABD01E950B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyRegisterResult))]
	public partial class PowerOfAttorneyRegisterResult : SafeComObject, IPowerOfAttorneyRegisterResult
	{
		public ReadonlyList ErrorsList
		{
			get { return new ReadonlyList(Errors); }
		}
	}

	[ComVisible(true)]
	[Guid("A0CF70AF-67F4-4B39-B058-956F2BAE5E27")]
	public interface IPowerOfAttorneyStatus
	{
		string Status { get; set; }
		Timestamp LastCheckAt { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyStatus")]
	[Guid("AAF34E07-2D02-44E5-A574-459A44060C28")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyStatus))]
	public partial class PowerOfAttorneyStatus : SafeComObject, IPowerOfAttorneyStatus
	{
	}

	[ComVisible(true)]
	[Guid("2178772B-F170-4CAE-872A-7F1A8DD0B8EC")]
	public interface IPowerOfAttorneyOperationError
	{
		string Code { get; set; }
		string Text { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyOperationError")]
	[Guid("E9C82338-7F45-46AD-BD97-BC7B45744E46")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyOperationError))]
	public partial class PowerOfAttorneyOperationError : SafeComObject, IPowerOfAttorneyOperationError
	{
	}

	[ComVisible(true)]
	[Guid("C13F573E-0EF3-4638-B83B-EE8BB3613627")]
	public interface IPowerOfAttorneyContent
	{
		byte[] Content { get; set; }
		byte[] Signature { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyContent")]
	[Guid("EBAAC087-F641-436E-85EF-243347F26886")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyContent))]
	public partial class PowerOfAttorneyContent : SafeComObject, IPowerOfAttorneyContent
	{
	}


	[ComVisible(true)]
	[Guid("4B9D4B86-C668-4D54-96F2-97CF1EBDDDA4")]
	public interface IPowerOfAttorneyContentV2
	{
		byte[] Content { get; set; }
		byte[] Signature { get; set; }
		PowerOfAttorneyFullId FullId { get; set; }
	}

	[ComVisible(true)]
	[Guid("8C805F16-DD4B-478C-BA50-1DBE48EC6DB0")]
	[ProgId("Diadoc.Api.PowerOfAttorneyContentV2")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyContentV2))]
	public partial class PowerOfAttorneyContentV2 : SafeComObject, IPowerOfAttorneyContentV2
	{
	}

	[ComVisible(true)]
	[Guid("9845E531-92E2-43D2-9AA1-8FB68EABC61E")]
	public interface IPowerOfAttorneyContentResponse
	{
		PowerOfAttorneyContentV2 Content { get; }
		ReadonlyList DelegationChainList { get; }
	}

	[ComVisible(true)]
	[Guid("F7FCAE63-4B24-46CA-8D09-1292ADB200B3")]
	[ProgId("Diadoc.Api.PowerOfAttorneyContentResponse")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyContentResponse))]
	public partial class PowerOfAttorneyContentResponse : SafeComObject, IPowerOfAttorneyContentResponse
	{
		public ReadonlyList DelegationChainList => new ReadonlyList(DelegationChain);
	}

	[ComVisible(true)]
	[Guid("AF93EA2C-7524-445C-B61F-5CC0A199C7A5")]
	public interface IPowerOfAttorneyPermissionsInfo
	{
		ReadonlyList PermissionsList { get; }
		string TransferPermissionLoss { get; set; }
		string JointPermissions { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyPermissionsInfo")]
	[Guid("20CE76E6-47B9-4C08-B934-8747201C8C36")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyPermissionsInfo))]
	public partial class PowerOfAttorneyPermissionsInfo : SafeComObject, IPowerOfAttorneyPermissionsInfo
	{
		public ReadonlyList PermissionsList
		{
			get { return new ReadonlyList(Permissions); }
		}
	}

	[ComVisible(true)]
	[Guid("F7E7EB7A-3D54-444C-A40D-B44F480A104E")]
	public interface IPowerOfAttorneyPermissions
	{
		string Type { get; set; }
		string TextPermission { get; set; }
		ReadonlyList MachineReadablePermissionList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyPermissions")]
	[Guid("51B68D13-0857-4E82-A92A-CF9304AD69DC")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyPermissions))]
	public partial class PowerOfAttorneyPermissions : SafeComObject, IPowerOfAttorneyPermissions
	{
		public ReadonlyList MachineReadablePermissionList
		{
			get { return new ReadonlyList(MachineReadablePermission); }
		}
	}

	[ComVisible(true)]
	[Guid("EB463FB5-FCFB-44EE-9319-1C6E13937ACA")]
	public interface IPowerOfAttorneyMachineReadablePermission
	{
		string Mnemonic { get; set; }
		string Code { get; set; }
		string Name { get; set; }
		ReadonlyList RestrictionsList { get; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyMachineReadablePermission")]
	[Guid("65BB8BA7-E38B-43CF-8BB1-D2D30FA10EE6")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyMachineReadablePermission))]
	public partial class PowerOfAttorneyMachineReadablePermission : SafeComObject, IPowerOfAttorneyMachineReadablePermission
	{
		public ReadonlyList RestrictionsList
		{
			get { return new ReadonlyList(Restrictions); }
		}
	}

	[ComVisible(true)]
	[Guid("31D91BD4-1521-4F85-A1C5-FD655C490135")]
	public interface IPowerOfAttorneyRestrictions
	{
		int Id { get; set; }
		string Code { get; set; }
		string Name { get; set; }
		string ValueName { get; set; }
		string ValueCode { get; set; }
		string ValueText { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyRestrictions")]
	[Guid("F5813D4C-52DF-454F-8134-636C97739A81")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyRestrictions))]
	public partial class PowerOfAttorneyRestrictions : SafeComObject, IPowerOfAttorneyRestrictions
	{
	}
	
	[ComVisible(true)]
	[Guid("D7F0A026-1580-480E-934F-89C90DF677FB")]
	public interface IPowerOfAttorneyDelegationInfo
	{
		string RootRegistrationNumber { get; set; }
		string ParentRegistrationNumber { get; set; }
		List<PowerOfAttorneyIssuer> RootIssuers { get; }
	}
	
	[ComVisible(true)]
	[ProgId("Diadoc.Api.PowerOfAttorneyDelegationInfo")]
	[Guid("2B4977C2-30AA-45F1-AFC3-032E6E751F4C")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPowerOfAttorneyDelegationInfo))]
	public partial class PowerOfAttorneyDelegationInfo : SafeComObject, IPowerOfAttorneyDelegationInfo
	{
	}
}

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("DAC091ED-7B0B-4E77-B1A7-3FF97914BA47")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "PowerOfAttorneyIssuerType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum PowerOfAttorneyIssuerType
	{
		UnknownIssuerType = 0,
		LegalEntity = 1,
		ForeignEntity = 2,
		IndividualEntity = 3,
		PhysicalEntity = 4
	}

	[ComVisible(true)]
	[Guid("FF262C58-A262-480B-B928-BE3BDCEE5D8B")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "PowerOfAttorneyValidationStatusNamedId", Namespace = "https://diadoc-api.kontur.ru")]
	public enum PowerOfAttorneyValidationStatusNamedId
	{
		UnknownStatus = 0,
		CanNotBeValidated = 1,
		IsValid = 2,
		IsNotValid = 3,
		ValidationError = 4,
		IsNotAttached = 5
	}

	[ComVisible(true)]
	[Guid("D7F903B8-E8DA-480F-A66E-9899DC89C6B2")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "PowerOfAttorneySendingType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum PowerOfAttorneySendingType
	{
		Unknown = 0,
		Metadata = 1,
		File = 2,
		DocumentContent = 3
	}

	[ComVisible(true)]
	[Guid("097F4C55-AB9A-4571-8A8E-3F1B0022CBF9")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "PowerOfAttorneyValidationCheckStatus", Namespace = "https://diadoc-api.kontur.ru")]
	public enum PowerOfAttorneyValidationCheckStatus
	{
		UnknownCheckStatus = 0,
		Ok = 1,
		Warning = 2,
		Error = 3
	}
}
