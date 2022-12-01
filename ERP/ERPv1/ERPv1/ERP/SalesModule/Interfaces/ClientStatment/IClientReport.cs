using ERPv1.ERP.SalesModule.ViewModel.ClientStatment;
using System;
using System.Collections.Generic;

namespace ERPv1.ERP.SalesModule.Interfaces.ClientStatment
{
    public interface IClientReport
    {
        decimal GetStartBalance(StatmentParams STParm, DateTime Start);
        List<StatmentTransaction> GetTransactions(StatmentParams STParm, DateTime Start, DateTime End);
        void UpdateStatment(ClientStatmentContainer vm);
    }
}