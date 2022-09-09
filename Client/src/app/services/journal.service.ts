import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Journal, JournalDto } from '../models/journal';
import { ReplaySubject, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class JournalService {
  baseUrl = environment.apiUrl;

  private journalSubject = new ReplaySubject<Journal>(1);
  journal$ = this.journalSubject.asObservable();

  constructor(private http: HttpClient) { }

  getAll(){
    return this.http.get<Journal[]>(`${this.baseUrl}/journal/user/journals`);
  }

  getJournalByName(){}

  getById(){
    let journal = this.getJournal();
    return this.http.get<Journal>(`${this.baseUrl}/journal/user/journals/${journal.id}`);
  }

  add(model: JournalDto){
    return this.http.post(`${this.baseUrl}/journal/User/journals/add`, model);
  }

  update(journal: Journal, model: JournalDto){
    return this.http.put<Journal>(`${this.baseUrl}/journal/user/journals/${journal.name}`, model);
  }

  delete(journal: Journal){
    return this.http.delete<Journal>(`${this.baseUrl}/journal/user/journals/journal/delete/${journal.id}`);
  }

  setJournal(journal: Journal){
    this.journalSubject.next(journal);
  }

  getJournal(){
    let selectedJournal: Journal;
    this.journal$.pipe(take(1)).subscribe(journal => selectedJournal = journal);
    return selectedJournal;
  }

}
