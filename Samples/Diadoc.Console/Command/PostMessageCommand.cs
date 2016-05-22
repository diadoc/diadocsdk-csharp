using System.Linq;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Console.Command
{
	class PostMessageCommand : ConsoleCommandBase
	{
		public PostMessageCommand(ConsoleContext consoleContext, string command = "post")
			: base(consoleContext, command, CommandType.AuthenticationRequired)
		{
			Description = "Post message";
		}

		protected override void PerformRunCommand(string[] args)
		{
			var newMsg = NewMessage.ReadFromConsole();
			var msg = new MessageToPost
			{
				FromBoxId = ConsoleContext.CurrentBoxId,
				ToBoxId = InputHelpers.AutocompleteBoxId(ConsoleContext, newMsg.ToBoxId),
			};
			msg.NonformalizedDocuments.AddRange(newMsg.Attachments
				.Where(e => e.AttachmentType == AttachmentType.Nonformalized)
				.Select(e => new NonformalizedAttachment
				{
					FileName = e.FileName,
					SignedContent = new SignedContent
					{
						Content = e.Content,
						Signature = ConsoleContext.SignByAttorney ? null : ConsoleContext.Crypt.Sign(e.Content, ConsoleContext.CurrentCert.RawData),
						SignByAttorney = ConsoleContext.SignByAttorney
					},
					Comment = e.Comment,
					NeedRecipientSignature = e.NeedRecipientSignature,
				}));
			msg.Invoices.AddRange(newMsg.Attachments
				.Where(e => e.AttachmentType == AttachmentType.Invoice)
				.Select(e => new XmlDocumentAttachment
				{
					SignedContent = new SignedContent
					{
						Content = e.Content,
						Signature = ConsoleContext.SignByAttorney ? null : ConsoleContext.Crypt.Sign(e.Content, ConsoleContext.CurrentCert.RawData),
						SignByAttorney = ConsoleContext.SignByAttorney,
					},
					Comment = e.Comment,
				}));
			var messagePosted = ConsoleContext.DiadocApi.PostMessage(ConsoleContext.CurrentToken, msg);
			System.Console.WriteLine("Было отправлено следующее письмо:");
			messagePosted.WriteToConsole();
		}
	}
}
