import { Component, Input, OnInit } from '@angular/core';
import { Journal } from 'src/app/models/journal';

@Component({
  selector: 'app-journal-card',
  templateUrl: './journal-card.component.html',
  styleUrls: ['./journal-card.component.scss'],
})
export class JournalCardComponent implements OnInit {
  @Input() journal: Journal;

  constructor() {}

  ngOnInit(): void {}
}
