import { Component, OnInit } from '@angular/core';
import { Login } from '../models/login';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  model: Login = {
    username: '',
    password: '',
  };

  test: any = {};

  constructor(public userService: UserService) {}

  ngOnInit(): void {}

  login() {
    this.userService.login(this.model).subscribe();
  }
}
