using ERPv1.ERP.SalesModule.ViewModel;
using ERPv1.ERP.SalesModule.ViewModel.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Checks.ViewModel.CollectCashCheck
{
    public class CollectCashCheckContainerVM
    {
        public CollectCashCheckContainerVM()
        {
            ClientData = new ClientData();
            SelectedCheck = new SelectedCheck();
            PaymentDetails = new ClientPaymentDetails();
        }

        public ClientData ClientData { get; set; }
        public SelectedCheck SelectedCheck { get; set; }
        public ClientPaymentDetails PaymentDetails { get; set; }
    }
}
