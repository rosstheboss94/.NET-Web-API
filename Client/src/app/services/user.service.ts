import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, ReplaySubject, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Login } from '../models/login';
import { Register } from '../models/register';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  baseUrl = environment.apiUrl;

  private currentUserSubject = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {}

  login(model: Login) {
    return this.http.post<User>(`${this.baseUrl}/user/login`, model).pipe(
      map((user: User) => {
        if (user) this.setUser(user);
      })
    );
  }

  register(model: Register) {
    return this.http.post<User>(`${this.baseUrl}/user/register`, model).pipe(
      map((user: User) => {
        if (user) this.setUser(user);
      })
    );
  }

  signout(){
    localStorage.removeItem('JWT-Token');
    this.currentUserSubject.next(null);
  }

  setUser(user: User) {
    this.currentUserSubject.next(user);
    localStorage.setItem('JWT-Token', user.token);
  }

  getUser() {
    let currentUser: User;
    this.currentUser$.pipe(take(1)).subscribe((user) => (currentUser = user));
    return currentUser;
  }
}
