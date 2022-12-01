using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ERPv1.Areas.GLArea.Controllers
{
    [Area("GLArea")]
    public class AccountChartController : Controller
    {
        private readonly IAccountGenerator _acount;
        private readonly IAccountListManager _Accountlist;
        private readonly IAccountUpdateChecksManager _accountChecks;
        private readonly IAccountUpdateEdit _edit;

        public AccountChartController(IAccountGenerator Acount,IAccountListManager Accountlist,IAccountUpdateChecksManager accountChecks,IAccountUpdateEdit edit)
        {
            _acount = Acount;
            this._Accountlist = Accountlist;
            _accountChecks = accountChecks;
            _edit = edit;
        }
        public IActionResult Index()
        {
            return View(_Accountlist.GetAllAccount());
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAccount(CreateAccountVM vm) 
        {
            if(ModelState.IsValid)
            {
                _acount.GeneratorAccount(vm);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
            
        }
        [HttpGet]
        public IActionResult Edit(string id)=>
                  View(_Accountlist.GetAccount(id));
        

        [HttpPost]
        public IActionResult Edit(UpdateAccountVM vm)
        {
            if(ModelState.IsValid)
            {
                _edit.SaveUpdateAccount(vm);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

       
        public IActionResult VerifyCurrency( string accNum,int CurrId) {

            if (!_accountChecks.ValidateCurrency(accNum, CurrId))
                return Json($"الرصيد اكبر من صفر . لايمكن تعديل العملة");
            return Json(true);
        }
        public IActionResult VerifyBranch(string accNum, int branchId) {
            if (!_accountChecks.ValidateBranch(accNum, branchId))
                return Json($"الرصيد اكبر من صفر . لايمكن تعديل العملة");
            return Json(true);
        }

    }
}
