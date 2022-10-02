namespace Api.Models;

public class Trade
{
    public int Id { get; set; }
    public string Type { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string Result { get; set; }
    public string Ticker { get; set; }
    public double Entry { get; set; }
    public double TakeProfit { get; set; }
    public double StopLoss { get; set; }
    public string RiskReward { get; set; }
    public string Notes { get; set; }
    public int JournalId { get; set; }
    public Journal Journal { get; set; }
} 