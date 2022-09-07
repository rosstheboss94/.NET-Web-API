import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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

  constructor(public userService: UserService, private router: Router) {}

  ngOnInit(): void {}

  login() {
    this.userService.login(this.model).subscribe(() => {
      this.router.navigateByUrl(`${this.userService.currentUser$}/journals`);
      this.linkActive = 'active';
    });
  }

  isActive(isActive: boolean){
    isActive ? this.linkActive = 'active' : this.linkActive = null;
  }

  signout(){
    this.userService.signout();
    this.router.navigateByUrl('/');
    this.linkActive = null;
    this.model.username = '';
    this.model.password = '';
  }
}
