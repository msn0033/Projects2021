
using ERPv1.ERP.PurchasesModule.ViewModel.SupplierStatment;
using System;
using System.Collections.Generic;

namespace ERPv1.ERP.PurchasesModule.Interfaces.SupplierStatment
{
    public interface ISupplierReport
    {
        decimal GetStartBalance(StatmentParams STParm, DateTime Start);
        List<StatmentTransaction> GetTransactions(StatmentParams STParm, DateTime Start, DateTime End);
        void UpdateStatment(SupplierStatmentContainer vm);
    }
}