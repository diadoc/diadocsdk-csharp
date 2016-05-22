using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Diadoc.Api
{
	[ComVisible(true)]
	[Guid("678CFEDA-7CEC-473B-AE0A-49EF40664EEA")]
	public interface ICertificate
	{
		string Name { get; }
		string Subject { get; }
		string Thumbprint { get; }
		DateTime ValidFrom { get; }
		DateTime ValidTo { get; }
		byte[] Content { get; }
	}

	[ComVisible(true)]
	[Guid("63851302-C369-4A2B-AEF3-F5A31DA84CC8")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICertificate))]
	public class Certificate : SafeComObject, ICertificate
	{
		public Certificate(X509Certificate2 cert)
		{
			Name = cert.GetNameInfo(X509NameType.SimpleName, false);
			Subject = cert.Subject;
			Thumbprint = cert.Thumbprint;
			ValidFrom = cert.NotBefore;
			ValidTo = cert.NotAfter;
			Content = cert.RawData;
		}

		public string Name { get; private set; }
		public string Subject { get; private set; }
		public string Thumbprint { get; private set; }
		public DateTime ValidFrom { get; private set; }
		public DateTime ValidTo { get; private set; }
		public byte[] Content { get; private set; }
	}
}