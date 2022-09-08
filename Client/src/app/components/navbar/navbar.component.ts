import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { Login } from '../../models/login';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})

export class NavbarComponent implements OnInit {
  model: Login = {
    username: '',
    password: '',
  };

  linkActive: string | null = null;

  currentUser: User;

  constructor(public userService: UserService, private router: Router) {}

  ngOnInit(): void {
    this.isAuthenticated();
  }

  login() {
    this.userService.login(this.model).subscribe(() => {
      this.setCurrentUser();
      this.router.navigateByUrl(`${this.currentUser.userName}/journals`);
      this.linkActive = 'active';
    });
  }

  isActive(isActive: boolean) {
    isActive ? (this.linkActive = 'active') : (this.linkActive = null);
  }

  signout() {
    this.userService.signout();
    this.router.navigateByUrl('/');
    this.linkActive = null;
    this.model.username = '';
    this.model.password = '';
  }

  isAuthenticated() {
    let user = JSON.parse(localStorage.getItem('App-User'));
    if (user) {
      this.userService.setUser(user);
      this.setCurrentUser();
      this.router.navigateByUrl(`${this.currentUser.userName}/journals`);
      this.linkActive = 'active';
    }
  }

  setCurrentUser(){
    this.currentUser = this.userService.getUser();
  }
}
