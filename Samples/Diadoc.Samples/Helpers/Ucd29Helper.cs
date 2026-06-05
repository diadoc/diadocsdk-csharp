using System.IO;
using System.Linq;
using Diadoc.Api.DataXml.ON_NKORSCHFDOPPR_UserContract_1_996_03_05_02_01;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.PowersOfAttorney;
using Certificate = Diadoc.Api.DataXml.ON_NKORSCHFDOPPR_UserContract_1_996_03_05_02_01.Certificate;
using PowerOfAttorney = Diadoc.Api.DataXml.ON_NKORSCHFDOPPR_UserContract_1_996_03_05_02_01.PowerOfAttorney;

namespace Diadoc.Samples.Helpers
{
	public static class Ucd29Helper
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

		public static UniversalCorrectionDocument BuildUserDataContract(string senderBoxId, byte[] certificateRawData, PowerOfAttorneyType powerOfAttorneyType)
		{
			return new UniversalCorrectionDocument
			{
				Function = UniversalCorrectionDocumentFunction.КСЧФДИС,
				DocumentDate = "01.01.2025",
				DocumentNumber = "134",

				Currency = "643",

				DocumentName = "Корректировочный счёт-фактура",
				DocumentCreator = "ЗАО Очень Древний Папирус",

				// Реквизиты исправления УКД (заполняются, если это исправление ранее выставленного УКД)
				// CorrectionRevisionDate = "05.01.2025",
				// CorrectionRevisionNumber = "1",

				// Идентификатор участника ЭДО — продавца/покупателя (необязательно)
				SenderFnsParticipantId = "2BM-7750370238-2024-12345678901234567",
				RecipientFnsParticipantId = "2BM-9500000005-2024-98765432109876543",

				/*
				Чтобы отметить в титуле признак алкогольной или табачной продукции нужно заполнить MetaData
				MetaData = new []
				{
					"1", //алкогольная продукция, подлежащая маркировке
					"2" //табачная продукция, сырье, никотинсодержащая продукция и никотиновое сырье
				},
				*/

				// Ссылки на исходные счета-фактуры, к которым составляется данный УКД
				Invoices = BuildInvoices(),

				Seller = BuildOrganizationInfo(
					"7750370238",
					"770100101",
					"ЗАО Очень Древний Папирус",
					"77"),

				Buyer = BuildOrganizationInfo(
					"9500000005",
					"667301001",
					"ООО Тестовое Юрлицо обычное",
					"66"),

				Table = BuildTable(),
				EventContent = BuildEventContent(),

				CommitmentTypes = new[]
				{
					new UniversalCorrectionDocumentCommitmentType
					{
						CommitmentTypeCode = "01",
						CommitmentTypeName = "Договор поставки"
					}
				},

				SellerInfoCircumPublicProc = new UniversalCorrectionDocumentSellerInfoCircumPublicProc
				{
					DateStateContract = "01.01.2025",
					NumberStateContract = "2025012500001",
					PersonalAccountSeller = "12345678901",
					SellerBudgetClassCode = "18210102010011000110",
					SellerTargetCode = "00000000000000000000",
					SellerTreasuryName = "УФК по г. Москве"
				},

				DocumentCreatorBase = new DocumentRequisitesType
				{
					DocumentName = "Доверенность",
					DocumentNumber = "123",
					DocumentDate = "01.01.2025"
				},

				Signers = BuildSigners(senderBoxId, certificateRawData, powerOfAttorneyType)
			};
		}

		private static InvoiceForCorrectionInfo[] BuildInvoices()
		{
			return new[]
			{
				new InvoiceForCorrectionInfo
				{
					Date = "01.12.2024",
					Number = "100",
					// Исправления к исходному счёту-фактуре (если УКД выставляется к исправленному СФ)
					Revision = new[]
					{
						new InvoiceForCorrectionInfoRevision
						{
							Date = "10.12.2024",
							Number = "1"
						}
					}
				}
			};
		}

		private static ExtendedOrganizationInfoUtd970 BuildOrganizationInfo(string inn, string kpp, string orgName, string region)
		{
			return new ExtendedOrganizationInfoUtd970
			{
				Item = new ExtendedOrganizationDetailsUtd970
				{
					Inn = inn,
					Kpp = kpp,
					OrgName = orgName,
					OrgType = OrganizationType_DatabaseOrder.Item2,
					Address = new AddressUtd970
					{
						Item = new RussianAddressUtd970
						{
							Region = region
						}
					}
				}
			};
		}

