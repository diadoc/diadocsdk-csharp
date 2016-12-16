using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("C83CC405-5F80-439C-8CD5-695D646D218E")]
	public enum SignerType
	{
		LegalEntity = 1,
		IndividualEntity = 2,
		PhysicalPerson = 3
	}
}