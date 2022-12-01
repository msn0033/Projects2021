using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel.AccountStatment
{
    public class StatmentContainer
    {
        public StatmentContainer()
        {
            StatmentParams = new StatmentParams();
            StatmentTransaction = new List<StatmentTransaction>();
        }
        public StatmentParams StatmentParams { get; set; }
        public List<StatmentTransaction> StatmentTransaction { get; set; }
        public string ReportURL { get; set; }

    }
}
