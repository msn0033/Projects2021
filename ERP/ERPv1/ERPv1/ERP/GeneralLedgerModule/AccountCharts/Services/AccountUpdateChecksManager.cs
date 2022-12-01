using ERPv1.Data;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Services
{
    public class AccountUpdateChecksManager : IAccountUpdateChecksManager
    {
        private ApplicationDbContext _db;

        public AccountUpdateChecksManager(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool ValidateCurrency(string AccNum, int CurrencyId)
        {
            var acc = _db.AccountChart.Find(AccNum);
            return CurrencyId != acc.CurrencyId && acc.Balance > 0 ? false : true;
        }
        public bool ValidateBranch(string AccNum, int BranchId)
        {
            var acc = _db.AccountChart.Find(AccNum);
            return BranchId != acc.BranchId && acc.Balance > 0?false:true;   
        }

        public IEnumerable<string> ValidateAccountData(UpdateAccountVM vm)
        {
            var error = new List<string>();
            var acc = _db.AccountChart.Find(vm.AccNum);
            if (vm.CurrencyId != acc.CurrencyId && acc.Balance > 0)
                error.Add("الرصيد يجب ان يكون صفر");
            if (vm.BranchId != acc.BranchId && acc.Balance > 0)
                error.Add("الرصيد يجب ان يكون صفر");
            return error;
        }
    }
}
