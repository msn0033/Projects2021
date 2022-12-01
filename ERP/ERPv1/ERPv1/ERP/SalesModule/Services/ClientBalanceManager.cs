using CRM.Model;
using ERPv1.CRM.Model;
using ERPv1.Data;
using ERPv1.ERP.SalesModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.Services
{
    public class ClientBalanceManager : IClientBalanceManager
    {
        private readonly ApplicationDbContext _db;

        public ClientBalanceManager(ApplicationDbContext db)
        {
            _db = db;

        }
        public decimal UpdateClientBalance(Contacts Client, decimal LocalAmount, bool plus)
        {
            if (plus)
                Client.ClientBalance += LocalAmount;
            else
                Client.ClientBalance -= LocalAmount;

            _db.Contacts.Update(Client);
            _db.SaveChanges();
            return Client.ClientBalance;
        }
        public void ManageClientBlanceInCurrency(int ClientId, string AccNum, int CurrencyId, decimal Amount, bool plus)
        {
            var ClientInCurrency = _db.ContactBalanceInCurrency
                        .Where(x => x.ContactId == ClientId &&
                               x.AccNum == AccNum &&
                               x.CurrencyId == CurrencyId).ToList();
            if (ClientInCurrency.Count > 0)
                UpdateBalanceInCurrency(ClientId, AccNum, CurrencyId, Amount, plus);
            else
                AddNewBalanceInCurrency(ClientId, AccNum, CurrencyId, Amount);
        }
        public void AddNewBalanceInCurrency(int ClientId, string AccNum, int CurrencyId, decimal AmountWithVAT)
        {

            _db.ContactBalanceInCurrency.Add(new ContactBalanceInCurrency()
            {
                AccNum = AccNum,
                ContactId = ClientId,
                CurrencyId = CurrencyId,
                Balance = AmountWithVAT
            });
            _db.SaveChanges();
        }
        public void UpdateBalanceInCurrency(int ClientId, string AccNum, int CurrencyId, decimal AmountWithVAT, bool plus)
        {
            var ClientInCurrency = _db.ContactBalanceInCurrency
                         .Where(x => x.ContactId == ClientId &&
                                x.AccNum == AccNum &&
                                x.CurrencyId == CurrencyId).FirstOrDefault();
            if (plus)
                ClientInCurrency.Balance += AmountWithVAT;
            else
                ClientInCurrency.Balance -= AmountWithVAT;

            _db.ContactBalanceInCurrency.Update(ClientInCurrency);
            _db.SaveChanges();
        }

    }
}
