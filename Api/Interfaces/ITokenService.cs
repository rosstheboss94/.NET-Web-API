using Api.Dtos;

namespace Api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUserDto appUserDto);
    }
}