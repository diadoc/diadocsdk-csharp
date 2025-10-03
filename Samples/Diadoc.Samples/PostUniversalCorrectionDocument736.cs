using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Diadoc.Api;
using Diadoc.Api.Cryptography;
using Diadoc.Api.DataXml.ON_NKORSCHFDOPPR_UserContract_1_996_03_05_01_03;
using Diadoc.Api.Proto.Events;
using NUnit.Framework;

namespace Diadoc.Samples
{
	[TestFixture]
	internal static class PostUniversalCorrectionDocument736
	{
		[Test]
		public static void Sample()
		{
			Console.WriteLine("Пример отправки универсального корректировочного документа (УКД) в формате приказа №736");
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
			var typeNamedId = "UniversalCorrectionDocument"; // строковый идентификатор типа УКД
			var function = "КСЧФ"; // функция, представляющая УКД как корректировочный счёт-фактуру
			var version = "ucd736_05_01_02"; // версия, отвечающая за то, что документ сформирован в формате приказа №736

			// Теперь создадим сам формализованный документ.
			// Мы должны получить xml, которая будет удовлетворять его схеме
			// C# SDK Диадока позволяет интеграторам создать объект типа UniversalCorrectionDocument,
			// который получается из кодогенерации упрощенной xsd-схемы титула документа:
			var userDataContract = BuildUserDataContract();

			// Теперь средствами универсального метода генерации мы получим из упрощенного xml
			// уже реальный титул документа, который будет удовлетворять приказу №736:
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
			var signature = crypt.Sign(content, certificate.RawData); // здесь лежит бинарное представление подписи к УКД
			Console.WriteLine("Создана подпись к документу.");

			// Теперь передадим в структуру информацию о файле.
			// Для этого воспользуемся универсальным полем DocumentAttachment — через него можно отправить любой тип.
			var documentAttachment = new DocumentAttachment
			{
				/*
				Чтобы Диадок знал, какой тип документа вы хотите отправить,
				нужно заполнить поле TypeNamedId (а также Function и Version, если у типа больше одной функции и версии).
				Узнать список доступных типов можно через метод-справочник GetDocumentTypes.
				
				Для наших целей (УКД с функцией КСЧФ в формате приказа №736) мы уже подобрали нужную комбинацию выше
				*/
				TypeNamedId = typeNamedId,
				Function = function,
				Version = version,

				// Теперь передадим сам файл УКД и сформированную к нему подпись:
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
			Console.WriteLine("Отправляем пакет из одного УКД с функцией КСЧФ...");
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

		private static UniversalCorrectionDocument BuildUserDataContract()
		{
			// Ниже перечислены минимально необходимые поля для генерации титула отправителя.
			var universalCorrectionDocument = new UniversalCorrectionDocument
			{
				Function = UniversalCorrectionDocumentFunction.КСЧФ,
				DocumentDate = "01.01.2020",
				DocumentNumber = "134",
				CurrencyName = UniversalCorrectionDocumentCurrencyName.Item1,
				Seller = new ExtendedOrganizationInfo_ForeignAddress1000
				{
					Item = new ExtendedOrganizationDetails_ForeignAddress1000
					{
						Inn = "7750370238",
						Kpp = "770100101",
						OrgName = "ЗАО Очень Древний Папирус",
						OrgType = OrganizationType.Item1,
						Address = new Address_ForeignAddress1000
						{
							Item = new RussianAddress
							{
								Region = "66"
							}
						}
					}
				},
				Buyer = new ExtendedOrganizationInfo_ForeignAddress1000
				{
					Item = new ExtendedOrganizationDetails_ForeignAddress1000
					{
						Inn = "9500000005",
						Kpp = "667301001",
						OrgType = OrganizationType.Item1,
						OrgName = "ООО Тестовое Юрлицо обычное",
						Address = new Address_ForeignAddress1000
						{
							Item = new RussianAddress
							{
								Region = "66"
							}
						}
					}
				},
				Table = new InvoiceCorrectionTable
				{
					Items = new[]
					{
						new ExtendedInvoiceCorrectionItem
						{
							Product = "товар",
							Unit = new ExtendedInvoiceCorrectionItemUnit
							{
								OriginalValue = "796",
								CorrectedValue = "797"
							},
							UnitName = new ExtendedInvoiceCorrectionItemUnitName
							{
								CorrectedValue = "kg",
								OriginalValue = "g"
							},
							Quantity = new ExtendedInvoiceCorrectionItemQuantity
							{
								OriginalValue = 10,
								OriginalValueSpecified = true,
								CorrectedValue = 11,
								CorrectedValueSpecified = true
							},
							Subtotal = new ExtendedInvoiceCorrectionItemSubtotal
							{
								ItemElementName = ItemChoiceType3.AmountsInc,
								Item = (decimal)10
							},
							TaxRate = new ExtendedInvoiceCorrectionItemTaxRate
							{
								OriginalValue = TaxRateUcd736AndUtd820.НДСисчисляетсяналоговымагентом
							}
						},
					},
					TotalsInc = new InvoiceTotalsDiff736
					{
						Total = (decimal)123.4
					}
				},
				Invoices = new[]
				{
					new InvoiceForCorrectionInfo
					{
						Date = "01.01.2020",
						Number = "123",
						Revision = new []
						{
							new InvoiceForCorrectionInfoRevision
							{
								Date = "02.01.2020",
								Number = "223"
							}
						}
					}
				},
				Currency = "643",
				DocumentCreator = "Наименование экономического субъекта",
			};

			// Передадим информацию о подписанте документа, т.е. персональные данные подписываемого сотрудника,
			// которые осядут в самом xml:
			universalCorrectionDocument.Signers = new[]
			{
				new ExtendedSignerDetails_CorrectionSellerTitle
				{
					SignerType = ExtendedSignerDetailsBaseSignerType.Item1,
					FirstName = "Иван",
					MiddleName = "Иванович",
					LastName = "Иванов",
					SignerOrganizationName = "ЗАО Очень Древний Папирус",
					Inn = "7750370238",
					Position = "директор"
				}
			};
			// Альтернативный способ заполнения данных подписанта:
			// отправить в хранилище Диадока аналогичный набор данных через метод PostExtendedSignerDetails
			// и затем использовать universalCorrectionDocument.Signers = new SignerReference[]

			return universalCorrectionDocument;
		}
	}
}
