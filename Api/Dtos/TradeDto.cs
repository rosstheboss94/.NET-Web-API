namespace Api.Dtos
{
    public class TradeDto
    {
        public string Type { get; set; }
        public string Result { get; set; }
        public string Ticker { get; set; }
        public double Entry { get; set; }
        public double TakeProfit { get; set; }
        public double StopLoss { get; set; }
        public string RiskReward { get; set; }
        public string Notes { get; set; }
    }
}