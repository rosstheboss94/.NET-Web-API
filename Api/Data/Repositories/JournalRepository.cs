using Api.Dtos;
using Api.Entities;
using Api.Extensions;
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

        public async Task<bool> AddAsync(string username, JournalDto journalDto)
        {
            var user = await _context.Users
                .Include(user => user.Journals)
                .Where(user => user.UserName == username)
                .FirstOrDefaultAsync();

            if(user != null)
            {
                if(user.Journals.Any(j => j.Name == journalDto.Name)) return false;

                user.Journals.Add(
                    new Journal 
                    {
                        Name = journalDto.Name,
                        Description = journalDto.Description,
                        AppUserId = user.Id
                    }
                );
                
                return await _context.SaveChangesAsync() > 0; 
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var journal = await _context.Journals
                .Where(journal => journal.Id == id)
                .FirstOrDefaultAsync();
            
            if(journal != null)
            {
                _context.Entry(journal).State = EntityState.Deleted;
                return await _context.SaveChangesAsync() > 0;
            }
            
            return false;
        }

        public async Task<Journal> GetJournalByNameAsync(string username, string journalName)
        {
            var user = await _context.Users
                .Include(user => user.Journals)
                .Where(user => user.UserName == username)
                .FirstOrDefaultAsync();
            
            if(user != null)
            {
                var journal = user.Journals.FirstOrDefault(journal => journal.Name == journalName);
                if(journal != null) return journal;
            }

            return null;            
        }

        public async Task<IEnumerable<Journal>> GetJournalsAsync(string username)
        {
            var journal = await _context.Journals
                .Where(j => j.AppUser.UserName == username)
                .ToListAsync();
            
            return journal;
        }

        public async Task<Journal> UpdateAsync(string username, string previousName,  JournalDto journalDto)
        {
            var user = await _context.Users
                .Include(user => user.Journals)
                .Where(user => user.UserName == username)
                .FirstOrDefaultAsync();
            
            if(user != null)
            {
                var journal = user.Journals.FirstOrDefault(journal => journal.Name == previousName);

                if(journal != null)
                {
                    journal.Name = journalDto.Name;
                    journal.Description = journalDto.Description;

                    await _context.SaveChangesAsync();
                    
                    return journal;
                }
            }

            return null;
        }
    }
}