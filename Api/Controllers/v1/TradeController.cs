using Api.Dtos;
using Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class TradeController : ControllerBase
{
    private readonly ITradeRepository _tradeRepository;

    public TradeController(ITradeRepository tradeRepository)
    {
        _tradeRepository = tradeRepository;
    }

    [HttpGet("user/journal/{journalId}/trades")]
    [AllowAnonymous]
    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> GetAllTrades(int journalId)
    {
        var trades = await _tradeRepository.GetAllAsync(journalId);
        
        if(trades == null) return BadRequest("Could not get trades");

        return Ok(trades);
    }

    [HttpPost("user/journal/{journalId}/trades/add")]
    public async Task<IActionResult> AddTrade(int journalId, TradeDto tradeDto)
    {
        var trade = await _tradeRepository.AddAsync(journalId, tradeDto);
        
        if(trade == null) return BadRequest("Something went wrong could not add trade");

        return Ok(trade);
    }

    [HttpPut("user/journal/trades/{tradeId}")]
    public async Task<IActionResult> UpdateTrade(int tradeId, TradeDto tradeDto)
    {
        var trade = await _tradeRepository.UpdateAsync(tradeId, tradeDto);
        
        if(trade == null) return BadRequest("Something went wrong could not update trade");
        
        return Ok(trade);
    }

    [HttpDelete("user/journal/trades/{tradeId}/delete")]
    public async Task<IActionResult> DeleteTrade(int tradeId)
    {
        if(await _tradeRepository.DeleteAsync(tradeId)) return Ok();
        
        return BadRequest("Something went wrong could not delete trade");
    }
}