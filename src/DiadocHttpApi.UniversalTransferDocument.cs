using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public GeneratedFile GenerateUniversalTransferDocumentXmlForSeller(
			string authToken,
			UniversalTransferDocumentSellerTitleInfo info,
			bool disableValidation = false,
			string documentVersion = null)
		{
			return GenerateUniversalTransferDocumentXml(authToken, info, false, disableValidation, documentVersion);
		}

		public GeneratedFile GenerateUniversalCorrectionDocumentXmlForSeller(
			string authToken,
			UniversalCorrectionDocumentSellerTitleInfo correctionInfo,
			bool disableValidation = false,
			string documentVersion = null)
		{
			return GenerateUniversalTransferDocumentXml(authToken, correctionInfo, true, disableValidation, documentVersion);
		}

		private GeneratedFile GenerateUniversalTransferDocumentXml<T>(
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
			return PerformGenerateXmlHttpRequest(authToken, query.BuildPathAndQuery(), protoInfo);
		}

		public GeneratedFile GenerateUniversalTransferDocumentXmlForBuyer(
			string authToken, 
			UniversalTransferDocumentBuyerTitleInfo info,
			string boxId, 
			string sellerTitleMessageId, 
			string sellerTitleAttachmentId)
		{
			var query = new PathAndQueryBuilder("/GenerateUniversalTransferDocumentXmlForBuyer");
			query.AddParameter("boxId", boxId);
			query.AddParameter("sellerTitleMessageId", sellerTitleMessageId);
			query.AddParameter("sellerTitleAttachmentId", sellerTitleAttachmentId);
			return PerformGenerateXmlHttpRequest(authToken, query.BuildPathAndQuery(), info);
		}
	}
}