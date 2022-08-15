namespace Api.Entities
{
    public enum TradeType
    {
        Forex, Stock, Crypto
    }

    public class Trade
    {
        public int Id { get; set; }
        public TradeType Type { get; set; }
        public string Result { get; set; }
        public string Ticker { get; set; }
        public double Entry { get; set; }
        public double TakeProfit { get; set; }
        public double StopLoss { get; set; }
        public string RiskReward { get; set; }
        public string Notes { get; set; }
    } 
}