using System.Text;
using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions;

public static class IdentityExtensions
{
    public static void AddIdentityExtensions(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT_Key").Value);

            options.SaveToken = true;
            
            options.TokenValidationParameters = new TokenValidationParameters
        	{
        		ValidateIssuer = false,
                ValidateAudience = false,
        		ValidateIssuerSigningKey = true,
        		IssuerSigningKey = new SymmetricSecurityKey(key)
        	};
        });

        builder.Services.AddIdentityCore<AppUser>(o =>
        {
            o.User.RequireUniqueEmail = true;
        })
        .AddSignInManager<SignInManager<AppUser>>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();       
    }
}
