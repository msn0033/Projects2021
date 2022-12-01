import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { Good } from 'src/app/Interface/Good.interface';
import { CartService } from 'src/app/service/cart.service';
import { GoodsService } from 'src/app/service/goods.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, OnDestroy {

  goods: Good[];
  goodsObservable: Subscription;
  num: number = -1;
 
  constructor(private good: GoodsService, private cartSer: CartService) { }

  ngOnInit(): void {
    this.goodsObservable = this.good.getAllGoods().subscribe(data => {
      this.goods = data.map(element => {
        return {
          id: element.payload.doc.id,
          name: element.payload.doc.data()['name'],
          price: element.payload.doc.data()['price'],
          photoUrl: element.payload.doc.data()['photoUrl']
        }
      })
    })
  }


  ShowAmount(index: number) {
    this.num = +index;
  }

  addToCart(amount: number) {
    let data: Good = this.goods[this.num];
    let shopping = {
      id: data.id,
      name: data.name,
      price: data.price,
      amount: +amount
    }
    this.cartSer.addToCart(shopping).then(re => { console.log(re); this.num = -1; })
  }

  ngOnDestroy(): void {
    this.goodsObservable.unsubscribe();
  }

}
