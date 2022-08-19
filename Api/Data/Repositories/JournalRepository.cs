using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class JournalRepository : IJournalRepository
    {
        private readonly AppDbContext _context;
        public JournalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddJournalAsync(string username, JournalDto journalDto)
        {
            
            var user = await _context.Users
                .Include(user => user.Journals)
                .Where(user => user.UserName == username)
                .FirstOrDefaultAsync();

            if(user != null)
            {
                user.Journals.Add(
                    new Journal 
                    {
                        Name = journalDto.Name,
                        Description = journalDto.Description,
                        DateCreated = DateTime.Now
                    }
                );
                
                return await _context.SaveChangesAsync() > 0; 
            }

            return false;
        }

        public async Task<Journal> GetJournalByAsync(AppUser user, string journalName)
        {
            return await _context.Journals
                .Where(journal => journal.AppUserId == user.Id && journal.Name == journalName)
                .FirstOrDefaultAsync();
        }
    }
}