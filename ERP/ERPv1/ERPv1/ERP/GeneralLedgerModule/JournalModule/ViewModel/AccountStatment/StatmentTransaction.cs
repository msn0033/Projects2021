using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel.AccountStatment
{
    public class StatmentTransaction
    {
        public string JornalId { get; set; }
        public string TransDate { get; set; }
        public string Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
       
        public decimal BalanceAfter { get; set; }

    }
}
