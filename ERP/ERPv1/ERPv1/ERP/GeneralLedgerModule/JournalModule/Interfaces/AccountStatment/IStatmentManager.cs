using ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel.AccountStatment;
using System;
using System.Collections.Generic;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModule.Interfaces.AccountStatment
{
    public interface IStatmentManager
    {
        decimal GetStartBalance(StatmentParams STParm, DateTime Start);
        List<StatmentTransaction> GetTransactions(StatmentParams STParm, DateTime Start, DateTime End);
        void UpdateStatment(StatmentContainer vm);
    }
}