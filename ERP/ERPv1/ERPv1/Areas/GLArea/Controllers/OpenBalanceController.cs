using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ERPv1.Areas.GLArea.Controllers
{
    [Area("GLArea")]
    public class OpenBalanceController : Controller
    {
        private readonly IOpeingBalanceManager _opeingBalanceManager;

        public OpenBalanceController(IOpeingBalanceManager opeingBalanceManager)
        {
            _opeingBalanceManager = opeingBalanceManager;
        }
        public IActionResult NewBalance()
        {
            return View(_opeingBalanceManager.NewOpeningTrans());
        }

        public JsonResult SaveOpeningBalance([FromBody] OpeningTransactionVM vm)
        {
            if(ModelState.IsValid)
            {
                _opeingBalanceManager.SaveOpeningTrans(vm);
                return Json(new { newLocation = "/GLArea/AccountChart/Index" });
            }
            return Json(new { newLocation="/home/ERrOrS/"});
        }
    }
}
