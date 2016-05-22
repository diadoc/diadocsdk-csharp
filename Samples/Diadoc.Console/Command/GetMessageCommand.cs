using System;
using System.Linq;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;

namespace Diadoc.Console.Command
{
	class GetMessageCommand : EntityCommandBase
	{
		public GetMessageCommand(ConsoleContext consoleContext, string command = "msg")
			: base(consoleContext, command, CommandType.AuthenticationRequired)
		{
			Usage = "<messageId>";
			Description = "Get message";
		}

		protected override void PerformRunCommand(string[] args)
		{
			if (args.Length < 1)
				throw new UsageException();

			var messageId = args[0];

			messageId = AutocompleteMessageId(messageId);
			var message = ConsoleContext.DiadocApi.GetMessage(ConsoleContext.CurrentToken, ConsoleContext.CurrentBoxId, messageId);
			LoadContentForEntities(message.MessageId, message.Entities, true);
			message.WriteToConsole();
			ProcessMessage(message);
			LoadContentForEntities(message.MessageId, message.Entities, false);
			SaveEntitiesToDirectory(messageId, message.Entities);

			System.Console.WriteLine();
			System.Console.WriteLine("Все вложения и подписи были сохранены в папку {0}", messageId);
		}

		private string AutocompleteMessageId(string messageId)
		{
			if (messageId != null && ConsoleContext.Events != null)
			{
				var foundId = ConsoleContext.Events.Events
					.Select(oldEvt => oldEvt.MessageId)
					.FirstOrDefault(id => id.StartsWith(messageId, StringComparison.InvariantCultureIgnoreCase));
				if (foundId != null) return foundId;
			}
			return messageId;
		}

		private void ProcessMessage(Message msg)
		{
			var patch = new MessagePatchToPost
			{
				BoxId = ConsoleContext.CurrentBoxId,
				MessageId = msg.MessageId,
			};
			var attachments = msg.Entities.Where(e => e.EntityType == EntityType.Attachment && e.AttachmentType != AttachmentType.AttachmentComment).ToArray();
			var signatures = attachments
				.Where(e => IsNeedSendSignature(msg, e) && AskAboutSignatureSending(e))
				.Select(e => GenerateRequestedSignature(msg.MessageId, e));
			patch.Signatures.AddRange(signatures);
			var receipts = attachments
				.Where(e => IsNeedSendReceipt(msg, e) && AskAboutReceiptSending(e))
				.Select(e => GenerateNewReceipt(msg.MessageId, e));
			patch.Receipts.AddRange(receipts);
			var correctionRequests = attachments
				.Where(e => IsNeedSendCorrectionrequest(msg, e) && AskAboutCorrectionRequestSending(e))
				.Select(e => GenerateNewCorrectionRequest(msg.MessageId, e));
			patch.CorrectionRequests.AddRange(correctionRequests);
			if (patch.CorrectionRequests.Count > 0 || patch.Receipts.Count > 0 || patch.Signatures.Count > 0)
			{
				var sentPatch = ConsoleContext.DiadocApi.PostMessagePatch(ConsoleContext.CurrentToken, patch);
				System.Console.WriteLine("Было отправлено следующее сообщение:");
				sentPatch.WriteToConsole();
			}
		}

		private static bool AskAboutSignatureSending(Entity entity)
		{
			return InputHelpers.YesNoChoice(false, "Вложение \"{0}\" запрашивает ответную подпись. Подписать?", entity.FileName);
		}

		private static bool AskAboutReceiptSending(Entity entity)
		{
			return InputHelpers.YesNoChoice(false, "Требуется отправить извещение о получении для вложения \"{0}\". Отправить сейчас?", entity.FileName);
		}

		private static bool AskAboutCorrectionRequestSending(Entity entity)
		{
			return InputHelpers.YesNoChoice(false, "Отправить уведомление об уточнении счета-фактуры \"{0}\"?", entity.FileName);
		}

