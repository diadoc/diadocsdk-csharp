using System;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[Obsolete("Use GenerateReceiptXml()")]
		public GeneratedFile GenerateDocumentReceiptXml(string authToken, string boxId, string messageId, string attachmentId, Signer signer)
		{
			var queryString = string.Format("/GenerateDocumentReceiptXml?boxId={0}&messageId={1}&attachmentId={2}", boxId, messageId, attachmentId);
			return PerformGenerateXmlHttpRequest(authToken, queryString, signer);
		}

		public GeneratedFile GenerateReceiptXml(string authToken, string boxId, string messageId, string attachmentId, Signer signer)
		{
			var queryString = string.Format("/GenerateReceiptXml?boxId={0}&messageId={1}&attachmentId={2}", boxId, messageId, attachmentId);
			return PerformGenerateXmlHttpRequest(authToken, queryString, signer);
		}
	}
}