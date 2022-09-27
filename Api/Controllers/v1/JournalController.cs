using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class JournalController : ControllerBase
{
    private readonly IJournalRepository _journalRepository;
    private readonly UserManager<AppUser> _userManager;

    public JournalController(IJournalRepository journalRepository, UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _journalRepository = journalRepository;
    }

    [HttpPost("user/journals/add")]
    public async Task<IActionResult> AddJournal(JournalDto journalDto)
    {
        var user = await GetUser();

        if(await _journalRepository.AddAsync(user, journalDto)) return Ok();

        return BadRequest("Something went wrong");
    }

    [HttpGet("user/journals")]
    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> GetAllJournals()
    {
        var user = await GetUser();

        var journals = await _journalRepository.GetAllAsync(user);

        if(journals == null) return BadRequest("Could not find your journals");

        return Ok(journals);
    }

    [HttpPut("user/journals/{previousJournalName}")]
    public async Task<ActionResult<Journal>> UpdateJournal(string previousJournalName, JournalDto journalDto)
    {
        var user = await GetUser();

        var journal = await _journalRepository.UpdateAsync(user, previousJournalName, journalDto);

        if(journal == null) BadRequest("Could not update your journal");

        return Ok(journal);
    }

    [HttpDelete("user/journals/journal/delete/{id}")]
    public async Task<IActionResult> DeleteJournal(int id)
    {
        var journal = await _journalRepository.DeleteAsync(id);

        if(journal) return Ok();

        return BadRequest("Could not delete your journal");
    }
    
    private async Task<AppUser> GetUser()
    {
        return await _userManager.FindByNameAsync(User.GetUsername());
    }
}