import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Music } from '../_models/Music';
import { Post } from '../_models/Post';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrlaccount = 'http://localhost:5000/api/account/';
  baseUrlpost = 'http://localhost:5000/api/post/';
constructor(private http: HttpClient) { }

  music() {
    return this.http.get<Music[]>(this.baseUrlaccount + 'music_types');
  }

  savepreference(userId: number, id: number) {
    return this.http.post(this.baseUrlaccount + userId + '/music_types/' + id, {});
  }

  getposts(userid: number) {
    return this.http.get<Post[]>(this.baseUrlpost + 'visit/' + userid);
  }
  getuserposts(userid: number) {
    return this.http.get<Post[]>(this.baseUrlpost + userid + '/posts');
  }

  sharepost(userId: number, model: any) {
    return this.http.post(this.baseUrlpost + userId, model);
  }

  like(userId: number, post: number) {
    return this.http.post(this.baseUrlpost + userId + '/like/' + post, {});
  }

  savevisitedprofile(userId: number, id: number) {
    return this.http.post(this.baseUrlpost + userId + '/visit/' + id, {});
  }
}
