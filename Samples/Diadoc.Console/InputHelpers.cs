using System;
using System.Linq;
using System.Text;

namespace Diadoc.Console
{
	public static class InputHelpers
	{
		public static bool YesNoChoice(bool defaultValue, string message, params object[] args)
		{
			System.Console.Write(message, args);
			System.Console.Write(" [Y/N]");
			char reqChar = Char.ToUpper(System.Console.ReadKey().KeyChar);
			System.Console.WriteLine();
			return defaultValue ? reqChar != 'N' : reqChar == 'Y';
		}

		public static string ReadPasswordFromConsole()
		{
			var sb = new StringBuilder();
			while (true)
			{
				var info = System.Console.ReadKey(true);
				if (info.Key == ConsoleKey.Enter) break;
				if (info.Key == ConsoleKey.Backspace)
				{
					if (sb.Length > 0)
					{
						--sb.Length;
						System.Console.Write("\b \b");
					}
				}
				else
				{
					sb.Append(info.KeyChar);
					System.Console.Write('*');
				}
			}
			System.Console.WriteLine();
			return sb.ToString();
		}

		public static string AutocompleteBoxId(ConsoleContext consoleContext, string boxId)
		{
			if (consoleContext.Boxes != null)
			{
				var foundId = consoleContext.Boxes
					.Select(b => b.BoxId)
					.FirstOrDefault(id => id.StartsWith(boxId, StringComparison.InvariantCultureIgnoreCase));
				if (foundId != null) return foundId;
			}
			return boxId;
		}
	}
}
