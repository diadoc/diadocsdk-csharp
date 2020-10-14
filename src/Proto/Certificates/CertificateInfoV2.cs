using System;
using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Certificates
{
	[ComVisible(true)]
	[Guid("8C6A6EA7-4F94-4649-B0F7-B67740C9FC7F")]
	public interface ICertificateInfoV2
	{
		string Thumbprint { get; }
		Com.CertificateType CertificateType { get; }
		DateTime ValidFromDateTime { get; }
		DateTime ValidToDateTime { get; }
		DateTime PrivateKeyValidFromDateTime { get; }
		DateTime PrivateKeyValidToDateTime { get; }
		string OrganizationName { get; }
		string Inn { get; }
		string UserFirstName { get; }
		string UserMiddleName { get; }
		string UserLastName { get; }
		string UserShortName { get; }
		bool IsDefault { get; }
	}

	[ComVisible(true)]
	[Guid("7D8610AD-91AD-4A76-8CEB-C9A4A24B5070")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICertificateInfoV2))]
	public partial class CertificateInfoV2 : SafeComObject, ICertificateInfoV2
	{
		public Com.CertificateType CertificateType
		{
			get { return (Com.CertificateType) Type; }
		}

		public DateTime ValidFromDateTime
		{
			get { return new DateTime(ValidFrom, DateTimeKind.Utc); }
		}

		public DateTime ValidToDateTime
		{
			get { return new DateTime(ValidTo, DateTimeKind.Utc); }
		}

		public DateTime PrivateKeyValidFromDateTime
		{
			get { return new DateTime(PrivateKeyValidFrom, DateTimeKind.Utc); }
		}

		public DateTime PrivateKeyValidToDateTime
		{
			get { return new DateTime(PrivateKeyValidTo, DateTimeKind.Utc); }
		}
	}
}