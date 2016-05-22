using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public GeneratedFile GenerateDocumentReceiptXml(string authToken, string boxId, string messageId, string attachmentId, Signer signer)
		{
			var queryString = string.Format("/GenerateDocumentReceiptXml?boxId={0}&messageId={1}&attachmentId={2}", boxId, messageId, attachmentId);
			return PerformGenerateXmlHttpRequest(authToken, queryString, signer);
		}
	}
}
