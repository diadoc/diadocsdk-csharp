namespace Diadoc.Console.Command
{
	class PrintCommand : ConsoleCommandBase
	{
		public PrintCommand(ConsoleContext consoleContext, string command = "print")
			: base(consoleContext, command, CommandType.AuthenticationRequired)
		{
			Usage = "<messageId> <documentId>";
		}

		protected override void PerformRunCommand(string[] args)
		{
			if (args.Length < 2)
				throw new UsageException();

			string messageId = args[0];
			string documentId = args[1];

			var printFormResult = ConsoleContext.DiadocApi.GeneratePrintForm(ConsoleContext.CurrentToken, ConsoleContext.CurrentBoxId, messageId, documentId);
			System.Console.WriteLine(printFormResult.ToString());
		}
	}
}
