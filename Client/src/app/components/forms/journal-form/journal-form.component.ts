import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JournalDto } from 'src/app/models/journal';
import { User } from 'src/app/models/user';
import { JournalService } from 'src/app/services/journal.service';
import { UserService } from 'src/app/services/user.service';
import { environment } from 'src/environments/environment';
import { take } from 'rxjs';

@Component({
  selector: 'app-journal-form',
  templateUrl: './journal-form.component.html',
  styleUrls: ['./journal-form.component.scss'],
})
export class JournalFormComponent implements OnInit {
  model: JournalDto = {
    name: '',
    description: '',
  };

  currentUser: User;

  baseUrl = environment.apiUrl;

  constructor(
    private journalService: JournalService,
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  create() {
    this.userService.currentUser$
      .pipe(take(1))
      .subscribe((user) => (this.currentUser = user));

    this.journalService.add(this.model).subscribe(() => {
      this.router.navigateByUrl(`/${this.currentUser}/journals`);
    });
  }
}
