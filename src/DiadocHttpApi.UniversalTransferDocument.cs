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
			var query = new PathAndQueryBuilder("/GenerateUniversalTransferDocumentXmlForSeller");
			if (disableValidation)
				query.AddParameter("disableValidation", "");
			return PerformGenerateXmlHttpRequest(authToken, query.BuildPathAndQuery(), info);
		}

		public GeneratedFile GenerateUniversalTransferDocumentXmlForBuyer(string authToken, UniversalTransferDocumentBuyerTitleInfo info,
			string boxId, string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			var queryString = string.Format("/GenerateUniversalTransferDocumentXmlForBuyer?boxId={0}&sellerTitleMessageId={1}&sellerTitleAttachmentId={2}", boxId, sellerTitleMessageId, sellerTitleAttachmentId);
			return PerformGenerateXmlHttpRequest(authToken, queryString, info);
		}

		public UniversalTransferDocumentSellerTitleInfo ParseUniversalTransferDocumentSellerTitleXml(byte[] xmlContent)
		{
			return PerformHttpRequest<UniversalTransferDocumentSellerTitleInfo>(null, "POST", "/ParseUniversalTransferDocumentSellerTitleXml", xmlContent);
		}

		public UniversalTransferDocumentBuyerTitleInfo ParseUniversalTransferDocumentBuyerTitleXml(byte[] xmlContent)
		{
			return PerformHttpRequest<UniversalTransferDocumentBuyerTitleInfo>(null, "POST", "/ParseUniversalTransferDocumentBuyerTitleXml", xmlContent);
		}
	}
}