using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel.AccountStatment
{
    public class StatmentParams
    {
        public StatmentParams()
        {
            StartDate = DateTimeOffset.Now.ToString("dd/MM/yyyy");
            int year = DateTime.Now.Year;
            EndDate = new DateTime(year, 12, 31).ToString("dd/MM/yyyy");
        }

        public string AccNum { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public decimal StartBalance { get; set; }

        public decimal EndBalance { get; set; }


    }
}
