using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Interfaces;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ERPv1.Areas.Expenditure.Controllers
{
    [Area("Expenditure")]
    public class NPController : Controller
    {
        private readonly INotesPayableManager _notesPayableManager;

        public NPController(INotesPayableManager notesPayableManager)
        {
            _notesPayableManager = notesPayableManager;
        }
        public IActionResult ManageNP()
        {
            var vm = _notesPayableManager.GetAllNP();
            return View(vm);
        }
        public JsonResult CollectCheck([FromBody] NPDetails np)
        {
            if (ModelState.IsValid)
            {
                _notesPayableManager.CollectNP(np);
                return Json(new { newLocation = "/Expenditure/NP/ManageNP" });
            }
            return Json(new { newLocation = "/Home/ERrOrS" });
        }
        public JsonResult MoveToCashCollection([FromBody] NPDetails np)
        {
            if (ModelState.IsValid)
            {
                _notesPayableManager.MoveCheckToCashPayment(np);
                return Json(new { newLocation = "/Expenditure/NP/ManageNP" });
            }
            return Json(new { newLocation = "/Home/ERrOrS" });
        }
        public JsonResult CollectCashCollection([FromBody] NPContainer vm)
        {
            var Erro = new List<string>();

            if (ModelState.IsValid)
            {
                _notesPayableManager.CollectCashNP(vm.SelectedNote, vm.PaymentDetails);
                return Json(new { newLocation = "/Expenditure/NP/ManageNP" });
            }
            else{

                Erro.AddRange(ModelState.Values.SelectMany(x=>x.Errors)
                                                  .Select(x=>x.ErrorMessage));               
                return Json(new { Problem = Erro });
            }
            
        }
    }
}
