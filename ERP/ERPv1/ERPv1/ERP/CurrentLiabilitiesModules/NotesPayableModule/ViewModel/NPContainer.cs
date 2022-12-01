using ERPv1.ERP.PurchasesModule.Model;
using ERPv1.ERP.PurchasesModule.ViewModel;
using ERPv1.ERP.PurchasesModule.ViewModel.SupplierPayment;
using ERPv1.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.ViewModel
{
    public class NPContainer: IValidatableObject
    {
        public NPContainer()
        {
            CheckUnderCollection = new List<NPDetails>();
            CheckCashCollection = new List<NPDetails>();
            PaymentDetails = new PaymentDetails();
            SelectedNote = new NPDetails();
            Messages = new List<string>();
            IsDetailAreaVisible = true;
            IsWaitingAreaVisible = false;
            IsMessageAreaVisible = false;
        }
        public List<NPDetails> CheckUnderCollection { get; set; }
        public List<NPDetails> CheckCashCollection { get; set; }
        public NPDetails SelectedNote { get; set; }
        public PaymentDetails PaymentDetails { get; set; }
        public List<string> Messages { get; set; }// يتم كتابة الاخطاء فيها عبر جافا سكريبت

        public bool IsWaitingAreaVisible { get; set; }//Upload Data =>load
        public bool IsDetailAreaVisible { get; set; }// Normal View
        public bool IsMessageAreaVisible { get; set; } //Show In Case an Error

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errorList = new List<ValidationResult>();
            DateTime PayDate;

            if (string.IsNullOrEmpty(PaymentDetails.PaymentDate))
                errorList.Add(new ValidationResult("ادخل تاريخ الدفع"));
            else
            {
               
                var IsValidPaymentDate = DateTime.TryParse(PaymentDetails.PaymentDate, out PayDate);
                if (!IsValidPaymentDate)
                    errorList.Add(new ValidationResult("تاريخ الدفع غير صحيح"));
            }
        
            if(PaymentDetails.PaymentAmount > SelectedNote.AmountLocal)
                errorList.Add(new ValidationResult("المبلغ المدفوع اكبر من المبلغ المستحق "));
            if (PaymentDetails.PaymentAmount <=0)
                errorList.Add(new ValidationResult("المبلغ المدفوع غير صحيح "));

            
            if(PaymentDetails.PaymentMethod == SupplierPaymentMethodEnum.Safe && string.IsNullOrEmpty(PaymentDetails.SafeAccNum))
                 errorList.Add(new ValidationResult("رجاء اختيار الخزنة "));
            if (PaymentDetails.PaymentMethod == SupplierPaymentMethodEnum.Bank && string.IsNullOrEmpty(PaymentDetails.BankAccNum))
                errorList.Add(new ValidationResult("رجاء اختيار البنك "));
            if (PaymentDetails.PaymentMethod == SupplierPaymentMethodEnum.check)
            {
                
                if (string.IsNullOrEmpty(PaymentDetails.BankAccNum))
                    errorList.Add(new ValidationResult("رجاء اختيار البنك "));
                if (string.IsNullOrEmpty(PaymentDetails.CheckNum))
                    errorList.Add(new ValidationResult("رجاء اختيار رقم الشيك "));

                if (string.IsNullOrEmpty(PaymentDetails.PaymentDueDate))
                    errorList.Add(new ValidationResult("رجاء اضافة تاريخ الاستحقاق"));
                else
                {
                    var IsValidPaymentDueDate = DateTime.TryParse(PaymentDetails.PaymentDueDate, out PayDate);
                    if (!IsValidPaymentDueDate)
                        errorList.Add(new ValidationResult("تاريخ استحقاق الشيك غير صحيح"));

                }

                if (string.IsNullOrEmpty(PaymentDetails.WritingDate))
                    errorList.Add(new ValidationResult("رجاء اضافة تاريخ كتابة الشيك"));
                else
                {
                    var IsValidWritingDate = DateTime.TryParse(PaymentDetails.WritingDate, out PayDate);
                    if (!IsValidWritingDate)
                        errorList.Add(new ValidationResult("تاريخ كتابة الشيك غير صحيح"));
                }
            }
            return errorList;
        }
    }
}
