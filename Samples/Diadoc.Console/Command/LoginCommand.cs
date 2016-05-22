namespace Diadoc.Console.Command
{
	public class LoginCommand : ConsoleCommandBase
	{
		public LoginCommand(ConsoleContext consoleContext, string command = "login") : base(consoleContext, command, CommandType.AuthenticationCommand)
		{
			Description = "Login with credentials";
		}

		protected override void PerformRunCommand(string[] args)
		{
			System.Console.Write("Login: ");
			var login = System.Console.ReadLine();
			System.Console.Write("Password: ");
			var password = InputHelpers.ReadPasswordFromConsole();
			ConsoleContext.CurrentToken = ConsoleContext.DiadocApi.Authenticate(login, password);
			ConsoleContext.ClearAuthenticationContext();
			System.Console.WriteLine("Аутентификация пройдена.");
			OutputHelpers.ShowBoxes(ConsoleContext);
		}
	}
}
