

using Diadoc.Api.Proto;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[NotNull]
		public CustomPrintFormDetectionResult DetectCustomPrintForms([NotNull] string authToken, [NotNull] string boxId, CustomPrintFormDetectionRequest request)
		{
			var queryString = BuildQueryStringWithBoxId("DetectCustomPrintForms", boxId);
			return PerformHttpRequest<CustomPrintFormDetectionRequest, CustomPrintFormDetectionResult>(
				authToken, 
				queryString,
				request);
		}
	}
}