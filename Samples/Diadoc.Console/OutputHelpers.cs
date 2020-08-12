using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Events;
using Diadoc.Console.Command;

namespace Diadoc.Console
{
	public static class OutputHelpers
	{
		public static void WriteToConsole(this Message msg)
		{
			System.Console.WriteLine();
			System.Console.WriteLine("MessageId: {0}", msg.MessageId);
			System.Console.WriteLine("From:      {0} ({1})", msg.FromTitle, msg.FromBoxId);
			System.Console.WriteLine("To:        {0} ({1})", msg.ToTitle, msg.ToBoxId);
			System.Console.WriteLine("Timestamp: {0}", msg.Timestamp);
			System.Console.WriteLine("Attachments:");
			WriteEntitiesToConsole(msg.Entities);
		}

		private static void WriteEntitiesToConsole(IEnumerable<Entity> entities)
		{
			foreach (var entity in entities.Where(e => e.EntityType == EntityType.Attachment && e.AttachmentType != AttachmentType.AttachmentComment))
			{
				var entityLocal = entity;
				var comment = entities.FirstOrDefault(c => c.ParentEntityId == entityLocal.EntityId &&
					c.EntityType == EntityType.Attachment && c.AttachmentType == AttachmentType.AttachmentComment);
				WriteEntityToConsole(entity, comment);
			}
		}

		private static void WriteEntityToConsole(Entity entity, Entity comment)
		{
			System.Console.WriteLine();
			System.Console.WriteLine("EntityId: {0}", entity.EntityId);
			System.Console.WriteLine("ParentEntityId: {0}", entity.ParentEntityId);
			System.Console.WriteLine("AttachmentType: {0}", entity.AttachmentType);
			System.Console.WriteLine("FileName: {0}", entity.FileName);
			if (entity.EntityType == EntityType.Attachment && entity.AttachmentType == AttachmentType.Nonformalized)
				System.Console.WriteLine("NeedRecipientSignature: {0}", entity.NeedRecipientSignature ? "Да" : "Нет");
			var documentInfo = entity.DocumentInfo;
			if (documentInfo != null)
			{
				System.Console.WriteLine("DocumentInfo.CounteragentBoxId: {0}", documentInfo.CounteragentBoxId);
				System.Console.WriteLine("DocumentInfo.DocumentDate: {0}", documentInfo.DocumentDate);
				System.Console.WriteLine("DocumentInfo.DocumentNumber: {0}", documentInfo.DocumentNumber);
				System.Console.WriteLine("DocumentInfo.DocumentType: {0}", documentInfo.DocumentType);
				System.Console.WriteLine("DocumentInfo.InitialDocumentIds: {0}", documentInfo.InitialDocumentIds.Aggregate(string.Empty, (s, id) => string.Format("{0}{1}:{2}, ", s, id.MessageId, id.EntityId)));
				System.Console.WriteLine("DocumentInfo.SubordinateDocumentIds: {0}", documentInfo.SubordinateDocumentIds.Aggregate(string.Empty, (s, id) => string.Format("{0}{1}:{2}, ", s, id.MessageId, id.EntityId)));
				if (documentInfo.NonformalizedDocumentMetadata != null)
					System.Console.WriteLine("DocumentInfo.NonformalizedDocumentMetadata.DocumentStatus: {0}", documentInfo.NonformalizedDocumentMetadata.DocumentStatus);
				if (documentInfo.InvoiceMetadata != null)
					System.Console.WriteLine("DocumentInfo.InvoiceMetadata.InvoiceStatus: {0}", documentInfo.InvoiceMetadata.InvoiceStatus);
				if (documentInfo.InvoiceRevisionMetadata != null)
					System.Console.WriteLine("DocumentInfo.InvoiceRevisionMetadata.InvoiceRevisionStatus: {0}", documentInfo.InvoiceRevisionMetadata.InvoiceRevisionStatus);
				if (documentInfo.InvoiceCorrectionMetadata != null)
					System.Console.WriteLine("DocumentInfo.InvoiceCorrectionMetadata.InvoiceCorrectionStatus: {0}", documentInfo.InvoiceCorrectionMetadata.InvoiceCorrectionStatus);
				if (documentInfo.InvoiceCorrectionRevisionMetadata != null)
					System.Console.WriteLine("DocumentInfo.InvoiceCorrectionRevisionMetadata.InvoiceCorrectionRevisionStatus: {0}", documentInfo.InvoiceCorrectionRevisionMetadata.InvoiceCorrectionRevisionStatus);
				if (documentInfo.TrustConnectionRequestMetadata != null)
					System.Console.WriteLine("DocumentInfo.TrustConnectionRequestMetadata.DocumentStatus: {0}", documentInfo.TrustConnectionRequestMetadata.TrustConnectionRequestStatus);
				if (documentInfo.ProformaInvoiceMetadata != null)
					System.Console.WriteLine("DocumentInfo.ProformaInvoiceMetadata.DocumentStatus: {0}", documentInfo.ProformaInvoiceMetadata.DocumentStatus);
				if (documentInfo.Torg12Metadata != null)
					System.Console.WriteLine("DocumentInfo.Torg12Metadata.DocumentStatus: {0}", documentInfo.Torg12Metadata.DocumentStatus);
				if (documentInfo.AcceptanceCertificateMetadata != null)
					System.Console.WriteLine("DocumentInfo.AcceptanceCertificateMetadata.DocumentStatus: {0}", documentInfo.AcceptanceCertificateMetadata.DocumentStatus);
				if (documentInfo.XmlTorg12Metadata != null)
					System.Console.WriteLine("DocumentInfo.XmlTorg12Metadata.DocumentStatus: {0}", documentInfo.XmlTorg12Metadata.DocumentStatus);
				if (documentInfo.XmlAcceptanceCertificateMetadata != null)
					System.Console.WriteLine("DocumentInfo.XmlAcceptanceCertificateMetadata.DocumentStatus: {0}", documentInfo.XmlAcceptanceCertificateMetadata.DocumentStatus);
			}

			if (comment?.Content?.Data != null)
			{
				System.Console.WriteLine("Комментарий:");
				System.Console.WriteLine(Encoding.UTF8.GetString(comment.Content.Data));
			}
		}

