using ERPv1.ERP.ERPSettings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Checks.ViewModel.CollectCashCheck
{
    public class SelectedCheck
    {
        public int ContactId { get; set; }
        public string ChKNum { get; set; }
        public decimal AmountForgin { get; set; }
        public decimal AmountLocal { get; set; }
        public Currency Currency { get; set; }
        public string CurrencyAbbr { get; set; }
        public string DueDate { get; set; }
        public string OrignalBankName { get; set; }
        public string CheckStatus { get; set; }
        public decimal Paid { get; set; }
    }
}
