using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPv1.ERP.CurrentAssetModules.Inventory.Interfaces;
using ERPv1.ERP.CurrentAssetModules.Inventory.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ERPv1.Areas.Store.Controllers
{
    [Area("Store")]
    public class StoreItemController : Controller
    {
        private readonly IStoreItemManager _storeItem;

        public StoreItemController(IStoreItemManager storeItem)
        {
            _storeItem = storeItem;
        }
        public async Task<IActionResult> Index()
        {
            return View(await  _storeItem.GetAllStoreItem());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StoreItemCreateVM vm)
        {
            if(ModelState.IsValid)
            {
                _storeItem.CreateStoreItem(vm);
                return RedirectToAction(nameof(this.Index));
            }
            return View(vm);
        }

    }
}
