using ERPv1.Data;
using ERPv1.ERP.CurrentAssetModules.Inventory.Interfaces;
using ERPv1.ERP.CurrentAssetModules.Inventory.ViewModel;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Inventory.Services
{
    public class StoreItemAccountManager : IStoreItemAccountManager
    {
        private IAccountGenerator _accountGenerator;
        private readonly ApplicationDbContext _db;

        public StoreItemAccountManager(IAccountGenerator accountGenerator, ApplicationDbContext db)
        {
            _accountGenerator = accountGenerator;
            _db = db;
        }

        //انشاء حسابين واحد مخزون وواحد مشتريات بنفس الوقت
        public StoreItemAccountHelper GenerateStoreItemAccounts(StoreItemCreateVM vm)
        {
            var Accounts = new StoreItemAccountHelper();//ارجاع رقم الحسابين الذي انشاناهم في كلاس - الذي سوف يرجع
            //StoreAcc //انشاء حساب في المخزون
            var StoreAcc = new CreateAccountVM();
            StoreAcc.AccountName = "Store - " + vm.Name;
            StoreAcc.AccountNameAr = "مخزون  " + vm.NameAr;
            StoreAcc.AccTypeId = 8;//المخزن
            StoreAcc.CurrencyId = 1;//العملة الرئيسية
            StoreAcc.BranchId = 1;//الفرع الرئيسي
            Accounts.StoreAccNum = _accountGenerator.CreateNewAccount(StoreAcc);

            //PurchaseAcc// انشا حساب في المشتريات
            var PurchaseAcc = new CreateAccountVM();
            PurchaseAcc.AccountName = "Purchase - " + vm.Name;
            PurchaseAcc.AccountNameAr = "مشتريات  " + vm.NameAr;
            PurchaseAcc.AccTypeId = 21;
            PurchaseAcc.CurrencyId = 1;
            PurchaseAcc.BranchId = 1;
            Accounts.PurchaseAccNum = _accountGenerator.CreateNewAccount(PurchaseAcc);

            return Accounts;
        }
    }
}
