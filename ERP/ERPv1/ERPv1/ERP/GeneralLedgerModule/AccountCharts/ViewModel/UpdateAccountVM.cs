using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel
{
    public class UpdateAccountVM
    {

        public string AccNum { get; set; }
        public decimal Balance { get; set; }
        [Required(ErrorMessage = "اكتب اسم الحساب"), StringLength(150)]
        [Display(Name = "اسم الحساب انجليزي")]
        public string AccountName { get; set; }//اسم الحساب

        [Required(ErrorMessage = "اكتب اسم الحساب"), StringLength(150)]
        [Display(Name = "اسم الحساب عربي")]
        public string AccountNameAr { get; set; }//اسم الحساب عربي

      
        [Required(ErrorMessage = "اختيار العملة")]
        [Display(Name = "العملة")]
        [Remote(action: "VerifyCurrency", controller: "AccountChart", areaName: "GLArea", AdditionalFields = nameof(AccNum))]
        public int CurrencyId { get; set; }//العملة

        [Display(Name = "الفرع")]
        [Required(ErrorMessage = "اختيار الفرع")]
        [Remote(action: "VerifyBranch", controller: "AccountChart", areaName: "GLArea", AdditionalFields = nameof(AccNum))]
        public int BranchId { get; set; }//الفرع 

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }
    }
}

