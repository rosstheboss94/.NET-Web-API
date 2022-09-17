using Api.Controllers;
using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Api.Test.ControllerTests;

public class UserControllerTest
{
    private readonly Mock<ITokenService> _TokenServiceMock;
    private readonly Mock<IHttpContextAccessor>? _IHttpContextAccessorMock = new();
    private readonly Mock<IUserClaimsPrincipalFactory<AppUser>>? _IUserClaimsPrincipalFactory = new();
    private readonly Mock<IOptions<IdentityOptions>>? _IOptionsMock = new();
    private readonly Mock<ILogger<SignInManager<AppUser>>>? _ILoggerMock = new();
    private readonly Mock<IAuthenticationSchemeProvider>? _IAuthenticationSchemeProviderMock = new();
    private readonly Mock<IUserConfirmation<AppUser>>? _IUserConfirmationMock = new();
    private readonly Mock<UserManager<AppUser>> _userManagerMock;
    private readonly Mock<SignInManager<AppUser>> _signInManagerMock;
    private readonly UserController _controller;
  

    public UserControllerTest()
    {
        _TokenServiceMock = new Mock<ITokenService>();
        _userManagerMock = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
        _signInManagerMock = new Mock<SignInManager<AppUser>>
            (
            _userManagerMock.Object, 
            _IHttpContextAccessorMock.Object, 
            _IUserClaimsPrincipalFactory.Object, 
            _IOptionsMock.Object, 
            _ILoggerMock.Object, 
            _IAuthenticationSchemeProviderMock.Object, 
            _IUserConfirmationMock.Object
            );

        _controller = new UserController(_userManagerMock.Object, _TokenServiceMock.Object, _signInManagerMock.Object);
    }

    [Theory]
    [InlineData("success",typeof(OkObjectResult))]
    [InlineData("failed", typeof(BadRequestObjectResult))]
    public async void Register_Is_User_Valid_Return_Ok_Or_Badrequest(string setIdentityResult, Type expectedResult)
    {
        //Arrange
        IdentityResult identityResult = IdentityResult.Success;
        if (setIdentityResult == "failed")
        {
            identityResult = IdentityResult.Failed();
            _userManagerMock.Setup(q => q.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new AppUser());
        }

        _TokenServiceMock.Setup(q => q.CreateToken(It.IsAny<AppUserDto>()))
            .Returns(It.IsAny<string>());

        _userManagerMock.Setup(q => q.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
            .ReturnsAsync(identityResult);

        var appUserDto = new AppUserDto { UserName = "user", Password = "Pa$$w0rd" };

        //Act
        var result = await _controller.Register(appUserDto);
       
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(expectedResult);

    }

    [Theory]
    [InlineData("no user failure",typeof(BadRequestObjectResult))]
    [InlineData("success", typeof(OkObjectResult))]
    [InlineData("unauthorized failure", typeof(UnauthorizedObjectResult))]
    public async void Login_Is_User_Valid_Return_Ok_Or_Badrequest(string failureOrSuccess, Type expectedResult)
    {
        //Arrange
        var signInResult = Microsoft.AspNetCore.Identity.SignInResult.Failed;
        if (failureOrSuccess == "success")
        {
            _userManagerMock.Setup(q => q.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new AppUser());
            signInResult = Microsoft.AspNetCore.Identity.SignInResult.Success;
        }

        if(failureOrSuccess == "unauthorized failure")
        {
            _userManagerMock.Setup(q => q.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new AppUser());
        }
        
        _signInManagerMock.Setup(q => q.CheckPasswordSignInAsync(It.IsAny<AppUser>(), It.IsAny<string>(), false))
            .ReturnsAsync(signInResult);

        var cpMock = new Mock<ClaimsPrincipal>();
        cpMock.Setup(q => q.AddIdentity(It.IsAny<ClaimsIdentity>()));
        cpMock.Setup(q => q.Identity.IsAuthenticated).Returns(true);

        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        _controller.ControllerContext.HttpContext.User = cpMock.Object;

        _TokenServiceMock.Setup(q => q.CreateToken(It.IsAny<AppUserDto>()))
            .Returns(It.IsAny<string>());

        _userManagerMock.Setup(q => q.UpdateAsync(It.IsAny<AppUser>()));

        var loginDto = new LoginDto { UserName = "user", Password = "Pa$$w0rd" };
      
        //Act
        var result = await _controller.Login(loginDto);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(expectedResult);

    }
}
