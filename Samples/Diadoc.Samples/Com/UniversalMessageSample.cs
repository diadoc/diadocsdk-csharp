using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Diadoc.Api;
using Diadoc.Api.Cryptography;
using Diadoc.Api.DataXml.UniversalMessage;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;
using Diadoc.Samples.Helpers;
using NUnit.Framework;
using Fio = Diadoc.Api.DataXml.UniversalSigner.Fio;

namespace Diadoc.Samples.Com
{
	[TestFixture]
	internal static class UniversalMessageSample
	{
		private const string TypeNamedId = "UniversalTransferDocument";
		private const string Function = "СЧФ";
		private const string Version = "utd970_05_03_01";
		private const PowerOfAttorneyType PoAType = PowerOfAttorneyType.None;

		private static readonly byte[] CertificateRawData = new X509Certificate2(File.ReadAllBytes(Constants.CertificatePath)).RawData;

		[Test]
		[SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
		public static void Sample()
		{
			var crypt = new WinApiCrypt();
			var diadocApi = new ComDiadocApi();

			diadocApi.Initialize(Constants.DefaultClientId, Constants.DefaultApiUrl);
			var authToken = diadocApi.AuthenticateWithPassword(Constants.DefaultLogin, Constants.DefaultPassword);

			// Подготовим документ
			var (messageId, documentId) = PrepareDocument(diadocApi, crypt, authToken);

			// Создаем ИоП
			var (contentReceipt, receiptSignature) = GenerateReceipt(diadocApi, crypt, authToken, messageId, documentId);

			// Создаем к нему парный УС
			var generatedUm = GeneratedReceiptUniversalMessage(diadocApi, authToken, messageId, documentId);

			// Теперь мы готовы к отправке ИоПа и УС. Делается это через метод PostMessagePatchV4
			var messagePatchToPostV2 = new MessagePatchToPostV2();
			messagePatchToPostV2.BoxId = Constants.DefaultToBoxId;
			messagePatchToPostV2.MessageId = messageId;

			var receiptAttachment = new ReceiptAttachment();
			receiptAttachment.ParentEntityId = documentId;
			receiptAttachment.SetSignedContent(new SignedContent
			{
				Content = contentReceipt,
				Signature = receiptSignature
			});

			var umAttachment = new UniversalMessageAttachment();
			umAttachment.ParentEntityId = documentId;
			umAttachment.CodeGroupValue = Api.Com.UniversalMessageCodeGroup.Receipt;
			var unsignedContent = new UnsignedContent();
			unsignedContent.Content = generatedUm.Content;
			umAttachment.SetUniversalMessageContent(unsignedContent);

			messagePatchToPostV2.Receipts.Add(receiptAttachment);
			messagePatchToPostV2.UniversalMessages.Add(umAttachment);

			var response = diadocApi.PostMessagePatchV4(authToken, messagePatchToPostV2);
			Console.WriteLine("ИоП + УС были успешно загружены.");

			var receipt = response.Entities.First(e => e.ParentEntityId == documentId && e.AttachmentType == AttachmentType.UniversalMessage);
			Console.WriteLine($"Идентификатор: {receipt.EntityId}");
		}

		private static GeneratedFile GeneratedReceiptUniversalMessage(ComDiadocApi diadocApi, string authToken, string messageId, string documentId)
		{
			Console.WriteLine("Создаём УС ...");
			var userDataContract = BuildUserDataContract().SerializeToXml();
			var generatedUm = diadocApi.GenerateUniversalMessage(
				authToken,
				Constants.DefaultToBoxId,
				messageId,
				documentId,
				userDataContract);

			Console.WriteLine("УС сгенерировано.");
			return generatedUm;
		}

		private static (byte[] contentReceipt, byte[] signatureReceipt) GenerateReceipt(ComDiadocApi diadocApi,
			WinApiCrypt crypt,
			string authToken,
			string messageId,
			string documentId)
		{
			// Теперь приступим к созданию самого извещения о получении.
			// Это — технологический документ в формате XML.
			// Самый простой способ получить его: использовать соответствующий метод генерации
			Console.WriteLine("Создаём извещение о получении...");

			var signerContent = new Api.DataXml.UniversalSigner.Signer()
			{
				Fio = new Fio
				{
					FirstName = "Подписант",
					LastName = "Подписантов",
					MiddleName = "Подписантович"
				},
				Position = new Api.DataXml.UniversalSigner.SignerPosition()
				{
					PositionSource = Api.DataXml.UniversalSigner.SignerPositionPositionSource.Manual,
					Value = "Должность"
				}
			}.SerializeToXml();

			var generatedReceipt = diadocApi.GenerateReceiptXmlV2(
				authToken,
				Constants.DefaultToBoxId,
				new ReceiptGenerationRequestV2
				{
					MessageId = messageId,
					AttachmentId = documentId,
					SignerContent = signerContent
				});
			Console.WriteLine("Извещение о получении сгенерировано.");

			// ИоП, как и любой документ, также должен быть подписан. Создаём к нему подпись
			Console.WriteLine("Создаём подпись...");
			var contentReceipt = generatedReceipt.Content;
			var certificate = new X509Certificate2(File.ReadAllBytes(Constants.CertificatePath));
			var signatureReceipt = crypt.Sign(contentReceipt, certificate.RawData);
			Console.WriteLine("Создана подпись к извещению о получении.");
			return (contentReceipt, signatureReceipt);
		}

		private static (string messageId, string documentId) PrepareDocument(ComDiadocApi diadocApi, WinApiCrypt crypt, string authToken)
		{
			var messageToPost = new MessageToPost
			{
				FromBoxId = Constants.DefaultFromBoxId,
				ToBoxId = Constants.DefaultToBoxId
			};

			var userDataContract = Utd970Helper.BuildUserDataContract(messageToPost.FromBoxId, CertificateRawData, PoAType);
			var generatedTitle = diadocApi.GenerateTitleXml(
				authToken,
				Constants.DefaultFromBoxId,
				TypeNamedId,
				Function,
				Version,
				0,
				userDataContract.SerializeToXml());

			var content = generatedTitle.Content;
			var signature = crypt.Sign(content, CertificateRawData);

			var documentAttachment = new DocumentAttachment
			{
				TypeNamedId = TypeNamedId,
				Function = Function,
				Version = Version
			};
			documentAttachment.SetSignedContent(Utd970Helper.BuildSignerContent(content, signature, PoAType));

			messageToPost.DocumentAttachments.Add(documentAttachment);

			var response = diadocApi.PostMessage(authToken, messageToPost);
			var messageId = response.MessageId;
			var documentId = response.Entities.First(e => string.IsNullOrEmpty(e.ParentEntityId)).EntityId;
			return (messageId, documentId);
		}

		// Искать, есть ли в сообщении ИоПы на конкретную сущность можно так:
		private static bool HasReceiptForAttachment(Message message, string attachmentId)
		{
			return message.Entities.Any(e => e.ParentEntityId == attachmentId
			                                 && (e.AttachmentType == AttachmentType.Receipt
			                                     || e.AttachmentType == AttachmentType.InvoiceReceipt));
		}

		private static UniversalMessage BuildUserDataContract()
		{
			// Ниже приведен пример заполнения полей для генерации УС.
			var universalMessage = new UniversalMessage
			{
				UniversalMessageInfos = new[]
				{
					new Diadoc.Api.DataXml.UniversalMessage.UniversalMessageInfo
					{
						StatusCode = "1999"
					}
				}
			};

			return universalMessage;
		}
	}
}
