using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Console.Command
{
	abstract class EntityCommandBase : ConsoleCommandBase
	{
		protected EntityCommandBase(ConsoleContext consoleContext, string command, CommandType commandType) : base(consoleContext, command, commandType) {}
		
		public void LoadContentForEntities(string messageId, IEnumerable<Entity> entities, bool interactive)
		{
			foreach (var entity in entities)
			{
				if (entity.Content == null || entity.Content.Data != null) continue;
				bool doLoad = true;
				if (interactive && entity.EntityType != EntityType.Signature && entity.AttachmentType != AttachmentType.AttachmentComment)
				{
					doLoad &= InputHelpers.YesNoChoice(false, "Чтобы загрузить содержимое вложения \"{0}\" [{1}] требуется сделать дополнительный запрос к серверу. Загрузить вложение?",
						entity.FileName, entity.AttachmentType);
				}
				if (doLoad)
					entity.Content.Data = ConsoleContext.DiadocApi.GetEntityContent(ConsoleContext.CurrentToken, ConsoleContext.CurrentBoxId, messageId, entity.EntityId);
			}
		}

		public static void SaveEntitiesToDirectory(string directoryName, IEnumerable<Entity> entities)
		{
			var dir = new DirectoryInfo(directoryName);
			dir.Create();

			foreach (var entity in entities.Where(e => e.Content != null))
			{
				if (entity.EntityType == EntityType.Attachment)
					File.WriteAllBytes(GetEntityDestinationPath(dir, entity.EntityId,
					                                            string.IsNullOrEmpty(entity.FileName) ? entity.EntityId : entity.FileName), entity.Content.Data);
				else if (entity.EntityType == EntityType.Signature)
					File.WriteAllBytes(GetEntityDestinationPath(dir, entity.ParentEntityId, entity.EntityId + ".p7s"), entity.Content.Data);
				else
				{
					var id = entity.ParentEntityId ?? entity.EntityId;
					File.WriteAllBytes(GetEntityDestinationPath(dir, id, entity.EntityId + "." + entity.EntityType), entity.Content.Data);
				}
			}
		}

		private static string GetEntityDestinationPath(DirectoryInfo dir, string entityId, string filename)
		{
			var attachmentDir = new DirectoryInfo(dir + "\\" + entityId);
			if (!attachmentDir.Exists) attachmentDir.Create();
			if (filename.Length > 30)
			{
				var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
				filename = fileNameWithoutExtension.Substring(0, Math.Min(fileNameWithoutExtension.Length, 30)) + Path.GetExtension(filename);
			}
			return attachmentDir.FullName + "\\" + filename;
		}
	}
}