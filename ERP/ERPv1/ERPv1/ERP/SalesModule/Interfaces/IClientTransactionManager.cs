using ERPv1.CRM.Model;
using ERPv1.ERP.SalesModule.ViewModel;
using ERPv1.ERP.SalesModule.ViewModel.Payment;

namespace ERPv1.ERP.SalesModule.Interfaces
{
    public interface IClientTransactionManager
    {
        void ClientSalesTransaction(SalesContainer vm, Contacts Contact, string InvoiceNum, string TransId);
        void ClientPaymentTransaction(ClientPaymentContainer vm, Contacts Client, string TransId, decimal BalanceAfter);


    }
}