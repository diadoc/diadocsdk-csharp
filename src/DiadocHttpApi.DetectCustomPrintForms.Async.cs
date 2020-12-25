using System.Threading;
using System.Threading.Tasks;
using Diadoc.Api.Proto;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<CustomPrintFormDetectionResult> DetectCustomPrintFormsAsync([NotNull] string authToken, [NotNull] string boxId, CustomPrintFormDetectionRequest request, CancellationToken ct = default)
		{
			var queryString = BuildQueryStringWithBoxId("DetectCustomPrintForms", boxId);
			return PerformHttpRequestAsync<CustomPrintFormDetectionRequest, CustomPrintFormDetectionResult>(
				authToken, 
				queryString,
				request,
				ct: ct);
		}
	}
}