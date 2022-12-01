using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Checks.ViewModel.ChecksInBank
{
    public class CheckInBankContainerVM
    {
        public CheckInBankContainerVM()
        {
            CheckInBankDetailsVM = new List<CheckInBankDetailsVM>();
        }
        public List<CheckInBankDetailsVM> CheckInBankDetailsVM { get; set; }
    }
}
