using System.Text;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Recognition;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public string Recognize(string authToken, string fileName, byte[] content)
		{
			var queryString = string.Format("/Recognize?filename={0}", fileName);
			var responseBytes = PerformHttpRequest(authToken, "POST", queryString, content);
			return Encoding.UTF8.GetString(responseBytes);
		}

		public Recognized GetRecognized(string authToken, string recognitionId)
		{
			var queryString = string.Format("/GetRecognized?recognitionId={0}", recognitionId);
			return PerformHttpRequest<Recognized>(authToken, "GET", queryString);
		}

		public RussianAddress ParseRussianAddress(string authToken, string address)
		{
			var queryString = string.Format("/ParseRussianAddress?address={0}", address);
			return PerformHttpRequest<RussianAddress>(authToken, "GET", queryString);
		}
	}
}
