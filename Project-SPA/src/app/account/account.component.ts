import { Component, OnInit } from '@angular/core';
import { Post } from '../_models/Post';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { AccountService } from '../_services/account.service';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  post: Post[];
  model: any = {};
  safeUrl: any;
  safeSrc: SafeResourceUrl;
  constructor(private accountService: AccountService, private alertify: AlertifyService, private router: Router,
              private authService: AuthService, public sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.getposts();
  }

  getposts() {
    this.accountService.getuserposts(this.authService.decodedToken.nameid).subscribe((result: Post[]) => {
      this.post = result;
    }, error => {
      this.alertify.error(error);
    });
}

delete(post) {
  this.accountService.delete_post(this.authService.decodedToken.nameid, post ).subscribe(next => {
    this.alertify.success('Deleted');
  }, error => {
    this.alertify.error(error);
  }, () => {
    this.router.navigate(['/myposts']);
  });
}

update(post) {
  this.accountService.update_post(this.authService.decodedToken.nameid, post, this.model ).subscribe(next => {
    this.alertify.success('Updated');
  }, error => {
    this.alertify.error(error);
  }, () => {
    this.router.navigate(['/myposts']);
  });
}
}
