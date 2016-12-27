using System;
using System.Runtime.InteropServices;
using Diadoc.Api.Com;
using Diadoc.Api.Proto.Documents.InvoiceDocument;

namespace Diadoc.Api.Proto.Documents.UniversalTransferDocument
{
	[ComVisible(true)]
	[Guid("2527922E-D94D-427A-9D1E-7F637B403FAD")]
	public interface IUniversalTransferDocumentMetadata
	{
		string Total { get; }
		string Vat { get; }
		int Currency { get; }
		Com.UniversalTransferDocumentStatus Status { get; }
		DateTime ConfirmationDateTime { get; }
		Com.InvoiceAmendmentFlags AmendmentFlags { get; }
	}

	[ComVisible(true)]
	[Guid("E7FEFDB3-7E2E-4A21-A298-F1133E016099")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUniversalTransferDocumentMetadata))]
	public partial class UniversalTransferDocumentMetadata
		: SafeComObject
		, IUniversalTransferDocumentMetadata
	{
		public DateTime ConfirmationDateTime
		{
			//ConfirmationDateTimeTicks - это метка времени уже в московском часовом поясе
			get { return new DateTime(ConfirmationDateTimeTicks); }
		}

		public Com.InvoiceAmendmentFlags AmendmentFlags
		{
			get { return (Com.InvoiceAmendmentFlags)InvoiceAmendmentFlags; }
		}

		public Com.UniversalTransferDocumentStatus Status
		{
			get { return (Com.UniversalTransferDocumentStatus)DocumentStatus; }
		}
	}

	[ComVisible(true)]
	[Guid("321C4914-F238-4D61-A1F5-D639EC9695EC")]
	public interface IUniversalTransferDocumentRevisionMetadata
	{
		string OriginalInvoiceNumber { get; }
		string OriginalInvoiceDate { get; }
		string Total { get; }
		string Vat { get; }
		int Currency { get; }
		Com.UniversalTransferDocumentStatus Status { get; }
		DateTime ConfirmationDateTime { get; }
		Com.InvoiceAmendmentFlags AmendmentFlags { get; }
	}

	[ComVisible(true)]
	[Guid("E499C254-F25A-44F0-822F-E9D8C19E2C9A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUniversalTransferDocumentRevisionMetadata))]
	public partial class UniversalTransferDocumentRevisionMetadata
		: SafeComObject
		, IUniversalTransferDocumentRevisionMetadata
	{
		public DateTime ConfirmationDateTime
		{
			get { return new DateTime(ConfirmationDateTimeTicks); }
		}

		public Com.InvoiceAmendmentFlags AmendmentFlags
		{
			get { return (Com.InvoiceAmendmentFlags)InvoiceAmendmentFlags; }
		}

		public Com.UniversalTransferDocumentStatus Status
		{
			get { return (Com.UniversalTransferDocumentStatus)DocumentStatus; }
		}
	}

	[ComVisible(true)]
	[Guid("2B68D9E8-2A96-487D-80C0-9806BE28F075")]
	public interface IUniversalCorrectionDocumentMetadata
	{
		string OriginalInvoiceNumber { get; }
		string OriginalInvoiceDate { get; }
		string OriginalInvoiceRevisionNumber { get; }
		string OriginalInvoiceRevisionDate { get; }
		string TotalInc { get; }
		string TotalDec { get; }
		string VatInc { get; }
		string VatDec { get; }
		int Currency { get; }
		Com.UniversalTransferDocumentStatus Status { get; }
		DateTime ConfirmationDateTime { get; }
		Com.InvoiceAmendmentFlags AmendmentFlags { get; }
	}

	[ComVisible(true)]
	[Guid("5FFDBDD7-3B40-4944-8404-03386932FC21")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUniversalCorrectionDocumentMetadata))]
	public partial class UniversalCorrectionDocumentMetadata
		: SafeComObject
		, IUniversalCorrectionDocumentMetadata
	{
		public DateTime ConfirmationDateTime
		{
			get { return new DateTime(ConfirmationDateTimeTicks); }
		}

		public Com.InvoiceAmendmentFlags AmendmentFlags
		{
			get { return (Com.InvoiceAmendmentFlags)InvoiceAmendmentFlags; }
		}

		public Com.UniversalTransferDocumentStatus Status
		{
			get { return (Com.UniversalTransferDocumentStatus)DocumentStatus; }
		}
	}

	[ComVisible(true)]
	[Guid("2D9EADF9-35DB-4354-8113-EE17835F50C4")]
	public interface IUniversalCorrectionDocumentRevisionMetadata
	{
		string OriginalInvoiceNumber { get; }
		string OriginalInvoiceDate { get; }
		string OriginalInvoiceRevisionNumber { get; }
		string OriginalInvoiceRevisionDate { get; }
		string OriginalInvoiceCorrectionNumber { get; }
		string OriginalInvoiceCorrectionDate { get; }
		string TotalInc { get; }
		string TotalDec { get; }
		string VatInc { get; }
		string VatDec { get; }
		int Currency { get; }
		Com.UniversalTransferDocumentStatus Status { get; }
		DateTime ConfirmationDateTime { get; }
		Com.InvoiceAmendmentFlags AmendmentFlags { get; }
	}

	[ComVisible(true)]
	[Guid("D9D6D9EA-D7BB-42F3-8331-933E9D6C4709")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUniversalCorrectionDocumentRevisionMetadata))]
	public partial class UniversalCorrectionDocumentRevisionMetadata
		: SafeComObject
		, IUniversalCorrectionDocumentRevisionMetadata
	{
		public DateTime ConfirmationDateTime
		{
			get { return new DateTime(ConfirmationDateTimeTicks); }
		}

		public Com.InvoiceAmendmentFlags AmendmentFlags
		{
			get { return (Com.InvoiceAmendmentFlags)InvoiceAmendmentFlags; }
		}

		public Com.UniversalTransferDocumentStatus Status
		{
			get { return (Com.UniversalTransferDocumentStatus)DocumentStatus; }
		}
	}
}