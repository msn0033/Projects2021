import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Friend } from '../class/friend.model';

@Injectable({
  providedIn: 'root'
})
export class PlaceholderService {
  list: Friend[];
  constructor(public http: HttpClient) { }
  getdata() {
    this.http.get('https://jsonplaceholder.typicode.com/posts').toPromise().then(res => {
      this.list = res as Friend[];
      console.log('placeholder', this.list)
      console.log('', this.list[1].title)
    })

  }
}

