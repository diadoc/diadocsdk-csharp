namespace Diadoc.Console.Command
{
	public class StatusCommand : ConsoleCommandBase
	{
		public StatusCommand(ConsoleContext consoleContext, string command = "status") : base(consoleContext, command, CommandType.AuthenticationNotRequired)
		{
			Description = "Show the working status";
		}

		protected override void PerformRunCommand(string[] args)
		{
			System.Console.WriteLine();
			System.Console.WriteLine("Status:");
			System.Console.WriteLine("Authenticated: {0}", ConsoleContext.IsAuthenticated() ? "YES" : "NO");
			if (ConsoleContext.IsAuthenticated())
			{
				System.Console.WriteLine("Selected Organization: {0}", ConsoleContext.CurrentOrgId ?? "None");
				System.Console.WriteLine("Selected Box: {0}", ConsoleContext.CurrentBoxId ?? "None");
			}
		}
	}
}
