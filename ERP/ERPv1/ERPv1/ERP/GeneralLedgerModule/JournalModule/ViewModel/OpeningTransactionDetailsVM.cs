using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel
{
    public class OpeningTransactionDetailsVM//تفاصيل القيد الافتتاحي body
    {
        public string AccNum { get; set; }
        public string AccountName { get; set; }
        public JournalSideEnum Side { get; set; }//مدين او دائن 
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string CurrencyAbbr { get; set; }
        public decimal UsedRate { get; set; }
        public int CurrencyId { get; set; }

    }
}
