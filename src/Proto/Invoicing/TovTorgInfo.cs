using System.Runtime.InteropServices;
using Diadoc.Api.Com;
using Diadoc.Api.Proto.Invoicing.Signers;

namespace Diadoc.Api.Proto.Invoicing
{
	[ComVisible(true)]
	[Guid("964A6EB7-CCF7-4256-8586-EDD2C29E6C91")]
	public interface ITovTorgSellerTitleInfo
	{
		Organizations.ExtendedOrganizationInfo Seller { get; set; }
		Organizations.ExtendedOrganizationInfo Buyer { get; set; }
		Organizations.ExtendedOrganizationInfo Shipper { get; set; }
		Organizations.ExtendedOrganizationInfo Consignee { get; set; }
		Organizations.ExtendedOrganizationInfo Carrier { get; set; }
		string Currency { get; set; }
		string CurrencyRate { get; set; }
		string DocumentDate { get; set; }
		string DocumentNumber { get; set; }
		string RevisionDate { get; set; }
		string RevisionNumber { get; set; }
		TovTorgTransferInfo TransferInfo { get; set; }
		string DocumentCreator { get; set; }
		string DocumentCreatorBase { get; set; }
		string OperationType { get; set; }
		string GovernmentContractInfo { get; set; }
		TovTorgTable Table { get; set; }
		AdditionalInfoId AdditionalInfoId { get; set; }
		string DocumentName { get; set; }

		ReadonlyList SignersList { get; }
		void AddSigner([MarshalAs(UnmanagedType.IDispatch)] object signer);

		ReadonlyList BasesList { get; }
		void AddBase([MarshalAs(UnmanagedType.IDispatch)] object baseInfo);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.TovTorgSellerTitleInfo")]
	[Guid("10EBA1DF-641C-4F47-9F73-14CB3D9F262B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ITovTorgSellerTitleInfo))]
	public partial class TovTorgSellerTitleInfo : SafeComObject, ITovTorgSellerTitleInfo
	{
		public ReadonlyList SignersList { get { return new ReadonlyList(Signers);} }
		public void AddSigner([MarshalAs(UnmanagedType.IDispatch)] object signer)
		{
			Signers.Add((ExtendedSigner)signer);
		}

		public ReadonlyList BasesList { get { return new ReadonlyList(Bases);} }
		public void AddBase([MarshalAs(UnmanagedType.IDispatch)] object baseInfo)
		{
			Bases.Add((TransferBase)baseInfo);
		}
	}

	[ComVisible(true)]
	[Guid("F4FD2FF2-B375-4F3C-B7C7-BA658967976E")]
	public interface ITovTorgBuyerTitleInfo
	{
		string DocumentCreator { get; set; }
		string DocumentCreatorBase { get; set; }
		string OperationCode { get; set; }
		string OperationContent { get; set; }
		string AcceptanceDate { get; set; }
		Employee Employee { get; set; }
		OtherIssuer OtherIssuer { get; set; }
		AdditionalInfoId AdditionalInfoId { get; set; }

		ReadonlyList SignersList { get; }
		void AddSigner([MarshalAs(UnmanagedType.IDispatch)] object signer);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.TovTorgBuyerTitleInfo")]
	[Guid("D0E91E3A-F06C-4831-B9F5-CDBD70AB6B09")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ITovTorgBuyerTitleInfo))]
	public partial class TovTorgBuyerTitleInfo : SafeComObject, ITovTorgBuyerTitleInfo
	{
		public ReadonlyList SignersList { get { return new ReadonlyList(Signers); } }
		public void AddSigner([MarshalAs(UnmanagedType.IDispatch)] object signer)
		{
			Signers.Add((ExtendedSigner)signer);
		}
	}

	[ComVisible(true)]
	[Guid("E8C55A06-CC22-4ECC-A5EE-02008AB4FF6E")]
	public interface ITovTorgTransferInfo
	{
		string OperationInfo { get; set; }
		string TransferDate { get; set; }
		string Attachment { get; set; }
		Employee Employee { get; set; }
		OtherIssuer OtherIssuer { get; set; }

		ReadonlyList WaybillsList { get; }
		void AddWaybill([MarshalAs(UnmanagedType.IDispatch)] object waybill);

		ReadonlyList AdditionalInfosList { get; }
		void AddAdditionalInfo([MarshalAs(UnmanagedType.IDispatch)] object additionalInfo);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.TovTorgTransferInfo")]
	[Guid("2AF6848D-E2CC-444A-9731-47726505CB96")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ITovTorgTransferInfo))]
	public partial class TovTorgTransferInfo : SafeComObject, ITovTorgTransferInfo
	{
		public ReadonlyList WaybillsList { get { return new ReadonlyList(Waybills); } }
		public void AddWaybill([MarshalAs(UnmanagedType.IDispatch)] object waybill)
		{
			Waybills.Add((Waybill)waybill);
		}

