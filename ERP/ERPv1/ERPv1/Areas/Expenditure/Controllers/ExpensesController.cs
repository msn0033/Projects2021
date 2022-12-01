using ERPv1.ERP.PurchasesModule.Interfaces;
using ERPv1.ERP.PurchasesModule.Interfaces.Expense;
using ERPv1.ERP.PurchasesModule.ViewModel.Expense;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.Areas.Expenditure.Controllers
{
    [Area("Expenditure")]
    public class ExpensesController : Controller
    {
        private readonly IExpensesManager _expensesManager;

        public ExpensesController(IExpensesManager expensesManager)
        {
            _expensesManager = expensesManager;
        }
        public IActionResult Index()
        {
            var vm = _expensesManager.GetAllExpenseItem();
            return View(vm);
        }
        [HttpGet]
        public IActionResult CreateExpenseItem()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult CreateExpenseItem(ExpenseCreationVM vm)
        {
            _expensesManager.AddNewExpenseItem(vm);
            return RedirectToAction(nameof(this.Index));
        }

        public IActionResult NewExpense()
        {
            var vm = new ExpenseContainerVM();
            vm.SaveURL = "/Expenditure/Expenses/SaveExpense";

            return View(vm);
        }

        public IActionResult SaveExpense([FromBody] ExpenseContainerVM vm )
        {
            _expensesManager.SaveNewExpense(vm);
            return Json(new {newLocation="/Home/Index" });
        }

    }
}
