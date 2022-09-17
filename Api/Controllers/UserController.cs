using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

public class UserController : ApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<AppUser> _signInManager;

    public UserController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenService = tokenService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(AppUserDto appUserDto)
    {
        var userExist = await _userManager.FindByNameAsync(appUserDto.UserName);

        if(userExist != null) return BadRequest("Username taken");
        
        var token =  _tokenService.CreateToken(appUserDto);
        var user = new AppUser
        {
            UserName = appUserDto.UserName,
            Email = appUserDto.Email,
            Token = token
        };
        var result = await _userManager.CreateAsync(user, appUserDto.Password);
        
        if(!result.Succeeded) return BadRequest();
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {   
        var user = await _userManager.FindByNameAsync(loginDto.UserName);

        if(user == null) return BadRequest("Username doesn't exists");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if(!result.Succeeded) return Unauthorized("Password incorrect");
     
        if(!User.Identity.IsAuthenticated)
        {
            var token = _tokenService.CreateToken(new AppUserDto
            {
                UserName = user.UserName,
                Email = user.Email
            });

            user.Token = token;

            await _userManager.UpdateAsync(user);
        }

        return Ok(new AppUserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Token = user.Token
        });
    }
}