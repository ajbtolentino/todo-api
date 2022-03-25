namespace ASPNetCoreMastersTodoList.Api.MIddleware
{
    public static class PerformaceLoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UsePerformaceLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PerformaceLoggerMiddleware>();
        }
    }
}
