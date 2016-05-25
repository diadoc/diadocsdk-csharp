using System;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace Diadoc.Api
{
    public partial class DiadocHttpApi
    {
        private const int DefaultDelayInSeconds = 15;
        public static TimeSpan WaitTaskDefaultTimeout = TimeSpan.FromMinutes(5);

        public TResult WaitTaskResult<TResult>(string authToken, string url, string taskId, TimeSpan? timeout = null,
            TimeSpan? delay = null)
        {
            var queryString = string.Format("{0}?taskId={1}", url, taskId);
            var stopwatch = Stopwatch.StartNew();
            timeout = timeout ?? WaitTaskDefaultTimeout;
            while (true)
            {
                var request = BuildHttpRequest(authToken, "GET", queryString, null);
                var response = HttpClient.PerformHttpRequest(request, HttpStatusCode.NoContent);

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    if (stopwatch.Elapsed > timeout)
                        throw new TimeoutException(string.Format("Can't GET '{0}'. Timeout {1}s expired.", queryString,
                            stopwatch.Elapsed.TotalSeconds));
                    
                    Thread.Sleep(delay ?? TimeSpan.FromSeconds(response.RetryAfter.HasValue ? Math.Min(response.RetryAfter.Value, DefaultDelayInSeconds) : DefaultDelayInSeconds));
                    continue;
                }
                return DeserializeResponse(request, response, Deserialize<TResult>);
            }
        }
    }
}
