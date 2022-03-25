namespace ASPNetCoreMastersTodoList.Api.MIddleware
{
    public static class CustomerErrorMessageMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomErrorMessage(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomerErrorMessageMiddleware>();
        }
    }
}
