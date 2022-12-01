using ERPv1.ERP.GeneralLedgerModule.JournalModule.Interfaces.AccountStatment;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel.AccountStatment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.Areas.GLArea.Controllers
{
    [Area("GLArea")]
    public class ReportController : Controller
    {
        private readonly IStatmentManager _statmentManager;

        public ReportController(IStatmentManager statmentManager)
        {
            _statmentManager= statmentManager;
        }
        public IActionResult AccountStatment()
        {
            var vm = new StatmentContainer();
            vm.ReportURL = "/GLArea/Report/BuildStatment";

            return View(vm);
        }
        public JsonResult BuildStatment ([FromBody]StatmentParams statmentParams)
        {

            var vm = new StatmentContainer();
            vm.StatmentParams = statmentParams;
          
            _statmentManager.UpdateStatment(vm);
            vm.ReportURL = "/GLArea/Report/BuildStatment";
            return Json( new { result=vm });
        }
    }
}
