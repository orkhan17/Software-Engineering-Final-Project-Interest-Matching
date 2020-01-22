import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-delete-profile',
  templateUrl: './delete-profile.component.html',
  styleUrls: ['./delete-profile.component.css']
})
export class DeleteProfileComponent implements OnInit {
  model: any = {};
  constructor(private accountService: AccountService, private alertify: AlertifyService, private router: Router,
              private authService: AuthService) { }

  ngOnInit() {
    this.model.Username = this.authService.decodedToken.unique_name;
  }

  delete() {
    this.model.username = this.authService.decodedToken.unique_name;
    this.authService.delete(this.model).subscribe(next => {
      this.alertify.success('Deleted');
      localStorage.removeItem('token');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.router.navigate(['/home']);
    });
  }

}
