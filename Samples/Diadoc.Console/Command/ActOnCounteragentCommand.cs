using Diadoc.Api.Proto;

namespace Diadoc.Console.Command
{
	public class ActOnCounteragentCommand : ConsoleCommandBase
	{
		public ActOnCounteragentCommand(ConsoleContext consoleContext, string command = "counteragent") : base(consoleContext, command, CommandType.AuthenticationRequired)
		{
			Usage = "-a|-b [ <boxId> ]";
			Description = "Perform operation on CA";
		}

		protected override void PerformRunCommand(string[] args)
		{
			if (args.Length < 2)
				throw new UsageException();

			var action = args[0];
			var boxId = args[1];

			System.Console.WriteLine("Comment:");
			var comment = System.Console.ReadLine();

			switch (action)
			{
				case "-a":
			        ConsoleContext.DiadocApi.AcquireCounteragentV3(ConsoleContext.CurrentToken, ConsoleContext.CurrentBoxId,
			            new AcquireCounteragentRequest
			            {
			                BoxId = boxId,
			                MessageToCounteragent = comment
			            });
					return;
				case "-b":
					ConsoleContext.DiadocApi.BreakWithCounteragentV2(ConsoleContext.CurrentToken, ConsoleContext.CurrentBoxId, boxId, comment);
					return;
			}
		}
	}
}
