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

        [HttpGet("{username}/{journalName}/trades")]
        public async Task<IActionResult> GetAllTrades(string username, string journalName)
        {
            var trades = await _tradeRepository.GetAllTradesAsync(username, journalName);
            
            if(trades == null) return BadRequest("Could not get trades");
            return Ok(trades);
        }

        [HttpPost("{username}/{journalName}/trades")]
        public async Task<IActionResult> AddTrade(string username, string journalName, TradeDto tradeDto)
        {
            var trade = await _tradeRepository.AddTradeAsync(username, journalName, tradeDto);
            
            if(trade == null) return BadRequest("Something went wrong could not add trade");

            return Ok(trade);
        }

        [HttpPut("{username}/{journalName}/trades/{id}")]
        public async Task<IActionResult> UpdateTrade(string username, string journalName, int id, TradeDto tradeDto)
        {
            var trade = await _tradeRepository.UpdateTradeAsync(username, journalName, id, tradeDto);
            
            if(trade == null) return BadRequest("Something went wrong could not update trade");

            return Ok(trade);
        }

        [HttpDelete("{username}/{journalName}/trades/{id}")]
        public async Task<IActionResult> DeleteTrade(string username, string journalName, int id)
        {
            if(await _tradeRepository.DeleteTradeAsync(username, journalName, id)) return Ok("Trade deleted");
            
            return BadRequest("Something went wrong could not delete trade");
        }
    }
}