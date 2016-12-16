using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Invoicing
{
	[ComVisible(true)]
	[Guid("2382317E-9E33-4C33-8D92-92C036E8E24F")]
	public interface ISigner
	{
		byte[] SignerCertificate { get; set; }
		SignerDetails SignerDetails { get; set; }
		string SignerCertificateThumbprint { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.Signer")]
	[Guid("9C4BCD03-2860-4B67-8DCA-412E7FDD8979")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISigner))]
	public partial class Signer : SafeComObject, ISigner
	{
	}

	[ComVisible(true)]
	[Guid("EDBC3A84-A61D-40AA-B3EA-D5AD4C144031")]
	public interface ISignerDetails
	{
		string Surname { get; set; }
		string FirstName { get; set; }
		string Patronymic { get; set; }
		string JobTitle { get; set; }
		string Inn { get; set; }
		string SoleProprietorRegistrationCertificate { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SignerDetails")]
	[Guid("449124AB-9A7F-464C-A6FE-1E9D476D146A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISignerDetails))]
	public partial class SignerDetails : SafeComObject, ISignerDetails
	{
	}
}
