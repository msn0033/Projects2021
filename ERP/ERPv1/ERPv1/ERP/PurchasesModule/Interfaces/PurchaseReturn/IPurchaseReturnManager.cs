using ERPv1.ERP.PurchasesModule.ViewModel.PurchaseReturn;

namespace ERPv1.ERP.PurchasesModule.Interface.PurchaseReturn
{
    public interface IPurchaseReturnManager
    {
        PurchaseReturnContainer GetPurchaseReturnData(int PurchaseId);
    }
}