using Api.Dtos;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ITradeRepository
    {
        Task<Trade> AddTradeAsync(string username, string journalName, TradeDto tradeDto);
    }
}