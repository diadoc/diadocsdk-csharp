using System.Linq;

namespace Diadoc.Console.Command
{
	public class GetDocumentsCommand : ConsoleCommandBase
	{
		public GetDocumentsCommand(ConsoleContext consoleContext, string command = "documents") : base(consoleContext, command, CommandType.AuthenticationRequired)
		{
			Description = "Get documents";
			Usage = "<filterCategory>";
		}

		protected override void PerformRunCommand(string[] args)
		{
			var filterCategory = args.FirstOrDefault() ?? "Any.Inbound";
			var documents = ConsoleContext.DiadocApi.GetDocuments(
				ConsoleContext.CurrentToken,
				ConsoleContext.CurrentBoxId,
				filterCategory,
				null,
				null,
				null,
				null,
				null,
				null,
				false,
				null);
			System.Console.WriteLine("Документы по фильтру {0} ({1}):", filterCategory, documents.TotalCount);
			foreach (var d in documents.Documents)
			{
				System.Console.WriteLine($"DocumentId: {d.EntityId}, Тип-Функция-Версия: {d.TypeNamedId}-{d.Function}-{d.Version}");
			}
		}
	}
}