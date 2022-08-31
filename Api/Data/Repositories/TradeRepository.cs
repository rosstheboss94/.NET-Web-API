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
            var journal = await _context.Journals
            .Include(j => j.Trades)
            .Where(j => j.AppUser.UserName == username && j.Name == journalName)
            .FirstOrDefaultAsync();

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

        public async Task<bool> DeleteTradeAsync(string username, string journalName, int id)
        {
            var trade = await _context.Trades
            .Where(t => t.Journal.AppUser.UserName == username && t.Id == id)
            .FirstOrDefaultAsync();

            if(trade != null)
            {
                _context.Entry(trade).State = EntityState.Deleted;
                return await _context.SaveChangesAsync() > 0;
            }

            return false; 
        }

        public async Task<IEnumerable<Trade>> GetAllTradesAsync(string username, string journalName)
        {
            return await _context.Trades
                .Where(t => t.Journal.AppUser.UserName == username && t.Journal.Name == journalName)
                .ToListAsync();
        }

        public async Task<Trade> UpdateTradeAsync(string username, string journalName, int id, TradeDto tradeDto)
        {
            var journal = await _context.Journals
            .Include(j => j.Trades)
            .Where(j => j.AppUser.UserName == username && j.Name == journalName)
            .FirstOrDefaultAsync();

            if(journal != null)
            {
                var trade = journal.Trades.FirstOrDefault(t => t.Id == id);
                if(trade != null)
                {
                    trade.Type = tradeDto.Type;
                    trade.Result = tradeDto.Result;
                    trade.Ticker = tradeDto.Ticker;
                    trade.Entry = tradeDto.Entry;
                    trade.TakeProfit = tradeDto.TakeProfit;
                    trade.RiskReward = tradeDto.RiskReward;
                    trade.Notes = tradeDto.Notes;

                    await _context.SaveChangesAsync();
                    return trade;
                }
            }
            
            return null;
        }
    }
}