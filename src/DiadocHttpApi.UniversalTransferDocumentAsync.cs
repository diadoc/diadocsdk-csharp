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
			bool disableValidation = false,
			string documentVersion = null)
		{

			return GenerateUniversalTransferDocumentXmlAsync(authToken, info, false, disableValidation, documentVersion);
		}

		public Task<GeneratedFile> GenerateUniversalCorrectionDocumentXmlForSellerAsync(
			string authToken,
			UniversalCorrectionDocumentSellerTitleInfo correctionInfo,
			bool disableValidation = false,
			string documentVersion = null)
		{
			return GenerateUniversalTransferDocumentXmlAsync(authToken, correctionInfo, true, disableValidation, documentVersion);
		}

		private Task<GeneratedFile> GenerateUniversalTransferDocumentXmlAsync<T>(
			string authToken, 
			T protoInfo, 
			bool isCorrection, 
			bool disableValidation = false,
			string documentVersion = null) 
			where T : class
		{
			var query = new PathAndQueryBuilder("/GenerateUniversalTransferDocumentXmlForSeller");
			query.AddParameter("documentVersion", documentVersion);
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
	}
}