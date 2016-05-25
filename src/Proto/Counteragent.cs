using System.Linq;
using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("6D52D398-C4C9-4ED0-B9A1-38D074C045C7")]
	public interface ICounteragentList
	{
		int TotalCount { get; }
		ReadonlyList CounteragentsList { get; }
	}

	[ComVisible(true)]
	[Guid("0165E953-29A3-40DA-9FCC-786ED727DA6A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (ICounteragentList))]
	public partial class CounteragentList : SafeComObject, ICounteragentList
	{
		public ReadonlyList CounteragentsList
		{
			get { return new ReadonlyList(Counteragents); }
		}

		public override string ToString()
		{
			return string.Join("\r\n", Counteragents.Select(c => c.ToString()).ToArray());
		}
	}

	[ComVisible(true)]
	[Guid("92320D36-B9E7-44A5-9F63-D799D42EAAF8")]
	public interface ICounteragent
	{
		string IndexKey { get; }
		Organization Organization { get; }
		long LastEventTimestampTicks { get; }
		string MessageFromCounteragent { get; }
		string MessageToCounteragent { get; }
		Com.CounteragentStatus CurrentCounteragentStatus { get; }
	}

	[ComVisible(true)]
	[Guid("839DA27C-80F7-45AB-A554-22C3DE3D624B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (ICounteragent))]
	public partial class Counteragent : SafeComObject, ICounteragent
	{
		public override string ToString()
		{
			return string.Format("IndexKey: {0}, Organization: {1}, CurrentStatus: {2}, LastEventTimestampTicks: {3}", IndexKey, Organization, CurrentStatus, LastEventTimestampTicks);
		}

		public Com.CounteragentStatus CurrentCounteragentStatus
		{
			get { return (Com.CounteragentStatus) CurrentStatus; }
		}
	}

	[ComVisible(true)]
	[Guid("0C809297-EFC6-4BBD-B952-678F55D3F2D2")]
	public interface ICounteragentCertificateList
	{
		ReadonlyList CertificatesList { get; }
	}

	[ComVisible(true)]
	[Guid("C5BCC37D-E415-4232-81F6-ED0B351EED81")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICounteragentCertificateList))]
	public partial class CounteragentCertificateList : SafeComObject, ICounteragentCertificateList
	{
		public ReadonlyList CertificatesList
		{
			get { return new ReadonlyList(Certificates); }
		}

		public override string ToString()
		{
			return string.Join("\r\n", Certificates.Select(c => c.ToString()).ToArray());
		}
	}

	[ComVisible(true)]
	[Guid("6550D3C1-1BB0-4043-B24B-1D98B529C363")]
	public interface ICertificate
	{
		byte[] RawCertificateData { get; }
	}

	[ComVisible(true)]
	[Guid("243319AA-3232-483A-BE8A-913747A971C7")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICertificate))]
	public partial class Certificate : SafeComObject, ICertificate
	{
	}
}