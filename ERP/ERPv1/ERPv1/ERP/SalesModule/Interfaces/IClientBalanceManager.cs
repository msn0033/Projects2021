using ERPv1.CRM.Model;

namespace ERPv1.ERP.SalesModule.Interfaces
{
    public interface IClientBalanceManager
    {
        void ManageClientBlanceInCurrency(int ClientId, string AccNum, int CurrencyId, decimal Amount, bool plus);
        void AddNewBalanceInCurrency(int ClientId, string AccNum, int CurrencyId, decimal Amount);
        void UpdateBalanceInCurrency(int ClientId, string AccNum, int CurrencyId, decimal Amount, bool plus);
        decimal UpdateClientBalance(Contacts Client, decimal LocalAmount, bool plus);
    }
}