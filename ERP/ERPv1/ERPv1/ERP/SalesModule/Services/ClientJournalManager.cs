using ERPv1.Data;
using ERPv1.ERP.CurrentAssetModules.Inventory.Model.Main;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel;
using ERPv1.ERP.SalesModule.Interfaces;
using ERPv1.ERP.SalesModule.ViewModel;
using ERPv1.ERP.SalesModule.ViewModel.Payment;
using ERPv1.Infrastructure.Extensions;
using ERPv1.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.Services
{
    public class ClientJournalManager : IClientJournalManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IUploadManager _uploadManager;
        private readonly IJournalManager _journalManager;

        public ClientJournalManager(ApplicationDbContext db, IUploadManager uploadManager, IJournalManager journalManager)
        {
            _db = db;
            _uploadManager = uploadManager;
            _journalManager = journalManager;

        }
        public string IncomeJournal(SalesContainer vm, CRM.Model.Contacts Contact, IFormFile Invoice)
        {

            var currency = _db.Currency.Find(vm.SalesSummary.CurrencyId);
            var journal = new JournalVM();
            journal.TransDate = vm.SalesSummary.InvoiceDate.ConvertDate().ToString();
            journal.TransDes = vm.SalesSummary.Description;
            if (Invoice != null)
            {
                journal.DocName = _uploadManager.UploadedFile(Invoice, "FinanceFiles");//upload File
            }

            //1- Client 
            var JD_Debit = new JournalDetailsVM();
            JD_Debit.AccNum = Contact.ClientAccNum;
            JD_Debit.Side = JournalSideEnum.Debit;
            JD_Debit.Debit = vm.SalesSummary.TotalWithVAT;
            JD_Debit.CurrencyId = vm.SalesSummary.CurrencyId;
            JD_Debit.UsedRate = currency.Rate;
            journal.JournalDetailsVM.Add(JD_Debit);

            //2- IncomeAcc

            var JD_Income = new JournalDetailsVM();
            JD_Income.AccNum = "3110000001";
            JD_Income.Side = JournalSideEnum.Credit;
            JD_Income.Credit = vm.SalesSummary.Amount;
            JD_Income.CurrencyId = vm.SalesSummary.CurrencyId;
            JD_Income.UsedRate = currency.Rate;
            journal.JournalDetailsVM.Add(JD_Income);

            // 3-Vat-Sales
            if (vm.SalesSummary.IsVAT)
            {
                var JD_VAT = new JournalDetailsVM();
                JD_VAT.AccNum = "2220000001";
                JD_VAT.Side = JournalSideEnum.Credit;
                JD_VAT.Credit = vm.SalesSummary.VATAmount;
                JD_VAT.CurrencyId = vm.SalesSummary.CurrencyId;
                JD_VAT.UsedRate = currency.Rate;
                journal.JournalDetailsVM.Add(JD_VAT);
            }


            var TransId = _journalManager.SaveJournal(journal);
            return TransId;
        }

        public string PurchaseJournal(SalesContainer vm, string InvoiceNum)
        {
            var currency = _db.Currency.Find(vm.SalesSummary.CurrencyId);
          

            var journal = new JournalVM();
            journal.TransDate = vm.SalesSummary.InvoiceDate.ConvertDate().ToString();
            journal.TransDes = vm.SalesSummary.Description;
            journal.JournalDetailsVM.AddRange(PurchaseJournalDetails(vm,currency, InvoiceNum));
            var TransId = _journalManager.SaveJournal(journal);
            return TransId;
        }

        public List<JournalDetailsVM> PurchaseJournalDetails(SalesContainer vm, Currency currency,string InvoiceNum)
        {
            List<JournalDetailsVM> JDs = new List<JournalDetailsVM>();
            TotalWithCurrency TotalWithCurrency = new TotalWithCurrency();
            foreach (var item in vm.SalesItemDetails)
            {
                var StoreItem = _db.StoreItems.Find(item.StoreItemId);
                var StoreItemBalance = _db.StoreItemBalanceDetails
                    .Include(x => x.StoreTransaction)
                    .Include(x=>x.StoreTransaction.PurchaseDetails.Currency)
                    .Where(x => x.StoreItemId == item.StoreItemId && x.CurrentBalance > 0).ToList();

                
                TotalWithCurrency.Total = 0;
                
                switch (StoreItem.StoreSystemEnums)
                {
                    case StoreSystemEnum.FIFO:
                        TotalWithCurrency = GetTotal(StoreItemBalance.OrderBy(x => x.Id).ToList(), StoreItem, item);
                        break;
                    case StoreSystemEnum.LIFO:
                        TotalWithCurrency = GetTotal(StoreItemBalance.OrderByDescending(x => x.Id).ToList(), StoreItem, item);

                        break;
                    case StoreSystemEnum.Averge:
                        TotalWithCurrency= GetTotal(StoreItemBalance.OrderBy(x => x.Id).ToList(), StoreItem, item);

                        break;

                    default:
                        break;
                }
               
                //Purchase Journal Details & Update Store Balance Details
                var JD_Debit = new JournalDetailsVM();
                JD_Debit.AccNum = StoreItem.PurchaseAccNum;
                JD_Debit.Side = JournalSideEnum.Debit;
                JD_Debit.Debit = TotalWithCurrency.Total;
                JD_Debit.CurrencyId = TotalWithCurrency.Currency.Id;
                JD_Debit.UsedRate = TotalWithCurrency.Currency.Rate;
                JDs.Add(JD_Debit);

                //Credit
                var JD_Credit = new JournalDetailsVM();
                JD_Credit.AccNum = StoreItem.StoreAccNum;
                JD_Credit.Side = JournalSideEnum.Credit;
                JD_Credit.Credit = TotalWithCurrency.Total;
                JD_Credit.CurrencyId = TotalWithCurrency.Currency.Id;
                JD_Credit.UsedRate = TotalWithCurrency.Currency.Rate;
                JDs.Add(JD_Credit);


                //update Store Item Balance & Qty
                StoreItem.Qty = StoreItem.Qty - item.Qty;
                StoreItem.Balance = StoreItem.Balance - TotalWithCurrency.Total;
                _db.StoreItems.Update(StoreItem);
                _db.SaveChanges();

                //Store Transaction
                var StoreTrans = new StoreTransaction();
                StoreTrans.InvoiceNum = InvoiceNum;
                StoreTrans.Qty = item.Qty;
                StoreTrans.QtyBalanceAfter = StoreItem.Balance;
                StoreTrans.StoreItemId = item.StoreItemId;
                StoreTrans.StoreTransTypeEnum = StoreTransTypeEnum.Sales;
                StoreTrans.UnitPrice = item.UnitPrice;
                _db.StoreTransactions.Add(StoreTrans);
                _db.SaveChanges();
            }//end loop
            return JDs;
        }
        private TotalWithCurrency GetTotal(List<StoreItemBalanceDetails> OrderedList, StoreItem StoreItem
                                 ,SalesItemDetails item)
        {
            decimal Total = 0;
            decimal Qty = item.Qty;
            Currency Currency=new Currency();
            TotalWithCurrency totalWithCurrency = new TotalWithCurrency();

            foreach (var balance in OrderedList)
            { 
               
                Currency = balance.StoreTransaction.PurchaseDetails.Currency;
               
               
                if (Qty == balance.CurrentBalance && Qty > 0)
                {
                    if(StoreItem.StoreSystemEnums!=StoreSystemEnum.Averge)
                        Total = Total + (Qty * balance.StoreTransaction.UnitPrice);
                    Qty = 0;
                    balance.CurrentBalance = 0;
                    _db.StoreItemBalanceDetails.Update(balance);
                    _db.SaveChanges();
                }
                else if (Qty < balance.CurrentBalance && Qty > 0)
                {
                    if (StoreItem.StoreSystemEnums != StoreSystemEnum.Averge)
                        Total = Total + (Qty * balance.StoreTransaction.UnitPrice );
                    balance.CurrentBalance = balance.CurrentBalance - Qty;
                    Qty = 0;
                    _db.StoreItemBalanceDetails.Update(balance);
                    _db.SaveChanges();
                }
                else if (Qty > balance.CurrentBalance && Qty > 0)
                {
                    if (StoreItem.StoreSystemEnums != StoreSystemEnum.Averge)
                            Total = Total + (balance.CurrentBalance * balance.StoreTransaction.UnitPrice);
                    Qty = Qty - balance.CurrentBalance;
                    balance.CurrentBalance = 0;
                    _db.StoreItemBalanceDetails.Update(balance);
                    _db.SaveChanges();
                }
                if (StoreItem.StoreSystemEnums == StoreSystemEnum.Averge)
                    Total = StoreItem.Balance / StoreItem.Qty;
            }

            totalWithCurrency.Total = Total;
            totalWithCurrency.Currency = Currency;

            return totalWithCurrency;
        }

        public string ClientPaymentJournal(ClientPaymentContainer vm, CRM.Model.Contacts Contact, decimal Amount)
        {
            //Safe -Bank - Check => Debit
            //Client =>Credit

            var currency = _db.Currency.Find(vm.SelectedBalance.CurrencyId);
            var journal = new JournalVM();
            journal.TransDate = vm.PaymentDetails.PaymentDate.ConvertDate().ToString();
            journal.TransDes = vm.PaymentDetails.Description;

            //Debit =>Safe -Bank - Check
            var JD_Debit = new JournalDetailsVM();
            JD_Debit.AccNum = vm.PaymentDetails.PaymentMethod == Model.ClientPaymentMethodEnum.Safe ?
                vm.PaymentDetails.SafeAccNum : vm.PaymentDetails.PaymentMethod == Model.ClientPaymentMethodEnum.Bank ?
                vm.PaymentDetails.BankAccNum : "1240000001";
            JD_Debit.Side = JournalSideEnum.Debit;
            JD_Debit.Debit = Amount;
            JD_Debit.CurrencyId = vm.SelectedBalance.CurrencyId;
            JD_Debit.UsedRate = currency.Rate;
            journal.JournalDetailsVM.Add(JD_Debit);


            // Credit =>Client
            var JD_Credit = new JournalDetailsVM();
            JD_Credit.AccNum = Contact.ClientAccNum; 
            JD_Credit.Side = JournalSideEnum.Credit;
            JD_Credit.Credit = Amount;
            JD_Credit.CurrencyId = vm.SelectedBalance.CurrencyId;
            JD_Credit.UsedRate = currency.Rate;
            journal.JournalDetailsVM.Add(JD_Credit);

            var TransId = _journalManager.SaveJournal(journal);
            return TransId;
        }
        public class TotalWithCurrency
        {
            public decimal Total;
            public Currency Currency;
        }

    }
}


