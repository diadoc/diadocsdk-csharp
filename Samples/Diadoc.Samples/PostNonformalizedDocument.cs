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
	internal static class PostNonformalizedDocument
	{
		// Подставьте сюда путь до неформализованного документа, который будет отправлен (пример: C:\file.txt)
		private const string NonformalizedDocumentPath = @"<Путь до файла>";

		[Test]
		public static void Sample()
		{
			Console.WriteLine("Пример отправки неформализованного документа");
			Console.WriteLine("============================================");

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

			// Подготовим контент и подпишем его через WinApiCrypt нашим сертификатом:
			var content = File.ReadAllBytes(NonformalizedDocumentPath); // здесь лежит бинарное представление неформализованного документа

			Console.WriteLine("Создаём подпись...");
			var certificate = new X509Certificate2(File.ReadAllBytes(Constants.CertificatePath));
			var signature = crypt.Sign(content, certificate.RawData); // здесь лежит бинарное представление подписи к документу
			Console.WriteLine("Создана подпись к документу.");


			// Теперь передадим в структуру информацию о файле.
			// Для этого воспользуемся универсальным полем DocumentAttachment — через него можно отправить любой тип.
			var documentAttachment = new DocumentAttachment
			{
				/*
				Чтобы Диадок знал, какой тип документа вы хотите отправить,
				нужно заполнить поле TypeNamedId (а также Function и Version, если у типа больше одной функции и версии).
				Узнать список доступных типов можно через метод-справочник GetDocumentTypes.
				*/
				TypeNamedId = "nonformalized",

				// Теперь передадим сам файл неформализованного документа и сформированную к нему подпись:
				SignedContent = new SignedContent
				{
					Content = content,
					Signature = signature
				},

				Comment = "Здесь можно указать любой текстовый комментарий, который нужно оставить к документу",
				CustomDocumentId = "Тут можно указать любой строковый идентификатор, например, для соответствия с вашей учётной системой",

				/*
				У каждого типа документа в Диадоке может быть свой набор метаданных.
				Их нужно указывать при отправке, если они обязательны.
				Узнать набор требуемых метаданных для конкретного набора (тип-функция-версия-порядковый номер титула)
				можно через тот же метод-справочник GetDocumentTypes: смотрите поля MetadataItems.

				Для нашего случая, связки nonformalized-default-v1, часть ответа метода GetDocumentTypes выглядит так:

				"MetadataItems": [
					{
						"Id": "FileName",
						"Type": "String",
						"IsRequired": true,
						"Source": "User"
					},
					{
						"Id": "DocumentNumber",
						"Type": "String",
						"IsRequired": false,
						"Source": "User"
					},
					{
						"Id": "DocumentDate",
						"Type": "Date",
						"IsRequired": false,
						"Source": "User"
					}
				]
				
				Это нужно читать так: для отправки неформализованного документа 
				обязательно нужно указывать только те метаданные, у которых IsRequired=true,
				а источник: User. Под условие подходит только FileName — имя файла документа. 
				
				Другие метаданные (DocumentNumber — номер документа, DocumentDate — дата документа) необязательны к заполнению,
				но их можно указать, и тогда Диадок будет возвращать их при запросе документа,
				будет отображать в веб-интерфейсе и т.п.
				*/

				Metadata =
				{
					new MetadataItem
					{
						Key = "FileName",
						// Из эстетических соображений можно обрезать расширение файла, но это необязательно.
						// Указанное здесь значение будет влиять на то, как неформализованный документ будет называться в Диадоке.
						// Оно может отличаться от реального названия файла.
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
