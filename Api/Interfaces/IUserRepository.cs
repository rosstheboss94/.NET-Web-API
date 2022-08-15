using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> CreateUserAsync(AppUserDto appUserDto);
        Task<AppUser> LoginUserAsync(LoginDto loginDto);
        Task<bool> CompleteAsync();


    }
}
