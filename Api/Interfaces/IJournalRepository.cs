using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IJournalRepository
    {
       Task<IEnumerable<Journal>> GetJournalsAsync(string username);
       Task<bool> AddJournalAsync(string username, JournalDto journalDto);
    }
}