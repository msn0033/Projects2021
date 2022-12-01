using ERP.PurchasesModule.ViewModel;
using Microsoft.AspNetCore.Http;

namespace ERPv1.ERP.PurchasesModule.Interfaces
{
    public interface IPurchaseManager
    {
        PurchaseContainer NewPurchase(int supplierId);
        void SavePurchase(PurchaseContainer vm,IFormFile InvoiceFile);
    }
}