import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Journal } from 'src/app/models/journal';
import { JournalService } from 'src/app/services/journal.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-journal-card',
  templateUrl: './journal-card.component.html',
  styleUrls: ['./journal-card.component.scss'],
})
export class JournalCardComponent implements OnInit {
  @Input() journal: Journal;
  @Output() onDelete = new EventEmitter<boolean>();
  toDelete: boolean = false;

  constructor(private journalService: JournalService, private userService: UserService, private router: Router) {}

  ngOnInit(): void {}

  edit(journal: Journal){
    let currentUser = this.userService.getUser();
    this.journalService.setJournal(journal);
    this.router.navigateByUrl(`/${currentUser}/journals/${journal.name}/edit`);
  }

  delete(journal: Journal){
    this.journalService.delete(journal).subscribe(() => {
      this.onDelete.emit(true);
    });
  }

  displayAlert(){ 
    this.toDelete = !this.toDelete;
  }
}
