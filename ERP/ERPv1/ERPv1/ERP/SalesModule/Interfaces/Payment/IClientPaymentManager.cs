using ERPv1.ERP.SalesModule.ViewModel.Payment;

namespace ERPv1.ERP.SalesModule.Interfaces.Payment
{
    public interface IClientPaymentManager
    {
        ClientPaymentContainer NewPayment(int ClientId);
        void SaveClientpayment(ClientPaymentContainer vm);
    }
}