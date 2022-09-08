import { Component, OnInit } from '@angular/core';
import { Journal } from 'src/app/models/journal';
import { User } from 'src/app/models/user';
import { JournalService } from 'src/app/services/journal.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-journal-list',
  templateUrl: './journal-list.component.html',
  styleUrls: ['./journal-list.component.scss']
})
export class JournalListComponent implements OnInit {
  journals: Journal[];
  initial = true;
  currentUser: User;

  constructor(private journalService: JournalService, private userService: UserService) { }

  ngOnInit(): void {
    this.currentUser = this.userService.getUser();
    this.getJournals(this.initial);
  }

  getJournals(reload: boolean){
    this.journalService.getAll().subscribe(
      (journals) => {
        this.journals = [...journals]
      }
    );
  }
}
