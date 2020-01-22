import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Post } from '../_models/Post';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  register = false;
  post: Post[];
  safeUrl: any;
  safeSrc: SafeResourceUrl;
  constructor(private accountService: AccountService, private alertify: AlertifyService,
              private authService: AuthService, public sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.get5posts();
  }

  get5posts() {
    this.accountService.get5posts().subscribe((result: Post[]) => {
      this.post = result;
    }, error => {
      this.alertify.error(error);
    });

}

  registerMode() {
    this.register = ! this.register;
  }

  cancelRegisterMode(register: boolean) {
    this.register = register;
  }

}
