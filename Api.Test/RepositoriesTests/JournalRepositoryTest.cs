using Api.Data;
using Api.Models;
using Api.Data.Repositories;
using Api.Dtos;
using FluentAssertions;
using Api.Test.Helpers;

namespace Api.Test.RepositoriesTests;

public class JournalRepositoryTest
{
    private readonly JournalRepository _journalRepository;
    private readonly AppUser _appUser;
    private readonly AppDbContext _context;

    public JournalRepositoryTest()
    {
        _context = TestDb.GetDatabaseContext();
        _journalRepository = new JournalRepository(_context);
        _appUser = new AppUser { Id = "1", UserName = "user", PasswordHash = "Pa$$w0rd", Email = "user@trader.com" };
    }

    [Fact]
    public async void AddAsync_ReturnsBool()
    {
        //Arrange
        var journalDto = new JournalDto { Name = "TestJournal2", Description = "TestDescription2" };

        //Act
        var result = await _journalRepository.AddAsync(_appUser, journalDto);

        //Assert
        result.Should().Be(true);  
    }

    [Fact]
    public async void UpdateAsync_Returns_Journal()
    {
        //Arrange
        var journalDto = new JournalDto { Name = "TestJournalUpdated", Description = "TestDescriptionUpdated" };
        var previousJournalName = "TestJournal";

        //Act
        var result = await _journalRepository.UpdateAsync(_appUser,previousJournalName, journalDto);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Journal>();
    }

    [Fact]
    public async void GetAllAsync_Returns_IEnumerable_Journal()
    {
        //Act
        var result = await _journalRepository.GetAllAsync(_appUser);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<Journal>>();
    }

    [Fact]
    public async void DeleteAsync_Returns_Bool()
    {
        //Arrange
        var journalId = 1;

        //Act
        var result = await _journalRepository.DeleteAsync(journalId);

        //Assert
        result.Should().BeTrue();
    }
}
