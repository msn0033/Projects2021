using ERP.PurchasesModule.ViewModel;
using ERPv1.CRM.Model;
using ERPv1.Data;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel;
using ERPv1.ERP.PurchasesModule.Interfaces;
using ERPv1.ERP.PurchasesModule.ViewModel;
using ERPv1.ERP.PurchasesModule.ViewModel.Expense;
using ERPv1.ERP.PurchasesModule.ViewModel.SupplierPayment;
using ERPv1.Infrastructure.Extensions;
using ERPv1.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.Services
{
    public class SupplierJournalsManager : ISupplierJournalsManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IJournalManager _journalManager;
        private readonly IUploadManager _uploadManager;

        public SupplierJournalsManager(ApplicationDbContext db, IJournalManager journalManager, IUploadManager uploadManager)
        {
            _db = db;
            _journalManager = journalManager;
            _uploadManager = uploadManager;

        }

        //Create Journal=> 1-StoreAccounts(It is more than one or one  this Products   ) 2- SupplierAcc 3-Vat-Purchase 
        public string PurchaseJournal(PurchaseContainer vm, CRM.Model.Contacts contact, IFormFile Invoice)
        {

            var currency = _db.Currency.Find(vm.PurchaseSummary.CurrencyId);
            var journal = new JournalVM();
            journal.TransDate = vm.PurchaseSummary.PurchaseDate.ConvertDate().ToString();
            journal.TransDes = vm.PurchaseSummary.Description;
            if (Invoice != null)
            {
                journal.DocName = _uploadManager.UploadedFile(Invoice, "FinanceFiles");//upload File
            }
            foreach (var item in vm.PurchasesDetails)
            {
                //1-StoreAccounts(It is more than one or one  this Products   )
                var StoreItem = _db.StoreItems.Find(item.StoreItemId);
                var JD_Store = new JournalDetailsVM();
                JD_Store.AccNum = StoreItem.StoreAccNum;
                JD_Store.Side = JournalSideEnum.Debit;
                JD_Store.Debit = item.QTY * item.UnitPrice;
                JD_Store.CurrencyId = vm.PurchaseSummary.CurrencyId;
                JD_Store.UsedRate = currency.Rate;
                journal.JournalDetailsVM.Add(JD_Store);
            }
            //2- SupplierAcc

            var JD_Supplier = new JournalDetailsVM();
            JD_Supplier.AccNum = contact.SupplierAccNum;
            JD_Supplier.Side = JournalSideEnum.Credit;
            JD_Supplier.Credit = vm.PurchaseSummary.TotalAmountWithVAT;
            JD_Supplier.CurrencyId = vm.PurchaseSummary.CurrencyId;
            JD_Supplier.UsedRate = currency.Rate;
            journal.JournalDetailsVM.Add(JD_Supplier);

            // 3-Vat-Purchase
            if (vm.PurchaseSummary.IsVAT)
            {
                var JD_VAT = new JournalDetailsVM();
                JD_VAT.AccNum = "1269000001";
                JD_VAT.Side = JournalSideEnum.Debit;
                JD_VAT.Debit = vm.PurchaseSummary.VATAmount;
                JD_VAT.CurrencyId = vm.PurchaseSummary.CurrencyId;
                JD_VAT.UsedRate = currency.Rate;
                journal.JournalDetailsVM.Add(JD_VAT);
            }


            var TransId = _journalManager.SaveJournal(journal);
            return TransId;
        }

        public string SupplierPaymentJournal(SupplierPaymentContainer vm, CRM.Model.Contacts contact, decimal LocalAmount)
        {

            var currency = _db.Currency.Find(vm.SelectedBalance.CurrencyId);
            var journal = new JournalVM();
            journal.TransDate = vm.PaymentDetails.PaymentDate.ConvertDate().ToString();
            journal.TransDes = vm.PaymentDetails.Description;
            var JD_Debit = new JournalDetailsVM();
            JD_Debit.AccNum = contact.SupplierAccNum;
            JD_Debit.Side = JournalSideEnum.Debit;
            JD_Debit.Debit = LocalAmount;
            JD_Debit.CurrencyId = vm.SelectedBalance.CurrencyId;
            JD_Debit.UsedRate = currency.Rate;
            journal.JournalDetailsVM.Add(JD_Debit);



            var JD_Credit = new JournalDetailsVM();
            JD_Credit.AccNum = vm.PaymentDetails.PaymentMethod == Model.SupplierPaymentMethodEnum.Safe ?
                vm.PaymentDetails.SafeAccNum : vm.PaymentDetails.PaymentMethod == Model.SupplierPaymentMethodEnum.Bank ?
                vm.PaymentDetails.BankAccNum : "2210000001";
            JD_Credit.Side = JournalSideEnum.Credit;
            JD_Credit.Credit = LocalAmount;
            JD_Credit.CurrencyId = vm.SelectedBalance.CurrencyId;
            JD_Credit.UsedRate = currency.Rate;
            journal.JournalDetailsVM.Add(JD_Credit);

            var TransId = _journalManager.SaveJournal(journal);
            return TransId;
        }

        public string ExpenseJournal(ExpenseContainerVM vm, string ExpenseAccNum, Contacts contacts,Currency currency)
        {
            //var currency = _db.Currency.Find(vm.ExpenseDetails.CurrencyId);

            var RestAmount = vm.ExpenseDetails.Amount - vm.PaymentDetails.PaymentAmount;

            var journal = new JournalVM();
            journal.TransDate = vm.PaymentDetails.PaymentDate.ConvertDate().ToString();
            journal.TransDes = vm.PaymentDetails.Description;
            var JD_Debit = new JournalDetailsVM();
            JD_Debit.AccNum = ExpenseAccNum;
            JD_Debit.Side = JournalSideEnum.Debit;
            JD_Debit.Debit = vm.ExpenseDetails.Amount;
            JD_Debit.CurrencyId = vm.ExpenseDetails.CurrencyId;
            JD_Debit.UsedRate = currency.Rate;
            journal.JournalDetailsVM.Add(JD_Debit);

            //Rest >0   => Update Supplier Balance , Supplier Transaction, Journal Transaction
            if (RestAmount > 0)
            {
                if (contacts != null)
                {
                    var JD_SupplierCredit = new JournalDetailsVM();
                    JD_SupplierCredit.AccNum = contacts.SupplierAccNum;
                    JD_SupplierCredit.Side = JournalSideEnum.Credit;
                    JD_SupplierCredit.Credit = RestAmount;
                    JD_SupplierCredit.CurrencyId = vm.ExpenseDetails.CurrencyId;
                    JD_SupplierCredit.UsedRate = currency.Rate;
                    journal.JournalDetailsVM.Add(JD_SupplierCredit);
                }

                var JD_Credit = new JournalDetailsVM();
                JD_Credit.AccNum = vm.PaymentDetails.PaymentMethod == Model.SupplierPaymentMethodEnum.Safe ?
                    vm.PaymentDetails.SafeAccNum : vm.PaymentDetails.PaymentMethod == Model.SupplierPaymentMethodEnum.Bank ?
                    vm.PaymentDetails.BankAccNum : "2210000001";//2210000001=>check  اوراق دفع
                JD_Credit.Side = JournalSideEnum.Credit;
                JD_Credit.Credit = vm.PaymentDetails.PaymentAmount;
                JD_Credit.CurrencyId = vm.ExpenseDetails.CurrencyId;
                JD_Credit.UsedRate = currency.Rate;
                journal.JournalDetailsVM.Add(JD_Credit);
            }
            if (contacts != null)
            {
                var JD_SupplierCredit = new JournalDetailsVM();
               
                {
                    JD_SupplierCredit.AccNum = contacts.SupplierAccNum;
                    JD_SupplierCredit.Side = JournalSideEnum.Credit;
                    JD_SupplierCredit.Credit = RestAmount;
                    JD_SupplierCredit.CurrencyId = vm.ExpenseDetails.CurrencyId;
                    JD_SupplierCredit.UsedRate = currency.Rate;
                    journal.JournalDetailsVM.Add(JD_SupplierCredit);
                }

      

            }
            if (vm.PaymentDetails.CustodyAccNum.Count()>0)
            {
                var JD_Credit2 = new JournalDetailsVM();
                {
                    JD_Credit2.AccNum = vm.PaymentDetails.CustodyAccNum;
                    JD_Credit2.Side = JournalSideEnum.Credit;
                    JD_Credit2.Credit = vm.PaymentDetails.PaymentAmount;
                    JD_Credit2.CurrencyId = vm.ExpenseDetails.CurrencyId;
                    JD_Credit2.UsedRate = currency.Rate;
                    journal.JournalDetailsVM.Add(JD_Credit2);
                }
            }

            var TransId = _journalManager.SaveJournal(journal);
            return TransId;

        }
    }
}
