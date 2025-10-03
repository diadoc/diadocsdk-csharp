using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Diadoc.Api;
using Diadoc.Api.Cryptography;
using Diadoc.Api.Proto.Events;
using NUnit.Framework;

namespace Diadoc.Samples
{
	[TestFixture]
	public class PostLargeDocumentWithShelf
	{
		// Подставьте сюда путь до неформализованного документа, который будет отправлен (пример: C:\file.txt)
		private const string NonformalizedDocumentPath = @"<Путь до файла>";

		[Test]
		public static void Sample()
		{
			Console.WriteLine("Пример отправки документа через Полку.");
			Console.WriteLine("Актуально для тяжеловесных файлов (больше мегабайта)");
			Console.WriteLine("====================================================");

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

			// Для отправки комплекта документов требуется подготовить структуру MessageToPost,
			// которая и будет содержать отправляемый комплект документов.

			// Для начала, укажем в структуре идентификаторы отправителя и получателя:
			var messageToPost = new MessageToPost
			{
				FromBoxId = Constants.DefaultFromBoxId,
				ToBoxId = Constants.DefaultToBoxId
			};

			// Отправим контент на Полку — временное хранилище для файлов, представляемое через API Диадока:

			var content = File.ReadAllBytes(NonformalizedDocumentPath); // здесь лежит бинарное представление неформализованного документа
			Console.WriteLine("Загружаем документ на Полку...");
			var uploadedFileShelfName = diadocApi.UploadFileToShelf(authToken, content);
			Console.WriteLine("Успешно. Путь до документа на Полке: " + uploadedFileShelfName);

			// Подпишем контент через WinApiCrypt нашим сертификатом и тоже отправим на Полку:
			var certificate = new X509Certificate2(File.ReadAllBytes(Constants.CertificatePath));

			Console.WriteLine("Создаём подпись...");
			var signature = crypt.Sign(content, certificate.RawData); // здесь лежит бинарное представление подписи к документу
			Console.WriteLine("Создана подпись к документу.");

			Console.WriteLine("Загружаем подпись на Полку...");
			var uploadedSignatureShelfName = diadocApi.UploadFileToShelf(authToken, signature);
			Console.WriteLine("Успешно. Путь до подписи на Полке: " + uploadedSignatureShelfName);

			// Теперь передадим в структуру информацию о файле.
			// Подробности заполнения типа и метаданных можно посмотреть в примере PostNonformalizedDocument.cs
			var documentAttachment = new DocumentAttachment
			{
				TypeNamedId = "nonformalized",

				// Здесь мы передаём не само бинарное представление, а только путь до Полки:
				SignedContent = new SignedContent
				{
					NameOnShelf = uploadedFileShelfName,
					SignatureNameOnShelf = uploadedSignatureShelfName
				},

				Metadata =
				{
					new MetadataItem
					{
						Key = "FileName",
						Value = Path.GetFileNameWithoutExtension(NonformalizedDocumentPath)
					}
				}
			};

			// Добавим информацию о документе в MessageToPost:
			messageToPost.DocumentAttachments.Add(documentAttachment);

			// Наконец отправляем подготовленный комплект документов через Диадок

			Console.WriteLine("Отправляем пакет из одного неформализованного документа...");
			Console.WriteLine("Из ящика: " + messageToPost.FromBoxId);
			Console.WriteLine("В ящик: " + messageToPost.ToBoxId);

			var response = diadocApi.PostMessage(authToken, messageToPost);

			// При необходимости можно обработать ответ сервера (например, можно получить
			// и сохранить для последующей обработки идентификатор сообщения)
			Console.WriteLine("Документ был успешно загружен.");
			Console.WriteLine("MessageID: " + response.MessageId);
			Console.WriteLine("Количество сущностей в сообщении: " + response.Entities.Count);

			// В ответе будет две сущности, т.к. контент и подпись к нему хранятся отдельно друг от друга.
			// Выведем информацию о самом документе. Это можно сделать так:
			var responseDocument = response.Entities.FirstOrDefault(e => string.IsNullOrEmpty(e.ParentEntityId)); // т.к. у документа нет "родительских сущностей"
			Console.WriteLine("Идентификатор документа: " + responseDocument.EntityId);
			Console.WriteLine("Название документа: " + responseDocument.DocumentInfo.Title);
		}
	}
}