		public ReadonlyList AdditionalInfosList { get { return new ReadonlyList(AdditionalInfos); } }
		public void AddAdditionalInfo([MarshalAs(UnmanagedType.IDispatch)] object additionalInfo)
		{
			AdditionalInfos.Add((AdditionalInfo)additionalInfo);
		}
	}

	[ComVisible(true)]
	[Guid("5DD97486-9874-463E-BDEC-5B0C4DC7A27A")]
	public interface ITovTorgTable
	{
		string TotalQuantity { get; set; }
		string TotalGross { get; set; }
		string TotalNet { get; set; }
		string TotalWithVatExcluded { get; set; }
		string TotalVat { get; set; }
		string Total { get; set; }

		ReadonlyList ItemsList { get; }
		void AddItem([MarshalAs(UnmanagedType.IDispatch)] object item);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.TovTorgTable")]
	[Guid("AB15C04D-C53C-4260-B9F4-0A2721BF5A58")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ITovTorgTable))]
	public partial class TovTorgTable : SafeComObject, ITovTorgTable
	{
		public ReadonlyList ItemsList { get { return new ReadonlyList(Items);} }
		public void AddItem([MarshalAs(UnmanagedType.IDispatch)] object item)
		{
			Items.Add((TovTorgItem)item);
		}
	}

	[ComVisible(true)]
	[Guid("ECA9B78C-7BAF-4C19-A69B-6925B261BD41")]
	public interface ITovTorgItem
	{
		string Product { get; set; }
		string Feature { get; set; }
		string Sort { get; set; }
		string VendorCode { get; set; }
		string ProductCode { get; set; }
		string UnitName { get; set; }
		string Unit { get; set; }
		string PackageType { get; set; }
		string QuantityInPack { get; set; }
		string Quantity { get; set; }
		string Gross { get; set; }
		string Net { get; set; }
		string ItemToRelease { get; set; }
		string Price { get; set; }
		string SubtotalWithVatExcluded { get; set; }
		Com.TaxRate TaxRateValue { get; set; }
		string Vat { get; set; }
		string Subtotal { get; set; }
		string ItemAccountDebit { get; set; }
		string ItemAccountCredit { get; set; }

		ReadonlyList AdditionalInfosList { get; }
		void AddAdditionalInfo([MarshalAs(UnmanagedType.IDispatch)] object additionalInfo);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.TovTorgItem")]
	[Guid("2D119E4C-727F-4869-8C20-D7D19EFF10F4")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ITovTorgItem))]
	public partial class TovTorgItem : SafeComObject, ITovTorgItem
	{
		public Com.TaxRate TaxRateValue
		{
			get { return (Com.TaxRate) TaxRate; }
			set { TaxRate = (TaxRate) value; }
		}

		public ReadonlyList AdditionalInfosList { get { return new ReadonlyList(AdditionalInfos);} }
		public void AddAdditionalInfo([MarshalAs(UnmanagedType.IDispatch)] object additionalInfo)
		{
			AdditionalInfos.Add((AdditionalInfo)additionalInfo);
		}
	}


}