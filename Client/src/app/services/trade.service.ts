import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Trade } from '../models/trade';

@Injectable({
  providedIn: 'root'
})
export class TradeService {

  constructor(private http: HttpClient) { }

  // getAllTrades(){
  //   return this.http.get<Trade>(`http://localhost:5028/api/trade`);
  // }
}
