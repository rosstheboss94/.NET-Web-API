using Microsoft.AspNetCore.Authorization;

namespace Api.Extensions;

public static class AuthorizationPolicyExtensions
{
    public static void AddAuthorizationPolicyExtensions(this IServiceCollection services)
    {
        services.AddAuthorization(options => 
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        });
    }
}