using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Certificates
{
	[ComVisible(true)]
	[Guid("7ED2CE25-AD3B-4D88-8C60-1AA9C4DD6314")]
	public interface ICertificateList
	{
		ReadonlyList CertificateInfosList { get; }
	}

	[ComVisible(true)]
	[Guid("2679E719-B6E1-4418-B778-5F783E49FC82")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICertificateList))]
	public partial class CertificateList : SafeComObject, ICertificateList
	{
		public ReadonlyList CertificateInfosList
		{
			get { return new ReadonlyList(Certificates); }
		}
	}
}