using Api.Data;
using Api.Data.Repositories;
using Api.Interfaces;
using Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class AppExtensions
{
    public static IServiceCollection Add(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options => 
        {
            options.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });

        services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        
        services.AddCors();

        services.AddScoped<IJournalRepository, JournalRepository>();
        services.AddScoped<ITradeRepository, TradeRepository>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
