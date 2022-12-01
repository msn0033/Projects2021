using ERPv1.CRM.Model;
using ERPv1.Data;
using ERPv1.ERP.SalesModule.Interfaces;
using ERPv1.ERP.SalesModule.Model;
using ERPv1.ERP.SalesModule.ViewModel;
using ERPv1.ERP.SalesModule.ViewModel.Payment;
using ERPv1.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.Services
{
    public class ClientTransactionManager : IClientTransactionManager
    {
        private readonly ApplicationDbContext _db;

        public ClientTransactionManager(ApplicationDbContext db)
        {
            _db = db;

        }
        public void ClientSalesTransaction(SalesContainer vm,Contacts Client,string InvoiceNum,string TransId) 
        {
            //Client Transaction
            var CT = new ClientTransaction()
            {
                InvoiceNum = InvoiceNum,
                ClientId = Client.Id,
                CurrencyId = vm.SalesSummary.CurrencyId,
                BalanceAfter = Client.ClientBalance,
                PaymentAccNum = Client.ClientAccNum,

                PaymentAmount = vm.SalesSummary.TotalWithVAT,
                PaymentDate = vm.SalesSummary.InvoiceDate.ConvertDate(),
                PaymentMethodEnum = ClientPaymentMethodEnum.Credit,
                JournalId = TransId
            };
            _db.ClientTransactions.Add(CT);
            _db.SaveChanges();
        }
        
        public void ClientPaymentTransaction (ClientPaymentContainer vm,Contacts Client,string TransId,decimal BalanceAfter)
        {
            var CT = new ClientTransaction()
            {
                InvoiceNum = null,
                ClientId = Client.Id,
                CurrencyId = vm.SelectedBalance.CurrencyId,
                BalanceAfter = BalanceAfter,
                PaymentAccNum = vm.PaymentDetails.PaymentMethod == Model.ClientPaymentMethodEnum.Safe ?
                vm.PaymentDetails.SafeAccNum : vm.PaymentDetails.PaymentMethod == Model.ClientPaymentMethodEnum.Bank ?
                vm.PaymentDetails.BankAccNum : "1240000001",

                PaymentAmount = vm.PaymentDetails.PaymentAmount,
                PaymentDate = vm.PaymentDetails.PaymentDate.ConvertDate(),
                PaymentMethodEnum = vm.PaymentDetails.PaymentMethod,
                JournalId = TransId
            };
            _db.ClientTransactions.Add(CT);
            _db.SaveChanges();
        }

    }
}
