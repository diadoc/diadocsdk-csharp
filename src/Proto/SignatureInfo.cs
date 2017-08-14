using System;
using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("8774CD83-0E8E-4FEC-8AEA-3CB854FCCE56")]
	public interface ISignatureInfo
	{
		DateTime SigningDateTime { get; }
		DateTime SignatureVerificationDateTime { get; }
		SignatureVerificationResult SignatureVerificationResult { get; set; }
		string Thumbprint { get; set; }
		string SerialNumber { get; set; }
		string Issuer { get; set; }
		string StartDate { get; set; }
		string EndDate { get; set; }
		string OrgName { get; set; }
		string OrgInn { get; set; }
		string JobTitle { get; set; }
		string FirstName { get; set; }
		string Surname { get; set; }
		string Snils { get; set; }
		string Email { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SignatureInfo")]
	[Guid("0FE4F19C-70AA-47F3-99C9-E10268850382")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISignatureInfo))]
	public partial class SignatureInfo : SafeComObject, ISignatureInfo
	{
		public DateTime SigningDateTime
		{
			get { return new DateTime(SigningTime.Ticks, DateTimeKind.Utc); }
		}

		public DateTime SignatureVerificationDateTime
		{
			get { return new DateTime(SignatureVerificationTime.Ticks, DateTimeKind.Utc); }
		}
	}
}
