
using ERPv1.Data;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Services
{
    public class AccountUpdateEdit : IAccountUpdateEdit
    {
        private ApplicationDbContext _db;


        public AccountUpdateEdit(ApplicationDbContext db)
        {
            _db = db;

        }
        public void SaveUpdateAccount(UpdateAccountVM vm)
        {
            var acc = _db.AccountChart.Find(vm.AccNum);
            //acc.AccNum = vm.AccNum;
            acc.AccountName = vm.AccountName;
            acc.AccountNameAr = vm.AccountNameAr;
            acc.BranchId = vm.BranchId;
            acc.CurrencyId = vm.CurrencyId;
            acc.Balance = vm.Balance;
            acc.IsActive = vm.IsActive;
            _db.AccountChart.Update(acc);
            _db.SaveChanges();
        }
    }
}
