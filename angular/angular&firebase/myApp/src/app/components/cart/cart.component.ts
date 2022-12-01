import { analyzeAndValidateNgModules } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Shopping } from 'src/app/Interface/shopping';
import { CartService } from 'src/app/service/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  carts: Shopping[] = [];
  sum: number = 0;
  constructor(private cartSer: CartService) { }

  ngOnInit(): void {
    this.cartSer.getCartAll().subscribe(result => {  
      this.carts = result.map(res => {
        return {
          id: res.payload.doc.id,
          name: res.payload.doc.data()['name'],
          price: res.payload.doc.data()['price'],
          amount: res.payload.doc.data()['amount'],
        }//end return
      });//end map
    })///end subscribe
   

  }

  delete(id: number) {
    this.cartSer.delete(id);
  }
  update(id: number, amount: number) {

    this.cartSer.updatePut(id, amount);

  }

  total() {
    this.sum = 0;
    for (let index = 0; index < this.carts.length; index++) {
      this.sum += (this.carts[index].price * this.carts[index].amount)
    }

  }



}
