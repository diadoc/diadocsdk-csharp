using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Diadoc.Console
{
	internal class NewMessage
	{
		public string ToBoxId { get; set; }
		private readonly List<DocumentToPost> documentsToPost = new List<DocumentToPost>();

		public List<DocumentToPost> DocumentsToPost => documentsToPost;

		public static NewMessage ReadFromConsole()
		{
			var msg = new NewMessage();
			System.Console.Write("Кому (BoxId): ");
			msg.ToBoxId = System.Console.ReadLine();

			while (true)
			{
				if (!InputHelpers.YesNoChoice(false, "Добавить вложение?"))
				{
					break;
				}

				msg.DocumentsToPost.Add(DocumentToPost.ReadFromConsole());
			}

			return msg;
		}
	}

	internal class DocumentToPost
	{
		public string TypeNamedId { get; set; }
		public string Function { get; set; }
		public string Version { get; set; }
		public Dictionary<string, string> Metadata { get; set; }
		public byte[] Content { get; set; }
		public string Comment { get; set; }
		public bool NeedRecipientSignature { get; set; }

		public static DocumentToPost ReadFromConsole()
		{
			var documentToPost = new DocumentToPost();
			System.Console.Write("Тип вложения (typeNamedId). Например, Nonformalized или UniversalTransferDocument: ");
			var typeNamedId = System.Console.ReadLine();
			documentToPost.TypeNamedId = typeNamedId ?? "Nonformalized";

			System.Console.Write("Функция вложения (enter чтобы пропустить):");
			documentToPost.Function = System.Console.ReadLine();

			System.Console.Write("Версия вложения (enter чтобы пропустить):");
			documentToPost.Version = System.Console.ReadLine();

			ReadFileWithComment(ref documentToPost);
			if (documentToPost.TypeNamedId == "Nonformalized")
			{
				if (InputHelpers.YesNoChoice(false, "Запросить ответную подпись?"))
					documentToPost.NeedRecipientSignature = true;
			}

			documentToPost.Metadata = ReadMetadata();

			return documentToPost;
		}

		private static void ReadFileWithComment(ref DocumentToPost documentToPost)
		{
			System.Console.Write("Путь до файла: ");
			var filePath = ReadFilePath();
			documentToPost.Content = File.ReadAllBytes(filePath);
			if (!InputHelpers.YesNoChoice(false, "Ввести комментарий?")) return;
			System.Console.WriteLine("Комментарий к вложению (конец - двойной перевод строки):");
			documentToPost.Comment = ReadCommentFromKeyboard();
		}

		private static Dictionary<string, string> ReadMetadata()
		{
			var metadata = new Dictionary<string, string>();

			if (!InputHelpers.YesNoChoice(false, "Добавить метаданные?")) return metadata;
			do
			{
				ReadKeyValue(metadata);
			} while (InputHelpers.YesNoChoice(false, "Добавить ещё значение?"));

			return metadata;
		}

		private static void ReadKeyValue(Dictionary<string, string> metadata)
		{
			string key;
			do
			{
				System.Console.Write("Ключ: ");
				key = System.Console.ReadLine();
			} while (string.IsNullOrEmpty(key));

			System.Console.Write("Значение: ");
			var value = System.Console.ReadLine();
			metadata[key] = value;
		}

		private static string ReadFilePath()
		{
			var sb = new StringBuilder();
			while (true)
			{
				var k = System.Console.ReadKey(true);
				if (k.Key == ConsoleKey.Tab)
				{
					if (sb.Length > 2)
					{
						var filePath = DoAutocompleteFilePath(sb.ToString());
						if (filePath == null)
						{
							sb.Clear();
							continue;
						}

						return filePath;
					}

					continue;
				}

				if (k.Key == ConsoleKey.Backspace)
				{
					if (sb.Length > 0)
					{
						--sb.Length;
						System.Console.Write("\b \b");
					}

					continue;
				}

				if (k.Key == ConsoleKey.Enter)
				{
					System.Console.WriteLine();
					break;
				}

				System.Console.Write(k.KeyChar);
				sb.Append(k.KeyChar);
			}

			return sb.ToString();
		}

		private static string DoAutocompleteFilePath(string currentFilePath)
		{
			var files = Directory.EnumerateFiles(Environment.CurrentDirectory)
				.Select(Path.GetFileName)
				.Where(f => f != null && f.StartsWith(currentFilePath, StringComparison.CurrentCultureIgnoreCase)).ToArray();
			switch (files.Length)
			{
				case 0:
					System.Console.WriteLine();
					System.Console.WriteLine("Нет таких файлов");
					break;
				case 1:
					var filePath = files.First();
					System.Console.WriteLine(filePath.Substring(currentFilePath.Length));
					return filePath;
				default:
					System.Console.WriteLine();
					foreach (var file in files)
						System.Console.WriteLine(file);
					break;
			}

			return null;
		}

		private static string ReadCommentFromKeyboard()
		{
			var content = "";
			var lastLineWasEmpty = false;
			while (true)
			{
				var line = System.Console.ReadLine();
				if (lastLineWasEmpty && string.IsNullOrEmpty(line)) return content;
				lastLineWasEmpty = string.IsNullOrEmpty(line);
				content += line + Environment.NewLine;
			}
		}
	}
}