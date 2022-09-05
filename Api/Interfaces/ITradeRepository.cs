using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ITradeRepository
    {
        Task<Trade> AddAsync(int journalId, TradeDto tradeDto);
        Task<Trade> UpdateAsync(int tradeId, TradeDto tradeDto);
        Task<IEnumerable<Trade>> GetAllAsync(int journalId);
        Task<bool> DeleteAsync(int tradeId);
    }
}