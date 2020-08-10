using System;
using System.Linq;
using Diadoc.Api.Proto;

namespace Diadoc.Console.Command
{
	public class ListCounteragentsCommand : ConsoleCommandBase
	{
		public ListCounteragentsCommand(ConsoleContext consoleContext, string command = "counteragents") : base(consoleContext, command, CommandType.AuthenticationRequired)
		{
			Usage = "<organizationId>";
			Description = "List counteragents";
		}

		protected override void PerformRunCommand(string[] args)
		{
			if (args.Length < 1)
				throw new UsageException();

			var orgId = args[0];
			orgId = AutocompleteOrgId(orgId);

			var myActiveCounteragents = ConsoleContext.DiadocApi.GetCounteragents(ConsoleContext.CurrentToken, orgId, "Active", null);
			var myInvitingCounteragents = ConsoleContext.DiadocApi.GetCounteragents(ConsoleContext.CurrentToken, orgId, "Inviting", null);
			var myIsInvitedCounteragents = ConsoleContext.DiadocApi.GetCounteragents(ConsoleContext.CurrentToken, orgId, "IsInvited", null);
			var myDeniedCounteragents = ConsoleContext.DiadocApi.GetCounteragents(ConsoleContext.CurrentToken, orgId, "Denied", null);

			PrintCounteragentList(myActiveCounteragents, "Active");
			PrintCounteragentList(myInvitingCounteragents, "Inviting");
			PrintCounteragentList(myIsInvitedCounteragents, "IsInvited");
			PrintCounteragentList(myDeniedCounteragents, "Denied");
		}

		private static void PrintCounteragentList(CounteragentList counteragentList, string title)
		{
			System.Console.WriteLine();
			System.Console.WriteLine($"{title} ({counteragentList.Counteragents.Count}/{counteragentList.TotalCount}): ");
			foreach (var counteragent in counteragentList.Counteragents)
				OutputHelpers.PrintOrganization(counteragent.Organization);
			System.Console.WriteLine();
		}

		private string AutocompleteOrgId(string orgId)
		{
			if (ConsoleContext.Orgs != null)
			{
				var foundId = ConsoleContext.Orgs
					.Select(o => o.OrgId)
					.FirstOrDefault(id => id.StartsWith(orgId, StringComparison.InvariantCultureIgnoreCase));
				if (foundId != null) return foundId;
			}

			return orgId;
		}
	}
}