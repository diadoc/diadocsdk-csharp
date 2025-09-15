using System.Linq;
using System.Net;
using System.Text;
using Diadoc.Api.Nel.Models;
using JetBrains.Annotations;

namespace Diadoc.Api.Nel
{
	internal static class NelReporterSender
	{
		internal static void SendReport([NotNull] string[] endpointsUrl, [NotNull] NelReport report, int iterator = 0)
		{
			if (!endpointsUrl.Any() || iterator >= endpointsUrl.Length)
				return;

			var json = Newtonsoft.Json.JsonConvert.SerializeObject(report);
			var data = Encoding.UTF8.GetBytes(json);
			var webRequest = (HttpWebRequest) WebRequest.Create(endpointsUrl[iterator]);
			webRequest.Method = "POST";
			webRequest.ContentType = "application/reports+json";
			webRequest.ContentLength = data.Length;
			webRequest.Timeout = 15000;
			webRequest.ReadWriteTimeout = 15000;

			webRequest.BeginGetRequestStream(r =>
			{
				try
				{
					using (var requestStream = webRequest.EndGetRequestStream(r))
						requestStream.Write(data, 0, data.Length);
					
					webRequest.BeginGetResponse(t =>
					{
						iterator++;
						try
						{
							using (var response = (HttpWebResponse) webRequest.EndGetResponse(t))
							{
								if ((int)response.StatusCode < 200 || (int)response.StatusCode > 299)
								{
									SendReport(endpointsUrl, report, iterator);
								}
							}
						}
						catch
						{
							SendReport(endpointsUrl, report, iterator);
						}
					}, null);
				}
				catch
				{
					// ignored
				}
			}, null);
		}
	}
}
