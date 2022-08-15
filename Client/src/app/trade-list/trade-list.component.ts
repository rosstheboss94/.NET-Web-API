import { Component, OnInit } from '@angular/core';
import { Trade } from '../models/trade';
import { TradeService } from '../services/trade.service';

@Component({
  selector: 'app-trade-list',
  templateUrl: './trade-list.component.html',
  styleUrls: ['./trade-list.component.css'],
})
export class TradeListComponent implements OnInit {
  trades: Trade[] = [];

  constructor(private tradeService: TradeService) {}

  ngOnInit(): void {
    this.getTrades();
  }

  getTrades() {
    this.tradeService.getAllTrades().subscribe((trades) => {
      console.log(trades);
    });
  }
}
