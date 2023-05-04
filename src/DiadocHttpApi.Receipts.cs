using System;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[Obsolete("Use GenerateReceiptXmlV2()")]
		public GeneratedFile GenerateDocumentReceiptXml(string authToken, string boxId, string messageId, string attachmentId, Signer signer)
		{
			return GenerateReceiptXml(authToken, boxId, messageId, attachmentId, signer);
		}

		[Obsolete("Use GenerateReceiptXmlV2()")]
		public GeneratedFile GenerateReceiptXml(string authToken, string boxId, string messageId, string attachmentId, Signer signer)
		{
			var queryBuilder = new PathAndQueryBuilder("/GenerateReceiptXml");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("messageId", messageId);
			queryBuilder.AddParameter("attachmentId", attachmentId);

			return PerformGenerateXmlHttpRequest(authToken, queryBuilder.BuildPathAndQuery(), signer);
		}

		public GeneratedFile GenerateReceiptXmlV2(string authToken, string boxId, ReceiptGenerationRequestV2 receiptGenerationRequest)
		{
			var queryBuilder = new PathAndQueryBuilder("/V2/GenerateReceiptXml");
			queryBuilder.AddParameter("boxId", boxId);

			return PerformGenerateXmlHttpRequest(authToken, queryBuilder.BuildPathAndQuery(), receiptGenerationRequest);
		}
	}
}
