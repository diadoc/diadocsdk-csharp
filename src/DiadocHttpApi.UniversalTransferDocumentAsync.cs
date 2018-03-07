using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<GeneratedFile> GenerateUniversalTransferDocumentXmlForSellerAsync(
			string authToken,
			UniversalTransferDocumentSellerTitleInfo info,
			bool disableValidation = false)
		{

			return GenerateUniversalTransferDocumentXmlAsync(authToken, info, false, disableValidation);
		}

		public Task<GeneratedFile> GenerateUniversalCorrectionDocumentXmlForSellerAsync(
			string authToken,
			UniversalCorrectionDocumentSellerTitleInfo correctionInfo,
			bool disableValidation = false)
		{
			return GenerateUniversalTransferDocumentXmlAsync(authToken, correctionInfo, true, disableValidation);
		}

		private Task<GeneratedFile> GenerateUniversalTransferDocumentXmlAsync<T>(string authToken, T protoInfo, bool isCorrection, bool disableValidation = false) where T : class
		{
			var query = new PathAndQueryBuilder("/GenerateUniversalTransferDocumentXmlForSeller");
			if (isCorrection)
				query.AddParameter("correction", "");
			if (disableValidation)
				query.AddParameter("disableValidation", "");
			return PerformGenerateXmlHttpRequestAsync(authToken, query.BuildPathAndQuery(), protoInfo);
		}

		public Task<GeneratedFile> GenerateUniversalTransferDocumentXmlForBuyerAsync(string authToken, UniversalTransferDocumentBuyerTitleInfo info,
			string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			var query = new PathAndQueryBuilder("/GenerateUniversalTransferDocumentXmlForBuyer");
			query.AddParameter("boxId", boxId);
			query.AddParameter("sellerTitleMessageId", sellerTitleMessageId);
			query.AddParameter("sellerTitleAttachmentId", sellerTitleAttachmentId);
			return PerformGenerateXmlHttpRequestAsync(authToken, query.BuildPathAndQuery(), info);
		}

		public Task<UniversalTransferDocumentSellerTitleInfo> ParseUniversalTransferDocumentSellerTitleXmlAsync(byte[] xmlContent)
		{
			var query = new PathAndQueryBuilder("/ParseUniversalTransferDocumentSellerTitleXml");
			query.AddParameter("documentVersion", "utd_05_01_04");
			return PerformHttpRequestAsync<UniversalTransferDocumentSellerTitleInfo>(null, "POST", query.BuildPathAndQuery(), xmlContent);
		}

		public Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalTransferDocumentBuyerTitleXmlAsync(byte[] xmlContent)
		{
			return PerformHttpRequestAsync<UniversalTransferDocumentBuyerTitleInfo>(null, "POST", "/ParseUniversalTransferDocumentBuyerTitleXml", xmlContent);
		}

		public Task<UniversalCorrectionDocumentSellerTitleInfo> ParseUniversalCorrectionDocumentSellerTitleXmlAsync(byte[] xmlContent)
		{
			var query = new PathAndQueryBuilder("/ParseUniversalCorrectionDocumentSellerTitleXml");
			query.AddParameter("documentVersion", "utd_05_01_04");
			return PerformHttpRequestAsync<UniversalCorrectionDocumentSellerTitleInfo>(null, "POST", query.BuildPathAndQuery(), xmlContent);
		}

		public Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalCorrectionDocumentBuyerTitleXmlAsync(byte[] xmlContent)
		{
			return PerformHttpRequestAsync<UniversalTransferDocumentBuyerTitleInfo>(null, "POST", "/ParseUniversalCorrectionDocumentBuyerTitleXml", xmlContent);
		}
	}
}