		public static void ShowBoxes(ConsoleContext consoleContext)
		{
			var myOrgs = consoleContext.DiadocApi.GetMyOrganizations(consoleContext.CurrentToken).Organizations;
			var myBoxes = myOrgs.SelectMany(o => o.Boxes).ToList();
			System.Console.WriteLine(myBoxes.Count == 0 ? "Доступных ящиков нет" : "Список доступных ящиков:");
			PrintBoxes(myOrgs);
			consoleContext.Orgs = myOrgs;
			consoleContext.Boxes = myBoxes;
		}

		public static void PrintBoxes(IEnumerable<Organization> orgs)
		{
			foreach (var org in orgs)
			{
				PrintOrganization(org);
			}
		}

		public static void PrintOrganization(Organization org)
		{
			System.Console.WriteLine();
			System.Console.WriteLine($"  BoxId: {org.Boxes[0].BoxId}, OrgId: {org.OrgId}");
			System.Console.WriteLine($"    ({org.Inn}-{org.Kpp} {org.FullName})");
			System.Console.WriteLine();
		}

		public static string FormatCommandSyntax(IConsoleCommand command)
		{
			return $"   {$"{command.Command} {command.Usage}",-35}    //{command.Description}";
		}

		public static void WriteToConsole(this MessagePatch patch)
		{
			System.Console.WriteLine();
			System.Console.WriteLine("  Ответ на сообщение : {0}", patch.MessageId);
			System.Console.WriteLine("  Отправлен          : {0}", patch.Timestamp);
			System.Console.WriteLine("  Вложения:");
			WriteEntitiesToConsole(patch.Entities);
		}
	}
}