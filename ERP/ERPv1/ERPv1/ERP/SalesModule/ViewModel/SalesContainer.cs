using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.ViewModel
{
    public class SalesContainer
    {
        public SalesContainer()
        {
            SalesItemDetails = new List<SalesItemDetails>();
            SalesSummary = new SalesSummary();
            ClientData = new ClientData();

            Messages = new List<string>();
            IsDetailAreaVisible = true;
            IsWaitingAreaVisible = false;
            IsMessageAreaVisible = false;
        }
        public ClientData ClientData { get; set; }
        public List<SalesItemDetails> SalesItemDetails { get; set; }
        public SalesSummary SalesSummary { get; set; }

        public string SaveURL { get; set; }

        public List<string> Messages { get; set; }//حفظ الاخطاء فيها

        public bool IsWaitingAreaVisible { get; set; }//Upload Data =>load
        public bool IsDetailAreaVisible { get; set; }// Normal View
        public bool IsMessageAreaVisible { get; set; } //Show In Case an Error

    }
}
