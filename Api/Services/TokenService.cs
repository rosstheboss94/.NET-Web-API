using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;         
        }

        public string CreateToken(AppUserDto appUserDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
		    var tokenKey = Encoding.UTF8.GetBytes(_config.GetSection("JWT_Key").Value);

		    var tokenDescriptor = new SecurityTokenDescriptor
		    {
		        Subject = new ClaimsIdentity(new Claim[]
		        {
			        new Claim(ClaimTypes.Name, appUserDto.UserName),
                    new Claim(ClaimTypes.Email, appUserDto.Email)                    
		        }),
		        Expires = DateTime.Now.AddDays(1),
		        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
		    };

		    var token = tokenHandler.CreateToken(tokenDescriptor);

		    return tokenHandler.WriteToken(token);
        }
    }
}