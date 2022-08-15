export interface Trade {
    id: number;
    type: number;
    result: string;
    ticker: string;
    entry: number;
    takeProfit: number;
    stopLoss: number;
    riskReward: string;
    notes: string;
}