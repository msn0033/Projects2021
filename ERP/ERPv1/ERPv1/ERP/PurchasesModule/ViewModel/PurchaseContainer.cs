using ERPv1.ERP.ERPSettings.Interfaces;
using ERPv1.ERP.ERPSettings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ERP.PurchasesModule.ViewModel
{
    public class PurchaseContainer
    {

        public PurchaseContainer()
        {
           
            SupplierData = new SupplierData();
            PurchaseSummary = new PurchaseSummary();
            PurchasesDetails = new List<PurchaseItemDetails>();

            Messages = new List<string>();
            IsDetailAreaVisible = true;
            IsWaitingAreaVisible = false;
            IsMessageAreaVisible = false;

        }

        public SupplierData SupplierData { get; set; }
        public PurchaseSummary PurchaseSummary { get; set; }
        public List<PurchaseItemDetails> PurchasesDetails { get; set; }

        public string SaveURL { get; set; }
        public List<string> Messages { get; set; }//حفظ الاخطاء فيها

        public bool IsWaitingAreaVisible { get; set; }//Upload Data =>load
        public bool IsDetailAreaVisible { get; set; }// Normal View
        public bool IsMessageAreaVisible { get; set; } //Show In Case an Error
    }
}
