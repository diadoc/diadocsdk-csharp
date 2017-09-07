using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("7D65BE82-89C0-4D3E-9686-30224D55E390")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName="TaxRate", Namespace = "https://diadoc-api.kontur.ru")]
	public enum TaxRate
	{
		NoVat = 0,
		Percent_0 = 1,
		Percent_10 = 2,
		Percent_18 = 3,
		Percent_20 = 4,
		Fraction_10_110 = 5,
		Fraction_18_118 = 6,
		TaxRateNotSpecified = 7,
	}
}