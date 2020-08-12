using System;
using System.Collections.Generic;
using System.Linq;
using Diadoc.Api;
using Diadoc.Api.Cryptography;
using Diadoc.Console.Command;

namespace Diadoc.Console
{
	internal class Program
	{
		private const string DefaultApiUrl = "https://diadoc-api.kontur.ru";

		private readonly ConsoleContext consoleContext;
		private readonly IList<IConsoleCommand> consoleCommands;

		private Program(DiadocApi diadocApi, ICrypt crypt)
		{
			consoleContext = new ConsoleContext
			{
				DiadocApi = diadocApi,
				Crypt = crypt
			};

			consoleCommands = new List<IConsoleCommand>
			{
				new AuthenticateCommand(consoleContext),
				new LoginCommand(consoleContext),
				new ListCounteragentsCommand(consoleContext),
				new ActOnCounteragentCommand(consoleContext),
				new SelectBoxCommand(consoleContext),
				new ShowBoxesCommand(consoleContext),
				new ListEventsCommand(consoleContext),
				new GetEventCommand(consoleContext),
				new GetMessageCommand(consoleContext),
				new PostMessageCommand(consoleContext),
				new PrintCommand(consoleContext),
				new ConfigureProxyCommand(consoleContext),
				new GetProxyStatusCommand(consoleContext),
				new SearchCommand(consoleContext),
				new GetDocumentsCommand(consoleContext),
				new StatusCommand(consoleContext),
				new ExitCommand(consoleContext)
			};
		}

		private static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				ShowUsage();
				return;
			}
			var apiClientId = args[0];
			var apiUrl = args.Length > 1 ? args[1] : DefaultApiUrl;
			try
			{
				var crypt = new WinApiCrypt();
				var diadoc = new DiadocApi(apiClientId, apiUrl, crypt);
				new Program(diadoc, crypt).Run();
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e);
				Environment.Exit(1);
			}
		}

		private static void ShowUsage()
		{
			System.Console.WriteLine("Usage: ddconsole.exe apiClientId [serverUrl]");
			System.Console.WriteLine("ApiClientId is your personal developer key (e.g. test-8ee1638deae84c86b8e2069955c2825a)");
			System.Console.WriteLine("Default serverUrl is {0}", DefaultApiUrl);
		}

		private IEnumerable<IConsoleCommand> GetAvailableCommands()
		{
			return consoleCommands.Where(c => c.IsInContext());
		}

		private void ShowHelp()
		{
			System.Console.WriteLine("SKBKontur Diadoc SDK. Sample application. http://www.diadoc.ru");
			System.Console.WriteLine("Доступные команды:");

			var availableCommands = GetAvailableCommands();
			foreach (var command in availableCommands)
				System.Console.WriteLine(OutputHelpers.FormatCommandSyntax(command));
		}

		private void Run()
		{
			ShowHelp();
			while (true)
			{
				try
				{
					System.Console.WriteLine();
					var userInput = System.Console.ReadLine();
					if (userInput == null) return; //in case user pressed ctrl+c
					if (userInput == "") continue;

					var commandLine = userInput.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
					var commandString = commandLine[0];
					var args = commandLine.Length > 1 ? commandLine.Skip(1) : new List<string>();
					var consoleCommand = GetAvailableCommands().FirstOrDefault(c => c.Command.Equals(commandString, StringComparison.OrdinalIgnoreCase));

					if (consoleCommand != null)
					{
						consoleCommand.RunCommand(args.ToArray());
						continue;
					}

					ShowHelp();
				}
				catch (Exception e)
				{
					System.Console.WriteLine("При выполнении операции произошла ошибка:");
					System.Console.WriteLine(e);
				}
			}
		}
	}
}
