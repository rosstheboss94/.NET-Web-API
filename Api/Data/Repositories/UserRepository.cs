using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser> CreateUserAsync(AppUserDto appUserDto)
        {
            var selectedUser = await _context.Users.AnyAsync(user => user.UserName == appUserDto.UserName 
                || user.Email == appUserDto.Email);

            if(!selectedUser)
            {
                var user = new AppUser
                {
                    UserName = appUserDto.UserName,
                    Password = appUserDto.Password,
                    Email = appUserDto.Email
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
