using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel
{
    public class AccountListVM
    { 
        [Display(Name ="رقم الحساب")]
        public string AccNum { get; set; }//رقم الحساب


        [Display(Name = "اسم الحساب")]
        public string AccountName { get; set; }//اسم الحساب


        [Display(Name = "اسم الحساب عربي")]
        public string AccountNameAr { get; set; }//اسم الحساب عربي   
        

        [Display(Name = "رقم الحساب الرئيسي")]
        public string ParentAcNum { get; set; }//رقم الحساب الرئيسي 


        [Display(Name = "نوع الحساب")]
        public string AccTypeName { get; set; }//نوع الحساب

        [Display(Name = "نوع الحساب")]
        public string AccTypeNameAr { get; set; }//نوع الحساب


        [Display(Name = "الرصيد")]
        public decimal Balance { get; set; }//الرصيد


        [Display(Name = "الرصيد الافتتاحي")]
        public decimal StartingBalance { get; set; }//رصيد اول المدة


        [Display(Name = "حساب رئيسي")]
        public bool IsParent { get; set; }//حساب رئيسي او حساب فرعي 


        [Display(Name = "العملة")]
        public string CurrencyAbbr { get; set; }


        [Display(Name = "فعال")]
        public bool IsActive { get; set; }// الحساب مفعل - غير مفعل


        [Display(Name = "اسم الفرع")]
        public string BranchName { get; set; }//اسم الفرع      
    }
}
