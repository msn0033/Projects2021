using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Checks.ViewModel.ChecksInBank
{
    public class CheckInBankDetailsVM
    {

        public int Id { get; set; }
        public bool Selected { get; set; }
        public string ClientName { get; set; }
        public string CheckNum { get; set; }
        public decimal CheckAmount { get; set; }
        public string DueDate { get; set; }
        public string OrginalBank { get; set; }
        public string CheckStatus { get; set; }
        public string BankAccNum { get; set; }
        public string BankAccName { get; set; }

    }
}
