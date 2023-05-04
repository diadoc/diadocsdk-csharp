using System;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[Obsolete("Use GenerateReceiptXmlV2Async()")]
		public Task<GeneratedFile> GenerateDocumentReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer)
		{
			return GenerateReceiptXmlAsync(authToken, boxId, messageId, attachmentId, signer);
		}

		[Obsolete("Use GenerateReceiptXmlV2Async()")]
		public Task<GeneratedFile> GenerateReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateReceiptXml");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("messageId", messageId);
			queryBuilder.AddParameter("attachmentId", attachmentId);

			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), signer);
		}

		public Task<GeneratedFile> GenerateReceiptXmlV2Async(string authToken, string boxId, ReceiptGenerationRequestV2 receiptGenerationRequest)
		{
			var queryBuilder = new PathAndQueryBuilder("/V2/GenerateReceiptXml");
			queryBuilder.AddParameter("boxId", boxId);

			return PerformGenerateXmlHttpRequestAsync(authToken, queryBuilder.BuildPathAndQuery(), receiptGenerationRequest);
		}
	}
}
