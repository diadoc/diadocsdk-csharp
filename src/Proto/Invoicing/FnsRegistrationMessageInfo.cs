using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Invoicing
{
	[ComVisible(true)]
	[Guid("D6093ED1-DF17-452D-8437-B2F151D3B86F")]
	public interface IFnsRegistrationMessageInfo
	{
		ReadonlyList CertificatesList { get; }
		void AddCertificate(byte[] certificate);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.FnsRegistrationMessageInfo")]
	[Guid("A8C3FF24-02F3-4FDD-900B-24D5FEBB4431")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IFnsRegistrationMessageInfo))]
	public partial class FnsRegistrationMessageInfo : SafeComObject, IFnsRegistrationMessageInfo
	{
		public ReadonlyList CertificatesList
		{
			get { return new ReadonlyList(Certificates); }
		}

		public void AddCertificate(byte[] certificate)
		{
			Certificates.Add(certificate);
		}
	}
}