		private static InvoiceCorrectionTable BuildTable()
		{
			return new InvoiceCorrectionTable
			{
				Items = new[]
				{
					BuildTableItem()
				},
				TotalsInc = new InvoiceTotals
				{
					SubtotalWithVatExcluded = 100m,
					Subtotal = 110m,
					SubtotalSpecified = true,
					Vat = 10m,
					VatSpecified = true
				},

				TotalsDec = new InvoiceTotals
				{
					SubtotalWithVatExcluded = 0m,
					Subtotal = 0m,
					SubtotalSpecified = true,
					Vat = 0m,
					VatSpecified = true
				}
			};
		}

		private static InvoiceCorrectionItem BuildTableItem()
		{
			return new InvoiceCorrectionItem
			{
				// Порядковый номер позиции в исходном СФ
				OriginalNumber = "1",

				Product = "Товар с маркировкой",
				Unit = "796",
				UnitName = "шт.",

				Okpd = "28.15.12.110",

				ItemTypeCode = "8471300000",

				// Дополнительная характеристика, идентификатор, вид, артикул
				AdditionalProperty = "0001",
				ItemCharact = "Высший сорт",
				ItemKind = "Электроник",
				ItemSeries = "Серия А",
				ItemArticle = "ART-001",
				ItemVendorCode = "VND-XYZ-001",
				ItemCategoryCode = "000000000000000000000CAT-01",

				ItemMark = InvoiceCorrectionItemItemMark.Item1,
				ItemMarkSpecified = true,

				ProductTypeCode = InvoiceCorrectionItemProductTypeCode.Item630,
				ProductTypeCodeSpecified = true,

				TaxRate = TaxRateWithTwentyTwoPercent.TwentyTwoFraction,

				// Изменение количества
				Quantity = new InvoiceCorrectionItemQuantity
				{
					OriginalValue = 10,
					OriginalValueSpecified = true,
					CorrectedValue = 12,
					CorrectedValueSpecified = true
				},

				// Изменение цены
				Price = new InvoiceCorrectionItemPrice
				{
					OriginalValue = 100m,
					OriginalValueSpecified = true,
					CorrectedValue = 100m,
					CorrectedValueSpecified = true
				},

				// Изменение стоимости без НДС
				SubtotalWithVatExcluded = new InvoiceCorrectionItemSubtotalWithVatExcluded
				{
					ItemElementName = ItemChoiceType.AmountsInc,
					Item = 200m,
					OriginalValue = 1000m,
					OriginalValueSpecified = true,
					CorrectedValue = 1200m,
					CorrectedValueSpecified = true
				},

				// Акциз
				Excise = new InvoiceCorrectionItemExcise
				{
					ItemElementName = ItemChoiceType1.AmountsInc,
					Item = 50m,
					OriginalValue = 500m,
					OriginalValueSpecified = true,
					CorrectedValue = 550m,
					CorrectedValueSpecified = true
				},

				// Изменение суммы НДС
				Vat = new InvoiceCorrectionItemVat
				{
					ItemElementName = ItemChoiceType2.AmountsInc,
					Item = 20m,
					OriginalValue = 100m,
					OriginalValueSpecified = true,
					CorrectedValue = 120m,
					CorrectedValueSpecified = true
				},

				// Изменение итоговой стоимости с НДС
				Subtotal = new InvoiceCorrectionItemSubtotal
				{
					ItemElementName = ItemChoiceType3.AmountsInc,
					Item = 220m,
					OriginalValue = 1100m,
					OriginalValueSpecified = true,
					CorrectedValue = 1320m,
					CorrectedValueSpecified = true
				},

				CustomsDeclarations = new[]
				{
					new InvoiceCorrectionItemCustomsDeclaration
					{
						ItemElementName = ItemChoiceType4.DeclarationNumbersInc,
						Item = new InvoiceCorrectionItemCustomsDeclarationDeclarationNumbersInc
						{
							DeclarationNumber = new[] { "10129010/010125/0000001" }
						},
						Country = "156",
						DeclarationNumberBefore = "10129010/010124/0000001",
						DeclarationNumberAfter = "10129010/010125/0000001"
					}
				},

				// Сведения о прослеживаемых товарах
				ItemTracingInfos = new[]
				{
					new InvoiceCorrectionItemItemTracingInfo
					{
						RegNumberUnit = "RU/12345/2025/00000000001",
						Unit = "796",
						ItemAddInfo = "Дополнительные сведения о прослеживаемом товаре",
						QuantityDiff = new InvoiceCorrectionItemItemTracingInfoQuantityDiff
						{
							OriginalQuantity = 10m,
							CorrectedQuantity = 12m,
							QuantityIncSpecified = true,
							QuantityInc = 2m
						},
						CostDiff = new InvoiceCorrectionItemItemTracingInfoCostDiff
						{
							OriginalCost = 1000m,
							CorrectedCost = 1200m,
							CostIncSpecified = true,
							CostInc = 200m
						}
					}
				},

				// Коды маркировки (КМ) до и после корректировки
				OriginalItemIdentificationNumbers = new[]
				{
					new ItemIdentificationNumbersItemIdentificationNumber
					{
						Gtin = "46070274787520",
						QuantityMark = "1",
						Units = new[] { "010460702747875021cTDJJ10ckV2Os" }
					}
				},
				CorrectedItemIdentificationNumbers = new[]
				{
					new ItemIdentificationNumbersItemIdentificationNumber
					{
						Gtin = "46070274787520",
						QuantityMark = "2",
						Units = new[] { "010460702747875021cTDJJ10ckV2Os", "010460702747875021xYZab10fgH3Kp" }
					}
				},

				// Дополнительные поля позиции
				AdditionalInfos = new[]
				{
					new AdditionalInfo100 { Id = "Доп.поле1", Value = "Значение1" },
					new AdditionalInfo100 { Id = "Доп.поле2", Value = "Значение2" }
				}
			};
		}

