namespace Diadoc.Console.Command
{
	class GetProxyStatusCommand : ConsoleCommandBase
	{
		public GetProxyStatusCommand(ConsoleContext consoleContext, string command = "proxystatus")
			: base(consoleContext, command, CommandType.AuthenticationNotRequired)
		{
			Description = "Get proxy status";
		}

		protected override void PerformRunCommand(string[] args)
		{
			System.Console.WriteLine(ConsoleContext.DiadocApi.UsingSystemProxy ? "Используем системные настройки прокси" : "Не используем системные настройки - работаем без прокси");
		}
	}
}
