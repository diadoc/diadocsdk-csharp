using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Diadoc.Api;
using Diadoc.Api.Cryptography;
using Diadoc.Api.DataXml.ON_NSCHFDOPPR_UserContract_970_05_03_01;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Samples
{
	internal static class PostUniversalTransferDocument970
	{
		public static void RunSample()
		{
			Console.WriteLine("Пример отправки универсального передаточного документа (УПД) в формате приказа №970");
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
			var typeNamedId = "UniversalTransferDocument"; // строковый идентификатор типа УПД
			var function = "СЧФ"; // функция, представляющая УПД как счёт-фактуру
			var version = "utd970_05_03_01"; // версия, отвечающая за то, что документ сформирован в формате приказа №970

			// Теперь создадим сам формализованный документ.
			// Мы должны получить xml, которая будет удовлетворять его схеме: https://www.diadoc.ru/docs/forms/upd?p=1210
			// C# SDK Диадока позволяет интеграторам создать объект типа UniversalTransferDocument,
			// который получается из кодогенерации упрощенной xsd-схемы титула документа:
			var userDataContract = BuildUserDataContract();

			// Теперь средствами универсального метода генерации мы получим из упрощенного xml
			// уже реальный титул документа, который будет удовлетворять приказу №970:
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
			var signature = crypt.Sign(content, certificate.RawData); // здесь лежит бинарное представление подписи к УПД
			Console.WriteLine("Создана подпись к документу.");

			// Теперь передадим в структуру информацию о файле.
			// Для этого воспользуемся универсальным полем DocumentAttachment — через него можно отправить любой тип.
			var documentAttachment = new DocumentAttachment
			{
				/*
				Чтобы Диадок знал, какой тип документа вы хотите отправить,
				нужно заполнить поле TypeNamedId (а также Function и Version, если у типа больше одной функции и версии).
				Узнать список доступных типов можно через метод-справочник GetDocumentTypes.
				
				Для наших целей (УПД с функцией СЧФ в формате приказа №970) мы уже подобрали нужную комбинацию выше:
				*/
				TypeNamedId = typeNamedId,
				Function = function,
				Version = version,

				// Теперь передадим сам файл УПД и сформированную к нему подпись:
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
			Console.WriteLine("Отправляем пакет из одного УПД с функцией СЧФ...");
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

		private static UniversalTransferDocument BuildUserDataContract()
		{
			// Ниже перечислены минимально необходимые поля для генерации титула отправителя.
			var universalTransferDocument = new UniversalTransferDocument
			{
				Function = UniversalTransferDocumentFunction.СЧФ,
				DocumentDate = "01.01.2020",
				DocumentNumber = "134",
				Sellers = new[]
				{
					new ExtendedOrganizationInfoUtd970
					{
						Item = new ExtendedOrganizationDetailsUtd970
						{
							Inn = "7750370238",
							Kpp = "770100101",
							OrgType = OrganizationType_DatabaseOrder.Item2, // ЮЛ
							OrgName = "ЗАО Очень Древний Папирус",
							Address = new AddressUtd970
							{
								Item = new RussianAddressUtd970
								{
									Region = "66"
								}
							}
						}
					}
				},
				Buyers = new[]
				{
					new ExtendedOrganizationInfoUtd970
					{
						Item = new ExtendedOrganizationDetailsUtd970
						{
							Inn = "9500000005",
							Kpp = "667301001",
							OrgType = OrganizationType_DatabaseOrder.Item2, // ЮЛ
							OrgName = "ООО Тестовое Юрлицо обычное",
							Address = new AddressUtd970
							{
								Item = new RussianAddressUtd970
								{
									Region = "66"
								}
							}
						}
					}
				},
				Table = new InvoiceTable
				{
					Item = new[]
					{
						new InvoiceTableItem
						{
							Product = "товар",
							Unit = "796",
							Quantity = 10,
							QuantitySpecified = true,
							TaxRate = TaxRateUtd970.TenPercent,
							Price = 10,
							PriceSpecified = true,
							SubtotalWithVatExcluded = 100,
							SubtotalWithVatExcludedSpecified = true,
							Vat = 10,
							VatSpecified = true,
							Subtotal = 110,
							SubtotalSpecified = true
						}
					},
					Total = 110,
					TotalSpecified = true,
					TotalWithVatExcluded = 0m,
					TotalWithVatExcludedSpecified = true,
					Vat = 10m,
					VatSpecified = true
				},
				Currency = "643",
				DocumentCreator = "Наименование экономического субъекта – составителя файла обмена счета-фактуры",
				DocumentShipments = new[]
				{
					new DocumentRequisitesType
					{
						DocumentName = "РеквНаимДок",
						DocumentNumber = "РеквНомерДок",
						DocumentDate = "18.03.2025"
					}
				},
			};

			// Передадим информацию о подписанте документа, т.е. персональные данные подписываемого сотрудника,
			// которые осядут в самом xml:
			universalTransferDocument.Signers = new Signers
			{
				Signer = new[]
				{
					new Signer
					{
						Fio = new Fio { FirstName = "Иван", MiddleName = "Иванович", LastName = "Иванов" },
						Position = new SignerPosition { PositionSource = SignerPositionPositionSource.Manual, Value = "директор" },
						SignerPowersConfirmationMethod = SignerSignerPowersConfirmationMethod.Item1,
						SignerPowersConfirmationMethodSpecified = true,
					}
				}
			};

			return universalTransferDocument;

			/*
			P.S. Объект UniversalTransferDocument подходит для генерации любого типа документа,
			который можно представить в формате приказа 970:
				- счёт-фактура (Invoice);
				- акт (XmlAcceptanceCertificate),
				- накладная (XmlTorg12),
				- и любой другой кастомный тип, доступный вашей организации
			*/
		}
	}
}
