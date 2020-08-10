using System;
using System.IO;
using System.Threading;
using Diadoc.Api;

namespace Diadoc.Console.Command
{
	class PrintCommand : ConsoleCommandBase
	{
		public PrintCommand(ConsoleContext consoleContext, string command = "print")
			: base(consoleContext, command, CommandType.AuthenticationRequired)
		{
			Usage = "<messageId> <documentId>";
		}

		protected override void PerformRunCommand(string[] args)
		{
			if (args.Length < 2)
				throw new UsageException();

			string messageId = args[0];
			string documentId = args[1];

			var dir = new DirectoryInfo("print-forms");
			if (!dir.Exists)
			{
				dir.Create();
			}

			PrintFormResult printFormResult;
			do
			{
				printFormResult = ConsoleContext.DiadocApi.GeneratePrintForm(ConsoleContext.CurrentToken, ConsoleContext.CurrentBoxId, messageId, documentId);
				var secondsToWait = printFormResult.RetryAfter;

				if (secondsToWait > 0)
				{
					System.Console.WriteLine($"Печатная форма ещё не готова, ждем {secondsToWait} секунд");
					Thread.Sleep(TimeSpan.FromSeconds(secondsToWait));
				}
			} while (!printFormResult.HasContent);

			var contentFileName = printFormResult.Content.FileName;
			System.Console.WriteLine($"Печатная форма готова, отправлена в папку {dir.Name}. Имя файла: {contentFileName}");
			File.WriteAllBytes($"{dir}\\{contentFileName}", printFormResult.Content.Bytes);
		}
	}
}