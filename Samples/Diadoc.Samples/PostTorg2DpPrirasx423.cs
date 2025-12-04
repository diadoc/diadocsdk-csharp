using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Diadoc.Api;
using Diadoc.Api.Cryptography;
using Diadoc.Api.DataXml.DP_PRIRASXPRIN_UserContract_1_994_01_05_01_01;
using Diadoc.Api.Proto.Events;
using NUnit.Framework;
using ExtendedSignerDetailsBaseSignerType = Diadoc.Api.DataXml.ExtendedSignerDetailsBaseSignerType;
using OrganizationType = Diadoc.Api.DataXml.OrganizationType;

namespace Diadoc.Samples
{
	[TestFixture]
	public class PostTorg2DpPrirasx423
	{
		[Test]
		public static void Sample()
		{
			Console.WriteLine("Пример отправки акта об установленном расхождении (ТОРГ-2) в формате приказа №423");
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
			var typeNamedId = "Torg2"; // строковый идентификатор типа акта об установленном расхождении
			var function = "default"; // функция, представляющая акт об установленном расхождении
			var version = "spar_torg2_05_01_01"; // версия, отвечающая за то, что документ сформирован в формате приказа №423
			
			// Теперь создадим сам формализованный документ.
			// C# SDK Диадока позволяет интеграторам создать объект типа Torg2SenderTitle,
			// который получается из кодогенерации упрощенной xsd-схемы титула документа:
			var userDataContract = BuildUserDataContract();
			
			// Теперь средствами универсального метода генерации мы получим из упрощенного xml
			// уже реальный титул документа, который будет удовлетворять приказу №423:
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
			var signature = crypt.Sign(content, certificate.RawData); // здесь лежит бинарное представление подписи к акту
			Console.WriteLine("Создана подпись к документу.");
			
			// Теперь передадим в структуру информацию о файле.
			// Для этого воспользуемся универсальным полем DocumentAttachment — через него можно отправить любой тип.
			var documentAttachment = new DocumentAttachment
			{
				/*
				Чтобы Диадок знал, какой тип документа вы хотите отправить,
				нужно заполнить поле TypeNamedId (а также Function и Version, если у типа больше одной функции и версии).
				Узнать список доступных типов можно через метод-справочник GetDocumentTypes.
				
				Для наших целей (акт с функцией default в формате приказа №423) мы уже подобрали нужную комбинацию выше:
				*/
				TypeNamedId = typeNamedId,
				Function = function,
				Version = version,

				// Теперь передадим сам файл акта и сформированную к нему подпись:
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
			Console.WriteLine("Отправляем пакет из одного акта ...");
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
			Console.WriteLine("Идентификатор документа: " + responseDocument?.EntityId);
			Console.WriteLine("Название документа: " + responseDocument?.DocumentInfo.Title);
		}

		private static Torg2SenderTitle BuildUserDataContract()
		{
			// Ниже перечислены минимально необходимые поля для генерации титула отправителя.
			var torg2SellerTitleInfo = new Torg2SenderTitle
			{
				DocumentName = "Акт об установленном расхождении",
				DocumentDate = "01.01.2024",
				DocumentNumber = "1",
				CircumstancesAcceptanceInfo = GetCircumstancesAcceptanceInfo(),
				EvidenceAcceptanceInfo = GetEvidenceAcceptanceInfo(),
				//Signers = GetSigners(),
				DocumentCreator = "НаимЭконСубСост - Составитель файла информации продавца",
				
				
			};
			
			// Передадим информацию о подписанте документа, т.е. персональные данные подписываемого сотрудника,
			// которые осядут в самом xml:
			torg2SellerTitleInfo.Signers = BuildSigners();
			// Альтернативный способ заполнения данных подписанта:
			// отправить в хранилище Диадока аналогичный набор данных через метод PostExtendedSignerDetails
			// и затем использовать universalTransferDocumentWithHyphens.UseSignerReferences(new SignerReference[])

			return torg2SellerTitleInfo;
		}

		private static object[] GetSigners()
		{
			return new[]
			{
				new ExtendedSignerDetails_Torg2Buyer
				{
					
				}
			};
		}

		private static EvidenceAcceptanceInfo GetEvidenceAcceptanceInfo()
		{
			return new EvidenceAcceptanceInfo
			{
				AcceptanceResults = new []
				{
					new AcceptanceResult
					{
						Assets = new AcceptanceResultAssets
						{
							PassportNumber = "2345"
						}
					}
				}
			};
		}

		private static CircumstancesAcceptanceInfo GetCircumstancesAcceptanceInfo()
		{
			return new CircumstancesAcceptanceInfo
			{
				Seller = new ExtendedOrganizationInfo_Torg2
				{
					Item = new ExtendedOrganizationDetails_Torg2
					{
						Inn = "7750370238",
						Kpp = "770100101",
						OrgType = (Api.DataXml.DP_PRIRASXPRIN_UserContract_1_994_01_05_01_01.OrganizationType)OrganizationType.LegalEntity,
						OrgName = "ЗАО Очень Древний Папирус",
						Address = new Address
						{
							Item = new RussianAddress
							{
								Region = "66"
							}
						}
					}
				},
				Buyer = new ExtendedOrganizationInfo_Torg2
				{
					Item = new ExtendedOrganizationDetails_Torg2
					{
						Inn = "9500000005",
						Kpp = "667301001",
						OrgType = (Api.DataXml.DP_PRIRASXPRIN_UserContract_1_994_01_05_01_01.OrganizationType)OrganizationType.LegalEntity,
						OrgName = "ООО Тестовое Юрлицо обычное",
						Address = new Address
						{
							Item = new RussianAddress
							{
								Region = "66"
							}
						}
					}
				}
			};
		}

		private static ExtendedSignerDetailsBase[] BuildSigners()
		{
			return new[]
				{
					new ExtendedSignerDetailsBase
					{
						SignerPowersBase = "SignerPowersBase",
						SignerType = (Api.DataXml.DP_PRIRASXPRIN_UserContract_1_994_01_05_01_01.ExtendedSignerDetailsBaseSignerType)ExtendedSignerDetailsBaseSignerType.LegalEntity,
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

