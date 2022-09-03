using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var key = Encoding.UTF8.GetBytes(config.GetSection("JWT_Token").Value);
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