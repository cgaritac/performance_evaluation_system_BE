using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace PACE.Extensions;

public static class AuthenticationExtension
{
    public static IServiceCollection AddMicrosoftEntraAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(jwtOptions =>
            {
                var identityOptions = new MicrosoftIdentityOptions();
                configuration.Bind("AzureAd", identityOptions);

                jwtOptions.TokenValidationParameters.ValidAudience = identityOptions.ClientId;
                jwtOptions.TokenValidationParameters.ValidIssuers = new[]
                {
                    $"{identityOptions.Instance}{identityOptions.TenantId}/v2.0"
                };
                jwtOptions.TokenValidationParameters.ValidateAudience = true;
                jwtOptions.TokenValidationParameters.ValidateIssuer = true;
                jwtOptions.TokenValidationParameters.ValidateLifetime = true;
                jwtOptions.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(5);
            },
            identityOptions =>
            {
                configuration.Bind("AzureAd", identityOptions);
            });

        return services;
    }
}
