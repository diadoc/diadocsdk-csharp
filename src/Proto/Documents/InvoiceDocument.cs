using System;
using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto.Documents.InvoiceDocument
{
	[ComVisible(true)]
	[Guid("429CA0E8-52C4-484B-9BE4-201688F8D5EE")]
	public interface IInvoiceMetadata
	{
		string Total { get; }
		string Vat { get; }
		int Currency { get; }
		Com.InvoiceStatus Status { get; }
		DateTime ConfirmationDateTime { get; }
		InvoiceAmendmentFlags AmendmentFlags { get; }
	}

	[ComVisible(true)]
	[Guid("47CBF8A8-36C9-4C1E-BD79-D011D6CEF936")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IInvoiceMetadata))]
	public partial class InvoiceMetadata : SafeComObject, IInvoiceMetadata
	{
		public DateTime ConfirmationDateTime
		{
			//ConfirmationDateTimeTicks - это метка времени уже в московском часовом поясе
			get { return new DateTime(ConfirmationDateTimeTicks); }
		}

		public InvoiceAmendmentFlags AmendmentFlags
		{
			get { return (InvoiceAmendmentFlags)InvoiceAmendmentFlags; }
		}

		public Com.InvoiceStatus Status
		{
			get { return (Com.InvoiceStatus)((int)InvoiceStatus); }
		}
	}

	[ComVisible(true)]
	[Guid("86BAA341-6E9E-4A0C-B4A5-45F9D740D7DD")]
	public interface IInvoiceRevisionMetadata
	{
		string OriginalInvoiceNumber { get; }
		string OriginalInvoiceDate { get; }
		string Total { get; }
		string Vat { get; }
		int Currency { get; }
		Com.InvoiceStatus Status { get; }
		DateTime ConfirmationDateTime { get; }
		InvoiceAmendmentFlags AmendmentFlags { get; }
	}

	[ComVisible(true)]
	[Guid("9BD6E4C6-02E8-475B-9BA6-92C75DF89304")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IInvoiceRevisionMetadata))]
	public partial class InvoiceRevisionMetadata : SafeComObject, IInvoiceRevisionMetadata
	{
		public DateTime ConfirmationDateTime
		{
			get { return new DateTime(ConfirmationDateTimeTicks); }
		}

		public InvoiceAmendmentFlags AmendmentFlags
		{
			get { return (InvoiceAmendmentFlags)InvoiceAmendmentFlags; }
		}

		public Com.InvoiceStatus Status
		{
			get { return (Com.InvoiceStatus)((int)InvoiceRevisionStatus); }
		}
	}

	[ComVisible(true)]
	[Guid("28EFB999-1A6F-42FE-997A-DD26605B40F9")]
	public interface IInvoiceCorrectionMetadata
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
		Com.InvoiceStatus Status { get; }
		DateTime ConfirmationDateTime { get; }
		InvoiceAmendmentFlags AmendmentFlags { get; }
	}

	[ComVisible(true)]
	[Guid("EC8563F8-9202-46B1-A005-8A5B2D2CCBFD")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IInvoiceCorrectionMetadata))]
	public partial class InvoiceCorrectionMetadata : SafeComObject, IInvoiceCorrectionMetadata
	{
		public DateTime ConfirmationDateTime
		{
			get { return new DateTime(ConfirmationDateTimeTicks); }
		}

		public InvoiceAmendmentFlags AmendmentFlags
		{
			get { return (InvoiceAmendmentFlags)InvoiceAmendmentFlags; }
		}

		public Com.InvoiceStatus Status
		{
			get { return (Com.InvoiceStatus)((int)InvoiceCorrectionStatus); }
		}
	}

	[ComVisible(true)]
	[Guid("E6CFF535-DE90-4D02-BCB5-870B29902B20")]
	public interface IInvoiceCorrectionRevisionMetadata
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
		Com.InvoiceStatus Status { get; }
		DateTime ConfirmationDateTime { get; }
		InvoiceAmendmentFlags AmendmentFlags { get; }
	}

	[ComVisible(true)]
	[Guid("411428B0-BB59-4C90-B376-3A3FA68DC1AB")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IInvoiceCorrectionRevisionMetadata))]
	public partial class InvoiceCorrectionRevisionMetadata : SafeComObject, IInvoiceCorrectionRevisionMetadata
	{
		public DateTime ConfirmationDateTime
		{
			get { return new DateTime(ConfirmationDateTimeTicks); }
		}

		public InvoiceAmendmentFlags AmendmentFlags
		{
			get { return (InvoiceAmendmentFlags)InvoiceAmendmentFlags; }
		}

		public Com.InvoiceStatus Status
		{
			get { return (Com.InvoiceStatus)((int)InvoiceCorrectionRevisionStatus); }
		}
	}
}
