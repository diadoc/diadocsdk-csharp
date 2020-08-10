using System;
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
			msg.DocumentAttachments.AddRange(newMsg.DocumentsToPost
				.Select(e =>
				{
					var document = new DocumentAttachment
					{
						TypeNamedId = e.TypeNamedId,
						Function = e.Function,
						Version = e.Version,
						SignedContent = new SignedContent
						{
							Content = e.Content,
							Signature = ConsoleContext.Crypt.Sign(e.Content, ConsoleContext.CurrentCert.RawData)
						},
						Comment = e.Comment,
						NeedRecipientSignature = e.NeedRecipientSignature,
					};

					foreach (var m in e.Metadata)
					{
						document.AddMetadataItem(new MetadataItem
						{
							Key = m.Key,
							Value = m.Value
						});
					}

					return document;
				}));
			var messagePosted = ConsoleContext.DiadocApi.PostMessage(ConsoleContext.CurrentToken, msg, Guid.NewGuid().ToString("N"));
			System.Console.WriteLine("Было отправлено следующее письмо:");
			messagePosted.WriteToConsole();
		}
	}
}