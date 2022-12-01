using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel
{
    public class CreateAccountVM
    {
        [Required(ErrorMessage ="اكتب اسم الحساب"), StringLength(150)]
        [Display(Name ="اسم الحساب انجليزي")]
        public string AccountName { get; set; }//اسم الحساب

        [Required(ErrorMessage = "اكتب اسم الحساب"), StringLength(150)]
        [Display(Name = "اسم الحساب عربي")]
        public string AccountNameAr { get; set; }//اسم الحساب عربي

        [Required(ErrorMessage = "اختيار نوع الحساب")]
        [Display(Name = "نوع الحساب")]
        public int AccTypeId { get; set; }//AccountChartCounter

        [Required(ErrorMessage = "اختيار العملة")]
        [Display(Name = "العملة")]
        public int CurrencyId { get; set; }//العملة

        [Display(Name = "الفرع")]
        [Required(ErrorMessage = "اختيار الفرع")]
        public int BranchId { get; set; }//الفرع 

    }
}
