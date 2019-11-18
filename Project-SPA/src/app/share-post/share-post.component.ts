import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Post } from '../_models/Post';

@Component({
  selector: 'app-share-post',
  templateUrl: './share-post.component.html',
  styleUrls: ['./share-post.component.css']
})
export class SharePostComponent implements OnInit {
  model: any = {};
  post: Post[];
  safeUrl: any;
  safeSrc: SafeResourceUrl;
  constructor(private accountService: AccountService, private alertify: AlertifyService, private router: Router,
              private authService: AuthService, public sanitizer: DomSanitizer) { }

  ngOnInit() {
  }
  sharepost() {
    this.accountService.sharepost(this.authService.decodedToken.nameid, this.model).subscribe(next => {
      this.alertify.success('Shared');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.router.navigate(['/myposts']);
    });
  }

  searchkeyword() {
    const word = ((document.getElementById('word') as HTMLInputElement).value);
    this.accountService.getsearch(word).subscribe((result: Post[]) => {
      this.post = result;
    }, error => {
      this.alertify.error(error);
    });
  }

  searchplaylist() {
    const word = ((document.getElementById('word') as HTMLInputElement).value);
    this.accountService.getplaylist(word).subscribe((result: Post[]) => {
      this.post = result;
    }, error => {
      this.alertify.error(error);
    });
  }

}
