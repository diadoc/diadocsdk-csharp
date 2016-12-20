using System.Runtime.InteropServices;
using Diadoc.Api.Com;
using Diadoc.Api.Proto.Invoicing.Signers;

namespace Diadoc.Api.Proto.Invoicing
{
	[ComVisible(true)]
	[Guid("14D78874-7D3E-4118-BB44-467075121F86")]
	public interface IUniversalTransferDocumentSellerTitleInfo
	{
		Com.FunctionType Function { get; set; }
		string DocumentName { get; set; }
		string DocumentDate { get; set; }
		string DocumentNumber { get; set; }
		string RevisionDate { get; set; }
		string RevisionNumber { get; set; }

		string Currency { get; set; }
		string CurrencyRate { get; set; }
		string DocumentCreator { get; set; }
		string DocumentCreatorBase { get; set; }
		string GovernmentContractInfo { get; set; }

		Organizations.ExtendedOrganizationInfo Seller { get; set; }
		Organizations.ExtendedOrganizationInfo Buyer { get; set; }
		Shipper Shipper { get; set; }
		Organizations.ExtendedOrganizationInfo Consignee { get; set; }
		InvoiceTable InvoiceTable { get; set; }
		AdditionalInfoId AdditionalInfoId { get; set; }
		TransferInfo TransferInfo { get; set; }

		ReadonlyList Signers { get; }
		void AddSigner([MarshalAs(UnmanagedType.IDispatch)] object signer);

