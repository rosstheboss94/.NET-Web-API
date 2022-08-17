using Api.Dtos;
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
            if(await _journalRepository.AddJournalAsync(username , journalDto))
            {
                return Ok();
            }

            return BadRequest("Something went wrong");
        }
    }
}