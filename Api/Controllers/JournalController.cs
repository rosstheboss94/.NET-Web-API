using System.Security.Claims;
using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    public class JournalController : ApiController
    {
        private readonly IJournalRepository _journalRepository;
        public JournalController(IJournalRepository journalRepository)
        {
            _journalRepository = journalRepository;
        }

        [HttpPost("{username}/journals/add")]
        public async Task<IActionResult> AddJournal(JournalDto journalDto)
        {
            var username = User.GetUsername();

            if(await _journalRepository.AddAsync(username, journalDto)) return Ok();

            return BadRequest("Something went wrong");
        }

        [HttpGet("{username}/journals/{journalName}")]
        public async Task<ActionResult<Journal>> GetJournal([FromRoute]string journalName)
        {
            var username = User.GetUsername();

            var journal = await _journalRepository.GetJournalByNameAsync(username, journalName);

            if(journal != null) return Ok(journal);

            return BadRequest("Could not find your journal");
        }

        [HttpGet("user/journals")]
        public async Task<ActionResult<IEnumerable<Journal>>> GetAllJournals()
        {
            var username = User.GetUsername();

            var journals = await _journalRepository.GetJournalsAsync(username);

            if(journals != null) return Ok(journals);

            return BadRequest("Could not find your journals");
        }

        [HttpPut("user/journals/{previousName}")]
        public async Task<ActionResult<Journal>> UpdateJournal(string previousName, JournalDto journalDto)
        {
            var username = User.GetUsername();

            var journal = await _journalRepository.UpdateAsync(username, previousName, journalDto);

            if(journal != null) return Ok(journal);

            return BadRequest("Could not update your journal");
        }

        [HttpDelete("user/journals/journal/delete/{id}")]
        public async Task<IActionResult> DeleteJournal(int id)
        {
            var journal = await _journalRepository.DeleteAsync(id);

            if(journal) return Ok();

            return BadRequest("Could not delete your journal");
        }
    }
}