using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Diadoc.Api;
using Diadoc.Api.Cryptography;
using Diadoc.Api.DataXml.DP_TOVTORGPR_UserContract_tovtorg_05_02_01;
using Diadoc.Api.Proto.Events;
using Address = Diadoc.Api.DataXml.DP_TOVTORGPR_UserContract_tovtorg_05_02_01.Address;
using ExtendedSignerDetailsBaseSignerType = Diadoc.Api.DataXml.ExtendedSignerDetailsBaseSignerType;
using OrganizationType = Diadoc.Api.DataXml.OrganizationType;
using RussianAddress = Diadoc.Api.DataXml.DP_TOVTORGPR_UserContract_tovtorg_05_02_01.RussianAddress;

namespace Diadoc.Samples
{
	internal static class PostXmlTorg12TovTorg551
	{
		public static void RunSample()
		{
			Console.WriteLine("Пример отправки накладной в формате приказа №551");
			Console.WriteLine("===================================================================================");

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

			// Перечислим связку идентификаторов, которая характеризует полный формализованный тип документа в Диадоке 
			var typeNamedId = "XmlTorg12"; // строковый идентификатор типа накладная
			var function = "default"; // функция
			var version = "tovtorg_05_02_01"; // версия, отвечающая за то, что документ сформирован в формате приказа №551

			// Теперь создадим сам формализованный документ.
			// C# SDK Диадока позволяет интеграторам создать объект типа TovTorgSellerTitleInfo,
			// который получается из кодогенерации упрощенной xsd-схемы титула документа:
			var userDataContract = BuildUserDataContract();

			// Теперь средствами универсального метода генерации мы получим из упрощенного xml
			// уже реальный титул документа, который будет удовлетворять приказу №551:
			Console.WriteLine("Генерируем титул отправителя...");
			var generatedTitle = diadocApi.GenerateTitleXml(
				authToken,
				Constants.DefaultFromBoxId,
				typeNamedId,
				function,
				version,
				0,
				userDataContract.SerializeToXml());
			Console.WriteLine("Титул отправителя был успешно сгенерирован.");

			// Подпишем полученный титул через WinApiCrypt нашим сертификатом:
			Console.WriteLine("Создаём подпись...");
			var content = generatedTitle.Content;
			var certificate = new X509Certificate2(File.ReadAllBytes(Constants.CertificatePath));
			var signature = crypt.Sign(content, certificate.RawData); // здесь лежит бинарное представление подписи к накладной
			Console.WriteLine("Создана подпись к документу.");

			// Теперь передадим в структуру информацию о файле.
			// Для этого воспользуемся универсальным полем DocumentAttachment — через него можно отправить любой тип.
			var documentAttachment = new DocumentAttachment
			{
				/*
				Чтобы Диадок знал, какой тип документа вы хотите отправить,
				нужно заполнить поле TypeNamedId (а также Function и Version, если у типа больше одной функции и версии).
				Узнать список доступных типов можно через метод-справочник GetDocumentTypes.
				
				Для наших целей (накладная с функцией default в формате приказа №551) мы уже подобрали нужную комбинацию выше:
				*/
				TypeNamedId = typeNamedId,
				Function = function,
				Version = version,

				// Теперь передадим сам файл накладной и сформированную к нему подпись:
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

				Для формализованных документов метаданные обычно достаются из xml самим Диадоком.
				Если у метаданных указан Source=Xml, отдельно передавать в MessageToPost их не нужно.
				*/
			};

			// Добавим информацию о документе в MessageToPost:
			messageToPost.DocumentAttachments.Add(documentAttachment);

			// Наконец отправляем подготовленный комплект документов через Диадок
			Console.WriteLine("Отправляем пакет из одной накладной ...");
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

		private static TovTorgSellerTitleInfo BuildUserDataContract()
		{
			// Ниже перечислены минимально необходимые поля для генерации титула отправителя.
			var tovTorgSellerTitleInfo = new TovTorgSellerTitleInfo
			{
				DocumentName = "Накладная",
				DocumentDate = "01.01.2024",
				DocumentNumber = "1",
				Seller = GetSellerInfo(),
				Buyer = GetBuyerInfo(),
				Currency = "643",
				DocumentCreator = "НаимЭконСубСост - Составитель файла информации продавца",
				Table = BuildTableSample(),
				Grounds = GetGrounds(),
				TransferInfo = GetTransferInfo()
			};

			// Передадим информацию о подписанте документа, т.е. персональные данные подписываемого сотрудника,
			// которые осядут в самом xml:
			tovTorgSellerTitleInfo.Signers = BuildSigners();
			// Альтернативный способ заполнения данных подписанта:
			// отправить в хранилище Диадока аналогичный набор данных через метод PostExtendedSignerDetails
			// и затем использовать universalTransferDocumentWithHyphens.UseSignerReferences(new SignerReference[])

			return tovTorgSellerTitleInfo;
		}

		private static ExtendedOrganizationInfo GetSellerInfo()
		{
			return new ExtendedOrganizationInfo
			{
				Item = new ExtendedOrganizationDetails
				{
					Inn = "7750370238",
					Kpp = "770100101",
					OrgType = (Api.DataXml.DP_TOVTORGPR_UserContract_tovtorg_05_02_01.OrganizationType)OrganizationType.LegalEntity,
					OrgName = "ЗАО Очень Древний Папирус",
					Address = new Address
					{
						Item = new RussianAddress
						{
							Region = "66"
						}
					}
				}
			};
		}

		private static ExtendedOrganizationInfo GetBuyerInfo()
		{
			return new ExtendedOrganizationInfo
			{
				Item = new ExtendedOrganizationDetails
				{
					Inn = "9500000005",
					Kpp = "667301001",
					OrgType = (Api.DataXml.DP_TOVTORGPR_UserContract_tovtorg_05_02_01.OrganizationType)OrganizationType.LegalEntity,
					OrgName = "ООО Тестовое Юрлицо обычное",
					Address = new Address
					{
						Item = new RussianAddress
						{
							Region = "66"
						}
					}
				}
			};
		}

		private static TovTorgTable BuildTableSample()
		{
			return new TovTorgTable
			{
				Total = 15,
				Item = new []
				{
					new TovTorgItem
					{
						Unit = "2581",
						Product = "Product",
						Feature = "Feature",
						Subtotal = 100,
						SubtotalSpecified = true
					}
				}
			};
		}

		private static GroundInfo[] GetGrounds()
		{
			return new[]
			{
				new GroundInfo
				{
					Name = "Основание",
					Date = "01.01.2024",
					Info = "Информация",
					Number = "1"
				}
			};
		}

		private static TovTorgTransferInfo GetTransferInfo()
		{
			return new TovTorgTransferInfo
			{
				OperationInfo = "Содержание операции",
				TransferDate = "01.04.2024"
			};
		}

		private static ExtendedSignerDetails_551_552[] BuildSigners()
		{
			return 
				new[]
				{
					new ExtendedSignerDetails_551_552
					{
						SignerPowersBase = "SignerPowersBase",
						SignerType = (Api.DataXml.DP_TOVTORGPR_UserContract_tovtorg_05_02_01.ExtendedSignerDetailsBaseSignerType)ExtendedSignerDetailsBaseSignerType.LegalEntity,
						FirstName = "Иван",
						MiddleName = "Иванович",
						LastName = "Иванов",
						SignerOrganizationName = "ЗАО Очень Древний Папирус",
						Inn = "7750370238",
						Position = "директор"
					}
				};
		}
	}
}
