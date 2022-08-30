using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class JournalController : ApiController
    {
        private readonly IJournalRepository _journalRepository;
        public JournalController(IJournalRepository journalRepository)
        {
            _journalRepository = journalRepository;
        }

        [HttpPost("{username}/journals/add")]
        public async Task<IActionResult> AddJournal(string username, JournalDto journalDto)
        {
            if(await _journalRepository.AddJournalAsync(username , journalDto)) return Ok();

            return BadRequest("Something went wrong");
        }

        [HttpGet("{username}/journals/{journalName}")]
        public async Task<ActionResult<Journal>> GetJournal(string username, string journalName)
        {
            var journal = await _journalRepository.GetJournalByNameAsync(username, journalName);

            if(journal != null) return Ok(journal);

            return BadRequest("Could not find your journal");
        }

        [HttpPut("{username}/journals/{journalName}")]
        public async Task<ActionResult<Journal>> UpdateJournal(string username, string journalName, JournalDto journalDto)
        {
            var journal = await _journalRepository.UpdateJournalAsync(username, journalName, journalDto);

            if(journal != null) return Ok(journal);

            return BadRequest("Could not update your journal");
        }

        [HttpDelete("{username}/journals/{journalName}")]
        public async Task<IActionResult> DeleteJournal(string username, string journalName)
        {
            var journal = await _journalRepository.DeleteJournalAsync(username, journalName);

            if(journal) return Ok();

            return BadRequest("Could not delete your journal");
        }
    }
}