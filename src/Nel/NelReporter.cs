using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using JetBrains.Annotations;

namespace Diadoc.Api.Nel
{
	public class NelReporter
	{
		private readonly string userAgent;

		public NelReporter([NotNull] string userAgent)
		{
			this.userAgent = userAgent;
		}

		public void SetNelInfo([CanBeNull] HttpResponse response)
		{
			if (response?.NelConfiguration != null && response.ReportTo != null)
			{
				NelInfo.NelConfiguration = response.NelConfiguration;
				NelInfo.ReportToConfigurations = response.ReportTo;
				NelInfo.ReportToConfigurations.ExpirationDate = new Timestamp(TimeSpan.FromSeconds(response.ReportTo.MaxAge).Ticks + DateTime.UtcNow.Ticks);
			}
		}

		public void SendNelReport([NotNull] HttpRequest request, [CanBeNull] HttpResponse response, [NotNull] Exception exception, string baseUrl)
		{
			try
			{
				var nelConfig = NelInfo.NelConfiguration;
				var reportToConfig = NelInfo.ReportToConfigurations;

				if (nelConfig == null || reportToConfig == null)
					return;

				var report = GenerateReport(request, response, exception, baseUrl + request.PathAndQuery);

				foreach (var endpoint in reportToConfig.Endpoints)
				{
					if (!string.IsNullOrEmpty(endpoint.Url) && reportToConfig.ExpirationDate.Ticks >= DateTime.UtcNow.Ticks)
					{
						SendReport(endpoint.Url, report);
					}
				}
			}
			catch
			{
				// No need to throw exceptions trying to send NEL reports
			}
		}

		[NotNull]
		private NelReport GenerateReport(
			[NotNull] HttpRequest request,
			[CanBeNull] HttpResponse response,
			[NotNull] Exception exception,
			[NotNull] string url)
		{
			var failureInfo = DeterminePhaseAndType(exception);
			var report = new NelReport
			{
				Age = 0,
				Type = "network-error",
				Url = url,
				UserAgent = userAgent,
				Body = new NelReportBody
				{
					Protocol = "http/1.1",
					Method = request.Method,
					Phase = failureInfo.Phase,
					Type = failureInfo.Detail
				}
			};

			if (response != null)
			{
				report.Body.StatusCode = (int) response.StatusCode;
			}
			else if (exception is WebException webEx && webEx.Response is HttpWebResponse httpResponse)
			{
				report.Body.StatusCode = (int) httpResponse.StatusCode;
			}
			else
			{
				report.Body.StatusCode = 0;
			}

			return report;
		}

		private static void SendReport([NotNull] string endpointUrl, [NotNull] NelReport report)
		{
			if (string.IsNullOrEmpty(endpointUrl))
				return;

			var json = Newtonsoft.Json.JsonConvert.SerializeObject(report);
			var data = Encoding.UTF8.GetBytes(json);
			var webRequest = (HttpWebRequest) WebRequest.Create(endpointUrl);
			webRequest.Method = "POST";
			webRequest.ContentType = "application/json";
			webRequest.ContentLength = data.Length;
			webRequest.Timeout = 15000;
			webRequest.ReadWriteTimeout = 15000;

			webRequest.BeginGetRequestStream(r =>
			{
				try
				{
					using (var requestStream = webRequest.EndGetRequestStream(r))
					{
						requestStream.Write(data, 0, data.Length);
					}

					webRequest.BeginGetResponse(t =>
					{
						try
						{
							using ((HttpWebResponse) webRequest.EndGetResponse(t))
							{
								// Just for make sure the request has been sent and resources are released
							}
						}
						catch
						{
							// ignored
						}
					}, null);
				}
				catch
				{
					// ignored
				}
			}, null);
		}

		private static FailureInfo DeterminePhaseAndType([NotNull] Exception exception)
		{
			if (exception is WebException webEx)
			{
				switch (webEx.Status)
				{
					case WebExceptionStatus.NameResolutionFailure:
						return new FailureInfo(NelPhaseConstants.Dns, "dns.name_not_resolve");
					case WebExceptionStatus.ProtocolError:
						return new FailureInfo(NelPhaseConstants.Application, "http.error");
					case WebExceptionStatus.TrustFailure:
						return new FailureInfo(NelPhaseConstants.Connection, "tls.cert.invalid");
					case WebExceptionStatus.ConnectFailure:
					case WebExceptionStatus.ProxyNameResolutionFailure:
					{
						if (webEx.InnerException is SocketException socketEx)
						{
							switch (socketEx.SocketErrorCode)
							{
								case SocketError.ConnectionReset:
									return new FailureInfo(NelPhaseConstants.Connection, "tcp.reset");
								case SocketError.ConnectionRefused:
									return new FailureInfo(NelPhaseConstants.Connection, "tcp.refused");
								case SocketError.TimedOut:
									return new FailureInfo(NelPhaseConstants.Connection, "tcp.timed_out");
								case SocketError.NetworkUnreachable:
								case SocketError.HostUnreachable:
									return new FailureInfo(NelPhaseConstants.Connection, "tcp.address_unreachable");
								case SocketError.AddressNotAvailable:
								case SocketError.AddressFamilyNotSupported:
								case SocketError.InvalidArgument:
									return new FailureInfo(NelPhaseConstants.Connection, "tcp.address_invalid");
							}
						}
					}
						return new FailureInfo(NelPhaseConstants.Connection, "tcp.failed");
				}
			}

			return new FailureInfo(NelPhaseConstants.Unknown, "unknown");
		}
	}
}
