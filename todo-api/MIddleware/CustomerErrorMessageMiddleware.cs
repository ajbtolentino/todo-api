using ASPNetCoreMastersTodoList.Api.BindingModels;
using System.Net;
using System.Text.Json;

namespace ASPNetCoreMastersTodoList.Api.MIddleware
{
    public class CustomerErrorMessageMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomerErrorMessageMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public CustomerErrorMessageMiddleware(RequestDelegate next, ILogger<CustomerErrorMessageMiddleware> logger, IHostEnvironment env)
        {
            this._next = next;
            this._logger = logger;
            this._env = env;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (context.Request.Headers.Keys.Contains("FullErrorMessage"))
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var response = _env.IsDevelopment()
                        ? new ApiExceptionModel(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                        : new ApiExceptionModel(context.Response.StatusCode, ex.Message, "Internal Server Error");

                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    var json = JsonSerializer.Serialize(response, options);

                    await context.Response.WriteAsync(json);
                }
                else
                    throw ex;
            }
        }
    }
}
