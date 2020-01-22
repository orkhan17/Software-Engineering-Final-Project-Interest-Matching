import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { Uuser } from '../_models/Uuser';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

  model: any = {};
  user: Uuser;
  constructor(private authService: AuthService, private alertify: AlertifyService, private router: Router ) { }
  ngOnInit() {
  }

  registiration() {
    this.authService.update(this.authService.decodedToken.nameid, this.model).subscribe(next => {
      this.alertify.success('Updated Successfully');
    }, error => {
      this.alertify.error(error);
    });
  }

}
