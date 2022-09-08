import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Journal } from 'src/app/models/journal';
import { User } from 'src/app/models/user';
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
  currentUser: User;

  constructor(private journalService: JournalService, private userService: UserService, private router: Router) {}

  ngOnInit(): void {
    this.currentUser = this.userService.getUser();
  }

  enter(journal: Journal){
    this.journalService.setJournal(journal);
    this.router.navigateByUrl(`/${this.currentUser.userName}/journals/${this.journal.name}/trades`);
  }

  edit(journal: Journal){
    this.journalService.setJournal(journal);
    this.router.navigateByUrl(`/${this.currentUser.userName}/journals/${journal.name}/edit`);
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
