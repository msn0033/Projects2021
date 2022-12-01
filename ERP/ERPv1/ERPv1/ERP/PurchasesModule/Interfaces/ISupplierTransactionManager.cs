using ERP.PurchasesModule.ViewModel;
using ERPv1.ERP.PurchasesModule.ViewModel;
using ERPv1.ERP.PurchasesModule.ViewModel.Expense;
using ERPv1.ERP.PurchasesModule.ViewModel.SupplierPayment;

namespace ERPv1.ERP.PurchasesModule.Interfaces
{
    public interface ISupplierTransactionManager
    {
        void PurchaseSupplierTransaction(PurchaseContainer vm, int purchaseId, string SupplierAccNum, string TransId, decimal BalanceAfter);
        void SupplierPaymentTransaction(SupplierPaymentContainer vm, decimal LocalAmount, string SupplierAccNum, string TransId, decimal BalanceAfter);
        void ExpenseSupplierTransaction(ExpenseContainerVM vm, string SupplierAccNum, string TransId, decimal BalanceAfter);
    }
}