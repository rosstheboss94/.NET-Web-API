using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IJournalRepository
    {
       Task<Journal> GetJournalByNameAsync(string username, string journalName);
       Task<bool> AddJournalAsync(string username, JournalDto journalDto);
       Task<Journal> UpdateJournalAsync(string username, string journalName, JournalDto journalDto);
       Task<bool> DeleteJournalAsync(string username, string journalName);
    }
}