import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JournalDto } from 'src/app/models/journal';
import { JournalService } from 'src/app/services/journal.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-journal-form',
  templateUrl: './journal-form.component.html',
  styleUrls: ['./journal-form.component.scss']
})
export class JournalFormComponent implements OnInit {
  model: JournalDto = {
    name: "",
    description: ""
  };

  baseUrl = environment.apiUrl;

  constructor(private journalService: JournalService, private router: Router) { }

  ngOnInit(): void {
  }

  create(){
     this.journalService.add(this.model).subscribe(() => {
       this.router.navigateByUrl(`/user/journals`);
     });
  }

}
