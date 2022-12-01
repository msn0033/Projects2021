using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.ViewModel.PurchaseReturn
{
    public class PurchaseStoreTransaction
    {
        public int StoreId { get; set; }
        
        public string StoreName { get; set; }
        
        public decimal Qty { get; set; }
       
        public decimal UnitPrice { get; set; }

        public decimal ReturnQty { get; set; }
     
       

    }
}
