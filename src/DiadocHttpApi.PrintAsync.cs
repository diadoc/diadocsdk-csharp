using System.Text;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Documents;
using JetBrains.Annotations;
using DocumentType = Diadoc.Api.Proto.DocumentType;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[ItemNotNull]
		public async Task<string> GeneratePrintFormFromAttachmentAsync(string authToken, DocumentType documentType, byte[] content, string fromBoxId = null)
		{
			var qsb = new PathAndQueryBuilder("/GeneratePrintFormFromAttachment")
				.With("documentType", documentType.ToString());
			if (!string.IsNullOrEmpty(fromBoxId))
				qsb.AddParameter("fromBoxId", fromBoxId);
			var responseBytes = await PerformHttpRequestAsync(authToken, "POST", qsb.BuildPathAndQuery(), content).ConfigureAwait(false);
			return Encoding.UTF8.GetString(responseBytes);
		}

		[ItemNotNull]
		public Task<PrintFormResult> GeneratePrintFormAsync(string authToken, string boxId, string messageId, string documentId)
		{
			var qsb = new PathAndQueryBuilder("/GeneratePrintForm")
				.With("boxId", boxId)
				.With("messageId", messageId)
				.With("documentId", documentId);
			return GetPrintFormResultAsync(authToken, qsb.BuildPathAndQuery());
		}

		[ItemNotNull]
		public Task<PrintFormResult> GetGeneratedPrintFormAsync(string authToken, DocumentType documentType, string printFormId)
		{
			var qsb = new PathAndQueryBuilder("/GetGeneratedPrintForm")
				.With("documentType", documentType.ToString())
				.With("printFormId", printFormId);
			return GetPrintFormResultAsync(authToken, qsb.BuildPathAndQuery());
		}

		[ItemNotNull]
		protected async Task<PrintFormResult> GetPrintFormResultAsync([CanBeNull] string authToken, [NotNull] string queryString)
		{
			var request = BuildHttpRequest(authToken, "GET", queryString, null);
			var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
			return response.RetryAfter.HasValue
				? new PrintFormResult(response.RetryAfter.Value)
				: new PrintFormResult(new PrintFormContent(response.ContentType, response.ContentDispositionFileName, response.Content));
		}

		[ItemNotNull]
		public Task<DocumentProtocolResult> GenerateDocumentProtocolAsync(string authToken, string boxId, string messageId, string documentId)
		{
			var qsb = new PathAndQueryBuilder("/GenerateDocumentProtocol")
				.With("boxId", boxId)
				.With("messageId", messageId)
				.With("documentId", documentId);
			var request = BuildHttpRequest(authToken, "GET", qsb.BuildPathAndQuery(), null);
			return GenerateDocumentProtocolAsync(request);
		}

		[ItemNotNull]
		protected async Task<DocumentProtocolResult> GenerateDocumentProtocolAsync([NotNull] HttpRequest request)
		{
			var response = await HttpClient.PerformHttpRequestAsync(request).ConfigureAwait(false);
			return response.RetryAfter.HasValue
				? new DocumentProtocolResult(response.RetryAfter.Value)
				: new DocumentProtocolResult(Deserialize<DocumentProtocol>(response.Content));
		}
	}
}