		ReadonlyList PaymentDocuments { get; }
		void AddPaymentDocument([MarshalAs(UnmanagedType.IDispatch)] object paymentDocument);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.UniversalTransferDocumentSellerTitleInfo")]
	[Guid("478D732D-5124-4705-8643-4E43E1FCAF46")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUniversalTransferDocumentSellerTitleInfo))]
	public partial class UniversalTransferDocumentSellerTitleInfo
		: SafeComObject
		, IUniversalTransferDocumentSellerTitleInfo
	{
		Com.FunctionType IUniversalTransferDocumentSellerTitleInfo.Function
		{
			get { return (Com.FunctionType)Function; }
			set { Function = (FunctionType)value; }
		}

		ReadonlyList IUniversalTransferDocumentSellerTitleInfo.Signers
		{
			get { return new ReadonlyList(Signers); }
		}

		void IUniversalTransferDocumentSellerTitleInfo.AddSigner(object signer)
		{
			Signers.Add((ExtendedSigner)signer);
		}

		ReadonlyList IUniversalTransferDocumentSellerTitleInfo.PaymentDocuments
		{
			get { return new ReadonlyList(PaymentDocuments); }
		}

		void IUniversalTransferDocumentSellerTitleInfo.AddPaymentDocument(object paymentDocument)
		{
			PaymentDocuments.Add((PaymentDocumentInfo)paymentDocument);
		}
	}

	[ComVisible(true)]
	[Guid("BA5F4899-F955-485A-ADF1-28110AA8FA90")]
	public interface IShipper
	{
		bool SameAsSeller { get; set; }
		Organizations.ExtendedOrganizationInfo OrgInfo { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.Shipper")]
	[Guid("611734B7-6186-4E50-A450-BFEF00F6E684")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IShipper))]
	public partial class Shipper
		: SafeComObject, IShipper
	{
	}

	[ComVisible(true)]
	[Guid("68075111-639F-4D65-BB59-9A016FED86B3")]
	public interface IUtdInvoiceTable
	{
		ReadonlyList Items { get; }
		void AddItem([MarshalAs(UnmanagedType.IDispatch)] object item);

		string TotalWithVatExcluded { get; set; }
		string Vat { get; set; }
		string Total { get; set; }
		string TotalNet { get; set; }
	}


	[ComVisible(true)]
	[ProgId("Diadoc.Api.InvoiceTable")]
	[Guid("516BD178-EC6C-4C8E-A5E4-B3419240AE2A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUtdInvoiceTable))]
	public partial class InvoiceTable
		: SafeComObject
		, IUtdInvoiceTable
	{
		ReadonlyList IUtdInvoiceTable.Items
		{
			get { return new ReadonlyList(Items); }
		}

		void IUtdInvoiceTable.AddItem(object item)
		{
			Items.Add((ExtendedInvoiceItem)item);
		}
	}

	[ComVisible(true)]
	[Guid("96DB97AF-2894-414C-BC5B-3225A326231C")]
	public interface IExtendedInvoiceItem
	{
		string Product { get; set; }
		string Unit { get; set; }
		string UnitName { get; set; }
		string Quantity { get; set; }
		string Price { get; set; }
		string Excise { get; set; }
		Com.TaxRate TaxRateValue { get; set; }
		string SubtotalWithVatExcluded { get; set; }
		string Vat { get; set; }
		string Subtotal { get; set; }
		Com.ItemMark ItemMarkValue { get; set; }
		string AdditionalProperty { get; set; }
		string ItemVendorCode { get; set; }
		string ItemToRelease { get; set; }
		string ItemAccountDebit { get; set; }
		string ItemAccountCredit { get; set; }

		ReadonlyList AdditionalInfo { get; }
		void AddAdditionalInfo([MarshalAs(UnmanagedType.IDispatch)] object item);

		ReadonlyList CustomsDeclarations { get; }
		void AddCustomsDeclaration([MarshalAs(UnmanagedType.IDispatch)] object item);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ExtendedInvoiceItem")]
	[Guid("508D2FE1-5F61-4158-8E59-9899D7E151B6")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IExtendedInvoiceItem))]
	public partial class ExtendedInvoiceItem
		: SafeComObject
		, IExtendedInvoiceItem
	{
		public Com.TaxRate TaxRateValue
		{
			get { return (Com.TaxRate)(int)TaxRate; }
			set { TaxRate = (TaxRate)(int)value; }
		}

		public Com.ItemMark ItemMarkValue
		{
			get { return (Com.ItemMark)(int)ItemMark; }
			set { ItemMark = (ItemMark)(int)value; }
		}

		ReadonlyList IExtendedInvoiceItem.AdditionalInfo
		{
			get {  return new ReadonlyList(AdditionalInfo);}
		}

		void IExtendedInvoiceItem.AddAdditionalInfo([MarshalAs(UnmanagedType.IDispatch)] object additionalInfo)
		{
			AdditionalInfo.Add((AdditionalInfo)additionalInfo);
		}

		ReadonlyList IExtendedInvoiceItem.CustomsDeclarations
		{
			get { return new ReadonlyList(CustomsDeclarations);}
		}

		void IExtendedInvoiceItem.AddCustomsDeclaration([MarshalAs(UnmanagedType.IDispatch)] object customsDeclaration)
		{
			CustomsDeclarations.Add((CustomsDeclaration)customsDeclaration);
		}
	}

	[ComVisible(true)]
	[Guid("6DA33A99-6E2E-4751-84B7-0CFECB129B16")]
	public interface ITransferInfo
	{
		string OperationInfo { get; set; }
		string OperationType { get; set; }
		string TransferDate { get; set; }
		string TransferTextInfo { get; set; }
		string CreatedThingTransferDate { get; set; }
		string CreatedThingInfo { get; set; }
		Organizations.ExtendedOrganizationInfo Carrier { get; set; }
		Employee Employee { get; set; }
		OtherIssuer OtherIssuer { get; set; }
		AdditionalInfoId AdditionalInfoId { get; set; }

		ReadonlyList TransferBase { get; }
		void AddTransferBase([MarshalAs(UnmanagedType.IDispatch)] object transferBase);

		ReadonlyList Waybill { get; }
		void AddWaybill([MarshalAs(UnmanagedType.IDispatch)] object waybill);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.TransferInfo")]
	[Guid("3906627F-3755-41BC-9D75-2DB1BCEC44C5")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ITransferInfo))]
	public partial class TransferInfo
		: SafeComObject
		, ITransferInfo
	{
		ReadonlyList ITransferInfo.TransferBase
		{
			get { return new ReadonlyList(TransferBase);}
		}

		void ITransferInfo.AddTransferBase(object transferBase)
		{
			TransferBase.Add((TransferBase)transferBase);
		}

		ReadonlyList ITransferInfo.Waybill
		{
			get { return new ReadonlyList(Waybill);}
		}

		void ITransferInfo.AddWaybill(object waybill)
		{
			Waybill.Add((Waybill)waybill);
		}
	}

	[ComVisible(true)]
	[Guid("C82440ED-F236-40B9-A1F0-8F6A16FB0248")]
	public interface ITransferBase
	{
		string BaseDocumentName { get; set; }
		string BaseDocumentNumber { get; set; }
		string BaseDocumentDate { get; set; }
		string BaseDocumentInfo { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.TransferBase")]
	[Guid("A06BFCBC-881C-4077-B60C-25C74194147B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ITransferBase))]
	public partial class TransferBase : SafeComObject, ITransferBase
	{
	}

	[ComVisible(true)]
	[Guid("94CF5FD1-56C6-4595-8647-7716775B2EFB")]
	public interface IWaybill
	{
		string TransferDocumentNumber { get; set; }
		string TransferDocumentDate { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.Waybill")]
	[Guid("FA51D2B3-2D45-478B-A142-C32EF038F13B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IWaybill))]
	public partial class Waybill : SafeComObject, IWaybill
	{
	}

	[ComVisible(true)]
	[Guid("73BC4DF0-8E3E-4A83-87C6-8193428F10BC")]
	public interface IEmployee
	{
		string EmployeePosition { get; set; }
		string EmployeeInfo { get; set; }
		string EmployeeBase { get; set; }
		string TransferSurname { get; set; }
		string TransferFirstName { get; set; }
		string TransferPatronymic { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.Employee")]
	[Guid("AC526A5F-5E16-4DFC-843D-D596CAF66035")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEmployee))]
	public partial class Employee : SafeComObject, IEmployee
	{
	}

	[ComVisible(true)]
	[Guid("C06746BE-7447-418C-815F-301A061E3CEC")]
	public interface IOtherIssuer
	{
		string TransferEmployeePosition { get; set; }
		string TransferEmployeeInfo { get; set; }
		string TransferOrganizationName { get; set; }
		string TransferOrganizationBase { get; set; }
		string TransferEmployeeBase { get; set; }
		string TransferSurname { get; set; }
		string TransferFirstName { get; set; }
		string TransferPatronymic { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.OtherIssuer")]
	[Guid("1F5956AB-F7AE-4C48-8CEC-9D072140FF74")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IOtherIssuer))]
	public partial class OtherIssuer
		: SafeComObject
		, IOtherIssuer
	{
	}

	[ComVisible(true)]
	[Guid("FF6BB278-1DF1-4AA6-9E2C-8C82732461A8")]
	public interface IAdditionalInfoId
	{
		string InfoFileId { get; set; }

		ReadonlyList AdditionalInfo { get; }
		void AddAdditionalInfo([MarshalAs(UnmanagedType.IDispatch)] object additionalInfo);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.AdditionalInfoId")]
	[Guid("60D6F43F-BDAE-4EBD-8894-99B8D3016249")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IAdditionalInfoId))]
	public partial class AdditionalInfoId
		: SafeComObject
		, IAdditionalInfoId
	{
		ReadonlyList IAdditionalInfoId.AdditionalInfo
		{
			get { return new ReadonlyList(AdditionalInfo); }
		}

		void IAdditionalInfoId.AddAdditionalInfo(object additionalInfo)
		{
			AdditionalInfo.Add((AdditionalInfo)additionalInfo);
		}
	}

	[ComVisible(true)]
	[Guid("82470E2B-2E1C-44D7-BF85-FB6EA755F3BF")]
	public interface IUniversalTransferDocumentBuyerTitleInfo
	{
		string DocumentCreator { get; set; }
		string DocumentCreatorBase { get; set; }
		string OperationCode { get; set; }
		string OperationContent { get; set; }
		string AcceptanceDate { get; set; }
		Employee Employee { get; set; }
		OtherIssuer OtherIssuer { get; set; }
		AdditionalInfoId AdditionalInfoId { get; set; }

		ReadonlyList Signers { get; }
		void AddSigner([MarshalAs(UnmanagedType.IDispatch)] object signer);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.UniversalTransferDocumentBuyerTitleInfo")]
	[Guid("8FFEB7B0-3BB6-412A-9216-476746ACD8B9")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUniversalTransferDocumentBuyerTitleInfo))]
	public partial class UniversalTransferDocumentBuyerTitleInfo
		: SafeComObject
		, IUniversalTransferDocumentBuyerTitleInfo
	{
		ReadonlyList IUniversalTransferDocumentBuyerTitleInfo.Signers
		{
			get { return new ReadonlyList(Signers); }
		}

		void IUniversalTransferDocumentBuyerTitleInfo.AddSigner(object signer)
		{
			Signers.Add((ExtendedSigner)signer);
		}
	}

	[ComVisible(true)]
	[Guid("F52C7767-48C6-4A4E-B472-07F03FC18B7D")]
	public interface IUniversalCorrectionDocumentSellerTitleInfo
	{
		Com.FunctionType Function { get; set; }
		string DocumentName { get; set; }
		string DocumentDate { get; set; }
		string DocumentNumber { get; set; }
		string Currency { get; set; }
		string CurrencyRate { get; set; }
		string CorrectionRevisionDate { get; set; }
		string CorrectionRevisionNumber { get; set; }
		string DocumentCreator { get; set; }
		string DocumentCreatorBase { get; set; }
		string GovernmentContractInfo { get; set; }
		Organizations.ExtendedOrganizationInfo Seller { get; set; }
		Organizations.ExtendedOrganizationInfo Buyer { get; set; }
		EventContent EventContent { get; set; }
		InvoiceCorrectionTable InvoiceCorrectionTable { get; set; }
		AdditionalInfoId AdditionalInfoId { get; set; }

		ReadonlyList Invoices { get; }
		void AddInvoice([MarshalAs(UnmanagedType.IDispatch)] object invoice);

		ReadonlyList Signers { get; }
		void AddSigner([MarshalAs(UnmanagedType.IDispatch)] object signer);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.UniversalCorrectionDocumentSellerTitleInfo")]
	[Guid("BBFFAAC0-CC06-406A-BC49-C51A69AC4A09")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUniversalCorrectionDocumentSellerTitleInfo))]
	public partial class UniversalCorrectionDocumentSellerTitleInfo
		: SafeComObject
		, IUniversalCorrectionDocumentSellerTitleInfo
	{
		Com.FunctionType IUniversalCorrectionDocumentSellerTitleInfo.Function
		{
			get { return (Com.FunctionType)(int)Function; }
			set { Function = (FunctionType)(int)value; }
		}

		ReadonlyList IUniversalCorrectionDocumentSellerTitleInfo.Invoices
		{
			get { return new ReadonlyList(Invoices); }
		}

		void IUniversalCorrectionDocumentSellerTitleInfo.AddInvoice([MarshalAs(UnmanagedType.IDispatch)] object invoice)
		{
			Invoices.Add((InvoiceForCorrectionInfo)invoice);
		}

		ReadonlyList IUniversalCorrectionDocumentSellerTitleInfo.Signers
		{
			get { return new ReadonlyList(Signers); }
		}

		void IUniversalCorrectionDocumentSellerTitleInfo.AddSigner([MarshalAs(UnmanagedType.IDispatch)] object signer)
		{
			Signers.Add((ExtendedSigner)signer);
		}
	}

	[ComVisible(true)]
	[Guid("A2A27E4F-8BC1-4F56-A761-3CD25EEDB458")]
	public interface IInvoiceForCorrectionInfo
	{
		string InvoiceDate { get; set; }
		string InvoiceNumber { get; set; }

		ReadonlyList InvoiceRevisions { get; }
		void AddInvoiceRevision([MarshalAs(UnmanagedType.IDispatch)] object invoiceRevision);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.InvoiceForCorrectionInfo")]
	[Guid("761B49F7-B0AA-46C4-AFC4-8A25211A3B38")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IInvoiceForCorrectionInfo))]
	public partial class InvoiceForCorrectionInfo
		: SafeComObject
		, IInvoiceForCorrectionInfo
	{
		ReadonlyList IInvoiceForCorrectionInfo.InvoiceRevisions
		{
			get { return new ReadonlyList(InvoiceRevisions); }
		}

		void IInvoiceForCorrectionInfo.AddInvoiceRevision([MarshalAs(UnmanagedType.IDispatch)] object invoiceRevision)
		{
			InvoiceRevisions.Add((InvoiceRevisionInfo)invoiceRevision);
		}
	}

	[ComVisible(true)]
	[Guid("12F3AB87-DB2B-4252-9491-0F0CAAF4EECB")]
	public interface IInvoiceRevisionInfo
	{
		string InvoiceRevisionDate { get; set; }
		string InvoiceRevisionNumber { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.InvoiceRevisionInfo")]
	[Guid("485C2BC3-A13C-4D9C-822A-D7F54F755A7D")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IInvoiceRevisionInfo))]
	public partial class InvoiceRevisionInfo : SafeComObject, IInvoiceRevisionInfo
	{
	}

	[ComVisible(true)]
	[Guid("1403BB3E-4A0C-4404-AEDE-16D24ECBC29B")]
	public interface IEventContent
	{
		string CostChangeInfo { get; set; }
		string TransferDocDetails { get; set; }
		string OperationContent { get; set; }
		string NotificationDate { get; set; }

		ReadonlyList CorrectionBase { get; }
		void AddCorrectionBase([MarshalAs(UnmanagedType.IDispatch)] object correctionBase);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.EventContent")]
	[Guid("8C9747F2-F902-4CA4-8353-475FD1066C6B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IEventContent))]
	public partial class EventContent : SafeComObject, IEventContent
	{
		ReadonlyList IEventContent.CorrectionBase
		{
			get { return new ReadonlyList(CorrectionBase); }
		}

		void IEventContent.AddCorrectionBase([MarshalAs(UnmanagedType.IDispatch)] object correctionBase)
		{
			CorrectionBase.Add((CorrectionBase)correctionBase);
		}
	}

	[ComVisible(true)]
	[Guid("5A65E145-273F-4F7E-BA8A-61AB19BA489F")]
	public interface ICorrectionBase
	{
		string BaseDocumentName { get; set; }
		string BaseDocumentNumber { get; set; }
		string BaseDocumentDate { get; set; }
		string AdditionalInfo { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.CorrectionBase")]
	[Guid("8D1AF36C-9D84-4664-957E-29D2351B7097")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ICorrectionBase))]
	public partial class CorrectionBase : SafeComObject, ICorrectionBase
	{
	}

	[ComVisible(true)]
	[Guid("5937663E-E9C6-4A27-8388-2B87534B347A")]
	public interface IUtdInvoiceCorrectionTable
	{
		InvoiceTotalsDiff TotalsInc { get; set; }
		InvoiceTotalsDiff TotalsDec { get; set; }

		ReadonlyList Items { get; }
		void AddItem([MarshalAs(UnmanagedType.IDispatch)] object item);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.InvoiceCorrectionTable")]
	[Guid("F433BB1C-57CB-43D1-8BA1-BBDAD602A3C8")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUtdInvoiceCorrectionTable))]
	public partial class InvoiceCorrectionTable : SafeComObject, IUtdInvoiceCorrectionTable
	{
		ReadonlyList IUtdInvoiceCorrectionTable.Items
		{
			get { return new ReadonlyList(Items);}
		}

		void IUtdInvoiceCorrectionTable.AddItem([MarshalAs(UnmanagedType.IDispatch)] object item)
		{
			Items.Add((ExtendedInvoiceCorrectionItem)item);
		}
	}

	[ComVisible(true)]
	[Guid("D2D73751-2271-4EE7-9534-BB95E0027C5E")]
	public interface IExtendedInvoiceCorrectionItem
	{
		string Product { get; set; }
		string ItemAccountDebit { get; set; }
		string ItemAccountCredit { get; set; }
		CorrectableInvoiceItemFields OriginalValues { get; set; }
		CorrectableInvoiceItemFields CorrectedValues { get; set; }
		InvoiceItemAmountsDiff AmountsInc { get; set; }
		InvoiceItemAmountsDiff AmountsDec { get; set; }

		ReadonlyList AdditionalInfo { get; }
		void AddAdditionalInfo([MarshalAs(UnmanagedType.IDispatch)] object additionalInfo);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.ExtendedInvoiceCorrectionItem")]
	[Guid("29FE030A-4CCD-44ED-B325-6B4587CBBC75")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IExtendedInvoiceCorrectionItem))]
	public partial class ExtendedInvoiceCorrectionItem
		: SafeComObject
		, IExtendedInvoiceCorrectionItem
	{
		ReadonlyList IExtendedInvoiceCorrectionItem.AdditionalInfo
		{
			get { return new ReadonlyList(AdditionalInfo); }
		}

		void IExtendedInvoiceCorrectionItem.AddAdditionalInfo([MarshalAs(UnmanagedType.IDispatch)] object additionalInfo)
		{
			AdditionalInfo.Add((AdditionalInfo)additionalInfo);
		}
	}
}