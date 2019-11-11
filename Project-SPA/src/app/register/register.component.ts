import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  @Output() cancelRegister = new EventEmitter();
  constructor(private authService: AuthService, private alertify: AlertifyService, private router: Router ) { }
  ngOnInit() {
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  registiration() {
    this.authService.register(this.model).subscribe(next => {
      this.alertify.success('Registered');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.authService.login(this.model).subscribe(() => {
        this.router.navigate(['/preference']);
      });
    });
  }


}
