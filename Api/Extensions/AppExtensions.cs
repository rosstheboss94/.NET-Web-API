using Api.Data;
using Api.Data.Repositories;
using Api.Interfaces;
using Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions
{
    public static class AppExtensions
    {
        public static IServiceCollection Add(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options => 
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            //services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJournalRepository, JournalRepository>();
            services.AddScoped<ITradeRepository, TradeRepository>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}