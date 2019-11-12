import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-share-post',
  templateUrl: './share-post.component.html',
  styleUrls: ['./share-post.component.css']
})
export class SharePostComponent implements OnInit {
  model: any = {};
  constructor(private accountService: AccountService, private alertify: AlertifyService, private router: Router,
              private authService: AuthService) { }

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
}
