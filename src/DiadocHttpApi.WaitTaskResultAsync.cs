using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public async Task<TResult> WaitTaskResultAsync<TResult>(
			string authToken,
			string url,
			string taskId,
			TimeSpan? timeout = null,
			TimeSpan? delay = null)
		{
			var queryString = $"{url}?taskId={taskId}";
			var stopwatch = Stopwatch.StartNew();
			timeout = timeout ?? WaitTaskDefaultTimeout;
			while (true)
			{
				var request = BuildHttpRequest(authToken, "GET", queryString, null);
				var response = await HttpClient.PerformHttpRequestAsync(request, HttpStatusCode.NoContent).ConfigureAwait(false);

				if (response.StatusCode == HttpStatusCode.NoContent)
				{
					if (stopwatch.Elapsed > timeout)
						throw new TimeoutException(
							$"Can't GET '{queryString}'. Timeout {stopwatch.Elapsed.TotalSeconds}s expired.");

#if NET40
					await TaskEx.Delay(delay ?? TimeSpan.FromSeconds(response.RetryAfter.HasValue ? Math.Min(response.RetryAfter.Value, DefaultDelayInSeconds) : DefaultDelayInSeconds));
#else
					await Task.Delay(delay ?? TimeSpan.FromSeconds(response.RetryAfter.HasValue ? Math.Min(response.RetryAfter.Value, DefaultDelayInSeconds) : DefaultDelayInSeconds));
#endif
					continue;
				}
				return DeserializeResponse(request, response, Deserialize<TResult>);
			}
		}
	}
}
