export class Friend {
  constructor(
    public paymentDetalisId: number = 0,
    public cardOwnerName: any = '',
    public cardNumber: string = '',
    public securityCode: string = '',
    public expirationDate: string = '',
    public checked:boolean=false
  ) { }
}



/*

```

        **Why is the data not showing? in table**

The data returns from the api correctly as it is displayed in the console
Why does it not appear in the table
Please Help


service

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Friend } from './friend.model';


@Injectable({
providedIn: 'root'
})
export class PaymentDetailService {

readonly url = "https://localhost:44311";
readonly api = "api/Pay";

list: Friend[] = [];
formData: Friend = new Friend();

constructor(private http: HttpClient) { }


postPaymentDetails() {

return this.http.post(`${this.url}/${this.api}`, this.formData)
}
refreshlist() {
this.http.get<Friend[]>(`${this.url}/${this.api}`)
  .toPromise()
  .then(res => {

    this.list = res
    console.log(this.list); //ok print array

  })
}
}




components.ts

import { AfterViewInit, Component, OnInit } from '@angular/core';
import { PaymentDetailService } from '../shared/payment-detail.service';

@Component({
selector: 'app-payment-details',
templateUrl: './payment-details.component.html',
styles: [
]
})
export class PaymentDetailsComponent implements OnInit {

constructor(public service: PaymentDetailService) { }

ngOnInit() {
this.service.refreshlist()
}
}


template:

   <tbody>
        <tr *ngFor="let pd of service.list ; index as i">
           <td>{{i}}</td>
           <td (click)="populateForm(pd)">{{pd.CardOnerName |json}}</td>
           <td (click)="populateForm(pd)">{{pd.CardNumber}}</td>
           <td (click)="populateForm(pd)">{{pd.ExpirationDate}}</td>
           <td><i class="far fa-trash-alt fa-lg text-danger"></i></td>
        </tr>
 </tbody>

```
[img][1]

[1]: https://i.stack.imgur.com/6xKDI.png
*/