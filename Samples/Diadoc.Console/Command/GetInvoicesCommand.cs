namespace Diadoc.Console.Command
{
	class GetInvoicesCommand : ConsoleCommandBase
	{
		public GetInvoicesCommand(ConsoleContext consoleContext, string command = "invoices") : base(consoleContext, command, CommandType.AuthenticationRequired)
		{
			Description = "Get invoices";
		}

		protected override void PerformRunCommand(string[] args)
		{
			var invoices = ConsoleContext.DiadocApi.GetDocuments(ConsoleContext.CurrentToken, ConsoleContext.CurrentBoxId, "Invoice.Inbound", null, null, null, null, null, null, false, null);
			System.Console.WriteLine("Входящие счета-фактуры ({0}):", invoices.TotalCount);
			foreach (var invoice in invoices.Documents)
				System.Console.WriteLine("#{0} CreationTimestamp: {1} DocumentDate: {2} Total: {3} ConfirmationDate: {4}", invoice.DocumentNumber, invoice.CreationTimestamp, invoice.DocumentDate, invoice.InvoiceMetadata.Total, invoice.InvoiceMetadata.ConfirmationDateTime);
		}
	}
}
