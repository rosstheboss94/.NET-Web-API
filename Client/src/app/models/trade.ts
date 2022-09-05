export interface Trade {
    id: number;
    type: string;
    result: string;
    ticker: string;
    entry: number;
    takeProfit: number;
    stopLoss: number;
    riskReward: string;
    notes: string;
    toDelete?: boolean;
}

export interface TradeDto {
    type: string;
    result: string;
    ticker: string;
    entry: number;
    takeProfit: number;
    stopLoss: number;
    riskReward: string;
    notes: string;
}