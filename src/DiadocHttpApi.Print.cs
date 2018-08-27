using System;
using System.Text;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Documents;
using JetBrains.Annotations;
using DocumentType = Diadoc.Api.Proto.DocumentType;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[NotNull]
		public string GeneratePrintFormFromAttachment(string authToken, DocumentType documentType, byte[] content, string fromBoxId = null)
		{
			var queryString = new StringBuilder();
			queryString.AppendFormat("/GeneratePrintFormFromAttachment?documentType={0}", documentType);
			if (!string.IsNullOrEmpty(fromBoxId))
				queryString.AppendFormat("&fromBoxId={0}", fromBoxId);
			var responseBytes = PerformHttpRequest(authToken, "POST", queryString.ToString(), content);
			return Encoding.UTF8.GetString(responseBytes);
		}

		[NotNull]
		public PrintFormResult GeneratePrintForm(string authToken, string boxId, string messageId, string documentId)
		{
			var queryString = string.Format("/GeneratePrintForm?boxId={0}&messageId={1}&documentId={2}", boxId, messageId, documentId);
			return GetPrintFormResult(authToken, queryString);
		}

		[NotNull]
		[Obsolete("Use GetGeneratedPrintForm without `documentType` parameter")]
		public PrintFormResult GetGeneratedPrintForm(string authToken, DocumentType documentType, string printFormId)
		{
			var queryString = string.Format("/GetGeneratedPrintForm?documentType={0}&printFormId={1}", documentType, printFormId);
			return GetPrintFormResult(authToken, queryString);
		}

		[NotNull]
		public PrintFormResult GetGeneratedPrintForm(string authToken, string printFormId)
		{
			var queryString = string.Format("/GetGeneratedPrintForm?printFormId={0}", printFormId);
			return GetPrintFormResult(authToken, queryString);
		}

		[NotNull]
		protected PrintFormResult GetPrintFormResult([CanBeNull] string authToken, [NotNull] string queryString)
		{
			var request = BuildHttpRequest(authToken, "GET", queryString, null);
			var response = HttpClient.PerformHttpRequest(request);
			return response.RetryAfter.HasValue
				? new PrintFormResult(response.RetryAfter.Value)
				: new PrintFormResult(new PrintFormContent(response.ContentType, response.ContentDispositionFileName, response.Content));
		}

		[NotNull]
		public DocumentProtocolResult GenerateDocumentProtocol(string authToken, string boxId, string messageId, string documentId)
		{
			var queryString = string.Format("/GenerateDocumentProtocol?boxId={0}&messageId={1}&documentId={2}", boxId, messageId, documentId);
			var request = BuildHttpRequest(authToken, "GET", queryString, null);
			return GenerateDocumentProtocol(request);
		}

		[NotNull]
		protected DocumentProtocolResult GenerateDocumentProtocol([NotNull] HttpRequest request)
		{
			var response = HttpClient.PerformHttpRequest(request);
			return response.RetryAfter.HasValue
				? new DocumentProtocolResult(response.RetryAfter.Value)
				: new DocumentProtocolResult(Deserialize<DocumentProtocol>(response.Content));
		}
	}
}
