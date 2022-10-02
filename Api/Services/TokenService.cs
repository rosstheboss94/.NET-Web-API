using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Dtos;
using Api.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

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
	        //Expires = DateTime.Now.AddHours(8),
	        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
	    };

	    var token = tokenHandler.CreateToken(tokenDescriptor);
		
	    return tokenHandler.WriteToken(token);
    }
}
