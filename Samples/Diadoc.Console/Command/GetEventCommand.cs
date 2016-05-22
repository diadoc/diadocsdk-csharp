using System;
using System.Linq;

namespace Diadoc.Console.Command
{
	class GetEventCommand : EntityCommandBase
	{
		public GetEventCommand(ConsoleContext consoleContext, string command = "event")
			: base(consoleContext, command, CommandType.AuthenticationRequired)
		{
			Usage = "<eventId>";
			Description = "Get event";
		}

		protected override void PerformRunCommand(string[] args)
		{
			if (args.Length < 1)
				throw new UsageException();

			var eventId = args[0];

			eventId = AutocompleteEventId(eventId);
			var @event = ConsoleContext.DiadocApi.GetEvent(ConsoleContext.CurrentToken, ConsoleContext.CurrentBoxId, eventId);
			if (@event.Message != null)
			{
				LoadContentForEntities(@event.Message.MessageId, @event.Message.Entities, true);
				@event.Message.WriteToConsole();
				LoadContentForEntities(@event.Message.MessageId, @event.Message.Entities, false);
			}
			else
			{
				LoadContentForEntities(@event.Patch.MessageId, @event.Patch.Entities, true);
				@event.Patch.WriteToConsole();
				LoadContentForEntities(@event.Patch.MessageId, @event.Patch.Entities, false);
			}
			SaveEntitiesToDirectory(eventId, @event.Entities);

			System.Console.WriteLine();
			System.Console.WriteLine("Все вложения и подписи были сохранены в папку {0}", eventId);
		}

		private string AutocompleteEventId(string eventId)
		{
			if (eventId != null && ConsoleContext.Events != null)
			{
				var foundId = ConsoleContext.Events.Events
					.Select(oldEvt => oldEvt.EventId)
					.FirstOrDefault(id => id.StartsWith(eventId, StringComparison.InvariantCultureIgnoreCase));
				if (foundId != null) return foundId;
			}
			return eventId;
		}
	}
}
