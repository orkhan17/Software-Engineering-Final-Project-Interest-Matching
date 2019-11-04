import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  register = false;
  constructor() { }

  ngOnInit() {
  }

  registerMode() {
    this.register = ! this.register;
  }

  cancelRegisterMode(register: boolean) {
    this.register = register;
  }

}
