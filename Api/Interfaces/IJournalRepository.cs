using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IJournalRepository
    {
       Task<Journal> GetJournalByNameAsync(string username, string journalName);
       Task<bool> AddAsync(string username, JournalDto journalDto);
       Task<Journal> UpdateAsync(string username, string previousName, JournalDto journalDto);
       Task<bool> DeleteAsync(int id);
       Task<IEnumerable<Journal>> GetJournalsAsync(string username);
    }
}