import { Component, OnInit } from '@angular/core';
import { Trade } from 'src/app/models/trade';

@Component({
  selector: 'app-trade-card',
  templateUrl: './trade-card.component.html',
  styleUrls: ['./trade-card.component.scss']
})
export class TradeCardComponent implements OnInit {
  trade: Trade;
  
  constructor() { }

  ngOnInit(): void {
  }

}
