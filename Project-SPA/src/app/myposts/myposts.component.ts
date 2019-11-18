import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { Post } from '../_models/Post';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-myposts',
  templateUrl: './myposts.component.html',
  styleUrls: ['./myposts.component.css']
})
export class MypostsComponent implements OnInit {
  post: Post[];
  safeUrl: any;
  safeSrc: SafeResourceUrl;
  constructor(private accountService: AccountService, private alertify: AlertifyService,
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

like(id) {
  this.accountService.like(this.authService.decodedToken.nameid, id).subscribe(next => {
    this.alertify.success('Liked successfully');
  }, error => {
    this.alertify.error(error);
  });
}




}
