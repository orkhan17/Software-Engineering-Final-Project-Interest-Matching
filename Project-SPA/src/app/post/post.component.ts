import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { Post } from '../_models/Post';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Search } from '../_models/search';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  post: Post[];
  safeUrl: any;
  safeSrc: SafeResourceUrl;
  constructor(private accountService: AccountService, private alertify: AlertifyService,
              private authService: AuthService, public sanitizer: DomSanitizer) { }

  ngOnInit() {
     this.getposts();
  }

  getposts() {
    this.accountService.getposts(this.authService.decodedToken.nameid).subscribe((result: Post[]) => {
      this.post = result;
    }, error => {
      this.alertify.error(error);
    });
}

  savevisit(id) {
    this.accountService.savevisitedprofile(this.authService.decodedToken.nameid, id).subscribe(next => {
      this.alertify.success('Added successfully');
    }, error => {
      this.alertify.error(error);
    });
  }

  like(id, accountid) {
    this.accountService.like(this.authService.decodedToken.nameid, id).subscribe(next => {
      this.alertify.success('Liked successfully');
      this.savevisit(accountid);
    }, error => {
      this.alertify.error(error);
    });
  }

 /* searchkeyword(word) {
    this.accountService.getsearch(word).subscribe((result: Post[]) => {
      this.search = result;
    }, error => {
      this.alertify.error(error);
    });
  } */


}
