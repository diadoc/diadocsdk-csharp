using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Diadoc.Api;
using Diadoc.Api.Cryptography;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;

namespace Diadoc.Samples
{
	internal static class PatchDocumentWithReceipt
	{
		private const string BoxId = "<Идентификатор ящика>";

		public static void RunSample()
		{
			Console.WriteLine("Пример добавления извещения о получении к документу");
			Console.WriteLine("===================================================");

			// Для использования API Диадока требуются:
			// 1. Крипто-API, предоставляемое операционной системой. Для систем на ОС Windows используйте класс WinApiCrypt.
			// 2. Экземпляр класса DiadocApi, проксирующий работу с Диадоком.
			var crypt = new WinApiCrypt();
			var diadocApi = new DiadocApi(
				Constants.DefaultClientId,
				Constants.DefaultApiUrl,
				crypt);

			// Авторизуемся в Диадоке. В этом примере используем авторизацию через логин-пароль:
			var authToken = diadocApi.Authenticate(Constants.DefaultLogin, Constants.DefaultPassword);
			// Также можно использовать авторизацию по сертификату, она описана в примере Authenticate.cs

			// Поищем в ящике документы, для которых нужно создать и подписать извещение о получении (он же ИоП).
			// Это можно сделать несколькими способами. Один из вариантов — через фильтрацию методом GetDocuments
			var documentList = diadocApi.GetDocuments(
				authToken,
				new DocumentsFilter
				{
					// Если вы успешно выполняли пример из PostUniversalTransferDocument820.cs,
					// в качестве ящика можно подставить ящик получателя
					BoxId = BoxId,
					FilterCategory = "Any.InboundHaveToCreateReceipt" // этот фильтр читается так: входящий документ любого типа, для которого нужно создать ИоП (на любую сущность)
				});

			// В зависимости от документооборота, ИоПы могут быть и на другие сущности:
			// например, при работе с счетом-фактурой требуется отправить ИоП на подтверждение оператора ЭДО.
			// Подробнее о всех сущностях можно прочитать в документации:
			// http://api-docs.diadoc.ru/ru/latest/http/GenerateReceiptXml.html
			// http://api-docs.diadoc.ru/ru/latest/howto/example_receive_invoice.html

			// Поэтому для примера из выборки возьмём первый подходящий документ, для которого нет ИоПа только к титулу отправителя:
			string messageId = null;
			string documentId = null;
			foreach (var document in documentList.Documents)
			{
				var message = diadocApi.GetMessage(authToken, BoxId, document.MessageId);
				if (!HasReceiptForAttachment(message, document.EntityId))
				{
					messageId = document.MessageId;
					documentId = document.EntityId;
					break;
				}
			}

			if (messageId == null && documentId == null)
			{
				Console.WriteLine("Подходящих документов нет, завершаем работу примера.");
				return;
			}

			Console.WriteLine($"Берём документ с идентификаторами MessageId={messageId}, EntityId={documentId}");

			// Теперь приступим к созданию самого извещения о получении.
			// Это — технологический документ в формате XML.
			// Самый простой способ получить его: использовать соответствующий метод генерации
			Console.WriteLine("Создаём извещение о получении...");
			var generatedReceipt = diadocApi.GenerateReceiptXml(
				authToken,
				BoxId,
				messageId,
				documentId, // здесь указываем идентификатор титула, т.к. мы создаём ИоП именно к нему
				new Signer
				{
					SignerDetails = new SignerDetails
					{
						FirstName = "Иван",
						Patronymic = "Иванович",
						Surname = "Иванов",
						Inn = "7750370238",
						JobTitle = "директор"
					}
				});
			Console.WriteLine("Извещение о получении сгенерировано.");

			// ИоП, как и любой документ, также должен быть подписан. Создаём к нему подпись
			Console.WriteLine("Создаём подпись...");
			var content = generatedReceipt.Content;
			var certificate = new X509Certificate2(File.ReadAllBytes(Constants.CertificatePath));
			var signature = crypt.Sign(content, certificate.RawData);
			Console.WriteLine("Создана подпись к извещению о получении.");

			// Теперь мы готовы к отправке ИоПа. Делается это через метод PostMessagePatch
			var messagePatchToPost = new MessagePatchToPost
			{
				BoxId = BoxId,
				MessageId = messageId
			};

			var receiptAttachment = new ReceiptAttachment
			{
				ParentEntityId = documentId, // наш ИоП будет относиться к документу, поэтому явно показываем, к какой сущности мы создаем ИоП
				SignedContent = new SignedContent
				{
					Content = content,
					Signature = signature
				}
			};
			messagePatchToPost.Receipts.Add(receiptAttachment);

			var response = diadocApi.PostMessagePatch(authToken, messagePatchToPost);
			Console.WriteLine("Извещение о получении было успешно загружено.");

			var receipt = response.Entities.First(
				e => e.ParentEntityId == documentId
					&& (e.AttachmentType == AttachmentType.Receipt
						|| e.AttachmentType == AttachmentType.InvoiceReceipt));
			Console.WriteLine($"Идентификатор: {receipt.EntityId}");
		}

		// Искать, есть ли в сообщении ИоПы на конкретную сущность можно так:
		private static bool HasReceiptForAttachment(Message message, string attachmentId)
		{
			return message.Entities.Any(
				e => e.ParentEntityId == attachmentId
					&& (e.AttachmentType == AttachmentType.Receipt
						|| e.AttachmentType == AttachmentType.InvoiceReceipt));
		}
	}
}