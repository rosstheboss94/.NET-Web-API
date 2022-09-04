import { Component, OnInit } from '@angular/core';
import { Journal } from 'src/app/models/journal';
import { JournalService } from 'src/app/services/journal.service';

@Component({
  selector: 'app-journal-list',
  templateUrl: './journal-list.component.html',
  styleUrls: ['./journal-list.component.scss']
})
export class JournalListComponent implements OnInit {
  journals: Journal[];
  initial = true;

  constructor(private journalService: JournalService) { }

  ngOnInit(): void {
    this.getJournals(this.initial);
  }

  getJournals(reload: boolean){
    this.journalService.getJournals().subscribe(
      (journals) => {
        this.journals = [...journals]
        this.initial = false;
      }
    );
  }
}
