using System;
using System.Threading.Tasks;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[Obsolete("Use GenerateReceiptXmlAsync()")]
		public Task<GeneratedFile> GenerateDocumentReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer)
		{
			var queryString = $"/GenerateDocumentReceiptXml?boxId={boxId}&messageId={messageId}&attachmentId={attachmentId}";
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, signer);
		}

		public Task<GeneratedFile> GenerateReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer)
		{
			var queryString = $"/GenerateReceiptXml?boxId={boxId}&messageId={messageId}&attachmentId={attachmentId}";
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, signer);
		}
	}
}