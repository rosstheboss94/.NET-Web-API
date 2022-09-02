import { Component, OnInit } from '@angular/core';
import { Login } from '../../models/login';
import { User } from '../../models/user';
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

  constructor(public userService: UserService) {}

  ngOnInit(): void {}

  login() {
    this.userService.login(this.model).subscribe();
  }
}
