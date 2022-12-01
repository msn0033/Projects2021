using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel
{
    public class OpeningTransactionVM:IValidatableObject // القيد الافتتاحي
    {
        public OpeningTransactionVM()
        {
            TransactionDetails = new List<OpeningTransactionDetailsVM>();
        }

        //Header OpeningTransaction
        public int CurrentFinancialPeriodId{ get; set; }// الفترة المالية الحالية
        [Required(ErrorMessage = "رجاء ادخال تاريخ القيد")]
        public string TransDate { get; set; }//تاريخ المعاملة
        [Required]
        public string TransDes { get; set; }//وصف العملية
        public SystemModulesEnum SystemModules { get; set; } = SystemModulesEnum.GL;
        public string UserName { get; set; }//اسم المستخدم الذي عمل القيد
        //body OpeningTransactionDetails
        public List<OpeningTransactionDetailsVM> TransactionDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var error = new List<ValidationResult>();
            var totalDebit = TransactionDetails.Sum(x => x.Debit * x.UsedRate);
            var totalCredit = TransactionDetails.Sum(x => x.Credit * x.UsedRate);
            if (totalDebit != totalCredit)
                error.Add(new ValidationResult("القيد غير متوازن"));
            return error;
        }
    }
}
