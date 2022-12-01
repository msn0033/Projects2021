using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel
{
    public class JournalVM:IValidatableObject
    {
        public JournalVM()
        {
            JournalDetailsVM = new List<JournalDetailsVM>();
            Messages = new List<string>();
        }
        [Required(ErrorMessage ="رجاء ادخال تاريخ القيد")]
        public string TransDate { get; set; }//تاريخ المعاملة
        [Required(ErrorMessage = "رجاء ادخال تفاصيل القيد")]
        public string TransDes { get; set; }//وصف العملية
        public SystemModulesEnum SystemModules { get; set; }
        public string UserName { get; set; }//اسم المستخدم الذي عمل القيد
        public string DocName { get; set; }//ملف -صورة
        public List<string> Messages { get; set; }
        public List<JournalDetailsVM> JournalDetailsVM { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
             var error = new List<ValidationResult>();
            var totalDebit = JournalDetailsVM.Sum(x => x.Debit * x.UsedRate);
            var totalCredit = JournalDetailsVM.Sum(x => x.Credit * x.UsedRate);
            if (totalDebit != totalCredit)
                error.Add(new ValidationResult("القيد غير متوازن"));
            if (JournalDetailsVM.Count(x=>x.AccNum.Length !=0)== 0)
                error.Add(new ValidationResult("رجاء اختيار حساب من القائمة"));
            DateTime TransactionDate;
            if(!DateTime.TryParse(TransDate,out TransactionDate))
            {
                error.Add(new ValidationResult("ادخل تاريخ صحيح"));
            }
            return error;
        }
    }
}
