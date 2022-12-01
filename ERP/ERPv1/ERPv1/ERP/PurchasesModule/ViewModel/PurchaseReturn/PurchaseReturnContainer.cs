using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.ViewModel.PurchaseReturn
{
    public class PurchaseReturnContainer
    {
        public PurchaseReturnContainer()
        {
            PurchaseReturnDetails = new PurchaseReturnDetails();
            PurchaseStoreTransaction = new List<PurchaseStoreTransaction>();
        }
        public PurchaseReturnDetails PurchaseReturnDetails;
        public List<PurchaseStoreTransaction> PurchaseStoreTransaction;
    }
}
