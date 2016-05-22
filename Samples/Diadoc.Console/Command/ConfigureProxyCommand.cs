namespace Diadoc.Console.Command
{
	class ConfigureProxyCommand : ConsoleCommandBase
	{
		public ConfigureProxyCommand(ConsoleContext consoleContext, string command = "proxy")
			: base(consoleContext, command, CommandType.AuthenticationNotRequired)
		{
			Description = "Configure proxy";
		}

		protected override void PerformRunCommand(string[] args)
		{
			if (!InputHelpers.YesNoChoice(true, "Использовать системный прокси? (по-умолчанию 'Y')"))
			{
				ConsoleContext.DiadocApi.DisableSystemProxyUsage();
				System.Console.WriteLine("Прокси не будет использоваться в последующих запросах.");
				return;
			}
			ConsoleContext.DiadocApi.EnableSystemProxyUsage();
			SetProxyCredentials();
			System.Console.WriteLine("Прокси настроен.");
		}

		private void SetProxyCredentials()
		{
			System.Console.Write("Имя пользователя: ");
			var username = System.Console.ReadLine();
			System.Console.Write("Пароль: ");
			var password = System.Console.ReadLine();
			ConsoleContext.DiadocApi.SetProxyCredentials(username, password);
		}
	}
}
