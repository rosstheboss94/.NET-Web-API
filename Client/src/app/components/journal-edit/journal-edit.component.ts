import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Journal, JournalDto } from 'src/app/models/journal';
import { User } from 'src/app/models/user';
import { JournalService } from 'src/app/services/journal.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-journal-edit',
  templateUrl: './journal-edit.component.html',
  styleUrls: ['./journal-edit.component.scss'],
})
export class JournalEditComponent implements OnInit {
  journal: Journal;

  model: JournalDto = {
    name: '',
    description: '',
  };
  currentUser: User;

  constructor(
    private journalService: JournalService,
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.currentUser = this.userService.getUser();
    this.getJournal();
  }

  getJournal() {
    this.journal = this.journalService.getJournal();
  }

  update() {
    this.journalService
      .update(this.journal, this.model)
      .subscribe(() =>
        this.router.navigateByUrl(`/${this.currentUser}/journals`)
      );
  }

  cancel() {
    this.router.navigateByUrl(`/${this.currentUser}/journals`);
  }
}
