import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Friend } from './friend.model';



@Injectable({
  providedIn: 'root'
})
export class PaymentDetailService {

  readonly url = "https://localhost:44311";
  readonly api = "api/Payment";

  list: Friend[] = [];
  formData: Friend = new Friend();

  constructor(private http: HttpClient) { }

  refreshlist() {
    console.log(`${this.url}/${this.api}`)
    this.http.get(`${this.url}/${this.api}`)
      .toPromise()
      .then(res => {
        this.list = res as Friend[]
      })
  }
  postPaymentDetails() {

    return this.http.post(`${this.url}/${this.api}`, this.formData)
  }
  putPaymentDetails() {
    return this.http.put(`${this.url}/${this.api}/${this.formData.paymentDetalisId}`, this.formData)
  }
  deletepaymentDetails(id: number) {
    return this.http.delete(`${this.url}/${this.api}/${id}`)
  }
}
