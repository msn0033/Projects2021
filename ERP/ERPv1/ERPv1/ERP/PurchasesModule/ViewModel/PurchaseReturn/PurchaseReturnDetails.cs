using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.ViewModel.PurchaseReturn
{
    public class PurchaseReturnDetails
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Date { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal TotalAmountWithVAT { get; set; }
        public string Invoice { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyAbber { get; set; }



    }
}
