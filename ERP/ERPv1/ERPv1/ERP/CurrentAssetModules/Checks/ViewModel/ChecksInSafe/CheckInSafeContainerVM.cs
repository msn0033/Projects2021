using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Checks.ViewModel.ChecksInSafe
{
    public class CheckInSafeContainerVM
    {
        public CheckInSafeContainerVM()
        {
            HafzaDetailsVM = new HafzaDetailsVM();
            CheckInSafeDetailsVM = new List<CheckInSafeDetailsVM>();
        }
        public HafzaDetailsVM HafzaDetailsVM { get; set; }
        public List<CheckInSafeDetailsVM> CheckInSafeDetailsVM { get; set; }
    }
}
