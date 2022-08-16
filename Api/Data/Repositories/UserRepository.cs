using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AppUser> CreateUserAsync(AppUserDto appUserDto)
        {
            var selectedUser = await _dbContext.Users.AnyAsync(user => user.UserName == appUserDto.UserName 
                || user.Email == appUserDto.Email);

            if(!selectedUser)
            {
                var user = new AppUser
                {
                    UserName = appUserDto.UserName,
                    Password = appUserDto.Password,
                    Email = appUserDto.Email
                };

                await _dbContext.Users.AddAsync(user);
                return user;
            }  
            return null;
        }

        public async Task<AppUser> LoginUserAsync(LoginDto loginDto)
        {
            var selectedUser = await _dbContext.Users
                .Where(user => user.UserName == loginDto.UserName)
                .Where(user => user.Password == loginDto.Password)
                .FirstOrDefaultAsync();
            
            if(selectedUser != null) return selectedUser;

            return null;
        }

        public async Task<bool> CompleteAsync()
        { 
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
