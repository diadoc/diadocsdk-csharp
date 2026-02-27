using System.IO;
using System.Linq;
using Diadoc.Api.DataXml.ON_NSCHFDOPPR_UserContract_970_05_03_01;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.PowersOfAttorney;
using Certificate = Diadoc.Api.DataXml.ON_NSCHFDOPPR_UserContract_970_05_03_01.Certificate;
using PowerOfAttorney = Diadoc.Api.DataXml.ON_NSCHFDOPPR_UserContract_970_05_03_01.PowerOfAttorney;

namespace Diadoc.Samples.Helpers
{
	public static class Utd970Helper
	{
		public static SignedContent BuildSignerContent(byte[] content, byte[] signature, PowerOfAttorneyType powerOfAttorneyType)
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

		public static UniversalTransferDocument BuildUserDataContract(string senderBoxId, byte[] certificateRawData, PowerOfAttorneyType powerOfAttorneyType)
		{
			// Ниже перечислены минимально необходимые поля для генерации титула отправителя.
			var universalTransferDocument = new UniversalTransferDocument
			{
				Function = UniversalTransferDocumentFunction.СЧФ,
				DocumentDate = "01.01.2020",
				DocumentNumber = "134",
				/*
				Чтобы отметить в титуле признак алкогольной или табачной продукции нужно заполнить MetaData
				MetaData = new [] 
				{
					"1", //алкогольная продукция, подлежащая маркировке
					"2" //табачная продукция, сырье, никотинсодержащая продукция и никотиновое сырье
				},
				*/
				Sellers = GetSellersInfo(), //Данные организации продавца
				Buyers = GetBuyersInfo(), //Данные организации покупателя
				Table = BuildTableSample(), //Табличная часть документа
				TransferInfo = new TransferInfo
				{
					OperationInfo = "Operation Info",
					TransferDate = "01.01.2020"
				},
				Currency = "643",
				DocumentShipments = new[]
				{
					new DocumentRequisitesType
					{
						DocumentName = "Документ об отгрузке",
						DocumentNumber = "DocumentNumber",
						DocumentDate = "01.02.2025"
					}
				},
				DocumentCreator = "Наименование экономического субъекта – составителя файла обмена счета-фактуры",

				// Передадим информацию о подписанте документа, т.е. персональные данные сотрудника, который подписывает документ.
				// Эти данные осядут в самом xml:
				Signers = BuildSigners(senderBoxId, certificateRawData, powerOfAttorneyType)
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

		private static InvoiceTable BuildTableSample()
		{
			return new InvoiceTable
			{
				Item = new[]
				{
					new InvoiceTableItem
					{
						Product = "товар",
						Unit = "796",
						Quantity = 10,
						QuantitySpecified = true,
						TaxRate = TaxRateWithTwentyTwoPercent.TenPercent,
						Price = 10,
						PriceSpecified = true,
						SubtotalWithVatExcluded = 100,
						SubtotalWithVatExcludedSpecified = true,
						Vat = 10,
						VatSpecified = true,
						Subtotal = 110,
						SubtotalSpecified = true,
						/*
						Пример заполнения сведений о маркированном товаре
						ItemIdentificationNumbers = new[]						 
						{
							new InvoiceTableItemItemIdentificationNumber()
								{TransPackageId = "PackageId"}
						}
						*/
					},
				},
				Total = 110,
				TotalSpecified = true,
				TotalWithVatExcluded = 0,
				TotalWithVatExcludedSpecified = true,
				Vat = 10m,
				VatSpecified = true,
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
							RegistrationNumber = "<Регистрационный номер МЧД в формате GUID>",
							RepresentativeInn = "<ИНН доверенного лица>"
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

		private static ExtendedOrganizationInfoUtd970[] GetSellersInfo()
		{
			return new[]
			{
				new ExtendedOrganizationInfoUtd970
				{
					Item = new ExtendedOrganizationDetailsUtd970
					{
						Inn = "7750370238",
						Kpp = "770100101",
						OrgType = OrganizationType_DatabaseOrder.Item2, //ЮЛ
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
			};
		}

		private static ExtendedOrganizationInfoUtd970[] GetBuyersInfo()
		{
			return new[]
			{
				new ExtendedOrganizationInfoUtd970
				{
					Item = new ExtendedOrganizationDetailsUtd970
					{
						Inn = "9500000005",
						Kpp = "667301001",
						OrgType = OrganizationType_DatabaseOrder.Item2, //ЮЛ
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
			};
		}
	}
}
