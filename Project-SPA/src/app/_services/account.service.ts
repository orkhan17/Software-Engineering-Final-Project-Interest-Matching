import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Music } from '../_models/Music';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'http://localhost:5000/api/account/';
constructor(private http: HttpClient) { }

  music() {
    return this.http.get<Music[]>(this.baseUrl + 'music_types');
  }

  savepreference(userId: number, id: number) {
    return this.http.post(this.baseUrl + userId + '/music_types/' + id, {});
  }
}
