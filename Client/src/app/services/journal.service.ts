import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Journal } from '../models/journal';

@Injectable({
  providedIn: 'root'
})
export class JournalService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getJournals(){
    return this.http.get<Journal[]>(`${this.baseUrl}/journal/User/journals`);
  }
}
