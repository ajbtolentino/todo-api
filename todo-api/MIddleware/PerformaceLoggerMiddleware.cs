using System.Diagnostics;

namespace ASPNetCoreMastersTodoList.Api.MIddleware
{
    public class PerformaceLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        readonly Stopwatch _stopwatch = new Stopwatch();
        
        public PerformaceLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.Keys.Contains("PerformanceMonitor"))
            {
                Stopwatch _stopwatch = new Stopwatch();
                _stopwatch.Start();

                context.Response.OnStarting(() =>
                {
                    _stopwatch.Stop();
                    if (!context.Response.Headers.Keys.Contains("Performance-Time"))
                        context.Response.Headers.Add("Performance-Time", _stopwatch.Elapsed.ToString());
                    return Task.CompletedTask;
                });
            }

            await _next.Invoke(context);
        }
    }
}
