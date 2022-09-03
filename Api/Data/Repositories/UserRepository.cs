using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        public UserRepository(AppDbContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<AppUser> CreateUserAsync(AppUserDto appUserDto)
        {
            var selectedUser = await _context.Users.AnyAsync(user => user.UserName == appUserDto.UserName.ToLower() 
                || user.Email == appUserDto.Email);

            if(!selectedUser)
            {
                var token = _tokenService.CreateToken(appUserDto);

                var user = new AppUser
                {
                    UserName = appUserDto.UserName.ToLower(),
                    Password = appUserDto.Password,
                    Email = appUserDto.Email,
                    Token = token
                };

                await _context.Users.AddAsync(user);
                return user;
            }  
            return null;
        }

        public async Task<AppUser> LoginUserAsync(LoginDto loginDto)
        {
            var selectedUser = await _context.Users
                .Where(user => user.UserName == loginDto.UserName)
                .Where(user => user.Password == loginDto.Password)
                .FirstOrDefaultAsync();
            
            if(selectedUser != null) return selectedUser;

            return null;
        }

        public async Task<bool> CompleteAsync()
        { 
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
