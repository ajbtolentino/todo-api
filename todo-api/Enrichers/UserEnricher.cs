using System;
using System.Security.Claims;
using Serilog.Core;
using Serilog.Events;

namespace ASPNetCoreMastersTodoList.Api.Enrichers
{
    public class UserEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserEnricher() : this(new HttpContextAccessor())
        {
        }

        public UserEnricher(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var user = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
            "UserId", user ?? "anonymous"));
        }
    }
}

