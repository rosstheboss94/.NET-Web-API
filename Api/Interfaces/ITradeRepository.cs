using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ITradeRepository
    {
        Task<Trade> AddTradeAsync(string username, string journalName, TradeDto tradeDto);
        Task<Trade> UpdateTradeAsync(string username, string journalName, int id, TradeDto tradeDto);
        Task<IEnumerable<Trade>> GetAllTradesAsync(string username, string journalName);
        Task<bool> DeleteTradeAsync(string username, string journalName, int id);
    }
}