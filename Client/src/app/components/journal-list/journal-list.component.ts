import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
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

  constructor(private journalService: JournalService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getJournals(this.initial);
  }

  getJournals(reload: boolean){
    this.journalService.getJournals().subscribe(
      (journals) => {
        this.journals = [...journals]
        //if(this.initial == false) this.toastr.error('Journal delete');
        //this.initial = false;
      }
    );
  }
}
