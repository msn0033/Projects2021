import { Injectable } from '@angular/core';
import { AngularFirestore } from '@angular/fire/firestore';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private afstore: AngularFirestore) { }
  
  addNewUser(id, name, address) {
    return this.afstore.doc('users/' + id).set({
      name,
      address
    })

  }
}
