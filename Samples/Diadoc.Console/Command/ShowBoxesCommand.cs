namespace Diadoc.Console.Command
{
	class ShowBoxesCommand : ConsoleCommandBase
	{
		public ShowBoxesCommand(ConsoleContext consoleContext, string command = "boxes") : base(consoleContext, command, CommandType.AuthenticationRequired)
		{
			Description = "Show available boxes";
		}

		protected override void PerformRunCommand(string[] args)
		{
			OutputHelpers.ShowBoxes(ConsoleContext);
		}
	}
}