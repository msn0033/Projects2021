using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.ViewModel.ClientStatment
{
    public class ClientStatmentContainer
    {
        public ClientStatmentContainer()
        {
            StatmentParams = new StatmentParams();
            StatmentTransaction = new List<StatmentTransaction>();
        }
        public StatmentParams StatmentParams { get; set; }
        public List<StatmentTransaction> StatmentTransaction { get; set; }
        public string ReportURL { get; set; }
    }
}
