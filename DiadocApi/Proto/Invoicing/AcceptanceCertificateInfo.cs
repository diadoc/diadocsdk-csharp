using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Invoicing
{
	[ComVisible(true)]
	[Guid("AFA7402F-8E07-43D0-99AF-5F0C632C6392")]
	public interface IAcceptanceCertificateSellerTitleInfo
	{
		string DocumentNumber { get; set; }
		string DocumentDate { get; set; }
		string DocumentTitle { get; set; }

		DiadocOrganizationInfo Seller { get; set; }
		DocflowParticipant Buyer { get; set; }
		AcceptanceCertificateSignatureInfo Signature { get; set; }
		Signer Signer { get; set; }
		string AdditionalInfo { get; set; }

		ReadonlyList WorksList { get; }
		void AddWork([MarshalAs(UnmanagedType.IDispatch)] object work);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.AcceptanceCertificateSellerTitleInfo")]
	[Guid("2C5F16C4-F078-4E44-83A5-EA02B1DF3139")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IAcceptanceCertificateSellerTitleInfo))]
	public partial class AcceptanceCertificateSellerTitleInfo : SafeComObject, IAcceptanceCertificateSellerTitleInfo
	{
		public ReadonlyList WorksList
		{
			get { return new ReadonlyList(Works); }
		}

		public void AddWork(object work)
		{
			Works.Add((WorkDescription) work);
		}
	}

	[ComVisible(true)]
	[Guid("3514174B-7CCC-4820-B7B4-F1BF260BD5EA")]
	public interface IAcceptanceCertificateBuyerTitleInfo
	{
		string Complaints { get; set; }
		AcceptanceCertificateSignatureInfo Signature { get; set; }
		Signer Signer { get; set; }
		string AdditionalInfo { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.AcceptanceCertificateBuyerTitleInfo")]
	[Guid("BAAD4FED-757F-4B42-A9CC-CF450292FAF0")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IAcceptanceCertificateBuyerTitleInfo))]
	public partial class AcceptanceCertificateBuyerTitleInfo : SafeComObject, IAcceptanceCertificateBuyerTitleInfo
	{
	}

	[ComVisible(true)]
	[Guid("17CF3053-6238-4C67-905B-AE362BC0E991")]
	public interface IAcceptanceCertificateSignatureInfo
	{
		string SignatureDate { get; set; }
		Official Official { get; set; }
		Attorney Attorney { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.AcceptanceCertificateSignatureInfo")]
	[Guid("577C3D17-D05A-466D-BAD3-DE78316462B7")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IAcceptanceCertificateSignatureInfo))]
	public partial class AcceptanceCertificateSignatureInfo : SafeComObject, IAcceptanceCertificateSignatureInfo
	{
	}

	[ComVisible(true)]
	[Guid("89C7176D-A489-47E5-A650-86AEC6D0CC0D")]
	public interface IWorkDescription
	{
		string StartingDate { get; set; }
		string CompletionDate { get; set; }
		string Vat { get; set; }
		string TotalWithVatExcluded { get; set; }
		string Total { get; set; }
		ReadonlyList ItemsList { get; }
		void AddItem([MarshalAs(UnmanagedType.IDispatch)] object item);
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.WorkDescription")]
	[Guid("A70BC08E-9F35-47DF-95AC-218A685A027E")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IWorkDescription))]
	public partial class WorkDescription : SafeComObject, IWorkDescription
	{
		public ReadonlyList ItemsList
		{
			get { return new ReadonlyList(Items); }
		}

		public void AddItem(object item)
		{
			Items.Add((WorkItem) item);
		}
	}

	[ComVisible(true)]
	[Guid("64F0CB80-6A4E-4771-AC78-566648655577")]
	public interface IWorkItem
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
		string AdditionalInfo { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.WorkItem")]
	[Guid("4FBCC331-09AE-4E34-B6DB-ABA5F94D0627")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof (IWorkItem))]
	public partial class WorkItem : SafeComObject, IWorkItem
	{
	}
}