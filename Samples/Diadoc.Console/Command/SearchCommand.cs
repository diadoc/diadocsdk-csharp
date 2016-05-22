using System;
using System.Linq;

namespace Diadoc.Console.Command
{
	class SearchCommand : ConsoleCommandBase
	{
		public SearchCommand(ConsoleContext consoleContext, string command = "search") : base(consoleContext, command, CommandType.AuthenticationRequired)
		{
			Description = "Search organization by INN/KPP";
		}

		protected override void PerformRunCommand(string[] args)
		{
			System.Console.Write("ИНН: ");
			var inn = System.Console.ReadLine();
			if (String.IsNullOrWhiteSpace(inn))
			{
				System.Console.WriteLine("Нельзя искать по пустому ИНН");
				return;
			}

			System.Console.Write("КПП (может быть пустым): ");
			var kpp = System.Console.ReadLine();
			var foundOrgs = ConsoleContext.DiadocApi.GetOrganizationsByInnKpp(inn, kpp).Organizations;
			System.Console.WriteLine(foundOrgs.Count == 0 ? "Организаций не найдено" : "Список найденных организаций:");
			OutputHelpers.PrintBoxes(foundOrgs);
			ConsoleContext.Boxes = foundOrgs.SelectMany(o => o.Boxes).ToList();
		}
	}
}