		private static EventContent BuildEventContent()
		{
			return new EventContent
			{
				OperationContent = EventContentOperationContent.Item1,
				OperationContentDetails = "Корректировка в связи с изменением количества товара",

				// Информация об изменении стоимости
				CostChangeInfo = "Согласовано письмом №456 от 15.12.2024",

				// Дата уведомления покупателя
				NotificationDate = "16.12.2024",

				// Документы-основания для корректировки
				CorrectionBases = new[]
				{
					new DocumentRequisitesType
					{
						DocumentName = "Соглашение об изменении количества",
						DocumentNumber = "456",
						DocumentDate = "15.12.2024",
						AdditionalInfo = "Доп. сведения о документе-основании"
					}
				},

				// Первичные документы, к которым относится корректировка
				TransferDocuments = new[]
				{
					new DocumentRequisitesType
					{
						DocumentName = "Товарная накладная",
						DocumentNumber = "100",
						DocumentDate = "01.12.2024",
						Id = "документ-id-в-диадоке",
						FileId = "файл-id",
						SystemId = "ERP-SYSTEM",
						SystemUrl = "https://erp.example.ru/doc/100",
						IdentificationDetails = new[]
						{
							new IdentificationDetails
							{
								Inn = "7750370238",
								OrgName = "ЗАО Очень Древний Папирус",
								StatusId = IdentificationDetailsStatusId.LegalEntity,
								StatusIdSpecified = true
							}
						}
					}
				}
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
						Position = new Position
						{
							PositionSource = PositionPositionSource.Manual,
							Value = "Работник"
						},
						SignerAdditionalInfo = new SignerAdditionalInfo
						{
							SignerAdditionalInfoSource = SignerAdditionalInfoSignerAdditionalInfoSource.StorageByTitleTypeId
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

		private static SignerPowersConfirmationMethod SignerPowersConfirmationMethodConvert(PowerOfAttorneyType powerOfAttorneyType)
		{
			switch (powerOfAttorneyType)
			{
				// Описание значений и другие варианты см. в xsd-схеме
				case PowerOfAttorneyType.None:
					return SignerPowersConfirmationMethod.Item6;
				case PowerOfAttorneyType.FileAsMeta:
					return SignerPowersConfirmationMethod.Item4;
				case PowerOfAttorneyType.InDocumentContent:
					return SignerPowersConfirmationMethod.Item3;
				default:
					return SignerPowersConfirmationMethod.Item6;
			}
		}
	}
}
