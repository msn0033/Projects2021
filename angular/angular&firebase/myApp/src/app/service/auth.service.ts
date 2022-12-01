import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { Observable } from 'rxjs';
import { User } from '../Interface/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  IsUser: Observable<any>;
  UserId:string='';
  constructor(private afauth: AngularFireAuth) {
    this.IsUser = afauth.user;    
  }

  signup(email, password) {
    return this.afauth.createUserWithEmailAndPassword(email, password)
  }
  SignIn(form) {
    let data: User = form.value;
    return this.afauth.signInWithEmailAndPassword(data.email, data.password);
  }
  SignOut() {
    return this.afauth.signOut();
  }
}
