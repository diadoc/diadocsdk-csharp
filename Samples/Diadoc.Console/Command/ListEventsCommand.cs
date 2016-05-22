using System.Linq;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Console.Command
{
	class ListEventsCommand : ConsoleCommandBase
	{
		public ListEventsCommand(ConsoleContext consoleContext, string command = "list")
			: base(consoleContext, command, CommandType.AuthenticationRequired)
		{
			Usage = "[ <afterEventId> ]";
			Description = "List events";
		}

		protected override void PerformRunCommand(string[] args)
		{
			var afterEventId = args.Length > 0 ? args[0] : null;

			var eventIdCurrent = afterEventId;
			do
			{
				ConsoleContext.Events = ConsoleContext.DiadocApi.GetNewEvents(ConsoleContext.CurrentToken, ConsoleContext.CurrentBoxId, eventIdCurrent);
				foreach (var boxEvent in ConsoleContext.Events.Events)
				{
					System.Console.Write("{0} {1} {2}", boxEvent.Timestamp, boxEvent.EventId, boxEvent.MessageId);
					if (boxEvent.Message != null)
						System.Console.WriteLine(" [{0} -> {1}]", boxEvent.Message.FromTitle, boxEvent.Message.ToTitle);
					else
						System.Console.WriteLine();
					var attachments = boxEvent.Entities
						.Where(e => e.EntityType == EntityType.Attachment && e.AttachmentType != AttachmentType.AttachmentComment);
					foreach (var attach in attachments)
						System.Console.WriteLine("\t{0} [{1}]", attach.FileName, attach.AttachmentType);
				}

				if (ConsoleContext.Events.TotalCount <= ConsoleContext.Events.Events.Count) break;
				eventIdCurrent = ConsoleContext.Events.Events.Last().EventId;
			} while (InputHelpers.YesNoChoice(false, "...Есть ещё письма... Показать следующую страницу?"));
		}
	}
}
