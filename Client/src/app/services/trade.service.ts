import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Trade, TradeDto } from '../models/trade';
import { JournalService } from './journal.service';

@Injectable({
  providedIn: 'root'
})

export class TradeService {
  baseUrl = environment.apiUrl;

  private tradeSubject = new ReplaySubject<Trade>(1);
  selectedTrade$ = this.tradeSubject.asObservable();

  constructor(private http: HttpClient, private journalService: JournalService) { }

  getAllTrades(){
    let journal = this.journalService.getJournal();
    return this.http.get<Trade[]>(`${this.baseUrl}/trade/user/journal/${journal.id}/trades`);
  }

  add(tradeDto: TradeDto){
    let journal = this.journalService.getJournal();
    return this.http.post<Trade>(`${this.baseUrl}/trade/user/journal/${journal.id}/trades/add`, tradeDto)
  }

  edit(tradeDto: TradeDto){
    let trade = this.getTrade();
    return this.http.put<Trade>(`${this.baseUrl}/trade/user/journal/trades/${trade.id}`, tradeDto)
  }

  delete(trade: Trade){
    return this.http.delete(`${this.baseUrl}/trade/user/journal/trades/${trade.id}/delete`);
  }

  setTrade(trade: Trade){
    this.tradeSubject.next(trade);
  }

  getTrade(){
    let selectedTrade: Trade;
    this.selectedTrade$.pipe(take(1)).subscribe(trade => selectedTrade = trade);
    return selectedTrade;
  }
}
