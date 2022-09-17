using Api.Controllers;
using Api.Data;
using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Api.Test.ControllerTests;

public class UserControllerTest
{
    private Mock<ITokenService> _TokenServiceMock;
    private Mock<IHttpContextAccessor> _IHttpContextAccessorMock;
    private Mock<IUserClaimsPrincipalFactory<AppUser>> _IUserClaimsPrincipalFactory;
    private Mock<IOptions<IdentityOptions>> _IOptionsMock;
    private Mock<ILogger<SignInManager<AppUser>>> _ILoggerMock;
    private Mock<IAuthenticationSchemeProvider> _IAuthenticationSchemeProviderMock;
    private Mock<IUserConfirmation<AppUser>> _IUserConfirmationMock;
    private Mock<UserManager<AppUser>> _UserManagerMock;
    private Mock<SignInManager<AppUser>> _SignInManagerMock;
    private UserController _controller;
  

    public UserControllerTest()
    {
        _TokenServiceMock = new Mock<ITokenService>();
        _UserManagerMock = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
        SetUpSignInManagerMock();
        _controller = new UserController(_UserManagerMock.Object, _TokenServiceMock.Object, _SignInManagerMock.Object);

        //_UserManager = new UserManager<AppUser>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
    }

    [Fact]
    public void Register_Is_User_Valid_Return_User_Or_Badrequest()
    {
        //Arrange     
        _TokenServiceMock.Setup(q => q.CreateToken(It.IsAny<AppUserDto>())).Returns(It.IsAny<string>());
        _UserManagerMock.Setup(q => q.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

        //Act
        _UserManagerMock.Object.Users.Any(u => u.UserName == "user");
        var appUserDto = new AppUserDto { UserName = "user", Password = "Pa$$w0rd" };
        var result = _controller.Register(appUserDto);
       
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

    }

    private async Task<AppDbContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var databaseContext = new AppDbContext(options);

        databaseContext.Database.EnsureCreated();

        if(!_UserManagerMock.Object.Users.Any())

        if(await databaseContext.Users.CountAsync() <= 0)
        {
            
            databaseContext.Users.Add(new AppUser { UserName = "user", Email = "user@trader.com"});
            await databaseContext.SaveChangesAsync();
        }

        return databaseContext;
    }
    private void SetUpSignInManagerMock()
    {
       
        _IHttpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _IUserClaimsPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<AppUser>>();
        _IOptionsMock = new Mock<IOptions<IdentityOptions>>();
        _ILoggerMock = new Mock<ILogger<SignInManager<AppUser>>>();
        _IAuthenticationSchemeProviderMock = new Mock<IAuthenticationSchemeProvider>();
        _IUserConfirmationMock = new Mock<IUserConfirmation<AppUser>>();
        _SignInManagerMock = new Mock<SignInManager<AppUser>>(_UserManagerMock.Object, _IHttpContextAccessorMock.Object, _IUserClaimsPrincipalFactory.Object, _IOptionsMock.Object, _ILoggerMock.Object, _IAuthenticationSchemeProviderMock.Object, _IUserConfirmationMock.Object);

    }
}
