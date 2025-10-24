using System;
using System.ComponentModel;
using Diadoc.Api.Http;
using Diadoc.Api.Nel.Models;
using Diadoc.Api.Proto;

namespace Diadoc.Api.Nel.Helper
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpResponseExtensions
	{
		public static void SetNelInfo(this HttpResponse response)
		{
			if (response?.NelConfiguration == null)
			{
				return;
			}

			if (response.ReportTo != null)
			{
				NelInfo.NelConfiguration = response.NelConfiguration;
				NelInfo.ReportToConfigurations = response.ReportTo;
				NelInfo.ReportToConfigurations.ExpirationDate = new Timestamp(TimeSpan.FromSeconds(response.ReportTo.MaxAge).Ticks + DateTime.UtcNow.Ticks);
			}

			if (response.ReportingEndpoints != null)
			{
				NelInfo.ReportingEndpoints = response.ReportingEndpoints;
			}
		}
	}
}
