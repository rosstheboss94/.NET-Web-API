using Api.Dtos;
using Api.Models;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories;

public class JournalRepository : IJournalRepository
{
    private readonly AppDbContext _context;

    public JournalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddAsync(AppUser appUser, JournalDto journalDto)
    {
        var user = await _context.Users
            .Include(user => user.Journals)
            .Where(user => user.UserName == appUser.UserName)
            .FirstOrDefaultAsync();
 
        
        if(user == null) return false;
        
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

    public async Task<bool> DeleteAsync(int id)
    {
        var journal = await _context.Journals
            .Where(journal => journal.Id == id)
            .FirstOrDefaultAsync();
        
        if(journal == null) return false;
        
        _context.Journals.Remove(journal);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Journal>> GetAllAsync(AppUser user)
    {
        var journals = await _context.Journals
            .Where(journal => journal.AppUser.Id == user.Id)
            .ToListAsync();
        
        return journals;
    }

    public async Task<Journal> UpdateAsync(AppUser user, string previousJournalName,  JournalDto journalDto)
    {
        var journal = await _context.Journals
            .Where(journal => journal.Name == previousJournalName
                && journal.AppUserId == user.Id)
            .FirstOrDefaultAsync();

        if(journal == null) return null;

        journal.Name = journalDto.Name;
        journal.Description = journalDto.Description;
        await _context.SaveChangesAsync();
        
        return journal;
    }
}