import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  currentUser: User;
  journal: Journal;
  toDelete: boolean = false;

  constructor(private journalService: JournalService, private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.currentUser = this.userService.getUser();
    this.getJournals();
  }

  getJournals(){
    this.journalService.getAll().subscribe(
      (journals) => {
        this.journals = [...journals]
      }
    );
  }

  enter(journal: Journal) {
    this.journalService.setJournal(journal);
    this.router.navigateByUrl(`/${this.currentUser.userName}/journals/${journal.name}/trades`);
  }

  edit(journal: Journal) {
    this.journalService.setJournal(journal);
    this.router.navigateByUrl(`/${this.currentUser.userName}/journals/${journal.name}/edit`);
  }

  delete(journal: Journal) {
    this.journalService.delete(journal).subscribe(() => {
      this.getJournals();
    });
  }

  displayAlert(journal: Journal) {
    journal.toDelete = !journal.toDelete;
    this.toDelete = !this.toDelete;
  }
}
