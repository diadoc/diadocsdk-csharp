using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using Diadoc.Api.Http;
using Diadoc.Api.Nel;
using Diadoc.Api.Nel.Helper;
using Diadoc.Api.Nel.Models;
using NUnit.Framework;
using Range = Diadoc.Api.Http.Range;

namespace Diadoc.Api.Tests
{
	[TestFixture]
	public class Nel_Tests
	{
		[Test]
		public void ReportingEndpointsTest_CheckExist()
		{
			var response = new HttpResponse(HttpStatusCode.OK, null, new NameValueCollection()
			{
				{ "Reporting-Endpoints", "csp-endpoint=\"https://example.com/csp-reports\"" }
			});

			var endpoints = response.ReportingEndpoints;

			Assert.That(endpoints, Is.Not.Null);
			Assert.That(endpoints.Length, Is.EqualTo(1));
			Assert.That(endpoints.First().Name, Is.EqualTo("csp-endpoint"));
			Assert.That(endpoints.First().Endpoint, Is.EqualTo("https://example.com/csp-reports"));
		}


		[Test]
		public void ReportingEndpointsTest_CheckExist_MultipleEndpoints()
		{
			var response = new HttpResponse(HttpStatusCode.OK, null, new NameValueCollection()
			{
				{
					"Reporting-Endpoints", "csp-endpoint=\"https://example.com/csp-reports\"," +
					                       "permissions-endpoint=\"https://example.com/permissions-policy-reports\""
				}
			});

			var endpoints = response.ReportingEndpoints;

			Assert.That(endpoints, Is.Not.Null);
			Assert.That(endpoints.Length, Is.EqualTo(2));
			Assert.That(endpoints.Select(s => s.Name).ToArray(), Is.EqualTo(new[]
			{
				"csp-endpoint",
				"permissions-endpoint"
			}));
			Assert.That(endpoints.Select(s => s.Endpoint).ToArray(), Is.EqualTo(new[]
			{
				"https://example.com/csp-reports",
				"https://example.com/permissions-policy-reports"
			}));
		}
		
		
		[Test]
		public void ReportTo_CheckExist()
		{
			var response = new HttpResponse(HttpStatusCode.OK, null, new NameValueCollection()
			{
				{
					"Report-To", "{ \"group\": \"csp-endpoints\",\n\"max_age\": 10886400,\n\"endpoints\": [\n{ \"url\": \"https://example.com/reports\" }]}"
				}
			});

			var reportTo = response.ReportTo;

			Assert.That(reportTo, Is.Not.Null);
			Assert.That(reportTo.Endpoints.Length, Is.EqualTo(1));
			Assert.That(reportTo.Endpoints.First().Url, Is.EqualTo("https://example.com/reports"));
			Assert.That(reportTo.MaxAge, Is.EqualTo(10886400));
			Assert.That(reportTo.Group, Is.EqualTo("csp-endpoints"));
			
		}
		
		[Test]
		public void ReportTo_CheckExist_MultipleEndpoints()
		{
			var response = new HttpResponse(HttpStatusCode.OK, null, new NameValueCollection()
			{
				{
					"Report-To", "{ \"group\": \"csp-endpoints\",\n \"max_age\": 10886400,\n \"endpoints\": [\n{ \"url\": \"https://example.com/reports\" },\n{ \"url\": \"https://backup.com/reports\" }\n] }"
				}
			});

			var reportTo = response.ReportTo;

			Assert.That(reportTo, Is.Not.Null);
			Assert.That(reportTo.Endpoints.Length, Is.EqualTo(2));
			Assert.That(reportTo.Endpoints.Select(s=> s.Url), Is.EqualTo(new []
			{
				"https://example.com/reports",
				"https://backup.com/reports"
			}));
			Assert.That(reportTo.MaxAge, Is.EqualTo(10886400));
			Assert.That(reportTo.Group, Is.EqualTo("csp-endpoints"));
			
		}
		
		[Test]
		public void Nel_CheckExist()
		{
			var response = new HttpResponse(HttpStatusCode.OK, null, new NameValueCollection()
			{
				{
					"NEL", "{ \"report_to\": \"name_of_reporting_group\", \"max_age\": 12345, \"include_subdomains\": false, \"success_fraction\": 0.0, \"failure_fraction\": 1.0 }"
				}
			});

			var nelConfiguration = response.NelConfiguration;

			Assert.That(nelConfiguration, Is.Not.Null);
			Assert.That(nelConfiguration.FailureFraction, Is.EqualTo(1.0));
			Assert.That(nelConfiguration.ReportTo, Is.EqualTo("name_of_reporting_group"));
			Assert.That(nelConfiguration.MaxAge, Is.EqualTo(12345));
			Assert.That(nelConfiguration.IncludeSubdomains, Is.EqualTo(false));
		}
		
		[Test]
		public void SetNelInfo_CheckSuccess()
		{
			var response = new HttpResponse(HttpStatusCode.OK, null, new NameValueCollection()
			{
				{ "NEL", "{ \"report_to\": \"name_of_reporting_group\", \"max_age\": 12345, \"include_subdomains\": false, \"success_fraction\": 0.0, \"failure_fraction\": 1.0 }" },
				{ "Report-To", "{ \"group\": \"csp-endpoints\",\n \"max_age\": 10886400,\n \"endpoints\": [\n{ \"url\": \"https://example.com/reports\" },\n{ \"url\": \"https://backup.com/reports\" }\n] }" },
				{ "Reporting-Endpoints", "csp-endpoint=\"https://example.com/csp-reports\"" }
			});

			response.SetNelInfo();
			
			Assert.That(NelInfo.NelConfiguration, Is.Not.Null);
			Assert.That(NelInfo.ReportingEndpoints?.Any() ?? false, Is.EqualTo(true));
			Assert.That(NelInfo.ReportToConfigurations, Is.Not.Null);
		}
		
		[Test]
		public void GenerateReport_CheckSuccess()
		{
			var request = new HttpRequest(
				"GET",
				"reports", 
				null,
				30,
				"Test",
				new Range(10, 30));
			
			var response = new HttpResponse(HttpStatusCode.OK, null, new NameValueCollection()
			{
				{ "NEL", "{ \"report_to\": \"name_of_reporting_group\", \"max_age\": 12345, \"include_subdomains\": false, \"success_fraction\": 0.0, \"failure_fraction\": 0.7 }" },
				{ "Report-To", "{ \"group\": \"csp-endpoints\",\n \"max_age\": 10886400,\n \"endpoints\": [\n{ \"url\": \"https://example.com/reports\" },\n{ \"url\": \"https://backup.com/reports\" }\n] }" },
				{ "Reporting-Endpoints", "csp-endpoint=\"https://example.com/csp-reports\"" }
			});

			var report = NelReportGenerator.GenerateReport(
				request,
				response,
				new WebException("Test", WebExceptionStatus.NameResolutionFailure),
				"reports",
				"Diadoc C# SDK",
				response?.NelConfiguration?.FailureFraction ?? 1.0);
			
			Assert.That(report, Is.Not.Null);
			Assert.That(report.UserAgent, Is.Not.Null.And.Match("Diadoc C# SDK"));
			Assert.That(report.Body.SamplingFraction, Is.EqualTo(0.7));
			Assert.That(report.Body.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
			Assert.That(report.Body.Phase, Is.EqualTo(NelPhaseConstants.Dns));
			Assert.That(report.Body.Type, Is.EqualTo("dns.name_not_resolved"));
		}
	}
}
