using System;
using System.Threading;
using System.Threading.Tasks;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[Obsolete("Use GenerateReceiptXmlAsync()")]
		public Task<GeneratedFile> GenerateDocumentReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer, CancellationToken ct = default)
		{
			var queryString = $"/GenerateDocumentReceiptXml?boxId={boxId}&messageId={messageId}&attachmentId={attachmentId}";
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, signer, ct: ct);
		}

		public Task<GeneratedFile> GenerateReceiptXmlAsync(string authToken, string boxId, string messageId, string attachmentId, Signer signer, CancellationToken ct = default)
		{
			var queryString = $"/GenerateReceiptXml?boxId={boxId}&messageId={messageId}&attachmentId={attachmentId}";
			return PerformGenerateXmlHttpRequestAsync(authToken, queryString, signer, ct: ct);
		}
	}
}