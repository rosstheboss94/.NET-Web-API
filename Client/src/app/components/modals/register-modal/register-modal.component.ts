import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Register } from 'src/app/models/register';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register-modal',
  templateUrl: './register-modal.component.html',
  styleUrls: ['./register-modal.component.scss'],
})
export class RegisterModalComponent implements OnInit {
  model: Register = {
    username: '',
    password: '',
    email: '',
  };

  currentUser: User;

  constructor(
    private userService: UserService,
    public bsModalRef: BsModalRef,
  ) {}

  ngOnInit(): void {}

  register() {
    this.userService.register(this.model).subscribe();
    this.currentUser = this.userService.getUser();
    this.bsModalRef.hide();
  }
}
