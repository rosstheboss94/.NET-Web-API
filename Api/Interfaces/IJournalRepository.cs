using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces;

public interface IJournalRepository
{
   Task<bool> AddAsync(AppUser user, JournalDto journalDto);
   Task<Journal> UpdateAsync(AppUser user, string previousJournalName, JournalDto journalDto);
   Task<bool> DeleteAsync(int id);
   Task<IEnumerable<Journal>> GetAllAsync(AppUser user);
}
