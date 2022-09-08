import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Journal } from 'src/app/models/journal';
import { TradeDto } from 'src/app/models/trade';
import { User } from 'src/app/models/user';
import { JournalService } from 'src/app/services/journal.service';
import { TradeService } from 'src/app/services/trade.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-edit-trade-form',
  templateUrl: './edit-trade-form.component.html',
  styleUrls: ['./edit-trade-form.component.scss'],
})
export class EditTradeFormComponent implements OnInit {
  model: TradeDto = {
    type: '',
    result: 'N/A',
    ticker: '',
    entry: 0,
    takeProfit: 0,
    stopLoss: 0,
    riskReward: '',
    notes: '',
  };

  currentUser: User;
  selectedJournal: Journal;

  constructor(
    private tradeService: TradeService,
    private userService: UserService,
    private journalService: JournalService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.currentUser = this.userService.getUser();
    this.selectedJournal = this.journalService.getJournal();
    this.setModel();
  }

  edit() {
    this.tradeService.edit(this.model).subscribe(() => {
      this.router.navigateByUrl(
        `${this.currentUser.userName}/journals/${this.selectedJournal.name}/trades`
      );
    });
  }

  setModel() {
    let trade = this.tradeService.getTrade();
    this.model = {
      type: trade.type,
      result: trade.result,
      ticker: trade.ticker,
      entry: trade.entry,
      takeProfit: trade.takeProfit,
      stopLoss: trade.stopLoss,
      riskReward: trade.riskReward,
      notes: trade.notes,
    };
  }

  cancel() {
    this.router.navigateByUrl(
      `${this.currentUser.userName}/journals/${this.selectedJournal.name}/trades`
    );
  }
}
