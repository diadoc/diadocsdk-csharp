namespace Diadoc.Console.Command
{
	class AttorneyCommand : ConsoleCommandBase
	{
		public AttorneyCommand(ConsoleContext consoleContext, string command = "attorney") : base(consoleContext, command, CommandType.AuthenticationRequired)
		{
			Usage = "enable|disable";
			Description = "Set attorney signature";
		}

		protected override void PerformRunCommand(string[] args)
		{
			if (args.Length < 1)
				throw new UsageException();

			var arg = args[0];

			if (arg == "enable") ConsoleContext.SignByAttorney = true;
			if (arg == "disable") ConsoleContext.SignByAttorney = false;
			System.Console.WriteLine("Режим формирования ЭЦП по доверенности {0}.", ConsoleContext.SignByAttorney ? "включен" : "выключен");
		}
	}
}
