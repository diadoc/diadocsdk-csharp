using System;
using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Invoicing
{
	[ComVisible(true)]
	[Guid("86367C02-671A-4E8E-9079-6A0E5AB0EE45")]
	public interface IInvoiceInfo
	{
		string InvoiceDate { get; set; }
		string InvoiceNumber { get; set; }
		string InvoiceRevisionNumber { get; set; }
		string InvoiceRevisionDate { get; set; }

		string Currency { get; set; }

		DiadocOrganizationInfo Seller { get; set; }
		DiadocOrganizationInfo Buyer { get; set; }
		ShipperOrConsignee Shipper { get; set; }
		ShipperOrConsignee Consignee { get; set; }
		Signer Signer { get; set; }

		string Vat { get; set; }
		string TotalWithVatExcluded { get; set; }
		string Total { get; set; }
		string AdditionalInfo { get; set; }

		ReadonlyList ItemsList { get; }
		void AddItem([MarshalAs(UnmanagedType.IDispatch)] object item);
		ReadonlyList PaymentDocumentsList { get; }
		void AddPaymentDocument([MarshalAs(UnmanagedType.IDispatch)] object paymentDocument);

		string Version { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.InvoiceInfo")]
	[Guid("81E82560-B4EE-4D25-BC53-14437B45561C")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IInvoiceInfo))]
	public partial class InvoiceInfo : SafeComObject, IInvoiceInfo
	{
		public ReadonlyList ItemsList
		{
			get { return new ReadonlyList(Items); }
		}

		public ReadonlyList PaymentDocumentsList
		{
			get { return new ReadonlyList(PaymentDocuments); }
		}

		public void AddItem(object item)
		{
			Items.Add((InvoiceItem) item);
		}

		public void AddPaymentDocument(object paymentDocument)
		{
			PaymentDocuments.Add((PaymentDocumentInfo) paymentDocument);
		}

		string IInvoiceInfo.Version
		{
			get
			{
				switch (Version)
				{
					case InvoiceFormatVersion.DefaultInvoiceFormatVersion:
						return "";
					case InvoiceFormatVersion.v5_01:
						return "5.01";
					case InvoiceFormatVersion.v5_02:
						return "5.02";
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			set
			{
				if (string.Equals(value, "5.01"))
					Version = InvoiceFormatVersion.v5_01;
				else if (string.Equals(value, "5.02"))
					Version = InvoiceFormatVersion.v5_02;
				else
					Version = InvoiceFormatVersion.DefaultInvoiceFormatVersion;
			}
		}
	}

	[ComVisible(true)]
	[Guid("0D994605-F12B-4B5D-80DF-B94E68481F4B")]
	public interface IInvoiceItem
	{
		string Product { get; set; }
		string Unit { get; set; }
		string Quantity { get; set; }
		string Price { get; set; }
		string Excise { get; set; }
		Com.TaxRate TaxRateValue { get; set; }
		string SubtotalWithVatExcluded { get; set; }
		string Vat { get; set; }
		string Subtotal { get; set; }
		string AdditionalInfo { get; set; }

		ReadonlyList CountriesOfOriginList { get; }
		ReadonlyList CustomsDeclarationNumbersList { get; }
		void AddCountryOfOrigin(string countryOfOrigin);
		void AddCustomsDeclarationNumber(string customsDeclarationNumber);

		ReadonlyList CustomsDeclarationsList { get; }
		void AddCustomsDeclaration([MarshalAs(UnmanagedType.IDispatch)] object customsDeclaration);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.InvoiceItem")]
	[Guid("BC496867-8210-402E-8A23-DC7C44AF7A58")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IInvoiceItem))]
	public partial class InvoiceItem : SafeComObject, IInvoiceItem
	{
		public ReadonlyList CountriesOfOriginList
		{
			get { return new ReadonlyList(CountriesOfOrigin); }
		}

		public ReadonlyList CustomsDeclarationNumbersList
		{
			get { return new ReadonlyList(CustomsDeclarationNumbers); }
		}

		public void AddCountryOfOrigin(string countryOfOrigin)
		{
			CountriesOfOrigin.Add(countryOfOrigin);
		}

		public void AddCustomsDeclarationNumber(string customsDeclarationNumber)
		{
			CustomsDeclarationNumbers.Add(customsDeclarationNumber);
		}

		public Com.TaxRate TaxRateValue
		{
			get { return (Com.TaxRate) ((int) TaxRate); }
			set { TaxRate = (TaxRate) ((int) value); }
		}

		public ReadonlyList CustomsDeclarationsList
		{
			get { return new ReadonlyList(CustomsDeclarations); }
		}

		public void AddCustomsDeclaration([MarshalAs(UnmanagedType.IDispatch)] object customsDeclaration)
		{
			CustomsDeclarations.Add((CustomsDeclaration) customsDeclaration);
		}
	}

	[ComVisible(true)]
	[Guid("EEFBD733-3893-4218-BACF-14AF8610F360")]
	public interface IPaymentDocumentInfo
	{
		string DocumentNumber { get; set; }
		string DocumentDate { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.PaymentDocumentInfo")]
	[Guid("25D835DD-386B-4CEA-BAB4-97CE037FBFB0")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IPaymentDocumentInfo))]
	public partial class PaymentDocumentInfo : SafeComObject, IPaymentDocumentInfo
	{
	}

	[ComVisible(true)]
	[Guid("5D2EFFA4-DDD0-4EFE-A9E5-3A539E4A9393")]
	public interface IShipperOrConsignee
	{
		bool SameAsSellerOrBuyer { get; set; }
		OrganizationInfo OrgInfo { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ShipperOrConsignee")]
	[Guid("C93FA906-63AA-4875-BA26-E39A7F3DD991")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IShipperOrConsignee))]
	public partial class ShipperOrConsignee : SafeComObject, IShipperOrConsignee
	{
	}

	[ComVisible(true)]
	[Guid("122D3A82-CFEF-4421-BE4D-DDFC8C38C029")]
	public interface IInvoiceCorrectionInfo
	{
		string InvoiceNumber { get; set; }
		string InvoiceDate { get; set; }
		string InvoiceRevisionNumber { get; set; }
		string InvoiceRevisionDate { get; set; }
		string InvoiceCorrectionNumber { get; set; }
		string InvoiceCorrectionDate { get; set; }
		string InvoiceCorrectionRevisionNumber { get; set; }
		string InvoiceCorrectionRevisionDate { get; set; }
		string Currency { get; set; }
		DiadocOrganizationInfo Seller { get; set; }
		DiadocOrganizationInfo Buyer { get; set; }
		Signer Signer { get; set; }
		InvoiceTotalsDiff TotalsInc { get; set; }
		InvoiceTotalsDiff TotalsDec { get; set; }
		string AdditionalInfo { get; set; }

		ReadonlyList ItemsList { get; }
		void AddItem([MarshalAs(UnmanagedType.IDispatch)] object item);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.InvoiceCorrectionInfo")]
	[Guid("964E24D7-2B41-4187-B512-08FEF6909F2C")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IInvoiceCorrectionInfo))]
	public partial class InvoiceCorrectionInfo : SafeComObject, IInvoiceCorrectionInfo
	{
		public ReadonlyList ItemsList
		{
			get { return new ReadonlyList(Items); }
		}

		public void AddItem(object item)
		{
			Items.Add((InvoiceCorrectionItem) item);
		}
	}

	[ComVisible(true)]
	[Guid("9CE3645F-3097-42E4-BEAF-A05627FC8F80")]
	public interface IInvoiceTotalsDiff
	{
		string TotalWithVatExcluded { get; set; }
		string Vat { get; set; }
		string Total { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.InvoiceTotalsDiff")]
	[Guid("F2C09CB5-88A0-4536-BA4D-278C13BB1845")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IInvoiceTotalsDiff))]
	public partial class InvoiceTotalsDiff : SafeComObject, IInvoiceTotalsDiff
	{
	}

	[ComVisible(true)]
	[Guid("3B5FBA28-29BB-4C84-AEB3-4072D5A27AB0")]
	public interface IInvoiceCorrectionItem
	{
		string Product { get; set; }
		CorrectableInvoiceItemFields OriginalValues { get; set; }
		CorrectableInvoiceItemFields CorrectedValues { get; set; }
		InvoiceItemAmountsDiff AmountsInc { get; set; }
		InvoiceItemAmountsDiff AmountsDec { get; set; }
		string AdditionalInfo { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.InvoiceCorrectionItem")]
	[Guid("B970675B-A285-44B6-B26B-15167DA63AC1")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IInvoiceCorrectionItem))]
	public partial class InvoiceCorrectionItem : SafeComObject, IInvoiceCorrectionItem
	{
	}

	[ComVisible(true)]
	[Guid("0E30D24B-0994-4667-AB0E-AF7A1461562E")]
	public interface ICorrectableInvoiceItemFields
	{
		string Unit { get; set; }
		string Quantity { get; set; }
		string Price { get; set; }
		string Excise { get; set; }
		Com.TaxRate TaxRateValue { get; set; }
		string SubtotalWithVatExcluded { get; set; }
		string Vat { get; set; }
		string Subtotal { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.CorrectableInvoiceItemFields")]
	[Guid("4841ABA5-5149-4A9E-8A6B-0D8CF7D607BA")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICorrectableInvoiceItemFields))]
	public partial class CorrectableInvoiceItemFields : SafeComObject, ICorrectableInvoiceItemFields
	{
		public Com.TaxRate TaxRateValue
		{
			get { return (Com.TaxRate) ((int) TaxRate); }
			set { TaxRate = (TaxRate) ((int) value); }
		}
	}

	[ComVisible(true)]
	[Guid("52F9A9CC-A4F2-4B03-B082-ECC20957E133")]
	public interface IInvoiceItemAmountsDiff
	{
		string Excise { get; set; }
		string SubtotalWithVatExcluded { get; set; }
		string Vat { get; set; }
		string Subtotal { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.InvoiceItemAmountsDiff")]
	[Guid("1EEF6C11-8810-4C8F-B343-0E1292946944")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IInvoiceItemAmountsDiff))]
	public partial class InvoiceItemAmountsDiff : SafeComObject, IInvoiceItemAmountsDiff
	{
	}

	[ComVisible(true)]
	[Guid("ACCBA4AF-77FA-49CA-AFDC-EDB27F4DF251")]
	public interface ICustomsDeclaration
	{
		string CountryCode { get; set; }
		string DeclarationNumber { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.CustomsDeclaration")]
	[Guid("C7497F31-742F-401E-B7A4-4D53A7BC4BDF")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICustomsDeclaration))]
	public partial class CustomsDeclaration : SafeComObject, ICustomsDeclaration
	{
	}
}