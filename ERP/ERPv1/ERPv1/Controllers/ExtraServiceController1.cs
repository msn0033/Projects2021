using ERPv1.ERP.CurrentAssetModules.Inventory.Interfaces;
using ERPv1.ERP.ERPSettings.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.Controllers
{
    public class ExtraServiceController1 : Controller
    {
        //private readonly IStoreItemManager _storeItemManager;
        //private readonly ITaxManager _tax;

        //public ExtraServiceController1(IStoreItemManager storeItemManager, ITaxManager tax)
        //{
        //    _storeItemManager = storeItemManager;
        //    _tax = tax;
        //}

        //public JsonResult GetStoreItemById(int Id)//يرجع الكمية الحالية للمنتج
        //{
        //    var Qty = _storeItemManager.GetStoreQtyById(Id);

        //    return Json(new { CurrentQty = Qty });

        //}

        //public JsonResult GetStoreTransacitonCurrencyById(int Id)//يرجع الكمية الحالية للمنتج
        //{
        //    var Rate = _storeItemManager.GetStoreQtyById(Id);

        //    return Json(new { CurrencyRate = Rate });

        //}
        //public JsonResult GetVatAmount()// ارجاع الضريبة 
        //{
        //    return Json(new { VAT = _tax.VatAmount() });
        //}
    }
}
