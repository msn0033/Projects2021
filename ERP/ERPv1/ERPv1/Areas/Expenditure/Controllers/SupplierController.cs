using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.PurchasesModule.ViewModel;
using ERPv1.CRM.Interfaces;
using ERPv1.CRM.ViewModel;
using ERPv1.Data;

using ERPv1.ERP.CurrentAssetModules.Inventory.Interfaces;
using ERPv1.ERP.ERPSettings.Interfaces;
using ERPv1.ERP.PurchasesModule.Interfaces;
using ERPv1.ERP.PurchasesModule.Interfaces.SupplierStatment;
using ERPv1.ERP.PurchasesModule.Services;
using ERPv1.ERP.PurchasesModule.ViewModel.SupplierPayment;
using ERPv1.ERP.PurchasesModule.ViewModel.SupplierStatment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPv1.Areas.Expenditure.Controllers
{
    [Area("Expenditure")]
    public class SupplierController : Controller
    {
        private readonly ISupplierGenerationManager _supplierGenerationManager;
        private readonly IPurchaseManager _purchaseManager;
        private readonly IStoreItemManager _storeItemManager;
        private readonly ITaxManager _tax;
        private readonly ISupplierPaymentsManager _supplierPaymentsManager;
        private readonly ISupplierReport _supplierReport;

        public SupplierController(
                                     ISupplierGenerationManager supplierGenerationManager
                                    ,IPurchaseManager purchaseManager
                                    ,IStoreItemManager storeItemManager,ITaxManager tax
                                    ,ISupplierPaymentsManager supplierPaymentsManager
                                    ,ISupplierReport supplierReport
                                     
                                   )
        {
            _supplierGenerationManager = supplierGenerationManager;
            _purchaseManager = purchaseManager;
            _storeItemManager = storeItemManager;
            _tax = tax;
            _supplierPaymentsManager = supplierPaymentsManager;
            _supplierReport = supplierReport;
        }
        public IActionResult Index()
        {
            return View(_supplierGenerationManager.GetAllSuppliers());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContactCreatingVM supplier)
        {
            if(ModelState.IsValid)
            {
                _supplierGenerationManager.AddNewSupplier(supplier);
                return RedirectToAction(nameof(this.Index));
            }
            return View(supplier);
        }

        public IActionResult NewPurchase(int Id)
        {
            var vm = _purchaseManager.NewPurchase(Id);
            vm.SaveURL = "/Expenditure/Supplier/SavePurchase";
            return View(vm);
        }
        public JsonResult SavePurchase([FromForm] IFormFile InvoFile)
        {
            List<string> errors = new List<string>();
            var body = Request.Form["vm"];
            var model = JsonConvert.DeserializeObject<PurchaseContainer>(body);
            if (ModelState.IsValid)
            {
                try
                {
                    _purchaseManager.SavePurchase(model,InvoFile);
                    return Json(new {newLocation="/Home/Index/" });
                }
                catch(Exception ex) 
                {
                    errors.Add(ex.Message);
                    errors.Add("Please Contact System Admin");
                    return Json(new { Problem = errors });
                }
               
            }
            else
            {
                errors.AddRange(ModelState.Values.SelectMany(x=>x.Errors).Select(x=>x.ErrorMessage).ToList());
                return Json(new { Problem=errors });
            }
        }
        public JsonResult GetStoreItemById(int Id)//يرجع الكمية الحالية للمنتج
        {
           var Qty = _storeItemManager.GetStoreQtyById(Id);
            
           return Json(new { CurrentQty = Qty });
       
        }
        public JsonResult GetVatAmount()// ارجاع الضريبة 
        {
            return Json(new { VAT = _tax.VatAmount() });
        }
        public IActionResult NewSupplierPayment(int Id)
        {
            var vm=_supplierPaymentsManager.NewPayment(Id);
            return View(vm);
        }
        public JsonResult SaveSupplierPayment([FromBody] SupplierPaymentContainer vm)
        {
            List<string> errors = new List<string>();
            if(ModelState.IsValid)
            {
                try
                {
                    _supplierPaymentsManager.SaveSupplierPayment(vm);
                    return Json(new { newLocation = "/Home/Index/" });
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                    errors.Add("please contact System Admin");
                    return Json(new { Problem = errors });
                   
                }
               
            }
            else
            {
                errors.AddRange(ModelState.Values
                                   .SelectMany(x => x.Errors)
                                   .Select(x => x.ErrorMessage));

                return Json(new { Problem = errors });
            }
        }

        public IActionResult SupplierStatment()
        {
            var vm = new SupplierStatmentContainer();
            vm.ReportURL = "/Expenditure/Supplier/BuildSupplierStatment";
            return View(vm);
        }
        public JsonResult BuildSupplierStatment([FromBody] StatmentParams statmentParams)
        {
             var vm = new SupplierStatmentContainer();
            if (ModelState.IsValid)
            {
                vm.StatmentParams = statmentParams;
                _supplierReport.UpdateStatment(vm);
                vm.ReportURL = "/Expenditure/Supplier/BuildSupplierStatment";
                vm.Alert = "تم بنجاح";
               
            }
            else
            {
                vm.ReportURL = "/Expenditure/Supplier/BuildSupplierStatment";
                vm.Alert = "يوجد خطاء";
            }
            return Json(new { result = vm });


        }

    }
}
