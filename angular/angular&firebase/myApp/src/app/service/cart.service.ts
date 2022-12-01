import { Injectable } from '@angular/core';
import { AngularFirestore } from '@angular/fire/firestore';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(private afstore: AngularFirestore, private auth: AuthService) { }

  addToCart(data) {
    console.log(data)
    return this.afstore.collection(`users/${this.auth.UserId}/cart`).add(data)
  }
  getCartAll() {
    return this.afstore.collection(`users/${this.auth.UserId}/cart`).snapshotChanges();
  }
  delete(id: number) {
    return this.afstore.doc(`users/${this.auth.UserId}/cart/${id}`).delete()
  }
  updatePut(id: number, amount: number) {
    return this.afstore.doc(`users/${this.auth.UserId}/cart/${id}`).update({ amount })
  }
}
