using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using System.Collections.Generic;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces
{
    public interface IAccountListManager
    {
        IEnumerable<AccountListVM> GetAllAccount();
        UpdateAccountVM GetAccount(string AccNum);
    }
}