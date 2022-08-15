using Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class TradeController : ApiController
    {
        private readonly AppDbContext _dbContext;
        public TradeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrades()
        {
            var trades = await _dbContext.Trades.ToListAsync();
            
            if(trades == null) return BadRequest("Could not get trades");
            return Ok(trades);
        }
    }
}