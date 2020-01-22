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
  get5posts() {
    return this.http.get<Post[]>(this.baseUrlpost + '5post');
  }
  getsearch(word: string) {
    return this.http.get<Post[]>(this.baseUrlpost + 'search/' + word);
  }
  getplaylist(link: string) {
    return this.http.get<Post[]>(this.baseUrlpost + 'playlist/' + link);
  }

  getuserposts(userid: number) {
    return this.http.get<Post[]>(this.baseUrlpost + userid + '/posts');
  }

  sharepost(userId: number, model: any) {
    return this.http.post(this.baseUrlpost + userId, model);
  }

  delete(userId: number, model: any) {
    return this.http.delete(this.baseUrlaccount + userId, model);
  }

  delete_post(userId: number, post: number) {
    return this.http.delete(this.baseUrlpost + userId + '/deletepost/' + post);
  }
  update_post(userId: number, post: number, model: any) {
    return this.http.put(this.baseUrlpost + userId + '/update/' + post, model);
  }

  like(userId: number, post: number) {
    return this.http.post(this.baseUrlpost + userId + '/like/' + post, {});
  }

  follow(userId: number, followingid: number) {
    return this.http.post(this.baseUrlpost + userId + '/follow/' + followingid, {});
  }
  deletepost(postid: number) {
    return this.http.put(this.baseUrlpost + 'delete/' + postid, {});
  }
  savevisitedprofile(userId: number, id: number) {
    return this.http.post(this.baseUrlpost + userId + '/visit/' + id, {});
  }
  getfollowersposts(userid: number) {
    return this.http.get<Post[]>(this.baseUrlpost + userid + '/following');
  }
}
