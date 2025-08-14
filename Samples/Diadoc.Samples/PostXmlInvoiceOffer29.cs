using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Diadoc.Api;
using Diadoc.Api.Cryptography;
using Diadoc.Api.DataXml.ON_CHETOP_UserContract_05_01_01;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.PowersOfAttorney;
using Certificate = Diadoc.Api.DataXml.ON_CHETOP_UserContract_05_01_01.Certificate;
using Electronic = Diadoc.Api.DataXml.ON_CHETOP_UserContract_05_01_01.Electronic;
using OrganizationType_DatabaseOrder = Diadoc.Api.DataXml.ON_CHETOP_UserContract_05_01_01.OrganizationType_DatabaseOrder;
using PowerOfAttorney = Diadoc.Api.DataXml.ON_CHETOP_UserContract_05_01_01.PowerOfAttorney;
using RussianAddressUtd970 = Diadoc.Api.DataXml.ON_CHETOP_UserContract_05_01_01.RussianAddressUtd970;
using Signer = Diadoc.Api.DataXml.ON_CHETOP_UserContract_05_01_01.Signer;
using SignerPosition = Diadoc.Api.DataXml.ON_CHETOP_UserContract_05_01_01.SignerPosition;
using SignerPositionPositionSource = Diadoc.Api.DataXml.ON_CHETOP_UserContract_05_01_01.SignerPositionPositionSource;
using Signers = Diadoc.Api.DataXml.ON_CHETOP_UserContract_05_01_01.Signers;
using SignerSignerPowersConfirmationMethod = Diadoc.Api.DataXml.ON_CHETOP_UserContract_05_01_01.SignerSignerPowersConfirmationMethod;
using Storage = Diadoc.Api.DataXml.ON_CHETOP_UserContract_05_01_01.Storage;
using StorageFullId = Diadoc.Api.DataXml.ON_CHETOP_UserContract_05_01_01.StorageFullId;
using StorageUseDefault = Diadoc.Api.DataXml.ON_CHETOP_UserContract_05_01_01.StorageUseDefault;
using TaxRateUtd970 = Diadoc.Api.DataXml.ON_CHETOP_UserContract_05_01_01.TaxRateUtd970;

