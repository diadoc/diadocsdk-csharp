using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Documents.BilateralDocument
{
	[ComVisible(true)]
	[Guid("06B8EC65-7749-410D-B713-445DDE01C020")]
	public interface ITrustConnectionRequestMetadata
	{
		Com.BilateralDocumentStatus Status { get; }
	}

	[ComVisible(true)]
	[Guid("ADB431BD-0A82-4463-8E08-C42D35EC712F")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ITrustConnectionRequestMetadata))]
	public partial class TrustConnectionRequestMetadata : SafeComObject, ITrustConnectionRequestMetadata
	{
		public Com.BilateralDocumentStatus Status
		{
			get { return (Com.BilateralDocumentStatus)((int)TrustConnectionRequestStatus); }
		}

	}

	[ComVisible(true)]
	[Guid("129D9547-DCB9-4295-90E2-E98AD24E5905")]
	public interface IBasicDocumentMetadata
	{
		Com.BilateralDocumentStatus Status { get; }
		string Total { get; }
		string Vat { get; }
		string Grounds { get; }
		Com.ReceiptStatus DocumentReceiptStatus { get; }
	}

	[ComVisible(true)]
	[Guid("FDDEB02B-4FB8-48B1-B801-604193EA5D00")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IBasicDocumentMetadata))]
	public partial class BasicDocumentMetadata : SafeComObject, IBasicDocumentMetadata
	{
		public Com.BilateralDocumentStatus Status { get { return (Com.BilateralDocumentStatus)((int)DocumentStatus); } }
		public Com.ReceiptStatus DocumentReceiptStatus { get { return (Com.ReceiptStatus)((int)ReceiptStatus); } }
	}

	[ComVisible(true)]
	[Guid("C556F4B7-59EE-42FB-967F-9D9DB7C4F152")]
	public interface IPriceListMetadata
	{
		Com.BilateralDocumentStatus Status { get; }
		string PriceListEffectiveDate { get; }
		string ContractDocumentDate { get; }
		string ContractDocumentNumber { get; }
		Com.ReceiptStatus DocumentReceiptStatus { get; }
	}

	[ComVisible(true)]
	[Guid("624B09DC-4632-4108-8982-CE6A491BF95E")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPriceListMetadata))]
	public partial class PriceListMetadata : SafeComObject, IPriceListMetadata
	{
		public Com.BilateralDocumentStatus Status { get { return (Com.BilateralDocumentStatus)((int)DocumentStatus); } }
		public Com.ReceiptStatus DocumentReceiptStatus { get { return (Com.ReceiptStatus)((int)ReceiptStatus); } }
	}

	[ComVisible(true)]
	[Guid("FE1764AC-FFBB-428B-A09F-2C95FB92A8C6")]
	public interface IContractMetadata
	{
		Com.BilateralDocumentStatus Status { get; }
		string ContractType { get; }
		string ContractPrice { get; }
		Com.ReceiptStatus DocumentReceiptStatus { get; }
	}

	[ComVisible(true)]
	[Guid("885E2935-E2DD-40BC-BCB1-E222C198F8D2")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IContractMetadata))]
	public partial class ContractMetadata : SafeComObject, IContractMetadata
	{
		public Com.BilateralDocumentStatus Status { get { return (Com.BilateralDocumentStatus)((int)DocumentStatus); } }
		public Com.ReceiptStatus DocumentReceiptStatus { get { return (Com.ReceiptStatus)((int)ReceiptStatus); } }
	}
}
