using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions;

public static class ApiVersioningExtensions
{
    public static void AddApiVersioningExtensions(this IServiceCollection services)
    {
        services.AddApiVersioning(options => 
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1,0);
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    }
}