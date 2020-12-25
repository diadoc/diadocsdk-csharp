using System;
using System.Net;

#if !NET35
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Diadoc.Api
{
    public static class WebRequestExtensions
    {
#if !NET35
        public static async Task<WebResponse> GetResponseAsync(this WebRequest request, CancellationToken ct = default)
        {
            using (ct.Register(request.Abort, useSynchronizationContext: false))
            {
                try
                {
                    var response = await request.GetResponseAsync();
                    return response;
                }
                catch (WebException ex)
                {
                    // WebException is thrown when request.Abort() is called,
                    // but there may be many other reasons,
                    // propagate the WebException to the caller correctly
                    if (ct.IsCancellationRequested)
                    {
                        // the WebException will be available as Exception.InnerException
                        throw new OperationCanceledException(ex.Message, ex, ct);
                    }

                    // cancellation hasn't been requested, rethrow the original WebException
                    throw;
                }
            }
        }
#endif
    }
}