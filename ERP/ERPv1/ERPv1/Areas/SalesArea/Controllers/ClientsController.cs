using ERPv1.CRM.Interfaces;
using ERPv1.CRM.ViewModel;
using ERPv1.ERP.SalesModule.Interfaces;
using ERPv1.ERP.SalesModule.Interfaces.ClientStatment;
using ERPv1.ERP.SalesModule.Interfaces.Payment;
using ERPv1.ERP.SalesModule.ViewModel;
using ERPv1.ERP.SalesModule.ViewModel.ClientStatment;
using ERPv1.ERP.SalesModule.ViewModel.Payment;
using ERPv1.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.Areas.SalesArea.Controllers
{
    [Area("SalesArea")]
    public class ClientsController : Controller
    {
        private readonly IClientGenerationManager _clientGenerationManager;
        private readonly ISalesManager _salesManager;
        private readonly IClientPaymentManager _clientPaymentManager;
        private readonly IClientReport _clientReport;

        public ClientsController(IClientGenerationManager clientGenerationManager
            , ISalesManager salesManager
            ,IClientPaymentManager clientPaymentManager
            , IClientReport clientReport)
        {
            _clientGenerationManager = clientGenerationManager;
            _salesManager = salesManager;
            _clientPaymentManager = clientPaymentManager;
            _clientReport = clientReport;
        }
        public IActionResult Index()
        {
            return View(_clientGenerationManager.GetAllClients());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }//انشاء عميل جديد
        [HttpPost]
        public IActionResult Create (ContactCreatingVM client)
        {
            if (ModelState.IsValid)
            {
                var feedback = _clientGenerationManager.AddNewClient(client);
                if (feedback.Done)
                    return RedirectToAction(nameof(this.Index));
                else
                {
                    foreach (var item in feedback.Errors)
                    {
                        ModelState.AddModelError("error", item);
                        
                    }

                }
            }
            return View(client);
        }//حفظ عميل جديد

        public ActionResult NewSale(int Id)//فاتورة جديدة
        {
            var vm= _salesManager.NewSales(Id);
            vm.SaveURL = "/SalesArea/Clients/SaveNewSale";
            return View(vm);
        }
        public IActionResult SaveNewSale([FromBody]SalesContainer vm)//حفظ الفاتورة
        {
            List<string> er = new List<string>();
            if(ModelState.IsValid)
            {
               
               var feedback= _salesManager.SaveNewSale(vm);
                if (feedback.Done)
                    return RedirectToAction(nameof(this.Index));
                else
                    foreach (var item in feedback.Errors)
                    {

                        ModelState.AddModelError("error", item);
                        er.Add(item);
                    }
            }
            return Json(new {error=er });   
        }

        public IActionResult ClientPayment(int Id)//سداد  عميل
        {
            var vm=_clientPaymentManager.NewPayment(Id);
            return View(vm);
        }

        public JsonResult SaveClientPayment ([FromBody] ClientPaymentContainer vm)// حفظ سداد العميل
        {
            if (ModelState.IsValid)
            {
                _clientPaymentManager.SaveClientpayment(vm);
                return Json(new { newLocation = "/SalesArea/Clients/Index/" });
            }
            else
            {
                List<string> er = new List<string>();
                var pro = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                er.AddRange(pro);
                return Json(new { Problem = er });
            }
        }

        public IActionResult ClientStatment()
        {
            var vm = new ClientStatmentContainer();
            vm.ReportURL = "/SalesArea/Clients/BuildClientStatment";
            return View(vm);
        }

        public JsonResult BuildClientStatment([FromBody] StatmentParams statment)
        {
            if (ModelState.IsValid)
            {
                var vm = new ClientStatmentContainer();
                vm.StatmentParams = statment;
                _clientReport.UpdateStatment(vm);
                vm.ReportURL = "/SalesArea/Clients/BuildClientStatment";
                return Json(new { result = vm });
            }
            return Json(new { result = 0 });
        }
        
        
    }
}
