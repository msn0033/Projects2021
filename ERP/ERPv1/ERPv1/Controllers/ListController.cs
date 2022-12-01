using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPv1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERPv1.Controllers
{
    public class ListController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ListController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetSafeAccount(int Id)//ترجع الخزنات  التي في الخزنة الاب
        {
            var safe = _db.AccountChart.Where(x => x.AccTypeId == 4 && x.CurrencyId == Id && x.IsParent == false)
                .Select(x => new SelectListItem
                {
                    Value=x.AccNum,
                    Text=x.AccountName +" ( "+x.Balance +" ) "

                }).ToList();
            return new JsonResult(safe);
        }
        public JsonResult GetBankAccount(int Id)// ترجع البنوك التي في البنك الاب
        {
            var bank = _db.AccountChart.Where(x => x.AccTypeId == 5 && x.CurrencyId == Id && x.IsParent == false)
                .Select(x => new SelectListItem
                {
                    Value = x.AccNum,
                    Text = x.AccountName + " ( " + x.Balance + " ) "
                }).ToList();
            return new JsonResult(bank);
        }
    }
}
