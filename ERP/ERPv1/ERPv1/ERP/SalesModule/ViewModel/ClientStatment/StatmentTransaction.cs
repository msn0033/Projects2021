using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.ViewModel.ClientStatment
{
    public class StatmentTransaction
    {
        public string TransDate { get; set; }
        public string JournalId { get; set; }
        public string Description { get; set; }
        public string InvoiceAcc { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }

        public decimal BalanceAfter { get; set; }

    }
}
