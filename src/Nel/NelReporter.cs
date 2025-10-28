using System;
using System.Collections.Generic;
using System.Linq;
using Diadoc.Api.Http;
using Diadoc.Api.Nel.Models;
using JetBrains.Annotations;

namespace Diadoc.Api.Nel
{
	internal class NelReporter
	{
		internal void SendNelReport(
			[NotNull] HttpRequest request,
			[CanBeNull] HttpResponse response,
			[NotNull] Exception exception,
			string baseUrl)
		{
			try
			{
				var nelConfig = response?.NelConfiguration ?? NelInfo.NelConfiguration;
				var reportToConfig = response?.ReportTo ?? NelInfo.ReportToConfigurations;
				var reportingEndpoints = response?.ReportingEndpoints ?? NelInfo.ReportingEndpoints ?? new ReportingEndpoints[0];
				var samplingFailureRate = response?.NelConfiguration?.FailureFraction ?? nelConfig?.FailureFraction ?? 1.0;

				if (nelConfig == null)
					return;

				if ((reportToConfig?.ExpirationDate != null && reportToConfig.ExpirationDate.Ticks < DateTime.UtcNow.Ticks) ||
				    Rolling() > samplingFailureRate)
					return;

				var report = NelReportGenerator.GenerateReport(
					request,
					response,
					exception,
					baseUrl + request.PathAndQuery,
					UserAgentBuilder.Build("C#"),
					samplingFailureRate);
				
				var endpoints = new List<string>();

				if (reportingEndpoints != null && reportingEndpoints.Any())
				{
					endpoints.AddRange(reportingEndpoints.Select(x => x.Endpoint));
				}

				if (reportToConfig?.Endpoints != null && reportToConfig.Endpoints.Any())
				{
					endpoints.AddRange(reportToConfig.Endpoints.Select(s => s.Url));
				}

				NelReporterSender.SendReport(endpoints.ToArray(), report);
			}
			catch
			{
				// No need to throw exceptions trying to send NEL reports
			}
		}

		private static double Rolling() => (double) (Guid.NewGuid().GetHashCode() & int.MaxValue) / int.MaxValue;
	}
}
