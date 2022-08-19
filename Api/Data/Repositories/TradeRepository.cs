using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly AppDbContext _context;
        public TradeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Trade> AddTradeAsync(string username, string journalName, TradeDto tradeDto)
        {
            // var user = await _context.Users
            //     .Include(user => user.Journals)
            //     .Where(user => user.UserName == username)
            //     .FirstOrDefaultAsync();
            var journal = await _context.Journals
            .Include(j => j.Trades)
            .Where(j => j.AppUser.UserName == username && j.Name == journalName)
            .FirstOrDefaultAsync();
            
            //var journal = user.Journals.FirstOrDefault(journal => journal.Name == journalName);

            if(journal != null)
            {
                var trade = new Trade
                    {
                        Type = tradeDto.Type,
                        Result = tradeDto.Result,
                        Ticker = tradeDto.Ticker,
                        Entry = tradeDto.Entry,
                        TakeProfit = tradeDto.TakeProfit,
                        StopLoss = tradeDto.StopLoss,
                        RiskReward = tradeDto.RiskReward,
                        Notes = tradeDto.Notes,
                        JournalId = journal.Id
                    };
                    
                    
                journal.Trades.Add(trade);
                await _context.SaveChangesAsync();

                return trade;
            }

            return null;
        }
    }
}