using ERP.PurchasesModule.ViewModel;
using ERPv1.ERP.PurchasesModule.Model;
using ERPv1.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.ViewModel.SupplierPayment
{
    public class SupplierPaymentContainer :IValidatableObject
    {
        public SupplierPaymentContainer()
        {
            PaymentDetails = new PaymentDetails();
            SupplierBalanceDetails = new List<SupplierBalanceDetails>();
            SelectedBalance = new SupplierBalanceDetails();
            SupplierData = new SupplierData();

            Messages = new List<string>();
            IsDetailAreaVisible = true;
            IsWaitingAreaVisible = false;
            IsMessageAreaVisible = false;

        }
        public PaymentDetails PaymentDetails { get; set; }//تفاصيل الدفع
        public List<SupplierBalanceDetails> SupplierBalanceDetails { get; set; }//(تفاصيل رصيد المورد -( العملة المحلي و الدولار او اي عملات اخرى 
        public SupplierBalanceDetails SelectedBalance { get; set; }//الدفع لاي رصيد تم تم اختيارة هل بالريال -الدولار-الجنية
        public SupplierData SupplierData { get; set; }//بيانات المورد
        
        public List<string> Messages { get; set; }// يتم كتابة الاخطاء فيها عبر جافا سكريبت

        public bool IsWaitingAreaVisible { get; set; }//Upload Data =>load
        public bool IsDetailAreaVisible { get; set; }// Normal View
        public bool IsMessageAreaVisible { get; set; } //Show In Case an Error

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            DateTime payDate;
            var IsValidDate = DateTime.TryParse(PaymentDetails.PaymentDate, out payDate);
            if (!IsValidDate)
            {
                errors.Add(new ValidationResult("تاريخ الدفع غير صحيح   "));
            }
            if(PaymentDetails.PaymentAmount<=0)
            {
                errors.Add(new ValidationResult("المبلغ المدفوع غير صحيح"));
            }
            if(PaymentDetails.PaymentAmount>SelectedBalance.Amount)
                errors.Add(new ValidationResult("لايمكنك دفع مبلغ اكبر من المديونية"));

            if (PaymentDetails.PaymentMethod == SupplierPaymentMethodEnum.Safe && string.IsNullOrEmpty(PaymentDetails.SafeAccNum))
                 errors.Add(new ValidationResult("رجاء اختيار رقم حساب الخزنة"));

            if(PaymentDetails.PaymentMethod==SupplierPaymentMethodEnum.Bank && string.IsNullOrEmpty(PaymentDetails.BankAccNum))
                errors.Add(new ValidationResult("رجاء اختيار رقم حساب البنك"));

            if(PaymentDetails.PaymentMethod==SupplierPaymentMethodEnum.check)
            {
                if(string.IsNullOrEmpty(PaymentDetails.BankAccNum))
                    errors.Add(new ValidationResult("رجاء اختيار رقم حساب البنك "));
                if(string.IsNullOrEmpty(PaymentDetails.CheckNum))
                    errors.Add(new ValidationResult("رجاء ادخال رقم الشيك"));
                if(string.IsNullOrEmpty(PaymentDetails.PaymentDueDate))
                    errors.Add(new ValidationResult("رجاء اضافة تاريخ الاستحقاق"));
               
                else
                {
                    DateTime dueTime;
                    var IsValidDueDate = DateTime.TryParse(PaymentDetails.PaymentDueDate, out dueTime);
                    if(!IsValidDate)
                        errors.Add(new ValidationResult("رجاء كتابة تاريخ استحقاق الشيك بشكل صحيح"));
                }

                if (string.IsNullOrEmpty(PaymentDetails.WritingDate))
                    errors.Add(new ValidationResult("رجاء اضافة تاريخ كتابة الشيك"));
                else
                {
                    DateTime WritingDate;
                    var IsValidWritingDate = DateTime.TryParse(PaymentDetails.WritingDate, out WritingDate);
                    if (!IsValidWritingDate)
                        errors.Add(new ValidationResult("رجاء كتابة تاريخ استحقاق الشيك بشكل صحيح"));
                }
            }

            return errors;
        } //Validation
    }
}
