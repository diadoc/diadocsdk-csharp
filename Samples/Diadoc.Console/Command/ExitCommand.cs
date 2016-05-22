using System;

namespace Diadoc.Console.Command
{
	class ExitCommand : ConsoleCommandBase
	{
		public ExitCommand(ConsoleContext consoleContext, string command = "exit") : base(consoleContext, command, CommandType.AuthenticationNotRequired) {}

		protected override void PerformRunCommand(string[] args)
		{
			Environment.Exit(0);
		}
	}
}
