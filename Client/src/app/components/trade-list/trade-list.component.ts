import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Journal } from 'src/app/models/journal';
import { User } from 'src/app/models/user';
import { JournalService } from 'src/app/services/journal.service';
import { UserService } from 'src/app/services/user.service';
import { Trade } from '../../models/trade';
import { TradeService } from '../../services/trade.service';

@Component({
  selector: 'app-trade-list',
  templateUrl: './trade-list.component.html',
  styleUrls: ['./trade-list.component.scss'],
})
export class TradeListComponent implements OnInit {
  trades: Trade[] = [];
  currentUser: User;
  selectedJournal: Journal;
  toDelete: boolean = false;

  constructor(
    private tradeService: TradeService,
    private userService: UserService,
    private journalService: JournalService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.currentUser = this.userService.getUser();
    this.selectedJournal = this.journalService.getJournal();
    this.getTrades();
  }

  getTrades() {
    this.tradeService.getAllTrades().subscribe((trades) => {
      this.trades = trades;
    });
  }

  add() {
    this.router.navigateByUrl(`/${this.currentUser.userName}/journals/${this.selectedJournal.name}/trades/add`);
  }

  edit(trade: Trade) {
    this.tradeService.setTrade(trade);
    this.router.navigateByUrl(`/${this.currentUser.userName}/journals/${this.selectedJournal.name}/trades/edit`);
  }

  delete(trade: Trade) {
    this.tradeService.delete(trade).subscribe(() => {
      this.getTrades();
    });
  }

  displayAlert(trade: Trade){
    trade.toDelete = !trade.toDelete; 
    this.toDelete = !this.toDelete;
  }
}
