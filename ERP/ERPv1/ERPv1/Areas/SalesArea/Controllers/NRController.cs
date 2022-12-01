using ERPv1.ERP.CurrentAssetModules.Checks.Interfaces;
using ERPv1.ERP.CurrentAssetModules.Checks.Interfaces.CollectCashCheck;
using ERPv1.ERP.CurrentAssetModules.Checks.ViewModel.ChecksInBank;
using ERPv1.ERP.CurrentAssetModules.Checks.ViewModel.ChecksInSafe;
using ERPv1.ERP.CurrentAssetModules.Checks.ViewModel.CollectCashCheck;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.Areas.SalesArea.Controllers
{
    [Area("SalesArea")]
    public class NRController : Controller
    {
        private readonly INRManager _nRManager;
        private readonly ICollectCashCheckManager _collectCashCheckManager;

        public NRController(INRManager nRManager , ICollectCashCheckManager collectCashCheckManager)
        {
            _nRManager = nRManager;
            _collectCashCheckManager = collectCashCheckManager;
        }
        public IActionResult CheckInSafe()
        {
            var vm = _nRManager.GetCheckInSafe();
            return View(vm);
        }

        public JsonResult MoveToBank([FromBody]CheckInSafeContainerVM vm)
        {
            if (ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                try
                {
                    _nRManager.MoveToBank(vm);
                    return Json(new { newLocation = "/SalesArea/NR/CheckInSafe" });
                }
                catch (Exception ex)
                {
                    errors.Add(ex.InnerException.Message);
                    return Json(new { errors = errors });
                }
            }
            else
            {

                return Json(new { errors = "خطاء" });
            }
               
            
        }

        public IActionResult CheckInBank()
        {
            var vm = _nRManager.GetCheckInBank();
            return View(vm);
        }

        public JsonResult CollectChecks([FromBody]CheckInBankContainerVM vm)
        {

           if(ModelState.IsValid)
            {
                
                _nRManager.CollectChecks(vm);
                return Json(new { newLocation = "/SalesArea/NR/CollectChecks" });

            }
           else
            {
                return Json(new { newLocation = "/Home/Index" });
            }
        }

 
        public IActionResult CollectCashCheck(string Id)
        {
           
           var vm= _collectCashCheckManager.NewCollectCashCheck(Id);
            return View(vm);
        }

        public JsonResult SaveCollectCashCheck([FromBody] CollectCashCheckContainerVM vm)
        {
            if (ModelState.IsValid)
            {

                _collectCashCheckManager.SaveCollectCashCheck(vm);
                return Json(new { newLocation = "/SalesArea/NR/CollectCashCheck" });

            }
            else
            {
                return Json(new { newLocation = "/Home/Index" });
            }
        }
    }
}
