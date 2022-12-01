using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel
{
    public class JournalDetailsVM
    {
        public int JournalDetailsId { get; set; }
        [Display(Name ="الحساب")]
        public string AccNum { get; set; }

        //public string AccountName { get; set; }//لا احتاجة
        public JournalSideEnum Side { get; set; }//مدين او دائن  zero or one  0/1
      
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string CurrencyAbbr { get; set; }
        public decimal UsedRate { get; set; }
        public int CurrencyId { get; set; }
    }
}
