using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(AppDbContext context, ITokenService tokenService, UserManager<IdentityUser> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _context = context;
        }

        public async Task<AppUser> CreateUserAsync(AppUserDto appUserDto)
        {
            //var selectedUser = await _context.Users.AnyAsync(user => user.UserName == appUserDto.UserName); 
                //|| user.Email == appUserDto.Email);
            var exist = await _userManager.Users.AnyAsync(user => user.UserName == appUserDto.UserName);

            if(exist)
            {
                var token = _tokenService.CreateToken(appUserDto);
            
                var user = new AppUser
                {
                    UserName = appUserDto.UserName.ToLower(),
                    Email = appUserDto.Email,
                    Token = token
                };

                var result = await _userManager.CreateAsync(user, appUserDto.Password);

                if(!result.Succeeded) return null;

                //await _context.Users.AddAsync(user);
                return user;
            }  
            return null;
        }

        public async Task<AppUser> LoginUserAsync(LoginDto loginDto)
        {
            var selectedUser = await _context.Users
                .Where(user => user.UserName == loginDto.UserName)
                //.Where(user => user.Password == loginDto.Password)
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
