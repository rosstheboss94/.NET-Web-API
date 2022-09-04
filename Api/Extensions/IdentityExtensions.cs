using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection Add(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var key = Encoding.UTF8.GetBytes(config.GetSection("JWT_Key").Value);
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
            		{
            			ValidateIssuer = false,
                        ValidateAudience = false,
            			ValidateIssuerSigningKey = true,
            			IssuerSigningKey = new SymmetricSecurityKey(key)
            		};
            });   

            return services;        
        }
    }
}