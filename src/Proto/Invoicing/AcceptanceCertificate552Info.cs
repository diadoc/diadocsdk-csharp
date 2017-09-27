using System.Runtime.InteropServices;
using Diadoc.Api.Com;
using Diadoc.Api.Proto.Invoicing.Organizations;
using Diadoc.Api.Proto.Invoicing.Signers;

namespace Diadoc.Api.Proto.Invoicing
{
		[ComVisible(true)]
		[Guid("85814002-A80F-4866-8C34-E4F943D9AD47")]
		public interface IAcceptanceCertificate552SellerTitleInfo
		{
			string Currency { get; set; }
			string CurrencyRate { get; set; }
			string DocumentDate { get; set; }
			string DocumentNumber { get; set; }
			string RevisionDate { get; set; }
			string RevisionNumber { get; set; }
			string DocumentCreator { get; set; }
			string DocumentCreatorBase { get; set; }
			string OperationType { get; set; }
			string OperationTitle { get; set; }
			string GovernmentContractInfo { get; set; }
			string DocumentName { get; set; }
			
			AcceptanceCertificate552TransferInfo TransferInfo { get; set; }
			
			AdditionalInfoId AdditionalInfoId { get; set; }
			
			ExtendedOrganizationInfo Seller { get; set; }
			ExtendedOrganizationInfo Buyer { get; set; }

			ReadonlyList SignersList { get; }
			void AddSigner([MarshalAs(UnmanagedType.IDispatch)] object signer);

			ReadonlyList GroundsList { get; }
			void AddGround([MarshalAs(UnmanagedType.IDispatch)] object ground);
			
			ReadonlyList WorksList { get; }
			void AddWork([MarshalAs(UnmanagedType.IDispatch)] object work);
		}


	[ComVisible(true)]
	[ProgId("Diadoc.Api.AcceptanceCertificate552SellerTitleInfo")]
	[Guid("AE9B5101-A381-4371-BE64-B6886978231E")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IAcceptanceCertificate552SellerTitleInfo))]
	public partial class AcceptanceCertificate552SellerTitleInfo : SafeComObject, IAcceptanceCertificate552SellerTitleInfo
	{
		public ReadonlyList WorksList
         		{
         			get { return new ReadonlyList(Works); }
         		}
         
		public void AddWork(object work)
		{
			Works.Add((AcceptanceCertificate552WorkDescription) work);
		}
		
		public ReadonlyList SignersList
		{
			get { return new ReadonlyList(Signers); }
		}

		public void AddSigner(object signer)
		{
			Signers.Add((ExtendedSigner) signer);
		}
		
		public ReadonlyList GroundsList
		{
			get { return new ReadonlyList(Grounds); }
		}

		public void AddGround(object ground)
		{
			Grounds.Add((GroundInfo) ground);
		}
	}
	
	[ComVisible(true)]
	[Guid("74C18114-CCDE-4F81-8CB4-7F319B44A5F6")]
	public interface IAcceptanceCertificate552TransferInfo
	{
		string OperationInfo { get; set; }
		string TransferDate { get; set; }
		string CreatedThingTransferDate { get; set; }
		string CreatedThingInfo { get; set; }
		
		ReadonlyList AdditionalInfoList { get; }
		void AddAdditionalInfo([MarshalAs(UnmanagedType.IDispatch)] object info);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.AcceptanceCertificate552TransferInfo")]
	[Guid("FBE66DDE-BFF0-444F-A7AD-988BBED40CF7")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IAcceptanceCertificate552TransferInfo))]
	public partial class AcceptanceCertificate552TransferInfo : SafeComObject, IAcceptanceCertificate552TransferInfo
	{
		public ReadonlyList AdditionalInfoList
		{
			get { return new ReadonlyList(AdditionalInfos); }
		}

		public void AddAdditionalInfo(object info)
		{
			AdditionalInfos.Add((AdditionalInfo) info);
		}
	}
	
	[ComVisible(true)]
	[Guid("52A7282F-BC4E-4511-AA85-1A4970D6379D")]
	public interface IAcceptanceCertificate552WorkDescription
	{
		string StartingDate { get; set; }
		string CompletionDate { get; set; }
		string TotalWithVatExcluded { get; set; }
		string TotalVat { get; set; }
		string Total { get; set; }
		ReadonlyList ItemsList { get; }
		void AddItem([MarshalAs(UnmanagedType.IDispatch)] object item);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.AcceptanceCertificate552WorkDescription")]
	[Guid("3C54BA92-BC99-4AF8-AF92-737D5CB04CB3")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IAcceptanceCertificate552WorkDescription))]
	public partial class AcceptanceCertificate552WorkDescription : SafeComObject, IAcceptanceCertificate552WorkDescription
	{
		public ReadonlyList ItemsList
		{
			get { return new ReadonlyList(Items); }
		}

		public void AddItem(object item)
		{
			Items.Add((AcceptanceCertificate552WorkItem) item);
		}
	}
	
	[ComVisible(true)]
	[Guid("3465C983-0469-4F91-9B1C-3A0ADE07D835")]
	public interface IAcceptanceCertificate552WorkItem
	{
		string Name { get; set; }
		string Description { get; set; }
		string UnitCode { get; set; }
		string UnitName { get; set; }
		string Price { get; set; }
		string Quantity { get; set; }
		string SubtotalWithVatExcluded { get; set; }
		string Vat { get; set; }
		string Subtotal { get; set; }
		string ItemAccountDebit { get; set; }
		string ItemAccountCredit { get; set; }
		
		TaxRate TaxRate { get; set; }
		
		ReadonlyList AdditionalInfoList { get; }
		void AddAdditionalInfo([MarshalAs(UnmanagedType.IDispatch)] object info);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.AcceptanceCertificate552WorkItem")]
	[Guid("89C8ABB6-89B2-40F9-8AFF-D47395F47DBA")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IAcceptanceCertificate552WorkItem))]
	public partial class AcceptanceCertificate552WorkItem : SafeComObject, IAcceptanceCertificate552WorkItem
	{
		public ReadonlyList AdditionalInfoList
		{
			get { return new ReadonlyList(AdditionalInfos); }
		}

		public void AddAdditionalInfo(object info)
		{
			AdditionalInfos.Add((AdditionalInfo) info);
		}
	}
	
	[ComVisible(true)]
	[Guid("7776927D-8953-459B-9890-EF002639EF0D")]
	public interface IAcceptanceCertificate552BuyerTitleInfo
	{
		string DocumentCreator { get; set; }
		string DocumentCreatorBase { get; set; }
		string OperationType { get; set; }
		string OperationContent { get; set; }
		string AcceptanceDate { get; set; }
		string CreatedThingAcceptDate { get; set; }
		string CreatedThingInfo { get; set; }
		
		AdditionalInfoId AdditionalInfoId { get; set; }
		
		ReadonlyList SignersList { get; }
		void AddSigner([MarshalAs(UnmanagedType.IDispatch)] object signer);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.AcceptanceCertificate552BuyerTitleInfo")]
	[Guid("EDF7C87A-0925-4792-9F49-AB7BB3ED12AA")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IAcceptanceCertificate552BuyerTitleInfo))]
	public partial class AcceptanceCertificate552BuyerTitleInfo : SafeComObject, IAcceptanceCertificate552BuyerTitleInfo
	{
		public ReadonlyList SignersList
		{
			get { return new ReadonlyList(Signers); }
		}

		public void AddSigner(object signer)
		{
			Signers.Add((ExtendedSigner) signer);
		}
	}
}