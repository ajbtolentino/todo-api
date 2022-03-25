using Microsoft.IdentityModel.Tokens;

namespace ASPNetCoreMastersTodoList.Api.AppSettings
{
    public class JwtOptions
    {
        public SecurityKey SecurityKey { get; set; }
    }
}