using Api.Dtos;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    public class TradeController : ApiController
    {
        private readonly ITradeRepository _tradeRepository;
        public TradeController(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        // [HttpGet]
        // public async Task<IActionResult> GetAllTrades()
        // {
        //     var trades = await _dbContext.Trades.ToListAsync();
            
        //     if(trades == null) return BadRequest("Could not get trades");
        //     return Ok(trades);
        // }

        [HttpPost("{username}/journals/trades/add")]
        public async Task<IActionResult> AddTrade(string username, string journalName, [FromBody]TradeDto tradeDto)
        {
            journalName = "Test Journal";

            var trade = await _tradeRepository.AddTradeAsync(username, journalName, tradeDto);
            
            if(trade == null) return BadRequest("Something went wrong could not add trade");

            return Ok(trade);
        }
    }
}