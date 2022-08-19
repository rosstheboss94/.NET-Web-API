using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IJournalRepository
    {
       Task<Journal> GetJournalByAsync(AppUser user, string journalName);
       Task<bool> AddJournalAsync(string username, JournalDto journalDto);
    }
}