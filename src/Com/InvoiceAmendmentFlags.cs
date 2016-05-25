using System;
using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[Flags]
	[ComVisible(true)]
	[Guid("5F9F07D8-2763-4EC3-A607-F3930D62E1D8")]
	public enum InvoiceAmendmentFlags
	{
		None = 0,
		AmendmentRequested = 1,
		Revised = 2,
		Corrected = 4,
	}

	public static class InvoiceAmendmentFlagsExtensions
	{
		public static bool IsRevised(this InvoiceAmendmentFlags status)
		{
			return (status & InvoiceAmendmentFlags.Revised) == InvoiceAmendmentFlags.Revised;
		}

		public static bool IsCorrected(this InvoiceAmendmentFlags status)
		{
			return (status & InvoiceAmendmentFlags.Corrected) == InvoiceAmendmentFlags.Corrected;
		}

		public static bool IsAmendmentRequested(this InvoiceAmendmentFlags status)
		{
			return (status & InvoiceAmendmentFlags.AmendmentRequested) == InvoiceAmendmentFlags.AmendmentRequested;
		}
	}
}
