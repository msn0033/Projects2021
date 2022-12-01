using ERPv1.ERP.PurchasesModule.ViewModel.SupplierPayment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.ViewModel.Expense
{
    public class ExpenseContainerVM : IValidatableObject
    {
        public ExpenseContainerVM()
        {

            ExpenseDetails = new ExpenseDetailsVM();
            PaymentDetails = new PaymentDetails();
            VAT = new VAT();
            
            Messages = new List<string>();
;        }
        public ExpenseDetailsVM ExpenseDetails { get; set; }
        public PaymentDetails PaymentDetails { get; set; }
       

        public bool IsWaitingAreaVisible { get; set; }//Upload Data =>load
        public bool IsDetailAreaVisible { get; set; }// Normal View
        public bool IsMessageAreaVisible { get; set; } //Show In Case an Error

        public List<string> Messages { get; set; }// يتم كتابة الاخطاء فيها عبر جافا سكريبت
        public VAT VAT { get; set; }
        public string SaveURL { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var valid = new List<ValidationResult>();
            if (PaymentDetails.PaymentMethod == Model.SupplierPaymentMethodEnum.Credit)
            {
                if (!ExpenseDetails.SupplierId.HasValue)
                    valid.Add(new ValidationResult("رجاء اختيار المورد"));
            }
            if (PaymentDetails.PaymentAmount < ExpenseDetails.Amount)
            {
                if (!ExpenseDetails.SupplierId.HasValue)
                    valid.Add(new ValidationResult("رجاء اختيار المورد"));
            }

            return valid;
        }
    }

    public class VAT
    {
        public bool IsVAT { get; set; }
        public decimal VATRate { get; set; }

    }
}
