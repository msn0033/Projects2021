using ERP.PurchasesModule.ViewModel;
using ERPv1.Data;
using ERPv1.ERP.PurchasesModule.Interfaces;
using ERPv1.ERP.PurchasesModule.Model;
using ERPv1.ERP.PurchasesModule.ViewModel;
using ERPv1.ERP.PurchasesModule.ViewModel.Expense;
using ERPv1.ERP.PurchasesModule.ViewModel.SupplierPayment;
using ERPv1.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.Services
{
    public class SupplierTransactionManager : ISupplierTransactionManager
    {
        private readonly ApplicationDbContext _db;

        public SupplierTransactionManager(ApplicationDbContext db)
        {
            _db = db;
        }
        public void PurchaseSupplierTransaction(PurchaseContainer vm, int purchaseId, string SupplierAccNum, string TransId, decimal BalanceAfter)
        {
            var trans = new SupplierTransaction();
            trans.SupplierId = vm.SupplierData.SupplierId;
            trans.PurchaseId = purchaseId;
            trans.JournalId = TransId;
            trans.PaymentMethodEnum = SupplierPaymentMethodEnum.Credit;
            trans.PaymentAccNum = SupplierAccNum;
            trans.PaymentDate = vm.PurchaseSummary.PurchaseDate.ConvertDate();
            trans.CurrencyId = vm.PurchaseSummary.CurrencyId;
            trans.PaymentAmount = vm.PurchaseSummary.TotalAmountWithVAT;
            trans.BalanceAfter = BalanceAfter;
            _db.SupplierTransactions.Add(trans);
            _db.SaveChanges();
        }

        public void SupplierPaymentTransaction(SupplierPaymentContainer vm,decimal LocalAmount,string SupplierAccNum, string TransId, decimal BalanceAfter)
        {
            var trans = new SupplierTransaction();
            trans.SupplierId = vm.SupplierData.SupplierId;
          
            trans.JournalId = TransId;
            trans.PaymentMethodEnum = vm.PaymentDetails.PaymentMethod;
            trans.PaymentAccNum = SupplierAccNum;
            trans.PaymentDate = vm.PaymentDetails.PaymentDate.ConvertDate();
            trans.CurrencyId = vm.SelectedBalance.CurrencyId;
            trans.PaymentAmount = LocalAmount;
            trans.BalanceAfter = BalanceAfter;
            _db.SupplierTransactions.Add(trans);
            _db.SaveChanges();
        }
        public void ExpenseSupplierTransaction(ExpenseContainerVM vm, string SupplierAccNum, string TransId, decimal BalanceAfter)
        {
            var trans = new SupplierTransaction();
            trans.SupplierId = vm.ExpenseDetails.SupplierId.Value;
            trans.JournalId = TransId;
            trans.PaymentMethodEnum = SupplierPaymentMethodEnum.Credit;
            trans.PaymentAccNum = SupplierAccNum;
            trans.PaymentDate = vm.ExpenseDetails.ExpenseDate.ConvertDate();
            trans.CurrencyId = vm.ExpenseDetails.CurrencyId;
            trans.PaymentAmount =vm.ExpenseDetails.Amount-vm.PaymentDetails.PaymentAmount;
            trans.BalanceAfter = BalanceAfter;
            _db.SupplierTransactions.Add(trans);
            _db.SaveChanges();
        }

    }
}
