using AutoMapper;
using ERPv1.Data;
using ERPv1.ERP.SalesModule.Interfaces;
using ERPv1.ERP.SalesModule.Model;
using ERPv1.ERP.SalesModule.ViewModel;
using ERPv1.Infrastructure;
using ERPv1.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.Services
{
    public class SalesManager : ISalesManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IClientBalanceManager _clientBalanceManager;
        private readonly IClientJournalManager _clientJournalManager;
        private readonly IClientTransactionManager _clientTransactionManager;

        public SalesManager(ApplicationDbContext db, IMapper mapper
            , IClientBalanceManager clientBalanceManager
            , IClientJournalManager clientJournalManager
            ,IClientTransactionManager clientTransactionManager)
        {
            _db = db;
            _mapper = mapper;
            _clientBalanceManager = clientBalanceManager;
            _clientJournalManager = clientJournalManager;
            _clientTransactionManager = clientTransactionManager;
        }
        public SalesContainer NewSales(int Id)
        {
            var vm = new SalesContainer();
            var Client = _db.Contacts.Find(Id);
            vm.ClientData = new ClientData()
            {
                ClientId = Client.Id,
                ClientName = Client.NameAr,
                Phone = Client.Phone1,
                Balance = Client.ClientBalance
            };


            return vm;
        }
        public FeedBack SaveNewSale(SalesContainer vm)
        {
            var feedBack = new FeedBack();
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {

                    var ClientData = _db.Contacts.Find(vm.ClientData.ClientId);
                    var Currency = _db.Currency.Find(vm.SalesSummary.CurrencyId);
                    var LocalAmount = vm.SalesSummary.TotalWithVAT * Currency.Rate;

                    //Update Balance in Table Contact
                    var BalanceAfter = _clientBalanceManager.UpdateClientBalance(ClientData, LocalAmount, true);

                    //Update Balance in Currency
                    _clientBalanceManager.ManageClientBlanceInCurrency(ClientData.Id, ClientData.ClientAccNum, Currency.Id, vm.SalesSummary.TotalWithVAT, true);

                    //Create New Invoice          
                    var InvoiceNum = NewInvoices(vm);
                    var m = vm.SalesSummary.Description;
                    vm.SalesSummary.Description =$"فاتورة بيع {InvoiceNum}  عناية :{m}";

                    //Journal => Income & Purchase
                    //Income Journal
                    var TransId = _clientJournalManager.IncomeJournal(vm, ClientData, null);


                    /*//Purchase Journal=>
                          Update StoreItem Table,
                          Update StoreItemWithDetails,
                          Store Transaction*/
                    TransId= _clientJournalManager.PurchaseJournal(vm, InvoiceNum);

                    //Client Transaction
                    _clientTransactionManager.ClientSalesTransaction(vm, ClientData, InvoiceNum, TransId);

                    _db.SaveChanges();
                    transaction.Commit();
                    feedBack.Done = true;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    feedBack.Done = false;
                    do
                    {
                        feedBack.Errors.Add(ex.Message);
                        ex = ex.InnerException;
                    } while (ex != null);
                }
            }

            return feedBack;
        }//end function

        #region Create New Invoice
        private string NewInvoices(SalesContainer vm)
        {
            var invoice = new Invoices();
            var inv = CreateNewInvoiceNum(vm.SalesSummary.InvoiceDate.ConvertDate());
            invoice.InvoiceNumYear = inv.InvNum;//رقم الفاتورة
            invoice.Year = vm.SalesSummary.InvoiceDate.ConvertDate().Year.ToString();//السنة
            invoice.InvoiceNum = invoice.InvoiceNumYear+"-"+ invoice.Year;//Primery Key
            invoice.InvoiceCount = inv.LastId;//Count
            invoice.ContactId = vm.ClientData.ClientId;
            invoice.CurrencyId = vm.SalesSummary.CurrencyId;
            invoice.Amount = vm.SalesSummary.Amount;
            invoice.InvoiceDate = vm.SalesSummary.InvoiceDate.ConvertDate();
            invoice.IsVAT = vm.SalesSummary.IsVAT;
            invoice.VATAmount = vm.SalesSummary.VATAmount;
            invoice.TotalWithVAT = vm.SalesSummary.TotalWithVAT;
            _db.Invoices.Add(invoice);
            _db.SaveChanges();
            return invoice.InvoiceNum;
        }

        private InvoiceNum CreateNewInvoiceNum(DateTime InvoiceDate)
        {



            //int  i = _db.Invoices.Count() == 0 ? 1 : (_db.Invoices.Max(x => x.InvoiceCount)) + 1;
            int i = _db.Invoices.Count() == 0 ? 1 : (_db.Invoices.Max(x => x.InvoiceCount)) + 1;

            if (i > 1) 
            { 
                int m = i - 1;
                var dat = _db.Invoices.Single(x => x.InvoiceCount == m && x.InvoiceDate.Year ==InvoiceDate.Year);
                if (InvoiceDate.Year > dat.InvoiceDate.Year)
                    i = 1;
            }
            return new InvoiceNum()
            {
                InvNum = i.ToString("0000000"),
                LastId = i
            };
        }

        class InvoiceNum
        {
            public string InvNum { get; set; }
            public int LastId { get; set; }

        }
        #endregion
    }
}
