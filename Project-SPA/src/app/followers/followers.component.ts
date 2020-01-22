import { Component, OnInit } from '@angular/core';
import { Post } from '../_models/Post';
import { SafeResourceUrl, DomSanitizer } from '@angular/platform-browser';
import { AccountService } from '../_services/account.service';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.css']
})
export class FollowersComponent implements OnInit {

  post: Post[];
  safeUrl: any;
  safeSrc: SafeResourceUrl;
  constructor(private accountService: AccountService, private alertify: AlertifyService,
              private authService: AuthService, public sanitizer: DomSanitizer) { }

  ngOnInit() {
     this.getposts();
     console.log(this.post);
  }

  getposts() {
    this.accountService.getfollowersposts(this.authService.decodedToken.nameid).subscribe((result: Post[]) => {
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
      this.getposts();
      this.savevisit(accountid);
    }, error => {
      this.alertify.error(error);
    });
  }

  follow(followingid) {
    this.accountService.follow(this.authService.decodedToken.nameid, followingid).subscribe(next => {
      this.alertify.success('Followed successfully');
      this.savevisit(followingid);
    }, error => {
      this.alertify.error(error);
    });
  }

}
