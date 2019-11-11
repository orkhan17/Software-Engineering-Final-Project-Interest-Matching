import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { Music } from '../_models/Music';
import { AccountService } from '../_services/account.service';
import { AuthService } from '../_services/auth.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-preference',
  templateUrl: './preference.component.html',
  styleUrls: ['./preference.component.css']
})
export class PreferenceComponent implements OnInit {
  musics: Music[];
  music1: number ;
  music2: number ;
  music3: number ;
  constructor(private accountService: AccountService, private alertify: AlertifyService, private router: Router,
              private authService: AuthService) { }

  ngOnInit() {
    this.music();
    this.music1 = 1;
    this.music2 = 1;
    this.music3 = 1;
  }

  music() {
      this.accountService.music().subscribe((result: Music[]) => {
        this.musics = result;
      }, error => {
        this.alertify.error(error);
      });
  }

  save() {
    this.accountService.savepreference(this.authService.decodedToken.nameid, this.music1).subscribe(next => {
      this.alertify.success('Added successfully');
    }, error => {
      this.alertify.error(error);
    });

    this.accountService.savepreference(this.authService.decodedToken.nameid, this.music2).subscribe(next => {
      this.alertify.success('Added successfully');
    }, error => {
      this.alertify.error(error);
    });

    this.accountService.savepreference(this.authService.decodedToken.nameid, this.music3).subscribe(next => {
      this.alertify.success('Added successfully');
    }, error => {
      this.alertify.error(error);
    });
  }
  onChange1($event) {
    this.music1 = $event.target.options[$event.target.options.selectedIndex].value;
    console.log(this.music1);
    }
  onChange2($event) {
    this.music2 = $event.target.options[$event.target.options.selectedIndex].value;
    console.log(this.music2);
    }
  onChange3($event) {
    this.music3 = $event.target.options[$event.target.options.selectedIndex].value;
    console.log(this.music3);
    }
}
