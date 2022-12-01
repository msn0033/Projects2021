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
    public class JournalController : Controller
    {
        private readonly IJournalManager _journalManager;

        public JournalController(IJournalManager journalManager)
        {
            _journalManager = journalManager;
        }
        
        public IActionResult CreateJournal()
        {
            return View(_journalManager.NewJournal());
        }

        public JsonResult GetAccountDetails([FromBody]string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var acc = _journalManager.GetAccountDetails(Id);
                return Json(new { Account = acc });
            }
            return Json(new { error = "رجاء اختار الحساب" });
        }

        public JsonResult SaveJournal([FromBody]JournalVM journalVM)
        {

            if (ModelState.IsValid)
            { 
                _journalManager.SaveJournal(journalVM);
                return Json(new { new_Location = "/GLArea/ccountChart/Index" });
            }
          
            else
            {
                var err = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage);
                return Json(new {eror=err });
            }
        }
    }
}
