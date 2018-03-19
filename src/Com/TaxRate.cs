using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("7D65BE82-89C0-4D3E-9686-30224D55E390")]
	//NOTE: Это хотели, чтобы можно было использовать XML-сериализацию для классов
	[XmlType(TypeName = "TaxRate", Namespace = "https://diadoc-api.kontur.ru")]
	public enum TaxRate
	{
		NoVat = Proto.Invoicing.TaxRate.NoVat,
		Percent_0 = Proto.Invoicing.TaxRate.Percent_0,
		Percent_10 = Proto.Invoicing.TaxRate.Percent_10,
		Percent_18 = Proto.Invoicing.TaxRate.Percent_18,
		Percent_20 = Proto.Invoicing.TaxRate.Percent_20,
		Fraction_10_110 = Proto.Invoicing.TaxRate.Fraction_10_110,
		Fraction_18_118 = Proto.Invoicing.TaxRate.Fraction_18_118,
		TaxedByAgent = Proto.Invoicing.TaxRate.TaxedByAgent
	}
}