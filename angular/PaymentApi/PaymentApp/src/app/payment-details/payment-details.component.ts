import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Friend } from '../shared/friend.model';

import { PaymentDetailService } from '../shared/payment-detail.service';


@Component({
  selector: 'app-payment-details',
  templateUrl: './payment-details.component.html',
  styles: [
  ]
})
export class PaymentDetailsComponent implements OnInit {


  constructor(public service: PaymentDetailService, private taster: ToastrService) { }

  ngOnInit() {
    this.service.refreshlist()

  }

  populateForm(selectedrow: any) {

    this.service.formData = Object.assign({}, selectedrow);
  }

  OnDelete(id: number) {

    this.service.deletepaymentDetails(id).subscribe(
      res => {
        this.service.refreshlist();
        this.taster.error('تم الحذف بنجاح', 'عملية حذف')
      },
      err => {
        console.log('delete errror');
      });

  }

  CheckAllOptions() {
    if (this.service.list.every(val => val.checked == true))
      this.service.list.forEach(val => val.checked = false);
    else
      this.service.list.forEach(val => val.checked = true);
  }

  OnDeleteAll() {
    for (let index = 0; index < this.service.list.length; index++) {
      if (this.service.list[index].checked == true) {
        this.service.deletepaymentDetails(this.service.list[index].paymentDetalisId).subscribe(
          res => {
            if (index == (this.service.list.length - 1)) {
              this.service.refreshlist();
              this.taster.error('تم الحذف بنجاح', 'عملية حذف')
            }
          },
          err => {
            console.log('delete errror');
          });
      }
      //this.OnDelete(this.service.list[index].paymentDetalisId);

    }//end loop
  }
}