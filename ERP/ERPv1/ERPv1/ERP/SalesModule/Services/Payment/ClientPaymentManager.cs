using ERPv1.Data;
using ERPv1.ERP.CurrentAssetModules.Checks.Interfaces;
using ERPv1.ERP.SalesModule.Interfaces;
using ERPv1.ERP.SalesModule.Interfaces.Payment;
using ERPv1.ERP.SalesModule.ViewModel.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.Services.Payment
{
    public class ClientPaymentManager : IClientPaymentManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IClientBalanceManager _clientBalanceManager;
        private readonly IClientTransactionManager _clientTransactionManager;
        private IClientJournalManager _clientJournalManager;
        private readonly INRManager _NRManager;

        public ClientPaymentManager(ApplicationDbContext db, IClientBalanceManager clientBalanceManager
            , IClientTransactionManager clientTransactionManager
            , IClientJournalManager clientJournalManager
            , INRManager nPManager)
        {
            _db = db;
            _clientBalanceManager = clientBalanceManager;
            _clientTransactionManager = clientTransactionManager;
            _clientJournalManager = clientJournalManager;
            _NRManager = nPManager;
        }
        public ClientPaymentContainer NewPayment(int ClientId)
        {

            var vm = new ClientPaymentContainer();
            var Client = _db.Contacts.FirstOrDefault(x => x.Id == ClientId);
            vm.ClientData.ClientId = ClientId;
            vm.ClientData.ClientName = Client.NameAr;
            vm.ClientData.Phone = Client.Phone1;
            vm.ClientData.Balance = Client.ClientBalance;
            vm.ClientBalanceDetails = _db.ContactBalanceInCurrency.Include(x => x.Currency)
                .Where(x => x.ContactId == ClientId && x.AccNum == Client.ClientAccNum)
                .Select(x => new ClientBalanceDetails()
                {

                    Amount = x.Balance,
                    CurrencyAbbr = x.Currency.CurrencyAbbrev,
                    CurrencyId = x.Currency.Id,
                    Rate = x.Currency.Rate,
                    LocalAmount = x.Balance * x.Currency.Rate

                }).ToList();
            return vm;
        }

        public void SaveClientpayment(ClientPaymentContainer vm)
        {

            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var Client = _db.Contacts.Find(vm.ClientData.ClientId);
                    var Currency = _db.Currency.Find(vm.SelectedBalance.CurrencyId);
                    var LocalAmount = vm.PaymentDetails.PaymentAmount * Currency.Rate;


                    //Update ClientBalance Contact
                    var BalanceAfter = _clientBalanceManager.UpdateClientBalance(Client, LocalAmount, false);

                    //Update ClientBalance In Currency
                    _clientBalanceManager.ManageClientBlanceInCurrency(Client.Id, Client.ClientAccNum, Currency.Id, vm.PaymentDetails.PaymentAmount, false);

                    //Journal
                    var TransId = _clientJournalManager.ClientPaymentJournal(vm, Client, vm.PaymentDetails.PaymentAmount);

                    //Add Client Transaction
                    _clientTransactionManager.ClientPaymentTransaction(vm, Client, TransId, BalanceAfter);

                    //if Check=> add new check
                    if (vm.PaymentDetails.PaymentMethod == Model.ClientPaymentMethodEnum.Check)
                        _NRManager.AddNewCheck(vm, Currency, TransId);

                    _db.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    var er = ex.Message;
                    transaction.Rollback();
                }
            }



        }

    }
}
