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

        public async Task<Trade> AddAsync(int journalId, TradeDto tradeDto)
        {
            var journal = await _context.Journals
            .Include(j => j.Trades)
            .Where(j => j.Id == journalId)
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

        public async Task<bool> DeleteAsync(int tradeId)
        {
            var trade = await _context.Trades
            .Where(t => t.Id == tradeId)
            .FirstOrDefaultAsync();

            if(trade != null)
            {
                _context.Entry(trade).State = EntityState.Deleted;
                return await _context.SaveChangesAsync() > 0;
            }

            return false; 
        }

        public async Task<IEnumerable<Trade>> GetAllAsync(int journalId)
        {
            return await _context.Trades
                .Where(t => t.JournalId == journalId)
                .OrderByDescending(t => t.DateCreated)
                .ToListAsync();
        }

        public async Task<Trade> UpdateAsync(int tradeId, TradeDto tradeDto)
        {
            // var journal = await _context.Journals
            // .Include(j => j.Trades)
            // .Where(j => j.AppUser.UserName == username && j.Name == journalName)
            // .FirstOrDefaultAsync();
            var trade = await _context.Trades
                .Where(t => t.Id == tradeId)
                .FirstOrDefaultAsync();
            // if(journal != null)
            // {
            //     var trade = journal.Trades.FirstOrDefault(t => t.Id == id);
                if(trade != null)
                {
                    trade.Type = tradeDto.Type;
                    trade.Result = tradeDto.Result;
                    trade.Ticker = tradeDto.Ticker;
                    trade.Entry = tradeDto.Entry;
                    trade.TakeProfit = tradeDto.TakeProfit;
                    trade.StopLoss = tradeDto.StopLoss;
                    trade.RiskReward = tradeDto.RiskReward;
                    trade.Notes = tradeDto.Notes;

                    await _context.SaveChangesAsync();
                    return trade;
                }
            //}
            
            return null;
        }
    }
}