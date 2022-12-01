import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { PaymentDetailService } from 'src/app/shared/payment-detail.service';

@Component({
  selector: 'app-payment-detail-form',
  templateUrl: './payment-detail-form.component.html',
  styles: [
  ]
})
export class PaymentDetailFormComponent implements OnInit {


  constructor(public service: PaymentDetailService, private toster: ToastrService) { }
  @ViewChild('paymentDetalisId') Id: ElementRef;
  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
    console.log(this.Id.nativeElement.value)
    if (this.Id.nativeElement.value == 0)
      this.insertRecord(form)
    else
      this.updateRecord(form)
  }
  insertRecord(form: NgForm) {
    this.service.postPaymentDetails().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshlist();
        console.log("عملية الاضافة تمت");
        this.toster.success('تم الاضافة بنجاح', 'عملية اضافة');
      },
      err => console.log(err))
  }
  updateRecord(form: NgForm) {
    this.service.putPaymentDetails().subscribe(
      res => {
        this.service.refreshlist();
        this.toster.info('تم علمية التحديث بنجاح', 'عملية تحديث البيانات')
      },
      err => {

      }
    );

    this.resetForm(form);
    this.Id.nativeElement.value = 0;
  }
  resetForm(form: NgForm) {
    form.form.reset();
  }
}