		private bool IsNeedSendSignature(Message msg, Entity entity)
		{
			var toBoxId = msg.ToBoxId;
			bool needMySignature = entity.NeedRecipientSignature && toBoxId == ConsoleContext.CurrentBoxId;
			return needMySignature && !msg.Entities.Any(e => e.EntityType == EntityType.Signature &&
				e.SignerBoxId == toBoxId &&
				e.ParentEntityId == entity.EntityId);
		}

		private bool IsNeedSendReceipt(Message msg, Entity entity)
		{
			bool needSend = false;
			switch (entity.AttachmentType)
			{
				case AttachmentType.Invoice:
					needSend = msg.ToBoxId == ConsoleContext.CurrentBoxId;
					break;
				case AttachmentType.InvoiceCorrectionRequest:
					needSend = msg.FromBoxId == ConsoleContext.CurrentBoxId;
					break;
				case AttachmentType.InvoiceConfirmation:
					needSend = true;
					break;
			}
			return needSend && !msg.Entities.Any(e =>
				e.EntityType == EntityType.Attachment &&
				e.AttachmentType == AttachmentType.InvoiceReceipt &&
				e.ParentEntityId == entity.EntityId);
		}

		private bool IsNeedSendCorrectionrequest(Message msg, Entity entity)
		{
			return entity.AttachmentType == AttachmentType.Invoice && msg.ToBoxId == ConsoleContext.CurrentBoxId;
		}

		private DocumentSignature GenerateRequestedSignature(string messageId, Entity entity)
		{
			return new DocumentSignature
			{
				ParentEntityId = entity.EntityId,
				Signature = ConsoleContext.SignByAttorney ? null : ConsoleContext.Crypt.Sign(GetEntityContent(messageId, entity), ConsoleContext.CurrentCert.RawData),
				SignByAttorney = ConsoleContext.SignByAttorney,
			};
		}

		private CorrectionRequestAttachment GenerateNewCorrectionRequest(string messageId, Entity entity)
		{
			var currentCertificateContent = ConsoleContext.CurrentCert.RawData;
			var correctionInfo = new InvoiceCorrectionRequestInfo
			{
				ErrorMessage = "your invoice is incorrect",
				Signer = new Signer
				{
					SignerCertificate = currentCertificateContent
				}
			};
			var correctionReq = ConsoleContext.DiadocApi.GenerateInvoiceCorrectionRequestXml(ConsoleContext.CurrentToken, ConsoleContext.CurrentBoxId, messageId, entity.EntityId, correctionInfo);
			return new CorrectionRequestAttachment
			{
				ParentEntityId = entity.EntityId,
				SignedContent = new SignedContent
				{
					Content = correctionReq.Content,
					Signature = ConsoleContext.SignByAttorney ? null : ConsoleContext.Crypt.Sign(correctionReq.Content, currentCertificateContent),
					SignByAttorney = ConsoleContext.SignByAttorney,
				},
			};
		}

		private ReceiptAttachment GenerateNewReceipt(string messageId, Entity entity)
		{
			var currentCertificateContent = ConsoleContext.CurrentCert.RawData;
			var signer = new Signer { SignerCertificate = currentCertificateContent };
			var receipt = ConsoleContext.DiadocApi.GenerateInvoiceDocumentReceiptXml(ConsoleContext.CurrentToken, ConsoleContext.CurrentBoxId, messageId, entity.EntityId, signer);
			return new ReceiptAttachment
			{
				ParentEntityId = entity.EntityId,
				SignedContent = new SignedContent
				{
					Content = receipt.Content,
					Signature = ConsoleContext.Crypt.Sign(receipt.Content, currentCertificateContent),
				},
			};
		}

		private byte[] GetEntityContent(string messageId, Entity entity)
		{
			return entity.Content == null ? null :
				(entity.Content.Data ?? ConsoleContext.DiadocApi.GetEntityContent(ConsoleContext.CurrentToken, ConsoleContext.CurrentBoxId, messageId, entity.EntityId));
		}
	}
}
