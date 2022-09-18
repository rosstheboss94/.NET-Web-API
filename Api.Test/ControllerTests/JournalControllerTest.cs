using Api.Controllers;
using Api.Data.Repositories;
using Api.Dtos;
using Api.Entities;
using Api.Test.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace Api.Test.ControllerTests;

public class JournalControllerTest
{
    private readonly Mock<JournalRepository> _journalRepoMock;
    private readonly Mock<UserManager<AppUser>> _userManagerMock;
    private readonly JournalController _controller;

    public JournalControllerTest()
    {
        var ctx = TestDb.GetDatabaseContext();
        _journalRepoMock = new Mock<JournalRepository>(ctx);
        _userManagerMock = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
        _controller = new JournalController(_journalRepoMock.Object, _userManagerMock.Object);
    }

    [Theory]
    [InlineData("success", typeof(OkResult))]
    [InlineData("failure", typeof(BadRequestObjectResult))]
    public async void AddJournal_Returns_Ok_Or_BadRequest(string failureOrSuccess, Type expectedResult)
    {
        //Arrange
        if (failureOrSuccess == "success")
        {
            _userManagerMock.Setup(q => q.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new AppUser
                {
                    Id = "1",
                    UserName = "user",
                    PasswordHash = "Pa$$w0rd",
                    Email = "user@trader.com"
                });
        }

        if (failureOrSuccess == "failure")
        {
            _userManagerMock.Setup(q => q.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new AppUser
                {
                    Id = "100",
                    UserName = "nouser",
                    PasswordHash = "nopassword",
                    Email = "unemail"
                });
        }


        var cpMock = new Mock<ClaimsPrincipal>();
        cpMock.Setup(q => q.AddIdentity(It.IsAny<ClaimsIdentity>()));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = cpMock.Object
            }
        };


        var journalDto = new JournalDto { Name = "Test Journal", Description = "Test Description" };

        //Act
        var result = await _controller.AddJournal(journalDto);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(expectedResult);
    }
}
