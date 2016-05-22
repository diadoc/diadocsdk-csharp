using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Diadoc.Api;
using Diadoc.Api.Cryptography;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Samples
{
	// Пример кода для демонстрации:
	// - как выполнить инициализацию программного API Диадок
	// - как подготовить к отправке и подписать неформализованный документ
	// - как отправить документ через программный API Диадок
	internal static class SendLargeNonformalizedSample
	{
		// URL веб-сервиса Диадок
		private const string DefaultApiUrl = "https://diadoc-api.kontur.ru";
		// идентификатор клиента
		private const string DefaultClientId = "test-8ee1638deae84c86b8e2069955c2825a";
		// GUID ящика отправителя
		private const string DefaultFromBoxId = "12345675-1234-1234-1234-123456789012";
		// GUID ящика получателя
		private const string DefaultToBoxId = "12345675-1234-1234-1234-123456789012";
		// Имя файла, содержимое которого будет отправлено через Диадок
		const string FileToSendName = "C:\\test.txt";
		// Файл с данными сертификата, который следует использовать для подписи
		private const string FileWithCertName = "C:\\cert.cer";
		// Логин для авторизации на сервере Диадок
		private const string DefaultLogin = "логин";
		// Пароль для авторизации на сервере Диадок
		private const string DefaultPassword = "пароль";

		private static byte[] ReadFileContent(string fName)
		{
			using (var file = new FileStream(fName, FileMode.Open))
			{
				var buffer = new MemoryStream();
				var data = new byte[4000];
				int count;
				while ((count = file.Read(data, 0, data.Length)) > 0)
				{
					buffer.Write(data, 0, count);
				}
				return buffer.ToArray();
			}
		}

		public static void PostLargeNonformalized()
		{
			// Для использования Диадок требуются:
			// 1. крипто-API, предоставляемое операционной системой (доступно через класс WinApiCrypt)
			// 2. экземпляр класса DiadocApi, проксирующий работу с веб-сервисом Диадок
			var crypt = new WinApiCrypt();
			var api = new DiadocApi(
				DefaultClientId,							// идентификатор клиента
				DefaultApiUrl,								// URL веб-сервиса Диадок
				crypt);

			// Можно использовать либо аутентификацию по логину/паролю, либо по сертификату
			var authToken = api.Authenticate(DefaultLogin, DefaultPassword);	

			// Для отправки комплекта документов через Диадок требуется подготовить структуру MessageToPost,
			// которая и будет содержать отправляемый комплект документов
			var message = new MessageToPost
			{
				// GUID ящика отправителя
				FromBoxId = DefaultFromBoxId,
				// GUID ящика получателя
				ToBoxId = DefaultToBoxId
			};

			// Читаем содержимое отправляемого файла
			var content = ReadFileContent(FileToSendName);

			// Загружаем отправляемый файл на "полку" (на сервер временного хранения файлов Диадок)
			var uploadedFileShelfName = api.UploadFileToShelf(authToken, content);

			// Для того, чтобы подписать файл, требуется сертификат
			var certContent = ReadFileContent(FileWithCertName);
			var cert = new X509Certificate2(certContent); 

			// Подписываем содержимое файла
			var signature = crypt.Sign(content, cert.RawData);

			// Формируем структуру для представления неформализованного (с точки зрения Диадока) документа
			var attachment = new NonformalizedAttachment
			{
				Comment = "Комментарий к отправляемому документу",  // Комментарий к отправляемому документу
				FileName = new FileInfo(FileToSendName).Name,		// Протокол обмена с Диадок требует наличия имени файла (без пути!)
				NeedRecipientSignature = false,						// Требуется ли подпись получателя
				DocumentDate = DateTime.Now.ToShortDateString(),	// Дата составления документа
				DocumentNumber = "123",								// Номер документа
				CustomDocumentId=  "",									// Строковый идентификатор документа (если требуется связать документы в пакете)
				SignedContent = new SignedContent
				{
					NameOnShelf = uploadedFileShelfName,			// Имя файла, ранее загруженного на "полку"
					Signature = signature							// Подпись к отправляемому содержимому
				}
			};

			// Документ подготовлен к отправке. Добавляем его в отправляемое сообщение
			message.AddAttachment(attachment);						

			// Отправляем подготовленный комплект документов через Диадок
			var response = api.PostMessage(authToken, message);

			// При необходимости можно обработать ответ сервера (например, можно получить 
			// и сохранить для последующей обработки идентификатор сообщения)
			Console.Out.WriteLine("Message was successfully sent.");
			Console.Out.WriteLine("The message ID is: "+response.MessageId);
		}

	}
}
