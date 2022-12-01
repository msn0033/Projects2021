using ERPv1.Data;
using ERPv1.ERP.PurchasesModule.Interface.PurchaseReturn;
using ERPv1.ERP.PurchasesModule.ViewModel.PurchaseReturn;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.Services.PurchaseReturn
{
    public class PurchaseReturnManager : IPurchaseReturnManager
    {
        private readonly ApplicationDbContext _db;

        public PurchaseReturnManager(ApplicationDbContext db)
        {
            _db = db;
        }
        public PurchaseReturnContainer GetPurchaseReturnData(int PurchaseId)
        {
            PurchaseReturnContainer vm = new PurchaseReturnContainer();
            vm.PurchaseReturnDetails = _db.Purchases.Include(x => x.SupplierDetails).Include(x => x.Currency).Where(x => x.Id == PurchaseId)
                .Select(x => new PurchaseReturnDetails()
                {
                    SupplierId = x.SupplierId,
                    SupplierName = x.SupplierDetails.NameAr,
                    Invoice = x.InvoiceNum,
                    TotalAmount = x.TotalAmount,
                    VATAmount = x.VATAmount,
                    TotalAmountWithVAT = x.TotalAmountWithVAT,
                    Date = x.PurchaseDate.ToString("dd/MM/yyyy"),
                    CurrencyId = x.CurrencyId,
                    CurrencyAbber = x.Currency.CurrencyAbbrev,
                }).FirstOrDefault();


            vm.PurchaseStoreTransaction = _db.StoreTransactions.Where(x => x.PurchaseId == PurchaseId)
                .Select(x => new PurchaseStoreTransaction() { }).ToList();


            return vm;
        }
    }
}
