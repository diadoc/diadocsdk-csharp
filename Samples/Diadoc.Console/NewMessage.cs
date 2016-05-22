using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Console
{
	internal class NewMessage
	{
		public string ToBoxId { get; set; }
		private readonly List<Attachment> attachments = new List<Attachment>();
		public List<Attachment> Attachments { get { return attachments; } }

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
				msg.Attachments.Add(Attachment.ReadFromConsole());
			}
			return msg;
		}
	}

	internal class Attachment
	{
		public AttachmentType AttachmentType { get; set; }
		public string FileName { get; set; }
		public byte[] Content { get; set; }
		public string Comment { get; set; }
		public bool NeedRecipientSignature { get; set; }

		public static Attachment ReadFromConsole()
		{
			var attachment = new Attachment();
			System.Console.Write("Тип вложения [text/invoice]: ");
			var attachmentType = System.Console.ReadLine();
			attachment.AttachmentType = ParseAttachmentType(attachmentType);
			ReadFileWithComment(ref attachment);
			if (attachment.AttachmentType == AttachmentType.Nonformalized)
			{
				if (InputHelpers.YesNoChoice(false, "Запросить ответную подпись?"))
					attachment.NeedRecipientSignature = true;
			}
			return attachment;
		}

		private static AttachmentType ParseAttachmentType(string attachmentType)
		{
			switch (attachmentType)
			{
				case "text":
					return AttachmentType.Nonformalized;
				case "invoice":
					return AttachmentType.Invoice;
				default:
					return AttachmentType.Nonformalized;
			}
		}

		private static void ReadFileWithComment(ref Attachment attachment)
		{
			System.Console.Write("Имя файла: ");
			var fileName = ReadFileName();
			attachment.FileName = Path.GetFileName(fileName);
			attachment.Content = File.ReadAllBytes(fileName);
			if (!InputHelpers.YesNoChoice(false, "Ввести комментарий?")) return;
			System.Console.WriteLine("Комментарий к вложению (конец - двойной перевод строки):");
			attachment.Comment = ReadCommentFromKeyboard();
		}

		private static string ReadFileName()
		{
			var sb = new StringBuilder();
			while (true)
			{
				var k = System.Console.ReadKey(true);
				if (k.Key == ConsoleKey.Tab)
				{
					if (sb.Length > 2)
					{
						var fileName = DoAutocompleteFileName(sb.ToString());
						if (fileName == null)
						{
							sb.Clear();
							continue;
						}
						return fileName;
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

		private static string DoAutocompleteFileName(string currentFileName)
		{
			var files = Directory.EnumerateFiles(Environment.CurrentDirectory)
				.Select(Path.GetFileName)
				.Where(fn => fn != null && fn.StartsWith(currentFileName, StringComparison.CurrentCultureIgnoreCase)).ToArray();
			switch (files.Length)
			{
				case 0:
					System.Console.WriteLine();
					System.Console.WriteLine("Нет таких файлов");
					break;
				case 1:
					var fileName = files.First();
					System.Console.WriteLine(fileName.Substring(currentFileName.Length));
					return fileName;
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
