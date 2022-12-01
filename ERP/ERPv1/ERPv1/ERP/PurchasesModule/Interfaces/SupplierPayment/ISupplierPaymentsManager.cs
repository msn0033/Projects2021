using ERPv1.ERP.PurchasesModule.ViewModel;
using ERPv1.ERP.PurchasesModule.ViewModel.SupplierPayment;

namespace ERPv1.ERP.PurchasesModule.Interfaces.SupplierStatment
{
    public interface ISupplierPaymentsManager
    {
        SupplierPaymentContainer NewPayment(int SupplierId);
        public void SaveSupplierPayment(SupplierPaymentContainer vm);
    }
}