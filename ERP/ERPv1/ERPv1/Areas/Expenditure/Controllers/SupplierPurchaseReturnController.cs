using ERPv1.ERP.PurchasesModule.Interface.PurchaseReturn;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.Areas.Expenditure.Controllers
{
    public class SupplierPurchaseReturnController : Controller
    {
        private readonly IPurchaseReturnManager _purchaseReturnManager;

        public SupplierPurchaseReturnController(IPurchaseReturnManager purchaseReturnManager)
        {

            _purchaseReturnManager = purchaseReturnManager;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
