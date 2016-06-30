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
	}
}