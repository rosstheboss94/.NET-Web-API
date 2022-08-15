using Api.Dtos;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        

        [HttpPost("register")]
        public async Task<IActionResult> Register(AppUserDto appUserDto)
        {
            var user = await _userRepository.CreateUserAsync(appUserDto);

            if(user != null)
            {
                await _userRepository.CompleteAsync();
                return Ok(user);
            } 

            return BadRequest("Username already exists");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userRepository.LoginUserAsync(loginDto);

            if(user != null) return Ok(user);

            return BadRequest("User doesn't Exists");
        }
    }
}