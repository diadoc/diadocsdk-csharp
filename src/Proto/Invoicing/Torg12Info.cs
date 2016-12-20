using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Invoicing
{
	[ComVisible(true)]
	[Guid("C6F1EA20-6BAD-4D71-9731-E57FC704906E")]
	public interface ITorg12SellerTitleInfo
	{
		string DocumentNumber { get; set; }
		string DocumentDate { get; set; }
		DocflowParticipant SellerDocflowParticipant { get; set; }
		DocflowParticipant BuyerDocflowParticipant { get; set; }
		OrganizationInfo Shipper { get; set; }
		OrganizationInfo Consignee { get; set; }
		OrganizationInfo Supplier { get; set; }
		OrganizationInfo Payer { get; set; }
		Grounds Grounds { get; set; }
		string WaybillDate { get; set; }
		string WaybillNumber { get; set; }
		string OperationCode { get; set; }
		string ParcelsQuantityTotal { get; set; }
		string ParcelsQuantityTotalInWords { get; set; }
		string GrossQuantityTotal { get; set; }
		string GrossQuantityTotalInWords { get; set; }
		string NetQuantityTotal { get; set; }
		string NetQuantityTotalInWords { get; set; }
		string QuantityTotal { get; set; }
		string TotalWithVatExcluded { get; set; }
		string Vat { get; set; }
		string Total { get; set; }
		string TotalInWords { get; set; }
		string SupplyDate { get; set; }
		Official SupplyAllowedBy { get; set; }
		Official SupplyPerformedBy { get; set; }
		Official ChiefAccountant { get; set; }
		Signer Signer { get; set; }
		string AdditionalInfo { get; set; }
		string AttachmentSheetsQuantity { get; set; }

		ReadonlyList ItemsList { get; }
		void AddItem([MarshalAs(UnmanagedType.IDispatch)] object item);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.Torg12SellerTitleInfo")]
	[Guid("2D875CE4-5E95-4EDF-A651-1D59A7FDD06F")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (ITorg12SellerTitleInfo))]
	public partial class Torg12SellerTitleInfo : SafeComObject, ITorg12SellerTitleInfo
	{
		ReadonlyList ITorg12SellerTitleInfo.ItemsList
		{
			get { return new ReadonlyList(Items); }
		}

		void ITorg12SellerTitleInfo.AddItem(object item)
		{
			Items.Add((Torg12Item) item);
		}
	}

	[ComVisible(true)]
	[Guid("88E6CACC-3804-4ECC-A656-5A940A97FF01")]
	public interface ITorg12BuyerTitleInfo
	{
		string ShipmentReceiptDate { get; set; }
		Attorney Attorney { get; set; }
		Official AcceptedBy { get; set; }
		Official ReceivedBy { get; set; }
		Signer Signer { get; set; }
		string AdditionalInfo { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.Torg12BuyerTitleInfo")]
	[Guid("C27A4238-614A-42E0-81F9-9EC45BC37BFE")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (ITorg12BuyerTitleInfo))]
	public partial class Torg12BuyerTitleInfo : SafeComObject, ITorg12BuyerTitleInfo
	{
	}

	[ComVisible(true)]
	[Guid("1A66E20D-373C-4B79-A070-B9E50A85C5B2")]
	public interface IDocflowParticipant
	{
		string BoxId { get; set; }
		string Inn { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocflowParticipant")]
	[Guid("4FA0BD1B-A19E-4E99-9283-FD6DE0E213CA")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IDocflowParticipant))]
	public partial class DocflowParticipant : SafeComObject, IDocflowParticipant
	{
	}

	[ComVisible(true)]
	[Guid("3DE15B04-9803-423D-B0A4-8655B7A64B5B")]
	public interface ITorg12Item
	{
		string Name { get; set; }
		string Feature { get; set; }
		string Sort { get; set; }
		string NomenclatureArticle { get; set; }
		string Code { get; set; }
		string UnitCode { get; set; }
		string UnitName { get; set; }
		string ParcelType { get; set; }
		string ParcelCapacity { get; set; }
		string ParcelsQuantity { get; set; }
		string GrossQuantity { get; set; }
		string Quantity { get; set; }
		string Price { get; set; }
		string TaxRate { get; set; }
		string SubtotalWithVatExcluded { get; set; }
		string Vat { get; set; }
		string Subtotal { get; set; }
		string AdditionalInfo { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.Torg12Item")]
	[Guid("B91CCE76-E5C1-4012-87C7-BFBBD97B57DE")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (ITorg12Item))]
	public partial class Torg12Item : SafeComObject, ITorg12Item
	{
	}

	[ComVisible(true)]
	[Guid("4EA98624-6603-4612-BB23-9CEA3AA423D1")]
	public interface IGrounds
	{
		string DocumentName { get; set; }
		string DocumentNumber { get; set; }
		string DocumentDate { get; set; }
		string AdditionalInfo { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.Grounds")]
	[Guid("FAD2DC66-BFE0-46BB-AB05-63683E6AB497")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IGrounds))]
	public partial class Grounds : SafeComObject, IGrounds
	{
	}
}