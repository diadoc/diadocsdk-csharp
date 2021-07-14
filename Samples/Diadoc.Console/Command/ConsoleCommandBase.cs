using System;
using System.Net;
using Diadoc.Api.Http;

namespace Diadoc.Console.Command
{
	public interface IConsoleCommand
	{
		string Command { get; }
		string Usage { get; }
		string Description { get; }
		void RunCommand(string[] args);
		bool IsInContext();
	}

	public abstract class ConsoleCommandBase : IConsoleCommand
	{
		protected readonly ConsoleContext ConsoleContext;

		protected ConsoleCommandBase(ConsoleContext consoleContext, string command, CommandType commandType = CommandType.AuthenticationRequired)
		{
			ConsoleContext = consoleContext;
			Command = command;
			CommandType = commandType;
			Usage = "";
			Description = "no description";
		}
		private CommandType CommandType { get; }

		public string Command { get; }
		public string Usage { get; protected set; }
		public string Description { get; protected set; }

		public virtual bool IsInContext()
		{
			switch (CommandType)
			{
				case CommandType.AuthenticationCommand:
					return !ConsoleContext.IsAuthenticated();
				case CommandType.AuthenticationRequired:
					return ConsoleContext.IsAuthenticated();
				case CommandType.AuthenticationNotRequired:
					return true;
			}

			return false;
		}

		public void RunCommand(string[] args)
		{
			try
			{
				if (RequiresAuthentication())
				{
					System.Console.WriteLine("Невозможно выполнить команду: не выполнена аутентификация");
					return;
				}

				PerformRunCommand(args);
			}
			catch (UsageException e)
			{
				HandleUsageException(e);
			}
			catch (HttpClientException e)
			{
				HandleHttpClientException(e);
			}
			catch (Exception e)
			{
				HandleUnknownException(e);
			}
		}

		private bool RequiresAuthentication()
		{
			return CommandType == CommandType.AuthenticationRequired && !ConsoleContext.IsAuthenticated();
		}

		protected abstract void PerformRunCommand(string[] args);

		private static void HandleHttpClientException(HttpClientException exception)
		{
			System.Console.WriteLine("При обработке команды произошла ошибка:");
			if (exception.ResponseStatusCode.HasValue &&
				!TryHandleExceptionCode(exception.ResponseStatusCode.Value, exception.RequestPathAndQuery))
			{
				HandleUnknownException(exception);
			}
			else
			{
				HandleUnknownException(exception);
			}
		}

		private void HandleUsageException(UsageException usageException)
		{
			System.Console.WriteLine("Неправильный синтакс комманды:");
			System.Console.WriteLine(OutputHelpers.FormatCommandSyntax(this));
		}

		private static void HandleUnknownException(Exception e)
		{
			System.Console.WriteLine("При выполнении операции произошла ошибка:");
			System.Console.WriteLine(e);
		}

		private static bool TryHandleExceptionCode(HttpStatusCode code, string queryString)
		{
			switch (code)
			{
				case HttpStatusCode.ProxyAuthenticationRequired:
					System.Console.WriteLine("Ошибка HTTP 407: Неправильные аутентификационные данные для прокси. Измените имя пользователя и пароль, используя команду proxy");
					return true;
				case HttpStatusCode.Forbidden:
					System.Console.WriteLine("Ошибка HTTP 403: Возможно, неправильные аутентификационные данные для прокси. Попробуйте изменить имя пользователя и пароль, используя команду proxy");
					return true;
				case HttpStatusCode.BadGateway:
					System.Console.WriteLine("Ошибка HTTP 502: Прокси не смог получить затребованный URL:" + queryString);
					return true;
				case HttpStatusCode.GatewayTimeout:
					System.Console.WriteLine("Ошибка HTTP 504: Прокси не смог получить затребованный URL:" + queryString + " - время ожидания истекло");
					return true;
				case HttpStatusCode.Unauthorized:
					System.Console.WriteLine("Ошибка HTTP 401: Доступ запрещен. Возможно, неправильный ключ разработчика");
					return true;
				default:
					return false;
			}
		}
	}
}
