using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ViewComponents

{
    public class SupplierPaymentMethodComponents :ViewComponent  
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
