using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model
{
    [Table(name: "Finance_GL_AccountChartCounter")]
    public class AccountChartCounter// عداد شجرة الحسابات
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string AccountType { get; set; }//نوع الحساب خزنة او عميل او مورد  او مخزن انجليزي
        [Required, StringLength(50)]
        public string AccountTypeAr { get; set; }//نوع الحساب خزنة او عميل او مورد  او مخزن عربي

        public AccountCategoryEnum AccountCategory { get; set; }//فـئـة الحساب -اصول-التزامات-ملكية

        [StringLength(50)]
        public string ParentAcNum { get; set; }//رقم الحساب الرئيسي

        public int Count { get; set; }//عدد الحسابات في الحساب الرئيسي
        public bool BalanceSheet { get; set; }//حساب الميزانية
        public bool IncomeStatement { get; set; }//قائمة الدخل
      

    }
}