namespace Diadoc.Samples
{
	internal static class PostXmlInvoiceOffer29
	{
		public static void RunSample()
		{
			Console.WriteLine("Пример отправки Счета-оферты в формате приказа №29");
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
			var typeNamedId = "XmlInvoiceOffer"; // строковый идентификатор типа Счет-оферта
			var function = "default"; // функция
			var version = "invoiceoffer29_05_01_01"; // версия, отвечающая за то, что документ сформирован в формате приказа №29

			// Укажем наш сертификат
			// Далее он понадобится для генерации подписанта в документе и подписания
			var certificateRawData = new X509Certificate2(File.ReadAllBytes(Constants.CertificatePath)).RawData;

			// Укажем способ передачи МЧД
			// Это поможет сформировать правильно контент документа и запрос
			// Подробнее о способах передачи - https://developer.kontur.ru/docs/diadoc-api/ru/latest/instructions/powerofattorney.html#powerofattorney-send

			var powerOfAttorneyType = PowerOfAttorneyType.None;

			// Теперь создадим сам формализованный документ.
			// C# SDK Диадока позволяет интеграторам создать объект типа XmlProformaInvoice,
			// который получается из кодогенерации упрощенной xsd-схемы титула документа.
			var userDataContract = BuildUserDataContract(messageToPost.FromBoxId, certificateRawData, powerOfAttorneyType);

			// Теперь средствами универсального метода генерации мы получим из упрощенного xml
			// уже реальный титул документа, который будет удовлетворять приказу №29
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

			var signature = crypt.Sign(content, certificateRawData); // здесь лежит бинарное представление подписи к Счету-оферте
			Console.WriteLine("Создана подпись к документу.");

			// Теперь передадим в структуру информацию о файле.
			// Для этого воспользуемся универсальным полем DocumentAttachment — через него можно отправить любой тип.
			var documentAttachment = new DocumentAttachment
			{
				/*
				Чтобы Диадок знал, какой тип документа вы хотите отправить,
				нужно заполнить поле TypeNamedId (а также Function и Version, если у типа больше одной функции и версии).
				Узнать список доступных типов можно через метод-справочник GetDocumentTypes.
				
				Для наших целей (Счет-оферта в формате приказа №29) мы уже подобрали нужную комбинацию выше:
				*/
				TypeNamedId = typeNamedId,
				Function = function,
				Version = version,

				// Теперь передадим сам файл Счета и сформированную к нему подпись и МЧД:
				SignedContent = BuildSignerContent(content, signature, powerOfAttorneyType),

				Comment = "Здесь можно указать любой текстовый комментарий, который нужно оставить к документу",
				CustomDocumentId = "Тут можно указать любой строковый идентификатор, например, для соответствия с вашей учётной системой"

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
			Console.WriteLine("Отправляем пакет из одного Счета...");
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

		private static SignedContent BuildSignerContent(byte[] content, byte[] signature, PowerOfAttorneyType powerOfAttorneyType)
		{
			// https://developer.kontur.ru/docs/diadoc-api/ru/latest/proto/SignedContent.html
			return new SignedContent
			{
				Content = content,
				Signature = signature,
				PowerOfAttorney = powerOfAttorneyType != PowerOfAttorneyType.None
					? BuildPowerOfAttorneyToPost(powerOfAttorneyType)
					: null
			};
		}

		private static XmlProformaInvoice BuildUserDataContract(string senderBoxId, byte[] certificateRawData, PowerOfAttorneyType powerOfAttorneyType)
		{
			// Ниже перечислены минимально необходимые поля для генерации титула отправителя.
			var xmlProformaInvoice = new XmlProformaInvoice
			{
				Function = XmlProformaInvoiceFunction.Item1,
				DocumentDate = "01.01.2020",
				DocumentNumber = "134",
				Seller = GetSellerInfo(), //Данные организации продавца
				Buyer = GetBuyerInfo(), //Данные организации покупателя
				Table = BuildTableSample(), //Табличная часть документа
				Currency = "643",
				
				// Передадим информацию о подписанте документа, т.е. персональные данные сотрудника, который подписывает документ.
				// Эти данные осядут в самом xml:
				Signers = BuildSigners(senderBoxId, certificateRawData, powerOfAttorneyType)
			};

			return xmlProformaInvoice;
		}

		private static ProformaInvoiceTable BuildTableSample()
		{
			return new ProformaInvoiceTable
			{
				Item = new[]
				{
					new ProformaInvoiceTableItem
					{
						Product = "товар",
						Unit = "796",
						Quantity = 10,
						QuantitySpecified = true,
						TaxRate = TaxRateUtd970.TenPercent,
						TaxRateSpecified = true,
						Price = 10,
						PriceSpecified = true,
						SubtotalWithVatExcluded = 100,
						VatWithoutDiscount = 10,
						VatWithoutDiscountSpecified = true,
						Vat = 10,
						VatSpecified = true,
						Subtotal = 110,
					}
				},
				Total = 110,
				TotalWithVatExcluded = 0,
				Vat = 10m,
				VatSpecified = true
			};
		}

		private static Signers BuildSigners(string senderBoxId, byte[] certificateRawData, PowerOfAttorneyType powerOfAttorneyType)
		{
			var signers = new Signers
			{
				BoxId = senderBoxId,
				Signer = new[]
				{
					new Signer
					{
						Certificate = new Certificate { CertificateBytes = certificateRawData },
						Position = new SignerPosition
						{
							// Автоматическое заполнение должности из настроек сотрудника указанных в сервисе
							// Более подробное описание работы см. в xsd-cхеме
							PositionSource = SignerPositionPositionSource.StorageByTitleTypeId
						},
						SignerPowersConfirmationMethod = SignerPowersConfirmationMethodConvert(powerOfAttorneyType),
						SignerPowersConfirmationMethodSpecified = true
					}
				}
			};

			if (powerOfAttorneyType != PowerOfAttorneyType.InDocumentContent) return signers;

			//Если мы собираемся заполнить данные о электронной доверенности в самом документе:
			signers.Signer.FirstOrDefault().PowerOfAttorney = BuildSignerElectronicPowerOfAttorney();
			return signers;
		}

		private static PowerOfAttorney BuildSignerElectronicPowerOfAttorney()
		{
			return new PowerOfAttorney
			{
				Electronic = new Electronic
				{
					Item = new Storage
					{
						FullId = new StorageFullId
						{
							IssuerInn = "<ИНН доверителя из МЧД>",
							RegistrationNumber = "<Регистрационный номер МЧД в формате GUID>"
						},
						// Подробнее о флаге UseDefault - https://developer.kontur.ru/docs/diadoc-api/ru/latest/proto/PowerOfAttorneyToPost.html?highlight=usedefault
						UseDefault = StorageUseDefault.@false
					}
				}
			};
		}

		private static PowerOfAttorneyToPost BuildPowerOfAttorneyToPost(PowerOfAttorneyType powerOfAttorneyType)
		{
			// PowerOfAttorneyToPost - https://developer.kontur.ru/docs/diadoc-api/ru/latest/proto/PowerOfAttorneyToPost.html
			switch (powerOfAttorneyType)
			{
				case PowerOfAttorneyType.None:
					return null;
				case PowerOfAttorneyType.FileAsMeta:
					return BuildPowerOfAttorneyToPostInMeta();
				case PowerOfAttorneyType.InDocumentContent:
					return BuildPowerOfAttorneyToPostInDocumentContent();
				default:
					return null;
			}
		}

		private static PowerOfAttorneyToPost BuildPowerOfAttorneyToPostInDocumentContent()
		{
			return new PowerOfAttorneyToPost
			{
				UseDocumentContent = true
			};
		}

		private static PowerOfAttorneyToPost BuildPowerOfAttorneyToPostInMeta()
		{
			const string powerOfAttorneyPath = @"<Путь до файла доверенности>";
			const string powerOfAttorneySignaturePath = @"<Путь до файла подписи доверенности>";

			var powerOfAttorneyContent = File.ReadAllBytes(powerOfAttorneyPath);
			var powerOfAttorneySignatureContent = File.ReadAllBytes(powerOfAttorneySignaturePath);

			return new PowerOfAttorneyToPost
			{
				Contents =
				{
					new PowerOfAttorneySignedContent
					{
						Content = new Content_v3 { Content = powerOfAttorneyContent },
						Signature = new Content_v3 { Content = powerOfAttorneySignatureContent }
					}
				}
			};
		}

		private static SignerSignerPowersConfirmationMethod SignerPowersConfirmationMethodConvert(PowerOfAttorneyType signerPowersConfirmationMethod)
		{
			switch (signerPowersConfirmationMethod)
			{
				// Описание значений и другие варианты см. в xsd-схеме
				case PowerOfAttorneyType.None:
					return SignerSignerPowersConfirmationMethod.Item6;
				case PowerOfAttorneyType.FileAsMeta:
					return SignerSignerPowersConfirmationMethod.Item4;
				case PowerOfAttorneyType.InDocumentContent:
					return SignerSignerPowersConfirmationMethod.Item3;
				default:
					return SignerSignerPowersConfirmationMethod.Item6;
			}
		}

		private static ExtendedOrganizationInfoXmlProformaInvoice29 GetSellerInfo()
		{
			return new ExtendedOrganizationInfoXmlProformaInvoice29
			{
				Item = new ExtendedOrganizationDetailsXmlProformaInvoice29
				{
					Inn = "7750370238",
					Kpp = "770100101",
					FnsParticipantId = "2BM-Seller",
					OrgType = OrganizationType_DatabaseOrder.Item2, //ЮЛ
					OrgName = "ЗАО Очень Древний Папирус",
					Address = new AddressXmlProformaInvoice29
					{
						Item = new RussianAddressUtd970
						{
							Region = "66"
						}
					},
					BankId = "123456789",
					CorrespondentAccount = "CorrespondentAccount",
					BankAccountNumber = "BankAccountNumber"
					
				}
			
			};
		}

		private static ExtendedOrganizationInfoXmlProformaInvoice29 GetBuyerInfo()
		{
			return new ExtendedOrganizationInfoXmlProformaInvoice29
			{
				Item = new ExtendedOrganizationDetailsXmlProformaInvoice29
				{
					Inn = "9500000005",
					Kpp = "667301001",
					FnsParticipantId = "2BM-Buyer",
					OrgType = OrganizationType_DatabaseOrder.Item2, //ЮЛ
					OrgName = "ООО Тестовое Юрлицо обычное",
					Address = new AddressXmlProformaInvoice29
					{
						Item = new RussianAddressUtd970
						{
							Region = "66"
						}
					}
				}
			};
		}

		private enum PowerOfAttorneyType
		{
			None, // Без МЧД
			FileAsMeta, // Файлом представленным в метаданных документа
			InDocumentContent // Информация об МЧД находится в содержимом документа
		}
	}
}
