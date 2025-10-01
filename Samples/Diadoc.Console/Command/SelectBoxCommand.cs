namespace Diadoc.Console.Command
{
	class SelectBoxCommand : ConsoleCommandBase
	{
		public SelectBoxCommand(ConsoleContext consoleContext, string command = "box") : base(consoleContext, command, CommandType.AuthenticationRequired)
		{
			Usage = "[ <boxId> ]";
			Description = "Select box";
		}

		protected override void PerformRunCommand(string[] args)
		{
			var boxId = args.Length > 0 ? args[0] : null;

			if (boxId == null)
			{
				OutputHelpers.ShowBoxes(ConsoleContext);
				return;
			}

			ConsoleContext.SetCurrentBoxWithBoxId(InputHelpers.AutocompleteBoxId(ConsoleContext, boxId));
			System.Console.WriteLine($"Текущий ящик, BoxId: {ConsoleContext.CurrentBoxId}");
			ConsoleContext.Events = null;
		}
	}
}
