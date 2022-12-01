using ERPv1.Data;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Interfaces;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.ViewModel;
using ERPv1.ERP.PurchasesModule.Interfaces;
using ERPv1.ERP.PurchasesModule.Interfaces.SupplierStatment;
using ERPv1.ERP.PurchasesModule.Model;
using ERPv1.ERP.PurchasesModule.ViewModel;
using ERPv1.ERP.PurchasesModule.ViewModel.SupplierPayment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.Services.SupplierStatment
{
    public class SupplierPaymentsManager : ISupplierPaymentsManager
    {
        private readonly ApplicationDbContext _db;
        private readonly ISupplierJournalsManager _supplierJournalsManager;
        private readonly ISupplierBalanceManager _supplierBalanceManager;
        private readonly ISupplierTransactionManager _supplierTransactionManager;
        private readonly INotesPayableManager _notesPayableManager;

        public SupplierPaymentsManager(ApplicationDbContext db,ISupplierBalanceManager supplierBalanceManager
                                        ,ISupplierJournalsManager supplierJournalsManager
                                        , ISupplierTransactionManager supplierTransactionManager
                                        ,INotesPayableManager notesPayableManager)
        {
            _db = db;
            _supplierJournalsManager = supplierJournalsManager;
            _supplierBalanceManager = supplierBalanceManager;
            _supplierTransactionManager = supplierTransactionManager;
            _notesPayableManager = notesPayableManager;

        }
        public SupplierPaymentContainer NewPayment(int SupplierId)
        {

            var vm = new SupplierPaymentContainer();
            var Supplier = _db.Contacts.FirstOrDefault(x => x.Id == SupplierId);
            vm.SupplierData.SupplierId = Supplier.Id;
            vm.SupplierData.SupplierName = Supplier.NameAr;
            vm.SupplierData.Phone = Supplier.Phone1;
            vm.SupplierData.Balance = Supplier.SupplierBalance;
            vm.SupplierBalanceDetails = _db.ContactBalanceInCurrency.Include(x => x.Currency)
                .Where(x => x.ContactId == Supplier.Id && x.AccNum == Supplier.SupplierAccNum)
                .Select(x => new SupplierBalanceDetails()
                {

                    Amount = x.Balance,
                    CurrencyAbbr = x.Currency.CurrencyAbbrev,
                    CurrencyId = x.Currency.Id,
                    Rate = x.Currency.Rate,
                    LocalAmount = x.Balance * x.Currency.Rate

                }).ToList();
            return vm;
        }
        public void SaveSupplierPayment(SupplierPaymentContainer vm)
        {
            /*
                 1-Update Balance Contact Table
                 2-Update Balance with Currency
                 3-Jounal Transaction
                 4-Supplier Transaction
                 5- If NP (Supplier check)Add Check and its history
              */
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {

                    //1-get Supplier Data
                    var supplier = _db.Contacts.Find(vm.SupplierData.SupplierId);
                    //var Currency = _db.Currency.Find(vm.SelectedBalance.CurrencyId);
                    //or
                    var LocalAmount = vm.PaymentDetails.PaymentAmount * vm.SelectedBalance.Rate;
                    //1-Update Balance Contact Table
                    var BalanceAfter = _supplierBalanceManager.UpdateSupplierBalance(supplier, LocalAmount, false);

                    //2-Update Balance With Currency
                    _supplierBalanceManager.UpdateBalanceInCurrency(supplier.Id, supplier.SupplierAccNum
                                                                    , vm.SelectedBalance.CurrencyId
                                                                    , vm.PaymentDetails.PaymentAmount, false);
                    //3-Journal Transaction
                    var TransId = _supplierJournalsManager.SupplierPaymentJournal(vm, supplier, vm.PaymentDetails.PaymentAmount);

                    // 4-Supplier Transaction
                    _supplierTransactionManager.SupplierPaymentTransaction(vm, LocalAmount, supplier.SupplierAccNum, TransId, BalanceAfter);

                    //  5- If NP (Supplier check)Add Check and its 
                    if (vm.PaymentDetails.PaymentMethod == SupplierPaymentMethodEnum.check)
                    {
                        var check = new NotesPayableCreationVM()
                        {
                            
                            AmountForgin = vm.PaymentDetails.PaymentAmount,
                            AmountLocal = LocalAmount,
                            BankAccountNum = vm.PaymentDetails.BankAccNum,
                            ChkNum = vm.PaymentDetails.CheckNum,
                            CurrencyId = vm.SelectedBalance.CurrencyId,
                            DueDate = vm.PaymentDetails.PaymentDueDate,
                            WritingDate = vm.PaymentDetails.WritingDate,
                            SupplierId = vm.SupplierData.SupplierId,

                        };

                        _notesPayableManager.SaveNewNP(check);
                        _notesPayableManager.SaveNewNPHistory(check, TransId);
                    }
                    _db.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    transaction.Rollback();
                }
            }

        }

    }
}
