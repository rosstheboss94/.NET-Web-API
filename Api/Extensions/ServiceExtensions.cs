using Api.Data.Repositories;
using Api.Interfaces;
using Api.Services;

namespace Api.Extensions;

public static class ServiceExtensions
{
    public static void AddServiceExtensions(this IServiceCollection services)
    {
        services.AddScoped<IJournalRepository, JournalRepository>();
        services.AddScoped<ITradeRepository, TradeRepository>();
        services.AddScoped<ITokenService, TokenService>();
    }
}