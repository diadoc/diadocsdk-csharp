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
			bool disableValidation = false)
		{

			return GenerateUniversalTransferDocumentXml(authToken, info, false, disableValidation);
		}

		public GeneratedFile GenerateUniversalCorrectionDocumentXmlForSeller(
			string authToken,
			UniversalCorrectionDocumentSellerTitleInfo correctionInfo,
			bool disableValidation = false)
		{
			return GenerateUniversalTransferDocumentXml(authToken, correctionInfo, true, disableValidation);
		}

		private GeneratedFile GenerateUniversalTransferDocumentXml<T>(string authToken, T protoInfo, bool isCorrection, bool disableValidation = false) where T : class
		{
			var query = new PathAndQueryBuilder("/GenerateUniversalTransferDocumentXmlForSeller");
			if (isCorrection)
				query.AddParameter("correction", "");
			if (disableValidation)
				query.AddParameter("disableValidation", "");
			return PerformGenerateXmlHttpRequest(authToken, query.BuildPathAndQuery(), protoInfo);
		}

		public GeneratedFile GenerateUniversalTransferDocumentXmlForBuyer(string authToken, UniversalTransferDocumentBuyerTitleInfo info,
			string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			var query = new PathAndQueryBuilder("/GenerateUniversalTransferDocumentXmlForBuyer");
			query.AddParameter("boxId", boxId);
			query.AddParameter("sellerTitleMessageId", sellerTitleMessageId);
			query.AddParameter("sellerTitleAttachmentId", sellerTitleAttachmentId);
			return PerformGenerateXmlHttpRequest(authToken, query.BuildPathAndQuery(), info);
		}

		public UniversalTransferDocumentSellerTitleInfo ParseUniversalTransferDocumentSellerTitleXml(byte[] xmlContent)
		{
			var query = new PathAndQueryBuilder("/ParseUniversalTransferDocumentSellerTitleXml");
			query.AddParameter("documentVersion", "utd_05_01_04");
			return PerformHttpRequest<UniversalTransferDocumentSellerTitleInfo>(null, "POST", query.BuildPathAndQuery(), xmlContent);
		}

		public UniversalTransferDocumentBuyerTitleInfo ParseUniversalTransferDocumentBuyerTitleXml(byte[] xmlContent)
		{
			return PerformHttpRequest<UniversalTransferDocumentBuyerTitleInfo>(null, "POST", "/ParseUniversalTransferDocumentBuyerTitleXml", xmlContent);
		}

		public UniversalCorrectionDocumentSellerTitleInfo ParseUniversalCorrectionDocumentSellerTitleXml(byte[] xmlContent)
		{
			var query = new PathAndQueryBuilder("/ParseUniversalCorrectionDocumentSellerTitleXml");
			query.AddParameter("documentVersion", "ucd_05_01_02");
			return PerformHttpRequest<UniversalCorrectionDocumentSellerTitleInfo>(null, "POST", query.BuildPathAndQuery(), xmlContent);
		}

		public UniversalTransferDocumentBuyerTitleInfo ParseUniversalCorrectionDocumentBuyerTitleXml(byte[] xmlContent)
		{
			return PerformHttpRequest<UniversalTransferDocumentBuyerTitleInfo>(null, "POST", "/ParseUniversalCorrectionDocumentBuyerTitleXml", xmlContent);
		}
	}
}