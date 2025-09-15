using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using Diadoc.Api.Http;
using Diadoc.Api.Nel.Helper;
using Diadoc.Api.Nel.Models;
using JetBrains.Annotations;

namespace Diadoc.Api.Nel
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class NelReportGenerator
	{
		[NotNull]
		public static NelReport GenerateReport([NotNull] HttpRequest request,
			[CanBeNull] HttpResponse response,
			[NotNull] Exception exception,
			[NotNull] string url,
			[NotNull] string userAgent,
			[NotNull] double samplingFraction)
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
					Protocol = "http/1.1", //WebRequest does not support http2
					Method = request.Method,
					Phase = failureInfo.Phase,
					Type = failureInfo.Detail,
					SamplingFraction = samplingFraction
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

		private static FailureInfo DeterminePhaseAndType([NotNull] Exception exception)
		{
			if (exception is WebException webEx)
			{
				switch (webEx.Status)
				{
					case WebExceptionStatus.NameResolutionFailure:
						return new FailureInfo(NelPhaseConstants.Dns, "dns.name_not_resolved");
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
