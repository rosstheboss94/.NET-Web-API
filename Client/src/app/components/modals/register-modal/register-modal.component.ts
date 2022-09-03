import { Component, OnInit } from '@angular/core';
import { Register } from 'src/app/models/register';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register-modal',
  templateUrl: './register-modal.component.html',
  styleUrls: ['./register-modal.component.scss']
})
export class RegisterModalComponent implements OnInit {
  model: Register = {
    username: "",
    password: "",
    email: ""
  };

  constructor(private userService: UserService) { }

  ngOnInit(): void {
  }

  register(){
    this.userService.register(this.model).subscribe();
  }

}
